using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class OccupationModel
    {

        public int Occupation_ID { get; set; }
        public string Occupation_Name { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class OccupationModelList
    {
        public List<OccupationModel> occupationModelList { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
    }
}
