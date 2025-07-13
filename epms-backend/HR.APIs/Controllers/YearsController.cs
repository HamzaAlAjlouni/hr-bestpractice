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
    public class YearsController : ApiController
    {
        private HRContext _Context;

        public YearsController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetYears()
        {
            try
            {
                List<YearEntity> lstYears = (from year in _Context.YearsCollection
                    select year).OrderBy(x => x.year).ToList();

                if (lstYears != null && lstYears.Count > 0)
                {
                    int year = DateTime.Now.Year;
                    YearEntity activeYear = lstYears.Where(x => x.year == year).FirstOrDefault();

                    if (activeYear != null)
                    {
                        activeYear.isActive = true;
                    }
                }

                return Ok(new { Data = lstYears, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int year,
            string CreatedBy)
        {
            try
            {
                YearEntity Scales = new YearEntity();

                Scales.year = year;
                Scales.CreatedBy = CreatedBy;
                Scales.CreatedDate = DateTime.Now;
                _Context.YearsCollection.Add(Scales);
                _Context.SaveChanges();
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Update(int id,
            int year, string ModifiedBy)
        {
            try
            {
                YearEntity Scales = _Context.YearsCollection.Where(x => x.id == id).FirstOrDefault();
                if (Scales != null)
                {
                    Scales.year = year;
                    Scales.ModifiedBy = ModifiedBy;
                    Scales.ModifiedDate = DateTime.Now;
                    _Context.Entry(Scales).State = System.Data.Entity.EntityState.Modified;
                    _Context.SaveChanges();
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
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var Scales = _Context.YearsCollection.Where(x => x.id == id).FirstOrDefault();
                if (Scales != null)
                {
                    _Context.YearsCollection.Remove(Scales);
                    _Context.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}