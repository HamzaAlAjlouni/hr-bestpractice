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
    public class UsersController : ApiController
    {
        private HRContext _Context;
        private HRContext db;
        private APIResult _apiResult;

        public UsersController()
        {
            _Context = new HRContext();
            db = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public APIResult ValidUser(
            string Username,
            string Password)
        {
            _apiResult = new APIResult();
            try
            {
                var lstUsers = (from Users in _Context.UsersCollection
                    join userRole in _Context.UsersRolesCollection on Users.USERNAME equals userRole.USERNAME
                    join role in _Context.RolesCollection on userRole.ROLE_ID equals role.ID
                    where Users.USERNAME.ToUpper() == Username.ToUpper()
                          && Users.PASSWORD == Password
                    select new
                    {
                        username = Users.USERNAME,
                        fullname = role.NAME == "Employee"
                            ? (from emp in _Context.EmployeesCollection
                                where emp.employee_number == Users.USERNAME
                                select emp.name1_1 + " " + emp.name1_4).FirstOrDefault()
                            : Users.NAME,
                        email = Users.USERNAME,
                        COMPANY_ID = Users.COMPANY_ID,
                        authToken = "d}C7cC3C=Ln?2x-",
                        refreshToken = "d}C7cC3C=Ln?2x-",
                        unitId = Users.unit_id,
                        Role = role.NAME,
                        RoleId = role.ID,
                        Position = role.NAME == "Employee"
                            ? (from emp in _Context.EmployeesCollection
                                join empPosition in _Context.EmployeesPostionsCollection on emp.ID equals empPosition
                                    .EMP_ID
                                join position in _Context.PositionCollection on empPosition.POSITION_ID equals position
                                    .ID
                                where emp.employee_number == Users.USERNAME
                                select position.NAME).FirstOrDefault()
                            : "",
                        Unit = role.NAME == "Employee"
                            ? (from emp in _Context.EmployeesCollection
                                join unit in _Context.UnitCollection on emp.UNIT_ID equals unit.ID
                                where emp.employee_number == Users.USERNAME
                                select unit.NAME).FirstOrDefault()
                            : ""
                    });

                var json = JsonConvert.SerializeObject(lstUsers);

                _apiResult.Data = json;
                _apiResult.IsError = false;

                return _apiResult;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                _apiResult.ErrorMessage = ex.Message;
                _apiResult.IsError = true;
                return _apiResult;
            }
        }


        [HttpPost]
        [AcceptVerbs("Options")]
        public APIResult LoadBy(
            string Username = null)
        {
            _apiResult = new APIResult();
            try
            {
                List<UsersEntity> lstUsers = (from Users in _Context.UsersCollection
                        where (Users.USERNAME.ToUpper().Contains(Username.ToUpper()) || string.IsNullOrEmpty(Username))
                        select Users
                    ).ToList();

                var json = JsonConvert.SerializeObject(lstUsers.ToArray());

                _apiResult.Data = json;
                _apiResult.IsError = false;

                return _apiResult;
            }
            catch (Exception ex)
            {
                _apiResult.ErrorMessage = ex.InnerException.StackTrace;
                _apiResult.IsError = true;
                return _apiResult;
            }
        }


        #region Local Resources

        [HttpGet]
        public IHttpActionResult GetPageLocalResources(string url, int orgID, string culture_name)
        {
            try
            {
                var resrouces = _Context.tbl_resourcesCollection.Where(x =>
                    x.url == url && x.org_id == orgID && x.culture_name.ToLower() == culture_name.ToLower()).ToList();

                return Ok(new { Data = resrouces, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion


        [HttpGet]
        public IHttpActionResult GetSystemUsers(int companyID)
        {
            try
            {
                var users = db.UsersCollection.Where(e => e.COMPANY_ID == companyID).Select
                (x => new
                {
                    USERNAME = x.USERNAME.ToUpper(),
                    x.NAME
                }).ToList();

                if (users != null)
                {
                    return Ok(new { Data = users, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetMenuPages(int companyID)
        {
            try
            {
                var meuns = db.MenusCollection.Where(e => e.COMPANY_ID == companyID).Select
                (x => new
                {
                    x.ID,
                    x.URL,
                    x.NAME
                }).ToList();

                if (meuns != null)
                {
                    return Ok(new { Data = meuns, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetUsersList(int companyID, string searchKey)
        {
            try
            {
                var users = string.IsNullOrEmpty(searchKey)
                    ? db.UsersCollection.Where(e => e.COMPANY_ID == companyID).Select
                    (x => new
                    {
                        x.USERNAME,
                        x.NAME,
                        x.PASSWORD
                    }).ToList()
                    : db.UsersCollection
                        .Where(e => e.COMPANY_ID == companyID && e.NAME.Trim().Contains(searchKey.Trim())).Select
                        (x => new
                        {
                            x.USERNAME,
                            x.NAME,
                            x.PASSWORD
                        }).ToList();
                if (users.Any())
                {
                    return Ok(new { Data = users, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult ResetAccountPassword(string newPassword, string username)
        {
            try
            {
                var user = _Context.UsersCollection.Where(a => a.USERNAME == username).FirstOrDefault();

                if (user == null)
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                if (string.IsNullOrEmpty(newPassword))
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "invalid password" });
                user.PASSWORD = newPassword;
                _Context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _Context.SaveChanges();
                return Ok(new { Data = "", IsError = false, ErrorMessage = "password upadted" });
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