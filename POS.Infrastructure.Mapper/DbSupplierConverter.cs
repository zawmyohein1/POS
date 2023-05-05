using POS.Domain.EntityModels;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions;
using System;

namespace POS.Infrastructure.Mapper
{
    public static class DbSupplierConverter
    {
        public static void ConvertModelToEntity(DbSupplierModel model, ref DbSupplier entity)
        {

            if (string.IsNullOrEmpty(model.Supplier_Name))
                throw new CustomException(CustomExceptionEnum.InvalidSupplierName);
      
            entity.Supplier_Name = model.Supplier_Name;
            entity.CountryCode = model.CountryCode;
            entity.DateCreated = model.DateCreated;
            entity.Email = model.Email;            
            entity.DateCreated = (model.DateCreated == null || model.DateCreated == default) ? DateTime.Now : model.DateCreated;
            entity.IsDeleted = model.IsDeleted;
            return;
        }

        public static DbSupplierModel ConvertEntityToModel(DbSupplier entity)
        {
            var model = new DbSupplierModel();
            if (entity != null)
            {
                model.Supplier_ID = entity.Supplier_ID;
                model.Supplier_Name = entity.Supplier_Name;
                model.Email = entity.Email;
                model.CountryCode=entity.CountryCode;           
                model.DateCreated = entity.DateCreated;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.NoSupplierInfoAvailiable);
            }

            return model;
        }
    }
}
