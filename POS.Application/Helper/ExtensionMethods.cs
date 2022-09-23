using POS.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.POS.Application.Helper
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserModel> WithoutPasswords(this IEnumerable<UserModel> users) 
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }
        public static UserModel WithoutPassword(this UserModel user) 
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}