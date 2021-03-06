using POS.Domain.Models;
using POS.Domain.ViewModels;
using POS.Infrastructure.Exceptions;
using System;

namespace POS.Infrastructure.Mapper
{
    public static class UserConverter
    {
        public static void ConvertModelToEntity(UserModel model, ref User entity)
        {

            if (string.IsNullOrEmpty(model.Email))
                throw new CustomException(CustomExceptionEnum.InvalidEmail);

            if (string.IsNullOrEmpty(model.Password))
                throw new CustomException(CustomExceptionEnum.InvalidPassword);

            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Password = model.Password;
            entity.Phone = model.Phone;
            entity.Role = model.Role;
            entity.Gender = model.Gender;
            entity.Created = (model.Created == null) ? DateTime.Now : model.Created;
            entity.IsDeleted = model.IsDeleted;
            return;
        }

        public static UserModel ConvertEntityToModel(User entity)
        {
            var model = new UserModel();
            if (entity != null)
            {
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Email = entity.Email;
                model.Password = entity.Password;
                model.Phone = entity.Phone;
                model.Gender = entity.Gender;
                model.Role = entity.Role;
                model.Created = entity.Created;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.NoUserInfoAvailiable);
            }

            return model;
        }
    }
}
