using POS.Infrastructure.Mapper;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Data.Helper;
using POS.Infrastructure.Logger;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using POS.Application.Helper;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.POS.Application.Helper;
using POS.Infrastructure.Exceptions;

namespace POS.Application.Services
{
    public class Departmentservice : IDepartmentservice
    {
        private IDepartmentRepository _repository;
        private ILoggerHelper _logger;
        private readonly AppSettings _appSettings;

        public Departmentservice(IDepartmentRepository repository, ILoggerHelper logger, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<DepartmentModelList> GetAllDepartments()
        {
            DepartmentModelList modelList = new DepartmentModelList();
            try
            {
                var entityList = await _repository.GetAllDepartmentsAsync();
                if (entityList != null)
                {
                    modelList.departmentModelList = entityList.Select<Department, DepartmentModel>((DepartmentEntity => { return DepartmentConverter.ConvertEntityToModel(DepartmentEntity); })).ToList();
                    modelList.ResultCode = (int)CustomExceptionEnum.Success;
                    modelList.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
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

        public async Task<DepartmentModel> CreateDepartment(DepartmentModel model)
        {
            var entity = new Department();
            DepartmentConverter.ConvertModelToEntity(model, ref entity);
            try
            {
                var duplicateEntity = await _repository.CheckDuplicate(entity);
                if (duplicateEntity == null)
                {
                    entity = await _repository.CreateDepartmentAsync(entity);
                    AuditTrail.InsertAuditTrail(AuditAction.Add, AuditModule.Department, AuditTrail.GetEntityInfo(entity), model.AuditUserName);
                    model.Department_ID = entity.Department_ID;
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.DepartmentNameAlreadyExist);
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

        public async Task<DepartmentModel> GetDepartmentById(int Id)
        {
            var model = new DepartmentModel();
            var entity = await _repository.GetDepartmentByIdAsync(Id);
            try
            {
                if (entity != null)
                {
                    model = DepartmentConverter.ConvertEntityToModel(entity);
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
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

        public async Task<DepartmentModel> UpdateDepartment(DepartmentModel model)
        {
            try
            {
                var entity = await _repository.GetDepartmentByIdAsync(model.Department_ID);
                if (entity != null)
                {
                    var duplicateEntity = await _repository.CheckDuplicate(entity);
                    if (duplicateEntity == null)
                    {
                        AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.Department, AuditTrail.GetEntityInfo(entity), model.AuditUserName);
                        DepartmentConverter.ConvertModelToEntity(model, ref entity);
                        await _repository.UpdateDepartmentAsync(entity);
                        AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.Department, AuditTrail.GetEntityInfo(entity), model.AuditUserName);
                        model.ResultCode = (int)CustomExceptionEnum.Success;
                        model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.DepartmentNameAlreadyExist);
                    }
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
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

        public async Task<DepartmentModel> DeleteDepartment(int Id, string DepartmentName)
        {
            var model = new DepartmentModel();
            try
            {
                var entity = await _repository.GetDepartmentByIdAsync(Id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    await _repository.DeleteDepartmentAsync(entity);
                    AuditTrail.InsertAuditTrail(AuditAction.Delete, AuditModule.Department, AuditTrail.GetEntityInfo(entity), model.AuditUserName);
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
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

    }
}
