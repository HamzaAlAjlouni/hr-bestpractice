using HR.Busniess;
using HR.Entities.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.OleDb;
using System.Data;
using System.IO;
using ExcelDataReader;
using HR.Entities;
using WebGrease.Css.Extensions;
using System.Data.Entity;
using System.Threading.Tasks;
using HR.Entities.Admin;

namespace HR.APIs.Controllers
{
    public class EmployeesController : ApiController
    {
        HRContext db;

        public EmployeesController()
        {
            db = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult LoadEmplyeeByCompanyID(int CompanyID, string LanguageCode, int? unitID)
        {
            try
            {
                var employee = (from emp in db.EmployeesCollection.Where(x => x.COMPANY_ID == CompanyID
                                                                              && x.IS_STATUS == 1 &&
                                                                              (unitID == null || x.UNIT_ID == unitID))
                    orderby emp.name1_1 + " " + emp.name1_4
                    select new
                    {
                        emp.ADDRESS,
                        // emp.Branches,
                        emp.BRANCH_ID,
                        // emp.Companies,
                        emp.COMPANY_ID,
                        emp.created_by,
                        emp.created_date,
                        emp.employee_number,
                        emp.END_DATE,
                        emp.ID,
                        emp.IMAGE,
                        emp.IS_STATUS,
                        emp.modified_by,
                        emp.modified_date,
                        name1_1 = (LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_1)) ||
                                  string.IsNullOrEmpty(emp.name2_1)
                            ? emp.name1_1
                            : emp.name2_1,
                        name1_2 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_2) ||
                                  string.IsNullOrEmpty(emp.name2_2)
                            ? emp.name1_2
                            : emp.name2_2,
                        name1_3 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_3) ||
                                  string.IsNullOrEmpty(emp.name2_3)
                            ? emp.name1_3
                            : emp.name2_3,
                        name1_4 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_4) ||
                                  string.IsNullOrEmpty(emp.name2_4)
                            ? emp.name1_4
                            : emp.name2_4,
                        // emp.Parent,
                        emp.PARENT_ID,
                        emp.PHONE1,
                        emp.PHONE2,
                        // emp.Scales,
                        emp.SCALE_ID,
                        emp.START_DATE,
                        emp.UNITS,
                        emp.UNIT_ID,
                    }).ToList();

