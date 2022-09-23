using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions;
using System;

namespace POS.Infrastructure.Mapper
{
    public static class OccupationConverter
    {
        public static void ConvertModelToEntity(OccupationModel model, ref Occupation entity)
        {

            if (string.IsNullOrEmpty(model.Occupation_Name))
                throw new CustomException(CustomExceptionEnum.InvalidOccupationName);

            if (model.Department_ID <= 0)
                throw new CustomException(CustomExceptionEnum.InvalidDepartmentID);            

            entity.Occupation_Name = model.Occupation_Name;
            entity.Department_ID = model.Department_ID;
            entity.IsDeleted = model.IsDeleted;
            return;
        }

        public static OccupationModel ConvertEntityToModel(Occupation entity)
        {
            var model = new OccupationModel();
            if (entity != null)
            {
                model.Occupation_ID = entity.Occupation_ID;
                model.Occupation_Name = entity.Occupation_Name;
                model.Department_ID = entity.Department_ID;
                model.Department_Name = entity.Department.Department_Name;
                model.IsDeleted = entity.IsDeleted;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.NoOccupationInfoAvailiable);
            }
            return model;
        }
    }
}
