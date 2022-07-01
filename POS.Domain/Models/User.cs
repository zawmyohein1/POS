using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is requrired")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Role { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
