using HR.Entities;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;


namespace HR.APIs.Controllers
{
    public class UploadController : ApiController
    {
        HRContext db;

        public UploadController()
        {
            db = new HRContext();
        }


        [HttpPost]
        public IHttpActionResult UploadFiles(int projectId, string createdby)
        {
            try
            {
                int i = 0;
                int cntSuccess = 0;
                var uploadedFileNames = new List<string>();
                string result = string.Empty;

                HttpResponseMessage response = new HttpResponseMessage();

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[i];
                        var filePath = HttpContext.Current.Server.MapPath("~/Resources/" + postedFile.FileName);
                        try
                        {
                            postedFile.SaveAs(filePath);
                            uploadedFileNames.Add(httpRequest.Files[i].FileName);
                            cntSuccess++;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        i++;
                    }
                }

                return Ok(new { Data = "200", IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}