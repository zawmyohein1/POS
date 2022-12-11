using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class DepartmentModel : BaseResponse
    {

        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class DepartmentModelList : BaseResponse
    {
        public List<DepartmentModel> departmentModelList { get; set; }
    }
}
