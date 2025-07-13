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
    public class UnitProjectsPerformanceController : ApiController
    {
        HRContext db;

        public UnitProjectsPerformanceController()
        {
            db = new HRContext();
        }


        public IHttpActionResult SaveUnitProjectsPerformance(
            UnitProjectsPerformanceEntity unitProjectsPerformanceEntity)
        {
            try
            {
                if (unitProjectsPerformanceEntity == null)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Bad Request" });
                }
                else
                {
                    db.UnitProjectsPerformanceCollection.Add(unitProjectsPerformanceEntity);
                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        public IHttpActionResult UpdateUnitProjectsPerformance(
            UnitProjectsPerformanceEntity unitProjectsPerformanceEntity)
        {
            try
            {
                //Modified by yousef sleit
                if (unitProjectsPerformanceEntity != null)
                {
                    db.Entry(unitProjectsPerformanceEntity).State = System.Data.Entity.EntityState.Modified;
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
        public IHttpActionResult GetUnitProjectsPerformance(int year, int companyId, int branchId, int unitId)
        {
            try
            {
                //Modified by yousef sleit
                UnitProjectsPerformanceEntity unitProjectsPerformance = new UnitProjectsPerformanceEntity();

                using (HRContext DB = new HRContext())
                {
                    unitProjectsPerformance = DB.UnitProjectsPerformanceCollection.Where(c =>
                            (c.CompanyId == companyId && c.Year == year && c.BranchId == branchId &&
                             c.UnitId == unitId))
                        .FirstOrDefault();
                }

                return Ok(new { Data = unitProjectsPerformance, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}