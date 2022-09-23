using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class DepartmentModel
    {

        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public string Description { get; set; }    
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class DepartmentModelList
    {
        public List<DepartmentModel> departmentModelList { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
    }
}
