using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class UserModel : BaseResponse
    {
        public int User_ID { get; set; }

        [Required(ErrorMessage = "User Name is requrired")]
        [StringLength(50)]
        [Display(Name = "User Name")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]

        public string Password { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0,dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class UserModelList : BaseResponse
    {
        public List<UserModel> userModelList { get; set; }
    }
}
