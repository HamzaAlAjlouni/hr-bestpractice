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
    public class EmployeeEducationController : ApiController
    {
        private HRContext _Context;

        public EmployeeEducationController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeEducations(
            int CompanyID,
            int EmployeeId,
            int YearId,
            string languageCode)
        {
            try
            {
                var lstEmployeeEducations = (
                    from empEdu in _Context.EmployeeEducationCollection.Where(e =>
                        e.company_id == CompanyID && e.employee_id == EmployeeId && e.year_id == YearId)
                    join eduType in _Context.CodesCollection.Where(d => d.MAJOR_NO == 12) on empEdu.type equals eduType
                        .MINOR_NO
                    join priority in _Context.CodesCollection.Where(d => d.MAJOR_NO == 14) on empEdu.priority equals
                        priority.MINOR_NO
                    join status in _Context.CodesCollection.Where(d => d.MAJOR_NO == 15) on empEdu.status equals status
                        .MINOR_NO
                    join eduMethod in _Context.CodesCollection.Where(d => d.MAJOR_NO == 13) on empEdu.method equals
                        eduMethod.MINOR_NO
                    select new
                    {
                        empEdu.company_id,
                        empEdu.created_by,
                        empEdu.created_date,
                        empEdu.employee_id,
                        empEdu.emp_competency_id,
                        empEdu.emp_obj_id,
                        empEdu.execution_period,
                        field = (languageCode == "en" ? empEdu.field : empEdu.field2),

                        empEdu.id,
                        empEdu.method,
                        empEdu.modified_by,
                        empEdu.modified_date,
                        empEdu.notes,
                        empEdu.priority,
                        empEdu.status,
                        empEdu.type,
                        empEdu.year_id,
                        priority_desc = (languageCode == "en" ? priority.NAME : priority.name2),
                        eduType_desc = (languageCode == "en" ? eduType.NAME : eduType.name2),
                        status_desc = (languageCode == "en" ? status.NAME : status.name2),
                        eduMethod_desc = (languageCode == "en" ? eduMethod.NAME : eduMethod.name2),
                    }).ToList();

                return Ok(new { Data = lstEmployeeEducations, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeDevelopmentPlan(
            int CompanyID,
            int YearId,
            string languageCode,
            int quarterFilter = -1,
            int unitId = -1)
        {
            try
            {
                var lstEmployeeEducations = (
                    from empEdu in _Context.EmployeeEducationCollection.Where(e =>
                        e.company_id == CompanyID && e.year_id == YearId && (e.execution_period ==
                                                                             (quarterFilter == -1
                                                                                 ? e.execution_period
                                                                                 : quarterFilter)))
                    join emp in _Context.EmployeesCollection.Where(e =>
                        e.COMPANY_ID == CompanyID && (e.UNIT_ID == (unitId == -1 ? e.UNIT_ID : unitId))) on empEdu
                        .employee_id equals emp.ID
                    join eduType in _Context.CodesCollection.Where(d => d.MAJOR_NO == 12) on empEdu.type equals eduType
                        .MINOR_NO
                    join priority in _Context.CodesCollection.Where(d => d.MAJOR_NO == 14) on empEdu.priority equals
                        priority.MINOR_NO
                    join status in _Context.CodesCollection.Where(d => d.MAJOR_NO == 15) on empEdu.status equals status
                        .MINOR_NO
                    join eduMethod in _Context.CodesCollection.Where(d => d.MAJOR_NO == 13) on empEdu.method equals
                        eduMethod.MINOR_NO
                    select new
                    {
                        empEdu.company_id,
                        empEdu.created_by,
                        empEdu.created_date,
                        empEdu.employee_id,
                        empEdu.emp_competency_id,
                        competency_id =
                            _Context.EmployeeCompetencyCollection.FirstOrDefault(a =>
                                a.Id == empEdu.emp_competency_id) == null
                                ? -1
                                : _Context.EmployeeCompetencyCollection.FirstOrDefault(a => a.Id == empEdu.emp_competency_id)
                                    .CompetencyId,
                        empEdu,
                        empEdu.emp_obj_id,
                        empEdu.execution_period,
                        empName = (languageCode == "en"
                            ? emp.name1_1 + " " + emp.name1_4
                            : emp.name2_1 + " " + emp.name2_4),
                        field = (languageCode == "en" ? empEdu.field : empEdu.field2),
                        empEdu.id,
                        empEdu.method,
                        empEdu.modified_by,
                        empEdu.modified_date,
                        empEdu.notes,
                        empEdu.priority,
                        empEdu.status,
                        empEdu.type,
                        empEdu.year_id,
                        priority_desc = (languageCode == "en" ? priority.NAME : priority.name2),
                        eduType_desc = (languageCode == "en" ? eduType.NAME : eduType.name2),
                        status_desc = (languageCode == "en" ? status.NAME : status.name2),
                        eduMethod_desc = (languageCode == "en" ? eduMethod.NAME : eduMethod.name2),
                    }).ToList();

                return Ok(new { Data = lstEmployeeEducations, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int company_id,
            int year_id,
            int employee_id,
            int? emp_competency_id,
            int? emp_obj_id,
            int execution_period,
            string field,
            int priority,
            int status,
            int type,
            int method,
            string created_by,
            string notes,
            string languageCode
        )
        {
            try
            {
                EmployeeEducationEntity employeeEducationEntity = new EmployeeEducationEntity();

                employeeEducationEntity.company_id = company_id;
                employeeEducationEntity.year_id = year_id;
                employeeEducationEntity.employee_id = employee_id;
                employeeEducationEntity.created_date = DateTime.Now;
                employeeEducationEntity.emp_competency_id = emp_competency_id;
                employeeEducationEntity.emp_obj_id = emp_obj_id;
                employeeEducationEntity.execution_period = execution_period;
                if (languageCode == "en")
                    employeeEducationEntity.field = field;
                else
                    employeeEducationEntity.field2 = field;
                employeeEducationEntity.priority = priority;
                employeeEducationEntity.status = status;
                employeeEducationEntity.type = type;
                employeeEducationEntity.method = method;
                employeeEducationEntity.created_by = created_by;
                employeeEducationEntity.created_date = DateTime.Now;
                employeeEducationEntity.modified_by = null;
                employeeEducationEntity.modified_date = null;
                employeeEducationEntity.notes = notes;


                _Context.Entry(employeeEducationEntity).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeEducationCollection.Add(employeeEducationEntity);
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
            int id,
            int company_id,
            int year_id,
            int employee_id,
            int? emp_competency_id,
            int? emp_obj_id,
            int execution_period,
            string field,
            int priority,
            int status,
            int type,
            int method,
            string modified_by,
            string notes,
            string languageCode
        )
        {
            try
            {
                EmployeeEducationEntity employeeEducationEntity =
                    _Context.EmployeeEducationCollection.Where(x => x.id == id).FirstOrDefault();
                if (employeeEducationEntity != null)
                {
                    employeeEducationEntity.company_id = company_id;
                    employeeEducationEntity.year_id = year_id;
                    employeeEducationEntity.employee_id = employee_id;
                    employeeEducationEntity.created_date = DateTime.Now;
                    employeeEducationEntity.emp_competency_id = emp_competency_id;
                    employeeEducationEntity.emp_obj_id = emp_obj_id;
                    employeeEducationEntity.execution_period = execution_period;
                    employeeEducationEntity.field = field;
                    if (languageCode == "en")
                        employeeEducationEntity.field = field;
                    else
                        employeeEducationEntity.field2 = field;
                    employeeEducationEntity.priority = priority;
                    employeeEducationEntity.status = status;
                    employeeEducationEntity.type = type;
                    employeeEducationEntity.method = method;
                    employeeEducationEntity.modified_by = modified_by;
                    employeeEducationEntity.modified_date = DateTime.Now;
                    employeeEducationEntity.notes = notes;

                    _Context.Entry(employeeEducationEntity).State = System.Data.Entity.EntityState.Modified;

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
        public IHttpActionResult Delete(long id)
        {
            try
            {
                EmployeeEducationEntity employeeEducationEntity =
                    _Context.EmployeeEducationCollection.Where(x => x.id == id).FirstOrDefault();
                if (employeeEducationEntity != null)
                {
                    _Context.EmployeeEducationCollection.Remove(employeeEducationEntity);
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
        public IHttpActionResult getByID(long id, string languageCode)
        {
            try
            {
                var employeeEducationEntity =
                    (from empEdu in _Context.EmployeeEducationCollection.Where(x => x.id == id)
                        select new
                        {
                            empEdu.company_id,
                            empEdu.created_by,
                            empEdu.created_date,
                            empEdu.employee_id,
                            empEdu.emp_competency_id,
                            empEdu.emp_obj_id,
                            empEdu.execution_period,
                            field = (languageCode == "en" ? empEdu.field : empEdu.field2),
                            empEdu.id,
                            empEdu.modified_by,
                            empEdu.method,
                            empEdu.modified_date,
                            empEdu.notes,
                            empEdu.priority,
                            empEdu.status,
                            empEdu.type,
                            empEdu.year_id
                        }).FirstOrDefault();
                if (employeeEducationEntity != null)
                {
                    return Ok(new { Data = employeeEducationEntity, IsError = false, ErrorMessage = string.Empty });
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