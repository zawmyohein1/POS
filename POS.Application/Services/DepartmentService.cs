using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Data.Helper;
using POS.Infrastructure.Logger;
using POS.Infrastructure.Exceptions;
using POS.Infrastructure.Data.UnitOfWork;


namespace POS.Application.Services
{
    public class DepartmentService : IDepartmentservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILoggerHelper _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, ILoggerHelper logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DepartmentModelList> GetAllDepartments()
        {
            DepartmentModelList modelList = new DepartmentModelList();
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    var entityList = await _unitOfWork.Department.FindAllAsync(x => x.IsDeleted == false);
                    if (entityList != null)
                    {
                        modelList.departmentModelList = entityList.Select<Department, DepartmentModel>((DepartmentEntity => { return _mapper.Map<DepartmentModel>(DepartmentEntity); })).ToList();
                        modelList.ResultCode = (int)CustomExceptionEnum.Success;
                        modelList.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
                    }
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
            try
            {
                var entity = _mapper.Map<Department>(model);
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var duplicateEntity = await _unitOfWork.Department.FindAsync(x => x.Department_Name == model.Department_Name && x.Department_ID != model.Department_ID && x.IsDeleted == false);
                        if (duplicateEntity == null)
                        {
                            entity = await _unitOfWork.Department.AddAsyn(entity);
                            _unitOfWork.SaveChangesAsync().Wait();
                            transaction.Commit();

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

        public async Task<DepartmentModel> GetDepartmentById(int id)
        {
            var model = new DepartmentModel();
            try
            {
                using (var tranaction = _unitOfWork.BeginTransaction())
                {
                    var entity = await _unitOfWork.Department.GetAsync(id);
                    try
                    {
                        if (entity != null)
                        {
                            model = _mapper.Map<DepartmentModel>(entity);
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

        public async Task<DepartmentModel> UpdateDepartment(DepartmentModel model)
        {
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var entity = await _unitOfWork.Department.GetAsync(model.Department_ID);
                        if (entity != null)
                        {
                            var duplicateEntity = await _unitOfWork.Department.FindAsync(x => x.Department_Name == model.Department_Name && x.Department_ID != model.Department_ID && x.IsDeleted == false);
                            if (duplicateEntity == null)
                            {
                                entity = _mapper.Map<Department>(model);
                                await _unitOfWork.Department.UpdateAsyn(entity, entity.Department_ID);
                                transaction.Commit();

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
                        transaction.Rollback();
                        model.ResultCode = (int)ex.ResultCode;
                        model.ResultDescription = ex.ResultDescription;

                        _logger.TraceLog(String.Format("Error Code : {0} ,Description : {1}", ex.ResultCode, ex.ResultDescription));
                    }
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

        public async Task<DepartmentModel> DeleteDepartment(int id, string name)
        {
            var model = new DepartmentModel();
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    var entity = await _unitOfWork.Department.GetAsync(id);
                    if (entity != null)
                    {
                        entity.IsDeleted = true;
                        _unitOfWork.Department.UpdateAsyn(entity, entity.Department_ID).Wait();
                        transaction.Commit();

                        model.ResultCode = (int)CustomExceptionEnum.Success;
                        model.ResultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                    }
                    else
                    {
                        throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
                    }
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
