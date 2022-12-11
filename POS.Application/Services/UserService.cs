using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using AutoMapper;
using POS.Application.Helper;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Data.Helper;
using POS.Infrastructure.Logger;
using WebApi.POS.Application.Helper;
using POS.Infrastructure.Exceptions;

namespace POS.Application.Services
{
    public class Userservice : IUserservice
    {
        private IGenericRepository<User> _grepository;
        private ILoggerHelper _logger;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public Userservice(IGenericRepository<User> grepository, ILoggerHelper logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _grepository = grepository;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<UserModelList> GetAllUsers()
        {
            UserModelList modelList = new UserModelList();

            try
            {
                var entities = await _grepository.GetAllAsyn();
                if (entities != null)
                {
                    modelList.userModelList = entities.Select<User, UserModel>((UserEntity =>
                    {
                        return _mapper.Map<UserModel>(UserEntity);
                    })).ToList();

                    modelList.ResultCode = (int)CustomExceptionEnum.Success;
                    modelList.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
                }
            }
            catch (CustomException ex)
            {
                modelList.ResultCode = (int)ex.ResultCode;
                modelList.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0},Description : {1}", ex.ResultCode, ex.ResultDescription));
            }
            catch (Exception ex)
            {
                modelList.ResultCode = (int)CustomExceptionEnum.UnknownException;
                modelList.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return modelList;
        }

        public async Task<UserModel> CreateUser(UserModel model)
        {
            var entity = _mapper.Map<User>(model);

            try
            {
                var duplicateEntity = await _grepository.FindAsync(x => x.Email == model.Email && x.User_ID != model.User_ID && x.IsDeleted == false);
                if (duplicateEntity == null)
                {
                    entity = await _grepository.AddAsyn(entity);

                    //insert recrod to audit trail table after insert user record
                    AuditTrail.InsertAuditTrail(AuditAction.Add, AuditModule.User, AuditTrail.GetEntityInfo(entity), model.AuditUserName);

                    model.User_ID = entity.User_ID;
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.UserEmailAlreadyExists);
                }
            }
            catch (CustomException ex)
            {
                model.ResultCode = (int)ex.ResultCode;
                model.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return model;
        }

        public async Task<UserModel> GetUserById(int Id)
        {
            var model = new UserModel();
            var entity = await _grepository.GetAsync(Id);
            try
            {
                if (entity != null)
                {
                    model = _mapper.Map<UserModel>(entity);
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
                }
            }
            catch (CustomException ex)
            {
                model.ResultCode = (int)ex.ResultCode;
                model.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return model;
        }

        public async Task<UserModel> UpdateUser(UserModel model)
        {
            try
            {
                var entity = await _grepository.GetAsync(model.User_ID);
                if (entity != null)
                {
                    var duplicateEntity = await _grepository.FindAsync(x => x.Email == model.Email && x.User_ID != model.User_ID && x.IsDeleted == false);
                    if (duplicateEntity == null)
                    {
                        //insert record to audit trail table before user edit record
                        AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.User, AuditTrail.GetEntityInfo(entity), model.AuditUserName);

                        entity = _mapper.Map<User>(model);
                        await _grepository.UpdateAsyn(entity, entity.User_ID);

                        //insert record to audit trail table after user edit record
                        AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.User, AuditTrail.GetEntityInfo(entity), model.AuditUserName);

                        model.ResultCode = (int)CustomExceptionEnum.Success;
                        model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.UserEmailAlreadyExists);
                    }
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
                }
            }
            catch (CustomException ex)
            {
                model.ResultCode = (int)ex.ResultCode;
                model.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));

            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return model;
        }

        public async Task<UserModel> DeleteUser(int Id, string userName)
        {
            var model = new UserModel();
            try
            {
                var entity = await _grepository.GetAsync(Id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    await _grepository.DeleteAsyn(entity);

                    AuditTrail.InsertAuditTrail(AuditAction.Delete, AuditModule.User, AuditTrail.GetEntityInfo(entity), model.AuditUserName);

                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
                }
            }
            catch (CustomException ex)
            {
                model.ResultCode = (int)ex.ResultCode;
                model.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return model;
        }

        public async Task<UserModel> LoginUser(UserModel model)
        {
            try
            {
                var entity = await _grepository.FindByAsyn(x => x.Email == model.Email && x.Password == model.Password);
                if (entity != null)
                {
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim("id", model.User_ID.ToString()) }),
                        Expires = DateTime.UtcNow.AddDays(7), //update later with configurable setting
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    model.Token = tokenHandler.WriteToken(token);

                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
                }
            }
            catch (CustomException ex)
            {
                model.ResultCode = (int)ex.ResultCode;
                model.ResultDescription = ex.ResultDescription;
                _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }

            return model.WithoutPassword();
        }
    }
}
