using HR.Entities;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class EmployeePerformanceController : ApiController
    {
        HRContext db;

        public EmployeePerformanceController()
        {
            db = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult getEmpPerfSegments(string name, int Year, int companyID)
        {
            try
            {
                var result = db.EmployeePerformanceSegmentCollection.Where(s => s.year == Year &&
                        s.CompanyId == companyID &&
                        string.IsNullOrEmpty(name) || s.name.ToLower().Contains(name.ToLower())).OrderBy(x => x.segment)
                    .ToList();

                if (result != null)
                {
                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult SaveEmpPerf_segments(
            string name,
            string desc,
            int segment,
            float perc_from,
            float perc_to,
            int year,
            int companyID)
        {
            try
            {
                EmployeePerformanceSegmentEntity empSeg = new EmployeePerformanceSegmentEntity();
                empSeg.name = name;
                empSeg.description = desc;
                empSeg.segment = segment;
                empSeg.percentage_from = perc_from;
                empSeg.percentage_to = perc_to;
                empSeg.CompanyId = companyID;
                empSeg.year = year;

                db.Entry(empSeg).State = System.Data.Entity.EntityState.Added;
                db.EmployeePerformanceSegmentCollection.Add(empSeg);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdateEmpPerf_segments(
            int id,
            string name,
            string desc,
            int segment,
            int year,
            float perc_from,
            float perc_to,
            int companyID)
        {
            try
            {
                var lst = db.EmployeePerformanceSegmentCollection;
                EmployeePerformanceSegmentEntity empSeg = lst.FirstOrDefault(s => s.id == id);
                var empSegCheckTo =
                    lst.OrderBy(x => x.percentage_from).FirstOrDefault(s => s.percentage_from > empSeg.percentage_from)
                        ?.percentage_from ?? int.MaxValue;
                var empSegCheckFrom = lst.OrderByDescending(x => x.percentage_to)
                    .FirstOrDefault(s => s.percentage_to < empSeg.percentage_to)?.percentage_to ?? -1;


                if (perc_from > empSegCheckFrom && perc_to < empSegCheckTo && perc_to > perc_from)
                {
                    empSeg.name = name;
                    empSeg.description = desc;
                    empSeg.segment = segment;
                    empSeg.percentage_from = perc_from;
                    empSeg.percentage_to = perc_to;
                    empSeg.CompanyId = companyID;
                    empSeg.year = year;
                    db.Entry(empSeg).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                if (empSegCheckTo == int.MaxValue)
                    empSegCheckTo = empSeg.percentage_to;
                if (empSegCheckFrom == -1)
                    empSegCheckFrom = 0;

                return Ok(new
                {
                    Data = string.Empty, IsError = true,
                    ErrorMessage =
                        $"please adding percentage from greater than {empSegCheckFrom} and percentage to less than {empSegCheckTo}. "
                });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult deleteEmpSegment(int id)
        {
            try
            {
                var empSeg = db.EmployeePerformanceSegmentCollection.Where(e => e.id == id).FirstOrDefault();
                if (empSeg != null)
                {
                    db.EmployeePerformanceSegmentCollection.Remove(empSeg);
                    db.SaveChanges();
                }
                else
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Cannot Delete Action Plan." });
                }

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult getEmpSegmentByID(long id)
        {
            try
            {
                var result = db.EmployeePerformanceSegmentCollection.Where(e => e.id == id).Select(e => new
                {
                    e.id,
                    e.CompanyId,
                    e.description,
                    e.name,
                    e.percentage_from,
                    e.percentage_to,
                    e.segment,
                    e.year
                }).FirstOrDefault();


                if (result != null)
                {
                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}