using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.Models
{
    public class Audit_Trail
    {
        [Key]
        public int Audit_Trail_ID { get; set; }
        [Required(ErrorMessage = "ActionID is required")]
        public int Action_ID { get; set; }
        [Required(ErrorMessage = "ModuleID is required")]
        public int Module_ID { get; set; }
        [Required(ErrorMessage = "Entity is required")]
        public string Entity { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(50)]
        public string User_Name { get; set; }
        [Required(ErrorMessage = "AuditTimestamp is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Audit_Timestamp { get; set; }
    }
}
