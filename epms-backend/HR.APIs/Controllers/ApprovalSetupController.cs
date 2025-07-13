using HR.Entities;
using HR.Entities.Infrastructure;
using HR.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class ApprovalSetupController : ApiController
    {
        HRContext db;

        public ApprovalSetupController()
        {
            db = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult GetAllApprovalSetup(int companyID, string pagename)
        {
            try
            {
                var approvalSetup = db.ApprovalSetupCollection.Where(
                    e => e.company_id == companyID &&
                         (string.IsNullOrEmpty(pagename) || e.name.ToLower().Contains(pagename.ToLower()))
                ).Select
                (x => new
                {
                    x.ID,
                    x.company_id,
                    x.name,
                    x.page_url,
                    reviewing_user = x.reviewing_user.ToUpper()
                }).ToList();

                if (approvalSetup != null)
                {
                    return Ok(new { Data = approvalSetup, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetApprovalSetupByID(int id)
        {
            try
            {
                var approvalSetup = db.ApprovalSetupCollection.Where(e => e.ID == id).Select
                (x => new
                {
                    x.ID,
                    x.company_id,
                    x.name,
                    x.page_url,
                    reviewing_user = x.reviewing_user.ToUpper()
                }).FirstOrDefault();

                if (approvalSetup != null)
                {
                    return Ok(new { Data = approvalSetup, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetApprovalSetupByURL(string page_url, int companyID)
        {
            try
            {
                var approvalSetup = db.ApprovalSetupCollection.Where(
                    e => e.company_id == companyID && e.page_url == page_url
                ).Select
                (x => new
                {
                    x.company_id,
                    x.ID,
                    x.name,
                    x.page_url,
                    x.reviewing_user
                }).ToList();

                if (approvalSetup.Any())
                {
                    return Ok(new { Data = approvalSetup, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult UpdateProjectStatus(int projectID, int status, string createdBy, string note = "")
        {
            try
            {
                var project = db.ProjectsCollection.Where(e => e.ID == projectID).FirstOrDefault();

                if (project != null)
                {
                    project.planned_status = status;

                    db.Entry(project).State = EntityState.Modified;
                    // add status history
                    var projectApprovalHistoryEntity = new ProjectApprovalHistoryEntity()
                    {
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.Now,
                        Note = note,
                        Status = status,
                        ProjectId = projectID
                    };
                    db.Entry(projectApprovalHistoryEntity).State = EntityState.Added;


                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetProjectStatus(int projectID)
        {
            try
            {
                var project = db.ProjectsCollection.Where(e => e.ID == projectID).FirstOrDefault();

                if (project != null)
                {
                    return Ok(new { Data = project, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetActionPlanStatus(int planID)
        {
            try
            {
                var actionPlan = db.ProjectActionPlanCollection.Where(e => e.ID == planID).FirstOrDefault();

                if (actionPlan != null)
                {
                    return Ok(new { Data = actionPlan, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult UpdateActionStatus(int PlanID, int status)
        {
            try
            {
                var plan = db.ProjectActionPlanCollection.Where(e => e.ID == PlanID).FirstOrDefault();

                if (plan != null)
                {
                    plan.planned_status = status;

                    db.Entry(plan).State = EntityState.Modified;


                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult UpdateActionAssessmentStatus(int PlanID, int status)
        {
            try
            {
                var plan = db.ProjectActionPlanCollection.Where(e => e.ID == PlanID).FirstOrDefault();

                if (plan != null)
                {
                    plan.assessment_status = status;

                    db.Entry(plan).State = EntityState.Modified;

                    db.SaveChanges();

                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult UpdateProjectAssessmentStatus(int projectID, int status, string createdBy = "",
            string note = "")
        {
            try
            {
                var project = db.ProjectsCollection.Where(e => e.ID == projectID).FirstOrDefault();

                if (project != null)
                {
                    project.assessment_status = status;

                    db.Entry(project).State = EntityState.Modified;
                    db.Entry(project).State = EntityState.Modified;
                    // add status history
                    var projectApprovalHistoryEntity = new ProjectApprovalHistoryEntity()
                    {
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.Now,
                        Note = note,
                        Status = status == 2 ? 4 : status == 3 ? 5 : 0,
                        ProjectId = projectID
                    };
                    db.Entry(projectApprovalHistoryEntity).State = EntityState.Added;


                    db.SaveChanges();


                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult SaveApproval(string name, string url, string user, int companyID)
        {
            try
            {
                ApprovalSetupEntity approval = new ApprovalSetupEntity();
                approval.name = name;
                approval.reviewing_user = user;
                approval.page_url = url;
                approval.company_id = companyID;

                db.Entry(approval).State = EntityState.Added;
                db.ApprovalSetupCollection.Add(approval);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                ;
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
        public IHttpActionResult UpdateApproval(int id, string name, string url, string user, int companyID)
        {
            try
            {
                ApprovalSetupEntity approval = db.ApprovalSetupCollection.Where(s => s.ID == id).FirstOrDefault();
                approval.name = name;
                approval.reviewing_user = user;
                approval.page_url = url;
                approval.company_id = companyID;

                db.Entry(approval).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                ;
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
        public IHttpActionResult DeleteApproval(int id)
        {
            try
            {
                ApprovalSetupEntity approval = db.ApprovalSetupCollection.Where(s => s.ID == id).FirstOrDefault();

                db.ApprovalSetupCollection.Remove(approval);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                ;
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