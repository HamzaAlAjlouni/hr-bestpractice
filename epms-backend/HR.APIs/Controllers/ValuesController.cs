using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class ValuesController : ApiController
    {
        HRContext db;

        public ValuesController()
        {
            db = new HRContext();
        }
    }
}