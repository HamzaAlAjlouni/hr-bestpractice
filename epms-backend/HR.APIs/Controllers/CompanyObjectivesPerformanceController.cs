using HR.Entities;
using HR.Entities.Admin;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class CompanyObjectivesPerformanceController : ApiController
    {
        HRContext db;

        public CompanyObjectivesPerformanceController()
        {
            db = new HRContext();
        }


        public IHttpActionResult SaveCompanyObjectivesPerformance(
            CompanyObjectivesPerformanceEntity companyObjectivesPerformanceEntity)
        {
            try
            {
                if (companyObjectivesPerformanceEntity == null)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Bad Request" });
                }
                else
                {
                    db.CompanyObjectivesPerformanceCollection.Add(companyObjectivesPerformanceEntity);
                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        public IHttpActionResult UpdateCompanyObjectivesPerformance(
            CompanyObjectivesPerformanceEntity companyObjectivesPerformanceEntity)
        {
            try
            {
                //Modified by yousef sleit
                if (companyObjectivesPerformanceEntity != null)
                {
                    db.Entry(companyObjectivesPerformanceEntity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult GetCompanyObjectivesPerformance(int year, int companyId)
        {
            try
            {
                //Modified by yousef sleit
                CompanyObjectivesPerformanceEntity companyObjectivesPerformance =
                    new CompanyObjectivesPerformanceEntity();

                using (HRContext DB = new HRContext())
                {
                    companyObjectivesPerformance = DB.CompanyObjectivesPerformanceCollection
                        .Where(c => (c.CompanyId == companyId && c.Year == year)).FirstOrDefault();
                }

                return Ok(new { Data = companyObjectivesPerformance, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}