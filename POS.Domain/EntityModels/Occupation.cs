using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POS.Domain.EntityModels
{
    public class Occupation
    {
        [Key]
        public int Occupation_ID { get; set; }
        [Required(ErrorMessage = "Occupation Name is requrired")]
        [StringLength(50)]
        public string Occupation_Name { get; set; }
        [Required(ErrorMessage ="Department ID is required")]
        public int Department_ID { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        [ForeignKey("Department_ID")]
        public virtual Department Department { get; set; }
    }
}
