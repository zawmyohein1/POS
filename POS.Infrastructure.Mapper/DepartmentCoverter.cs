using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions;
using System;

namespace POS.Infrastructure.Mapper
{
    public static class DepartmentConverter
    {
        public static void ConvertModelToEntity(DepartmentModel model, ref Department entity)
        {

            if (string.IsNullOrEmpty(model.Department_Name))
                throw new CustomException(CustomExceptionEnum.InvalidDepartmentName);     

            entity.Department_Name = model.Department_Name;
            entity.Description = model.Description;                  
            entity.IsDeleted = model.IsDeleted;
            return;
        }

        public static DepartmentModel ConvertEntityToModel(Department entity)
        {
            var model = new DepartmentModel();
            if (entity != null)
            {
                model.Department_ID  = entity.Department_ID;
                model.Department_Name = entity.Department_Name;
                model.Description = entity.Description;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.NoDepartmentInfoAvailiable);
            }
            return model;
        }
    }
}
