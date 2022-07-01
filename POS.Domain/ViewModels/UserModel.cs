using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class UserModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Role { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0,dd/MM/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime Created { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class UserModelList
    {
        public List<UserModel> userModelList { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
    }
}
