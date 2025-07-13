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
    public class PositionsController : ApiController
    {
        private HRContext _Context;

        public PositionsController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetPositions(
            string Name,
            int CompanyID,
            string languageCode,
            int? unitId)
        {
            try
            {
                if (unitId > 0)
                {
                    var positions = (from pos in _Context.PositionCollection
                        join empPos in _Context.EmployeesPostionsCollection on pos.ID equals empPos.POSITION_ID
                        join emp in _Context.EmployeesCollection on empPos.EMP_ID equals emp.ID
                        where pos.COMPANY_ID == CompanyID && emp.UNIT_ID == unitId
                        select pos.ID ).Distinct().ToList();
                    
                    var lstFilteredPositions = (from pos in _Context.PositionCollection
                        //where (string.IsNullOrEmpty(Name) || positions.NAME.ToUpper().Contains(Name))
                        where pos.COMPANY_ID == CompanyID && positions.Any(a=>a ==pos.ID)
                        select new
                        {
                            pos.CODE,
                            pos.COMPANY_ID,
                            pos.created_by,
                            pos.created_date,
                            pos.ID,
                            pos.IS_MANAGMENT,
                            pos.modified_by,
                            pos.modified_date,
                            NAME = (languageCode == "en" ? pos.NAME : pos.name2)
                        }).ToList();
                    var filteredList = (from pos in lstFilteredPositions
                        where (string.IsNullOrEmpty(Name) || pos.NAME.ToUpper().Contains(Name))
                        select pos).ToList();

                    return Ok(new { Data = filteredList, IsError = false, ErrorMessage = string.Empty });
                }

                var lstPosition = (from pos in _Context.PositionCollection
                    //where (string.IsNullOrEmpty(Name) || positions.NAME.ToUpper().Contains(Name))
                    where pos.COMPANY_ID == CompanyID
                    select new
                    {
                        pos.CODE,
                        pos.COMPANY_ID,
                        pos.created_by,
                        pos.created_date,
                        pos.ID,
                        pos.IS_MANAGMENT,
                        pos.modified_by,
                        pos.modified_date,
                        NAME = (languageCode == "en" ? pos.NAME : pos.name2)
                    }).ToList();


                var list = (from pos in lstPosition
                    where (string.IsNullOrEmpty(Name) || pos.NAME.ToUpper().Contains(Name))
                    select pos).ToList();

                return Ok(new { Data = list, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            string Code,
            int CompanyID,
            string CreatedBy,
            int IsManagment,
            string Name,
            string languageCode)
        {
            try
            {
                var projActionPlan = _Context.PositionCollection.Where(x => x.NAME == Name && x.COMPANY_ID == CompanyID)
                    .FirstOrDefault();
                if (projActionPlan != null)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "That Position Exist Before" });
                }

                PositionEntity oPositionEntity = new PositionEntity();

                oPositionEntity.CODE = Code;
                oPositionEntity.COMPANY_ID = CompanyID;
                oPositionEntity.created_by = CreatedBy;
                oPositionEntity.created_date = DateTime.Now;
                oPositionEntity.IS_MANAGMENT = IsManagment;
                if (languageCode == "en")
                    oPositionEntity.NAME = Name;
                else
                    oPositionEntity.name2 = Name;

                _Context.Entry(oPositionEntity).State = System.Data.Entity.EntityState.Added;
                _Context.PositionCollection.Add(oPositionEntity);
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
            string Code,
            string ModifiedBy,
            int IsManagment,
            string Name,
            string languageCode)
        {
            try
            {
                PositionEntity oPositionEntity = _Context.PositionCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oPositionEntity != null)
                {
                    oPositionEntity.CODE = Code;
                    oPositionEntity.modified_by = ModifiedBy;
                    oPositionEntity.modified_date = DateTime.Now;
                    oPositionEntity.IS_MANAGMENT = IsManagment;
                    if (languageCode == "en")
                        oPositionEntity.NAME = Name;
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
                var oPositionEntity = _Context.PositionCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oPositionEntity != null)
                {
                    //check if there is employees assigned to this position
                    var employees = (from emp in _Context.EmployeesCollection
                        join emp_Position in _Context.EmployeesPostionsCollection on emp.ID equals emp_Position.EMP_ID
                        join _pos in _Context.PositionCollection on emp_Position.POSITION_ID equals _pos.ID
                        select new
                        {
                           posID= _pos.ID,
                            emp.ID
                        }).Count(a=>a.posID == ID);
                    if (employees > 0)
                    {
                        
                        return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "There are Employees Assigned to This Job Title " });

                    }

                    _Context.PositionCollection.Remove(oPositionEntity);
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
                var position = (from pos in _Context.PositionCollection
                    where pos.ID == ID
                    select new
                    {
                        pos.CODE,
                        pos.COMPANY_ID,
                        pos.created_by,
                        pos.created_date,
                        pos.ID,
                        pos.IS_MANAGMENT,
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