using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class ServiceResponseResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object NewObject { get; set; }
    }
}