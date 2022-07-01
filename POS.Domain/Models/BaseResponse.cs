using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Domain.Models
{
    public abstract class BaseResponse
    {       
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }     

    }
}
