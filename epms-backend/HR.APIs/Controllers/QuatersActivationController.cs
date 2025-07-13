using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using System.Data.Entity;
using HR.Entities.Admin;
using Newtonsoft.Json;

namespace HR.APIs.Controllers
{
    public class QuatersActivationController : ApiController
    {
        private HRContext _Context;

        public QuatersActivationController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult GetQuatersActivation()
        {
            try
            {
                var list = _Context.QuatersActiviationCollection.OrderBy(a => a.Name).Select(a => new
                    {
                        a.ID,
                        a.Name,
                        a.Status,
                        a.CreatedBy,
                        a.CreatedDate,
                        a.ModifiedBy,
                        a.ModifiedDate,
                        StatusValue = a.Status == 1 ? "Active" : "InActive"
                    }
                ).ToList();

                return Ok(new { Data = list, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdateQuaterActivation(int ID, int Status, string modified)
        {
            try
            {
                var quarter = _Context.QuatersActiviationCollection.Where(a => a.ID == ID).FirstOrDefault();

                if (quarter != null)
                {
                    if (Status == 1 || Status == 0)
                    {
                        quarter.Status = Status;
                        quarter.ModifiedBy = modified;
                        quarter.ModifiedDate = DateTime.Now;
                        _Context.Entry(quarter).State = EntityState.Modified;

                        _Context.SaveChanges();
                    }
                    else
                    {
                        return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "invalid status" });
                    }
                }
                else
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "quarter does not exit" });
                }


                return Ok(new { Data = "", IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}