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
    public class UnitesController : ApiController
    {
        private HRContext _Context;
        private APIResult _apiResult;

        public UnitesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult Save(
            string Code,
            int CompanyID,
            string CreatedBy,
            int UnitType,
            string Name,
            string Fax,
            string Phone1,
            string Phone2,
            string Address,
            string LanguageCode)
        {
            try
            {
                // check if unit already exist 
                var unit = _Context.UnitCollection.Any(a =>
                    a.NAME.Trim()==Name.Trim().ToUpper() || a.CODE.Trim()==Code.Trim().ToUpper());

                if (unit)
                    return Ok(new
                        { Data = string.Empty, IsError = true, ErrorMessage = "unit name or username already used" });


                UnitEntity oUnitEntity = new UnitEntity();

                oUnitEntity.CODE = Code;
                oUnitEntity.COMPANY_ID = CompanyID;
                oUnitEntity.created_by = CreatedBy;
                oUnitEntity.created_date = DateTime.Now;
                oUnitEntity.C_UNIT_TYPE_ID = UnitType;
                if (LanguageCode == "en")
                    oUnitEntity.NAME = Name;
                else
                    oUnitEntity.name2 = Name;

                oUnitEntity.FAX = Fax;
                oUnitEntity.PHONE1 = Phone1;
                oUnitEntity.PHONE2 = Phone2;
                oUnitEntity.ADDRESS = Address;


                _Context.Entry(oUnitEntity).State = System.Data.Entity.EntityState.Added;
                _Context.UnitCollection.Add(oUnitEntity);
                _Context.SaveChanges();
                // create user for the unit
                // create user for this employee
                var user = new UsersEntity();
                user.NAME = Name;
                user.created_date = DateTime.Now;
                user.created_by = "Admin";
                user.unit_id = oUnitEntity.ID;
                user.USERNAME = oUnitEntity.CODE;
                user.PASSWORD = "123456";
                user.COMPANY_ID = CompanyID;

                _Context.UsersCollection.Add(user);
                _Context.Entry(user).State = System.Data.Entity.EntityState.Added;
                _Context.SaveChanges();

                // create user role 
                var userRole = new UsersRolesEntity();
                userRole.USERNAME = user.USERNAME;
                userRole.created_date = DateTime.Now;
                userRole.created_by = "Admin";
                userRole.ROLE_ID = (from role in _Context.RolesCollection where role.NAME == "Unit" select role.ID)
                    .First();
                _Context.UsersRolesCollection.Add(userRole);
                _Context.Entry(userRole).State = System.Data.Entity.EntityState.Added;
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
            int CompanyID,
            string ModifiedBy,
            int UnitType,
            string Name,
            string Fax,
            string Phone1,
            string Phone2,
            string Address,
            string LanguageCode)
        {
            try
            {
                UnitEntity oUnitEntity = _Context.UnitCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oUnitEntity != null)
                {
                    var unitCheck= _Context.UnitCollection
                        .FirstOrDefault(x => x.ID != ID && (x.CODE.Trim().ToUpper()==Code.Trim().ToUpper() || x.NAME.Trim().ToUpper()==Name.Trim().ToUpper()));
                    // check if code or name already exist
                    if (unitCheck!=null)
                        return Ok(new
                        {
                            Data = string.Empty, IsError = true, ErrorMessage = "unit name or username already used"
                        });
                    
                    var userRole = _Context.UsersRolesCollection.FirstOrDefault(a => a.USERNAME == oUnitEntity.CODE.Trim());
                    var user = _Context.UsersCollection.FirstOrDefault(a => a.USERNAME == oUnitEntity.CODE.Trim());

                    if (userRole == null)
                    {
                        // create user and user role records

                        // create user for this employee
                        user = new UsersEntity();
                        user.NAME = Name;
                        user.created_date = DateTime.Now;
                        user.created_by = "ADMIN";
                        user.unit_id = ID;
                        user.USERNAME = Code;
                        user.PASSWORD = "123456";
                        user.COMPANY_ID = oUnitEntity.COMPANY_ID;

                        _Context.UsersCollection.Add(user);
                        _Context.Entry(user).State = System.Data.Entity.EntityState.Added;
                        _Context.SaveChanges();

                        // create user role 
                        userRole = new UsersRolesEntity();
                        userRole.USERNAME = Code;
                        userRole.created_date = DateTime.Now;
                        userRole.created_by = "ADMIN";
                        userRole.ROLE_ID =
                            (from role in _Context.RolesCollection where role.NAME == "Unit" select role.ID)
                            .First();

                        _Context.UsersRolesCollection.Add(userRole);
                        _Context.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                        _Context.SaveChanges();
                    }

                    else
                    {
                        // need to change employee number in users and users roles tables

                        if (oUnitEntity.CODE != Code)
                        {
                            var userPassword = user.PASSWORD;
                            // delete user role and recreate it with the new username 
                            _Context.UsersRolesCollection.Remove(userRole);
                            _Context.SaveChanges();
                            _Context.UsersCollection.Remove(user);
                            _Context.SaveChanges();
                            // create user for this employee
                            user = new UsersEntity();
                            user.NAME = Name;
                            user.created_date = DateTime.Now;
                            user.created_by = "ADMIN";
                            user.unit_id = ID;
                            user.USERNAME = Code;
                            user.PASSWORD = "123456";
                            user.COMPANY_ID = oUnitEntity.COMPANY_ID;
                            _Context.UsersCollection.Add(user);
                            _Context.Entry(user).State = System.Data.Entity.EntityState.Added;

                            _Context.SaveChanges();
                            // create user role 
                            userRole = new UsersRolesEntity();
                            userRole.USERNAME = user.USERNAME;
                            userRole.created_date = DateTime.Now;
                            userRole.created_by = "ADMIN";
                            userRole.ROLE_ID =
                                (from role in _Context.RolesCollection where role.NAME == "Unit" select role.ID)
                                .First();

                            _Context.UsersRolesCollection.Add(userRole);
                            _Context.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                            _Context.SaveChanges();
                        }
                    }

                    oUnitEntity.CODE = Code;
                    oUnitEntity.COMPANY_ID = CompanyID;
                    oUnitEntity.modified_by = ModifiedBy;
                    oUnitEntity.modified_date = DateTime.Now;
                    oUnitEntity.C_UNIT_TYPE_ID = UnitType;
                    if (LanguageCode == "en")
                        oUnitEntity.NAME = Name;
                    else
                        oUnitEntity.name2 = Name;
                    oUnitEntity.FAX = Fax;
                    oUnitEntity.PHONE1 = Phone1;
                    oUnitEntity.PHONE2 = Phone2;
                    oUnitEntity.ADDRESS = Address;

                    _Context.Entry(oUnitEntity).State = System.Data.Entity.EntityState.Modified;

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
        [AcceptVerbs("Options")]
        public IHttpActionResult GetUnitsByUniteName(
            int CompanyID,
            string UniteName,
            string LanguageCode)
        {
            try
            {
                var lstUnit = (
                    from unit in _Context.UnitCollection
                    where unit.COMPANY_ID == CompanyID
                          && (string.IsNullOrEmpty(UniteName) || unit.NAME.ToUpper().Contains(UniteName.ToUpper()))
                    select new
                    {
                        unit.ID,
                        unit.ADDRESS,
                        unit.CODE,
                        unit.COMPANY_ID,
                        unit.created_by,
                        unit.created_date,
                        unit.C_UNIT_TYPE_ID,
                        unit.FAX,
                        unit.modified_by,
                        unit.modified_date,
                        NAME = LanguageCode == "en" ? unit.NAME : unit.name2,
                        unit.parent_id,
                        unit.PHONE1,
                        unit.PHONE2,
                    }
                ).ToList();

                return Ok(new { Data = lstUnit, IsError = false, ErrorMessage = string.Empty });
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
                var UnitEntity = _Context.UnitCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (UnitEntity != null)
                {
                    _Context.UnitCollection.Remove(UnitEntity);
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
        public IHttpActionResult getByID(long ID, string LanguageCode)
        {
            try
            {
                var UnitEntity = (from unit in _Context.UnitCollection.Where(x => x.ID == ID)
                    select new
                    {
                        unit.ID,
                        unit.ADDRESS,
                        unit.CODE,
                        unit.COMPANY_ID,
                        unit.created_by,
                        unit.created_date,
                        unit.C_UNIT_TYPE_ID,
                        unit.FAX,
                        unit.modified_by,
                        unit.modified_date,
                        NAME = LanguageCode == "en" ? unit.NAME : unit.name2,
                        unit.parent_id,
                        unit.PHONE1,
                        unit.PHONE2,
                    }).FirstOrDefault();
                if (UnitEntity != null)
                {
                    return Ok(new { Data = UnitEntity, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
        /******************************************************/
        /******************************************************/
        /******************************************************/
        /******************************************************/

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetUnites(
            int CompanyID, string LanguageCode)
        {
            _apiResult = new APIResult();
            try
            {
                var lstUnites = (from unit in _Context.UnitCollection
                    where unit.COMPANY_ID == CompanyID
                    select new
                    {
                        unit.ID,
                        unit.ADDRESS,
                        unit.CODE,
                        unit.COMPANY_ID,
                        unit.created_by,
                        unit.created_date,
                        unit.C_UNIT_TYPE_ID,
                        unit.FAX,
                        unit.modified_by,
                        unit.modified_date,
                        NAME = LanguageCode == "en" ? unit.NAME : unit.name2,
                        unit.parent_id,
                        unit.PHONE1,
                        unit.PHONE2,
                    }).OrderBy(a => a.NAME).ToList();


                return Ok(new { Data = lstUnites, IsError = false, ErrorMessage = string.Empty });
                ;
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.InnerException.StackTrace });
                ;
            }
        }


        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetUnits(int CompanyID, string LanguageCode = "en", string UniteName = "")
        {
            _apiResult = new APIResult();
            try
            {
                var lstUnites = (from unit in _Context.UnitCollection
                    where unit.COMPANY_ID == CompanyID
                          && (string.IsNullOrEmpty(UniteName) || unit.NAME.ToUpper().Contains(UniteName.ToUpper()))
                    orderby unit.NAME
                    select new
                    {
                        unit.ID,
                        unit.ADDRESS,
                        unit.CODE,
                        unit.COMPANY_ID,
                        unit.created_by,
                        unit.created_date,
                        unit.C_UNIT_TYPE_ID,
                        unit.FAX,
                        unit.modified_by,
                        unit.modified_date,
                        NAME = LanguageCode == "en" ? unit.NAME : unit.name2,
                        unit.parent_id,
                        unit.PHONE1,
                        unit.PHONE2,
                        empCount = _Context.EmployeesCollection.Count(a => a.UNIT_ID == unit.ID)
                    }).ToList();


                return Ok(new { Data = lstUnites, IsError = false, ErrorMessage = string.Empty });
                ;
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.InnerException.StackTrace });
                ;
            }
        }
    }
}