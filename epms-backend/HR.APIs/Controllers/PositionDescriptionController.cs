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
    public class PositionDescriptionController : ApiController
    {
        private HRContext _Context;

        public PositionDescriptionController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetPositionDescription(
            int PositionID, string languageCode)
        {
            try
            {
                var lstPosition = (from pos in _Context.PositionDescriptionEntityCollection
                    where pos.Position_ID == PositionID
                    select new
                    {
                        pos.created_by,
                        pos.created_date,
                        pos.id,
                        pos.modified_by,
                        pos.modified_date,
                        Name = (languageCode == "en" ? pos.Name : pos.name2),
                        pos.Position_ID
                    }).ToList();


                return Ok(new { Data = lstPosition, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        public IHttpActionResult GetPositionDescriptionByEmployee(
            int EmployeeID, string languageCode)
        {
            try
            {
                var lstPosition = (from pos in _Context.PositionDescriptionEntityCollection
                    join emp in _Context.EmployeesPostionsCollection on pos.Position_ID equals emp.POSITION_ID
                    where emp.EMP_ID == EmployeeID
                    select new
                    {
                        pos.created_by,
                        pos.created_date,
                        pos.id,
                        pos.modified_by,
                        pos.modified_date,
                        Name = (languageCode == "en" ? pos.Name : pos.name2),
                        pos.Position_ID
                    }).ToList();


                return Ok(new { Data = lstPosition, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int PositionID,
            string CreatedBy,
            string Name,
            string languageCode)
        {
            try
            {
                PositionDescriptionEntity oPositionEntity = new PositionDescriptionEntity();

                oPositionEntity.Position_ID = PositionID;
                oPositionEntity.created_by = CreatedBy;
                oPositionEntity.created_date = DateTime.Now;
                if (languageCode == "en")
                    oPositionEntity.Name = Name;
                else
                    oPositionEntity.name2 = Name;

                _Context.Entry(oPositionEntity).State = System.Data.Entity.EntityState.Added;
                _Context.PositionDescriptionEntityCollection.Add(oPositionEntity);
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
            string ModifiedBy,
            string Name,
            string languageCode)
        {
            try
            {
                PositionDescriptionEntity oPositionEntity =
                    _Context.PositionDescriptionEntityCollection.Where(x => x.id == ID).FirstOrDefault();
                if (oPositionEntity != null)
                {
                    oPositionEntity.modified_by = ModifiedBy;
                    oPositionEntity.modified_date = DateTime.Now;
                    if (languageCode == "en")
                        oPositionEntity.Name = Name;
                    else
                        oPositionEntity.name2 = Name;

                    _Context.Entry(oPositionEntity).State = System.Data.Entity.EntityState.Modified;

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
                var oPositionEntity = _Context.PositionDescriptionEntityCollection.Where(x => x.id == ID)
                    .FirstOrDefault();
                if (oPositionEntity != null)
                {
                    _Context.PositionDescriptionEntityCollection.Remove(oPositionEntity);
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
                var oPositionEntity = (from pos in _Context.PositionDescriptionEntityCollection
                    where pos.id == ID
                    select new
                    {
                        pos.created_by,
                        pos.created_date,
                        pos.id,
                        pos.modified_by,
                        pos.modified_date,
                        Name = (languageCode == "en" ? pos.Name : pos.name2),
                        pos.Position_ID
                    }).FirstOrDefault();
                if (oPositionEntity != null)
                {
                    return Ok(new { Data = oPositionEntity, IsError = false, ErrorMessage = string.Empty });
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