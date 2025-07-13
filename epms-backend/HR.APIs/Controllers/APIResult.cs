using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.APIs.Controllers
{
    public class APIResult
    {
        public APIResult()
        {
            this.Data = null;
            this.ErrorMessage = string.Empty;
            this.IsError = false;
        }

        public string Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError { get; set; }
    }
}