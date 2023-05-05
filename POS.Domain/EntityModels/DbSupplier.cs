using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.EntityModels
{
    public class DbSupplier
    {
        [Key]
        public int Supplier_ID { get; set; }
        [Required(ErrorMessage = "Supplier Name is requrired")]
        [StringLength(50)]
        public string Supplier_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        public string Email { get; set; }
       
        [StringLength(2)]
        public string CountryCode { get; set; }
     
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
