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
    public class ScalesController : ApiController
    {
        private HRContext _Context;
        private APIResult _apiResult;

        public ScalesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult GetScales(
            int CompanyID)
        {
            _apiResult = new APIResult();
            try
            {
                var lstscale = (from scale in _Context.ScalesCollection
                    where scale.COMPANY_ID == CompanyID
                    select scale).OrderBy(x => x.SCALE_NUMBER).ToList();


                return Ok(new { Data = lstscale, IsError = false, ErrorMessage = string.Empty });
                ;
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.InnerException.StackTrace });
                ;
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int ScaleNumber,
            int CompanyID,
            string CreatedBy, string Name, string ScaleCode, string languageCode)
        {
            try
            {
                ScalesEntity Scales = new ScalesEntity();
                var IsExist = (from scale in _Context.ScalesCollection
                    where scale.COMPANY_ID == CompanyID && scale.SCALE_NUMBER == ScaleNumber
                    select scale).Count() > 0;
                if (IsExist)
                {
                    return Ok(new
                        { Data = string.Empty, IsError = true, ErrorMessage = "This Scale is already exist." });
                }

                Scales.SCALE_NUMBER = ScaleNumber;
                Scales.SCALE_CODE = ScaleCode;
                Scales.COMPANY_ID = CompanyID;
                Scales.created_by = CreatedBy;
                Scales.created_date = DateTime.Now;
                if (languageCode == "en")
                    Scales.NAME = Name;
                else
                    Scales.name2 = Name;
                _Context.Entry(Scales).State = System.Data.Entity.EntityState.Added;
                _Context.ScalesCollection.Add(Scales);
                _Context.SaveChanges();
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Update(
            int ID,
            int ScaleNumber,
            int CompanyID, string ModifiedBy, string Name, string ScaleCode, string languageCode)
        {
            try
            {
                ScalesEntity Scales = _Context.ScalesCollection.Where(x => x.ID == ID).FirstOrDefault();
                var IsExist = (from scale in _Context.ScalesCollection
                    where scale.COMPANY_ID == CompanyID && (scale.NAME == Name || scale.name2 == Name) &&
                          scale.ID != Scales.ID
                    select scale).Count() > 0;
                if (IsExist)
                {
                    return Ok(new
                        { Data = string.Empty, IsError = true, ErrorMessage = "This Scale is already exist." });
                }

                if (Scales != null)
                {
                    Scales.SCALE_NUMBER = ScaleNumber;
                    Scales.SCALE_CODE = ScaleCode;
                    Scales.COMPANY_ID = CompanyID;
                    Scales.modified_by = ModifiedBy;
                    Scales.modified_date = DateTime.Now;
                    if (languageCode == "en")
                        Scales.NAME = Name;
                    else
                        Scales.name2 = Name;
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
        public IHttpActionResult Delete(long ID)
        {
            try
            {
                var Scales = _Context.ScalesCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (Scales != null)
                {
                    _Context.ScalesCollection.Remove(Scales);
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
        public IHttpActionResult getByID(long ID, string languageCode)
        {
            try
            {
                var position = (from pos in _Context.ScalesCollection
                    where pos.ID == ID
                    select new
                    {
                        pos.COMPANY_ID,
                        pos.created_by,
                        pos.created_date,
                        pos.ID,
                        pos.SCALE_CODE,
                        pos.SCALE_NUMBER,
                        pos.modified_by,
                        pos.modified_date,
                        NAME = (languageCode == "en" ? pos.NAME : pos.name2)
                    }).FirstOrDefault();
                if (position != null)
                {
                    return Ok(new { Data = position, IsError = false, ErrorMessage = string.Empty });
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