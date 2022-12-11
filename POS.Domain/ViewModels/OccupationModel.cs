using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.ViewModels
{
    public class OccupationModel:BaseResponse
    {
        public int Occupation_ID { get; set; }
        public string Occupation_Name { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditUserName { get; set; }
        public string Token { get; set; }
    }
    public class OccupationModelList:BaseResponse
    {
        public List<OccupationModel> occupationModelList { get; set; }
    }
}
