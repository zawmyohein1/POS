using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.EntityModels
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        [Required(ErrorMessage = "Department is requrired")]
        [StringLength(50)]
        public string Department_Name { get; set; }     
        [StringLength(100)]
        public string Description { get; set; }     

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        public virtual ICollection<Occupation> Occupations { get; set; }
    }
}