                return Ok(new { Data = employee, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult SearchEmployees(string empNumber, string empName, int? unitID, int? position,
            int? status, string LanguageCode, long companyId)
        {
            try
            {
                //var emp2222 = db.EmployeesCollection.Where(x => x.ID == 258).FirstOrDefault();
                //db.EmployeesCollection.Remove(emp2222);
                //db.SaveChanges();
                var employees = (from emp in db.EmployeesCollection
                    join emp_Position in db.EmployeesPostionsCollection on emp.ID equals emp_Position.EMP_ID
                    join _pos in db.PositionCollection on emp_Position.POSITION_ID equals _pos.ID
                    select new
                    {
                        emp.ID, emp_Position.POSITION_ID, _pos.name2, _pos.NAME, emp_Position.EMP_ID,
                        emp.employee_number, emp.name1_1, emp.name1_2, emp.name1_3,
                        emp.name1_4, emp.name2_1, emp.name2_2, emp.name2_3, emp.name2_4, emp.Parent, emp.UNITS,
                        emp.UNIT_ID, emp.IS_STATUS
                    }).AsQueryable();
                var cnt = employees.Count();
                if (!string.IsNullOrEmpty(empNumber))
                {
                    employees = employees.Where(x => x.employee_number.ToLower().Contains(empNumber.ToLower()))
                        .AsQueryable();
                }

                if (!string.IsNullOrEmpty(empName))
                {
                    employees = employees.Where(x =>
                        x.name1_1.ToLower().Contains(empName.ToLower()) ||
                        x.name1_2.ToLower().Contains(empName.ToLower()) ||
                        x.name1_3.ToLower().Contains(empName.ToLower()) ||
                        x.name1_4.ToLower().Contains(empName.ToLower())).AsQueryable();
                }

                if (unitID != null)
                {
                    employees = employees.Where(x => x.UNIT_ID == unitID);
                }

                if (status != null)
                {
                    employees = employees.Where(x => x.IS_STATUS == status).AsQueryable();
                }

                if (position != null)
                {
                    employees = employees.Where(x => x.POSITION_ID == position);
                }

                var final = employees.ToList().Select(x =>
                {
                    return new
                    {
                        FullName = ((LanguageCode == "en" && !string.IsNullOrEmpty(x.name1_1)) ||
                                    string.IsNullOrEmpty(x.name2_1)
                                       ? x.name1_1
                                       : x.name2_1 + " ") + " " +
                                   (LanguageCode == "en" && !string.IsNullOrEmpty(x.name1_2) ||
                                    string.IsNullOrEmpty(x.name2_2)
                                       ? x.name1_2
                                       : x.name2_2 + " ") + " " +
                                   (LanguageCode == "en" && !string.IsNullOrEmpty(x.name1_3) ||
                                    string.IsNullOrEmpty(x.name2_3)
                                       ? x.name1_3
                                       : x.name2_3 + " ") + " " +
                                   (LanguageCode == "en" && !string.IsNullOrEmpty(x.name1_4) ||
                                    string.IsNullOrEmpty(x.name2_4)
                                       ? x.name1_4
                                       : x.name2_4),
                        x.ID,
                        x.employee_number,
                        Unit = ((LanguageCode == "en" && !string.IsNullOrEmpty(x.UNITS.NAME)) ||
                                string.IsNullOrEmpty(x.UNITS.name2)
                            ? x.UNITS.NAME
                            : x.UNITS.name2),
                        Manager = ((LanguageCode == "en" && !string.IsNullOrEmpty(x.Parent?.name1_1)) ||
                                   string.IsNullOrEmpty(x.Parent?.name2_1)
                                      ? x.Parent?.name1_1
                                      : x.Parent?.name2_1) + " " +
                                  ((LanguageCode == "en" && !string.IsNullOrEmpty(x.Parent?.name1_4)) ||
                                   string.IsNullOrEmpty(x.Parent?.name2_4)
                                      ? x.Parent?.name1_4
                                      : x.Parent?.name2_4),
                        Status = (x.IS_STATUS == 1)
                            ? (LanguageCode == "en" ? "Active" : "فعال")
                            : (LanguageCode == "en" ? "Inactive" : "غير فعال"),

                        positionId = x?.POSITION_ID,
                        Position = (LanguageCode == "en" ? x?.NAME : x?.name2),
                    };
                });

                //var ss2 = employees.Select(x=>x.ID).ToList();
                //var id = 0;
                //var ss1 = final.Select(x => x.ID).ToList();

                //ss2.ForEach(itemmm => {
                //    if (!ss1.Contains(itemmm))
                //        id = itemmm;
                //});
                //var ss = employees.Where(x=>ss1.Contains(x.ID)==false).ToList();
                //var ss3 = employees.Where(x=>!ss1.Contains(x.ID)).ToList();
                return Ok(new
                    { Data = final.ToList(), aa = final.Count(), IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpPost]
        public IHttpActionResult SaveEmployee()
        {
            try
            {
                var empData = JsonConvert.DeserializeObject<dynamic>(HttpContext.Current.Request.Form["empData"]);

                string imagePath = ConfigurationManager.AppSettings["AppURL"] + "Default.jpg";
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/EmployeeImages/" + file.FileName));
                        imagePath = ConfigurationManager.AppSettings["AppURL"] + file.FileName;
                    }
                }

                string no = empData.EmpNumber;
                int crg = empData.CompanyID;

                if (db.EmployeesCollection.Where(x => x.employee_number == no && x.COMPANY_ID == crg).ToList().Count >
                    0)
                {
                    return Ok(new
                        { Data = string.Empty, IsError = true, ErrorMessage = "Employee Number Already Exist." });
                    ;
                }

                EmployeesEntity employee = new EmployeesEntity();


                employee.employee_number = empData.EmpNumber;

                if (empData.LanguageCode == "en")
                {
                    employee.name1_1 = empData.firstName;
                    employee.name1_2 = empData.secondName;
                    employee.name1_3 = empData.thirdName;
                    employee.name1_4 = empData.famaliyName;
                }
                else
                {
                    employee.name2_1 = empData.firstName;
                    employee.name2_2 = empData.secondName;
                    employee.name2_3 = empData.thirdName;
                    employee.name2_4 = empData.famaliyName;
                }

                employee.UNIT_ID = empData.unitID;
                employee.SCALE_ID = empData.scaleID;
                employee.ADDRESS = empData.address;
                employee.PHONE1 = empData.phone1;
                employee.PHONE2 = empData.phone2;
                employee.IS_STATUS = empData.status;

                employee.created_by = empData.Username;
                employee.created_date = DateTime.Now;
                employee.START_DATE = DateTime.Now;
                employee.BRANCH_ID = empData.Branch;
                employee.COMPANY_ID = empData.CompanyID;
                employee.PARENT_ID = empData.ManagerID;
                employee.IMAGE = imagePath;


                db.EmployeesCollection.Add(employee);
                db.Entry(employee).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                EmployeePositionsEntity emp_pos = new EmployeePositionsEntity();

                emp_pos.created_by = empData.Username;
                emp_pos.created_date = DateTime.Now;
                emp_pos.EMP_ID = db.EmployeesCollection.Select(x => x.ID).Max();
                emp_pos.POSITION_ID = empData.postionID;
                emp_pos.YEAR = db.YearsCollection.Where(x => x.id == DateTime.Now.Year).Select(x => x.id)
                    .FirstOrDefault();

                db.EmployeesPostionsCollection.Add(emp_pos);
                db.Entry(emp_pos).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                // create user for this employee
                var user = new UsersEntity();
                user.NAME = empData.firstName;
                user.created_date = DateTime.Now;
                user.created_by = "Admin";
                user.unit_id = empData.unitID;
                user.USERNAME = empData.EmpNumber;
                user.PASSWORD = "123456";
                user.COMPANY_ID = empData.CompanyID;

                db.UsersCollection.Add(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                // create user role 
                var userRole = new UsersRolesEntity();
                userRole.USERNAME = user.USERNAME;
                userRole.created_date = DateTime.Now;
                userRole.created_by = "Admin";
                userRole.ROLE_ID =
                    (from role in db.RolesCollection where role.NAME == "Employee" select role.ID).First();

                db.UsersRolesCollection.Add(userRole);
                db.Entry(userRole).State = System.Data.Entity.EntityState.Added;
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

        [HttpPost]
        public IHttpActionResult UpdateEmployee()
        {
            try
            {
                var empData = JsonConvert.DeserializeObject<dynamic>(HttpContext.Current.Request.Form["empData"]);

                int EmpID = empData.ID;
                EmployeesEntity employee = db.EmployeesCollection.Where(x => x.ID == EmpID).FirstOrDefault();


                string imagePath = ConfigurationManager.AppSettings["AppURL"] + "Default.jpg";
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/EmployeeImages/" + file.FileName));
                        employee.IMAGE = ConfigurationManager.AppSettings["AppURL"] + file.FileName;
                    }
                }

                var userRole = db.UsersRolesCollection.FirstOrDefault(a => a.USERNAME == employee.employee_number);
                var user = db.UsersCollection.FirstOrDefault(a => a.USERNAME == employee.employee_number);

                if (userRole == null)
                {
                    // create user and user role records

                    // create user for this employee
                    user = new UsersEntity();
                    user.NAME = empData.firstName;
                    user.created_date = DateTime.Now;
                    user.created_by = "ADMIN";
                    user.unit_id = empData.unitID;
                    user.USERNAME = empData.EmpNumber;
                    user.PASSWORD = "123456";
                    user.COMPANY_ID = empData.CompanyID;

                    db.UsersCollection.Add(user);
                    db.Entry(user).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();

                    // create user role 
                    userRole = new UsersRolesEntity();
                    userRole.USERNAME = user.USERNAME;
                    userRole.created_date = DateTime.Now;
                    userRole.created_by = "ADMIN";
                    userRole.ROLE_ID = (from role in db.RolesCollection where role.NAME == "Employee" select role.ID)
                        .First();

                    db.UsersRolesCollection.Add(userRole);
                    db.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }

                else
                {
                    // need to change employee number in users and users roles tables

                    if (employee.employee_number != (string)empData.EmpNumber)
                    {
                        var userPassword = user.PASSWORD;
                        // delete user role and recreate it with the new username 
                        db.UsersRolesCollection.Remove(userRole);
                        db.UsersCollection.Remove(user);
                        db.SaveChanges();
                        // create user for this employee
                        user = new UsersEntity();
                        user.NAME = empData.firstName;
                        user.created_date = DateTime.Now;
                        user.created_by = "ADMIN";
                        user.unit_id = empData.unitID;
                        user.USERNAME = empData.EmpNumber;
                        user.PASSWORD = userPassword !="123456" ? userPassword : "123456";
                        user.COMPANY_ID = empData.CompanyID;
                        db.UsersCollection.Add(user);
                        db.Entry(user).State = System.Data.Entity.EntityState.Added;

                        db.SaveChanges();
                        // create user role 
                        userRole = new UsersRolesEntity();
                        userRole.USERNAME = user.USERNAME;
                        userRole.created_date = DateTime.Now;
                        userRole.created_by = "ADMIN";
                        userRole.ROLE_ID =
                            (from role in db.RolesCollection where role.NAME == "Employee" select role.ID)
                            .First();

                        db.UsersRolesCollection.Add(userRole);
                        db.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                }

                employee.ID = empData.ID;
                employee.employee_number = empData.EmpNumber;
                if (empData.LanguageCode == "en")
                {
                    employee.name1_1 = empData.firstName;
                    employee.name1_2 = empData.secondName;
                    employee.name1_3 = empData.thirdName;
                    employee.name1_4 = empData.famaliyName;
                }
                else
                {
                    employee.name2_1 = empData.firstName;
                    employee.name2_2 = empData.secondName;
                    employee.name2_3 = empData.thirdName;
                    employee.name2_4 = empData.famaliyName;
                }

                employee.UNIT_ID = empData.unitID;
                //employee.postionID = empData.postionID;
                employee.SCALE_ID = empData.scaleID;
                employee.ADDRESS = empData.address;
                employee.PHONE1 = empData.phone1;
                employee.PHONE2 = empData.phone2;
                employee.IS_STATUS = empData.status;

                employee.created_by = empData.Username;
                // employee.created_date = DateTime.Now;
                // employee.START_DATE = DateTime.Now;
                employee.BRANCH_ID = empData.Branch;
                employee.COMPANY_ID = empData.CompanyID;
                employee.PARENT_ID = empData.ManagerID;

                db.EmployeesCollection.Add(employee);
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                EmployeePositionsEntity emp_pos =
                    db.EmployeesPostionsCollection.Where(x => x.EMP_ID == EmpID).FirstOrDefault();

                if (emp_pos != null)
                {
                    emp_pos.POSITION_ID = empData.postionID;

                    db.EmployeesPostionsCollection.Add(emp_pos);
                    db.Entry(emp_pos).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    EmployeePositionsEntity emp_pos_new = new EmployeePositionsEntity();

                    emp_pos_new.created_by = empData.Username;
                    emp_pos_new.created_date = DateTime.Now;
                    emp_pos_new.EMP_ID = EmpID;
                    emp_pos_new.POSITION_ID = empData.postionID;
                    emp_pos_new.YEAR = db.YearsCollection.Where(x => x.id == DateTime.Now.Year).Select(x => x.id)
                        .FirstOrDefault();

                    db.EmployeesPostionsCollection.Add(emp_pos_new);
                    db.Entry(emp_pos_new).State = System.Data.Entity.EntityState.Added;
                }

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
        public IHttpActionResult LoadEmplyeeByID(int empID, string LanguageCode)
        {
            try
            {
                var employee = db.EmployeesCollection.Where(x => x.ID == empID).Select(x => new
                {
                    x.ID,
                    x.IMAGE,
                    x.IS_STATUS,
                    name1_1 = LanguageCode == "en" ? x.name1_1 : x.name2_1,
                    name1_2 = LanguageCode == "en" ? x.name1_2 : x.name2_2,
                    name1_3 = LanguageCode == "en" ? x.name1_3 : x.name2_3,
                    name1_4 = LanguageCode == "en" ? x.name1_4 : x.name2_4,
                    x.employee_number,
                    x.UNIT_ID,
                    x.SCALE_ID,
                    x.PHONE1,
                    x.PHONE2,
                    PositionID = db.EmployeesPostionsCollection.Where(a => a.EMP_ID == x.ID)
                        .OrderByDescending(a => a.YEAR).Select(a => a.POSITION_ID).FirstOrDefault(),
                    ManagerID = x.PARENT_ID,
                }).FirstOrDefault();
                return Ok(new { Data = employee, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult LoadManagers(int? empID, string LanguageCode)
        {
            try
            {
                var employees = (from emp in db.EmployeesCollection.Where(x => (empID == null || x.ID != empID))
                    select new
                    {
                        emp.ADDRESS,
                        emp.Branches,
                        emp.BRANCH_ID,
                        emp.Companies,
                        emp.COMPANY_ID,
                        emp.created_by,
                        emp.created_date,
                        emp.employee_number,
                        emp.END_DATE,
                        emp.ID,
                        emp.IMAGE,
                        emp.IS_STATUS,
                        emp.modified_by,
                        emp.modified_date,
                        name1_1 = (LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_1)) ||
                                  string.IsNullOrEmpty(emp.name2_1)
                            ? emp.name1_1
                            : emp.name2_1,
                        name1_2 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_2) ||
                                  string.IsNullOrEmpty(emp.name2_2)
                            ? emp.name1_2
                            : emp.name2_2,
                        name1_3 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_3) ||
                                  string.IsNullOrEmpty(emp.name2_3)
                            ? emp.name1_3
                            : emp.name2_3,
                        name1_4 = LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_4) ||
                                  string.IsNullOrEmpty(emp.name2_4)
                            ? emp.name1_4
                            : emp.name2_4,

                        fullName = ((LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_1)) ||
                                    string.IsNullOrEmpty(emp.name2_1)
                                       ? emp.name1_1
                                       : emp.name2_1) + " " +
                                   ((LanguageCode == "en" && !string.IsNullOrEmpty(emp.name1_4)) ||
                                    string.IsNullOrEmpty(emp.name2_4)
                                       ? emp.name1_4
                                       : emp.name2_4),
                        emp.Parent,
                        emp.PARENT_ID,
                        emp.PHONE1,
                        emp.PHONE2,

                        emp.Scales,
                        emp.SCALE_ID,
                        emp.START_DATE,
                        emp.UNITS,
                        emp.UNIT_ID,
                    }).ToList();


                return Ok(new { Data = employees, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult RemoveEmployee(int empID)
        {
            try
            {
                var employees = db.EmployeesCollection.Where(x => x.ID == empID).FirstOrDefault();

                var empPosition = db.EmployeesPostionsCollection.Where(x => x.EMP_ID == empID).FirstOrDefault();
                var userRole = db.UsersRolesCollection.FirstOrDefault(a => a.USERNAME == employees.employee_number);
                var user = db.UsersCollection.FirstOrDefault(a => a.USERNAME == employees.employee_number);

                
                if (empPosition != null)
                {
                    db.EmployeesPostionsCollection.Remove(empPosition);
                    db.SaveChanges();
                }

                if (userRole != null)
                {
                    db.UsersRolesCollection.Remove(userRole);
                    db.SaveChanges();
                }
                if (user != null)
                {
                    db.UsersCollection.Remove(user);
                    db.SaveChanges();
                }

                db.EmployeesCollection.Remove(employees);
                
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

        #region Employee Structure

        [HttpGet]
        public IHttpActionResult LoadEmployeesChart(int compID, int? branch, int? unit, int? Manager,
            string LanguageCode)
        {
            try
            {
                var structure = db.EmployeesCollection.Select(x => new
                {
                    id = x.ID,
                    pid = x.PARENT_ID,
                    name = (LanguageCode == "en" ? x.name1_1 + " " + x.name1_4 : x.name2_1 + " " + x.name2_4),
                    unit = x.UNITS.NAME,
                    scale = x.Scales.NAME,
                    img = x.IMAGE
                }).ToList();
                return Ok(new { Data = structure, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        public dynamic SomeMethod(string LanguageCode)
        {
            // here you get your `list`
            var tree = GetTree(db.EmployeesCollection.ToList(), null, LanguageCode);
            return tree;
        }

        public List<empStrunct> GetTree(List<EmployeesEntity> list, long? parent, string LanguageCode)
        {
            return list.Where(x => x.PARENT_ID == parent).Select(x => new empStrunct
            {
                id = x.ID,
                Name = x.name1_1 + " " + x.name1_4,
                Unit = x.UNITS.NAME,
                img = x.IMAGE,
                title = db.EmployeesPostionsCollection.Where(a => a.EMP_ID == x.ID).Select(a => a.Positions.NAME)
                    .FirstOrDefault(),
                children = GetTree(list, x.ID, LanguageCode)
            }).ToList();
        }

        [HttpGet]
        public IHttpActionResult LoadEmployeesNewChart(int compID, int? branch, int? unit, int? Manager,
            string LanguageCode)
        {
            try
            {
                var structure = SomeMethod(LanguageCode); //db.EmployeesCollection.Select(x => new
                //{
                //    id = x.ID,
                //    pid = x.PARENT_ID,
                //    name = x.name1_1 + " " + x.name1_4,
                //    unit = x.UNITS.NAME,
                //    scale = x.Scales.NAME,
                //    img = x.IMAGE,
                //    children = db.EmployeesCollection.Where(w => w.PARENT_ID == x.ID).Select(w => new { 
                //    w.ID,
                //    name=w.name1_1 +" " + w.name1_4
                //    })
                //}).ToList();
                return Ok(new { Data = structure, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdateManager(int empID, int managerID)
        {
            try
            {
                var employee = db.EmployeesCollection.Where(x => x.ID == empID).FirstOrDefault();
                employee.PARENT_ID = managerID;


                db.EmployeesCollection.Add(employee);
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Import File, Adding By Yousef Sleit

        [HttpPost]
        public HttpResponseMessage Import([FromUri] string userName, [FromUri] int companyId, [FromUri] int branchId)
        {
            try
            {
                string message = "";
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }

                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        message = "This file format is not supported";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();
                    var finalRecords = excelRecords.Tables[0];
                    if (finalRecords.Rows.Count == 0)
                    {
                        message = "No Data Found";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                    }

                    if (finalRecords.Columns.Count < 12)
                    {
                        message = "Missing columns";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                    }
                    else
                    {
                        List<EmployeesEntity> lst = new List<EmployeesEntity>();
                        for (int i = 0; i < finalRecords.Rows.Count; i++)
                        {
                            EmployeesEntity oEmp = new EmployeesEntity();
                            if (finalRecords.Rows[i][0].ToString() != ""
                                && finalRecords.Rows[i][1].ToString() != ""
                                && finalRecords.Rows[i][4].ToString() != ""
                                && finalRecords.Rows[i][5].ToString() != ""
                                && finalRecords.Rows[i][6].ToString() != ""
                                && finalRecords.Rows[i][7].ToString() != "")
                            {
                                oEmp.employee_number = finalRecords.Rows[i][0].ToString();
                                oEmp.name1_1 = finalRecords.Rows[i][1].ToString();
                                oEmp.name1_2 = finalRecords.Rows[i][2].ToString();
                                oEmp.name1_3 = finalRecords.Rows[i][3].ToString();
                                oEmp.name1_4 = finalRecords.Rows[i][4].ToString();
                                using (HRContext DB = new HRContext())
                                {
                                    string unit = finalRecords.Rows[i][5].ToString().ToUpper();
                                    string position = finalRecords.Rows[i][6].ToString().ToUpper();
                                    string scale = finalRecords.Rows[i][7].ToString().ToUpper();
                                    int unitId = DB.UnitCollection.SingleOrDefault(m => m.NAME.ToUpper() == unit).ID;
                                    int positionId = DB.PositionCollection
                                        .SingleOrDefault(m => m.NAME.ToUpper() == position).ID;
                                    int scaleId = DB.ScalesCollection.SingleOrDefault(m => m.NAME.ToUpper() == scale)
                                        .ID;
                                    if (unitId != 0 && positionId != 0 && scaleId != 0)
                                    {
                                        oEmp.UNIT_ID = unitId;
                                        oEmp.SCALE_ID = scaleId;
                                        oEmp.positionId = positionId;
                                    }
                                    else
                                    {
                                        message = "Check if the using Unit, Position And Scale exist";
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                                    }
                                }

                                if (finalRecords.Rows[i][8].ToString() != "")
                                {
                                    oEmp.PARENT_ID = int.Parse(finalRecords.Rows[i][8].ToString());
                                }

                                oEmp.PHONE1 = finalRecords.Rows[i][9].ToString();
                                oEmp.PHONE1 = finalRecords.Rows[i][10].ToString();
                                if (finalRecords.Rows[i][11].ToString().ToUpper() == "ACTIVE")
                                {
                                    oEmp.IS_STATUS = 1;
                                }

                                oEmp.created_by = userName;
                                oEmp.created_date = DateTime.Now;
                                oEmp.COMPANY_ID = companyId;
                                oEmp.BRANCH_ID = branchId;
                                oEmp.START_DATE = DateTime.Now;
                                lst.Add(oEmp);
                            }
                            else
                            {
                                message = "This file Missing manditory Fields";
                                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                            }
                        }

                        if (lst.Count > 0)
                        {
                            using (HRContext DB = new HRContext())
                            {
                                foreach (var item in lst)
                                {
                                    DB.Entry(item).State = System.Data.Entity.EntityState.Added;
                                    DB.SaveChanges();
                                    EmployeePositionsEntity emp_pos = new EmployeePositionsEntity();
                                    emp_pos.created_by = userName;
                                    emp_pos.created_date = DateTime.Now;
                                    emp_pos.EMP_ID = item.ID;
                                    emp_pos.POSITION_ID = item.positionId;
                                    emp_pos.YEAR = DB.YearsCollection.Where(x => x.id == DateTime.Now.Year)
                                        .Select(x => x.id).FirstOrDefault();
                                    DB.EmployeesPostionsCollection.Add(emp_pos);
                                    DB.Entry(emp_pos).State = System.Data.Entity.EntityState.Added;
                                    DB.SaveChanges();
                                }
                            }
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, "");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion
    }


    public class empStrunct
    {
        public long id { set; get; }
        public string Name { set; get; }
        public string Unit { set; get; }
        public string img { set; get; }

        public string title { set; get; }
        public dynamic children { set; get; }
    }
}