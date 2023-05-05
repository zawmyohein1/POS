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
using POS.Domain.IServices;
using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Logger;
using WebApi.POS.Application.Helper;
using POS.Infrastructure.Exceptions;
using POS.Infrastructure.Data.UnitOfWork;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace POS.Application.Services
{
    public class DbSupplierService : IDbSupplierservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILoggerHelper _logger;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public DbSupplierService(IUnitOfWork unitOfWork, ILoggerHelper logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<DbSupplierModelList> GetAllDbSuppliers()
        {
            DbSupplierModelList modelList = new DbSupplierModelList();

            try
            {            
                var entities = await _unitOfWork.DbSupplier.FindAllAsync(x => x.IsDeleted == false);
                if (entities != null)
                {
                    modelList.supplierModelList = entities.Select<DbSupplier, DbSupplierModel>((SupplierEntity =>
                    {
                        return _mapper.Map<DbSupplierModel>(SupplierEntity);
                    })).ToList();

                    modelList.ResultCode = (int)CustomExceptionEnum.Success;
                    modelList.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoSupplierInfoAvailiable);
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

        public async Task<DbSupplierModel> CreateDbSupplier(DbSupplierModel model)
        {
            try
            {
                var entity = _mapper.Map<DbSupplier>(model);
                try
                {
                    try
                    {
                        //_unitOfWork.StartTransaction();

                        var duplicateEntity = await _unitOfWork.DbSupplier.FindAsync(x => x.Email == model.Email && x.Supplier_ID != model.Supplier_ID && x.IsDeleted == false);
                        if (duplicateEntity == null)
                        {
                            entity = await _unitOfWork.DbSupplier.AddAsyn(entity);
                            _unitOfWork.SaveChanges();
                            _unitOfWork.Commit();

                            model.Supplier_ID = entity.Supplier_ID;
                            model.ResultCode = (int)CustomExceptionEnum.Success;
                            model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);

                            //insert recrod to audit trail table after insert supplier record
                            //AuditTrail.InsertAuditTrail(AuditAction.Add, AuditModule.Supplier, AuditTrail.GetEntityInfo(entity), model.AuditSupplierName);
                        }
                        else
                        {
                            throw new CustomException(CustomExceptionEnum.SupplierEmailAlreadyExist);
                        }
                    }
                    catch (CustomException ex)
                    {
                        _unitOfWork.Rollback();
                        model.ResultCode = (int)ex.ResultCode;
                        model.ResultDescription = ex.ResultDescription;
                        _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.Rollback();
                        model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                        model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                        _logger.LogError(ex);
                    }
                }

                catch (Exception ex)
                {
                    model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                    _logger.LogError(ex);
                }
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }

            return model;
        }

        public async Task<DbSupplierModel> GetDbSupplierById(int Id)
        {
            var model = new DbSupplierModel();

            try
            {
                _unitOfWork.StartTransaction();
                var entity = await _unitOfWork.DbSupplier.GetAsync(Id);
                if (entity != null)
                {
                    model = _mapper.Map<DbSupplierModel>(entity);
                    model.ResultCode = (int)CustomExceptionEnum.Success;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                else
                {
                    throw new CustomException(CustomExceptionEnum.NoSupplierInfoAvailiable);
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

        public async Task<DbSupplierModel> UpdateDbSupplier(DbSupplierModel model)
        {
            try
            {
                try
                {
                    _unitOfWork.StartTransaction();
                    var entity = await _unitOfWork.DbSupplier.GetAsync(model.Supplier_ID);
                    if (entity != null)
                    {
                        var duplicateEntity = await _unitOfWork.DbSupplier.FindAsync(x => x.Email == model.Email && x.Supplier_ID != model.Supplier_ID && x.IsDeleted == false);
                        if (duplicateEntity == null)
                        {
                            //AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.Supplier, AuditTrail.GetEntityInfo(entity), model.AuditSupplierName);

                            entity = _mapper.Map<DbSupplier>(model);
                            await _unitOfWork.DbSupplier.UpdateAsyn(entity, entity.Supplier_ID);
                            _unitOfWork.Commit();

                            model.ResultCode = (int)CustomExceptionEnum.Success;
                            model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);

                            //AuditTrail.InsertAuditTrail(AuditAction.EditBefore, AuditModule.Supplier, AuditTrail.GetEntityInfo(entity), model.AuditSupplierName);
                        }
                        else
                        {
                            throw new CustomException(CustomExceptionEnum.SupplierEmailAlreadyExist);
                        }
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.NoSupplierInfoAvailiable);
                    }
                }
                catch (CustomException ex)
                {
                    _unitOfWork.Rollback();
                    model.ResultCode = (int)ex.ResultCode;
                    model.ResultDescription = ex.ResultDescription;
                    _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                    _logger.LogError(ex);
                }
            }
            catch (Exception ex)
            {
                model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.LogError(ex);
            }
            return model;
        }

        public async Task<DbSupplierModel> DeleteDbSupplier(int Id, string supplierName)
        {
            var model = new DbSupplierModel();
            try
            {
                try
                {
                    _unitOfWork.StartTransaction();
                    var entity = await _unitOfWork.DbSupplier.GetAsync(Id);
                    if (entity != null)
                    {
                        entity.IsDeleted = true;
                        _unitOfWork.DbSupplier.UpdateAsyn(entity, entity.Supplier_ID).Wait();

                        _unitOfWork.Commit();

                        model.ResultCode = (int)CustomExceptionEnum.Success;
                        model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);

                        //AuditTrail.InsertAuditTrail(AuditAction.Delete, AuditModule.Supplier, AuditTrail.GetEntityInfo(entity), model.AuditSupplierName);
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.NoSupplierInfoAvailiable);
                    }
                }
                catch (CustomException ex)
                {
                    _unitOfWork.Rollback();

                    model.ResultCode = (int)ex.ResultCode;
                    model.ResultDescription = ex.ResultDescription;
                    _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();

                    model.ResultCode = (int)CustomExceptionEnum.UnknownException;
                    model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                    _logger.LogError(ex);
                }
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
