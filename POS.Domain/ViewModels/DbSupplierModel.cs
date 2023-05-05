using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class DbSupplierModel : BaseResponse
    {
        public int Supplier_ID { get; set; }

        [Required(ErrorMessage = "Supplier Name is requrired")]
        [StringLength(50)]
        [Display(Name = "Supplier Name")]
        public string Supplier_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }       

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0,dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditSupplierName { get; set; }
        public string Token { get; set; }
    }
    public class DbSupplierModelList : BaseResponse
    {
        public List<DbSupplierModel> supplierModelList { get; set; }
    }
}
