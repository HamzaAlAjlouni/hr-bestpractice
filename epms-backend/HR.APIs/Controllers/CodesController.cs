using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;

namespace HR.APIs.Controllers
{
    public class CodesController : ApiController
    {
        private HRContext _Context;
        private APIResult _apiResult;

        public CodesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetCodes(
            int MajorNo,
            int CompanyID,
            string LanguageCode = "en"
        )
        {
            _apiResult = new APIResult();
            try
            {
                var lstCodes = (from codes in _Context.CodesCollection
                    where codes.MAJOR_NO == MajorNo
                          && codes.MINOR_NO != 0
                          && codes.COMPANY_ID == CompanyID
                    select new
                    {
                        //LanguageCode
                        codes.CODE,
                        codes.COMPANY_ID,
                        codes.created_by,
                        codes.created_date,
                        codes.ID,
                        codes.MAJOR_NO,
                        codes.MINOR_NO,
                        codes.modified_by,
                        codes.modified_date,
                        NAME = (LanguageCode == "en" ? codes.NAME : codes.name2)
                    }).ToList();

                return Ok(new { Data = lstCodes, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}