using HR.Entities;
using HR.Entities.Infrastructure;
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
    public class ActionPlansController : ApiController
    {
        HRContext db;

        public ActionPlansController()
        {
            db = new HRContext();
        }


        #region Projects Action Plans

        [HttpGet]
        public IHttpActionResult GetProjectActionPlans(string languageCode, long? projectId, long? projectKpiID,
            int? empID, long? UnitId)
        {
            try
            {
                projectKpiID = (projectKpiID.HasValue ? projectKpiID : 0);
                var projActionPlan2 = db.ProjectActionPlanCollection.Select(x => x.project_kpi_id).ToList();

                //var tt = db.ObjectiveKpiCollection.ToList();
                if (projectKpiID == 0)
                {
                    var projActionPlan = (from actionPlan in db.ProjectActionPlanCollection.Where(e =>
                                    (empID == null || e.emp_id == empID) &&
                                    (projectId == null || e.projectID == projectId) //&&
                                //(projectKpiID == null || e.project_kpi_id == projectKpiID)
                            )
                            join _proj in db.ProjectsCollection on actionPlan.projectID equals _proj.ID
                            //join objKpi in db.ObjectiveKpiCollection.Where(x => x.is_obj_kpi == 2) on actionPlan.project_kpi_id equals objKpi.ID
                            join emp in db.EmployeesCollection on actionPlan.emp_id equals emp.ID
                            join unit in db.UnitCollection on _proj.UnitId equals unit.ID
                            select new
                            {
                                actionPlan.ID,
                                unitId = unit.ID,
                                objKpiName = (actionPlan.project_kpi_id == 0
                                    ? "All"
                                    : db.ObjectiveKpiCollection.Where(ss => ss.ID == actionPlan.project_kpi_id)
                                        .FirstOrDefault().Name),
                                unitName = unit.NAME,
                                empName = emp.name1_1 + " " + emp.name1_2,
                                actionPlan.project_kpi_id,
                                KpiName = _proj.Name,
                                actionPlan.action_cost,
                                actionPlan.action_name,
                                actionPlan.planned_status,
                                actionPlan.action_req,
                                actionPlan.action_date,
                                actionPlan.action_weight
                            })
                        .Select(x => new
                        {
                            ID = x.ID,
                            ProjectName = x.KpiName,
                            KpiName = x.objKpiName,
                            // KpiName = tt.FirstOrDefault(d => d.ID == x.project_kpi_id).Name,
                            PlanName = x.action_name,
                            req = x.action_req,
                            EmpName = x.empName,
                            Unit_Id = x.unitId,
                            Unit_Name = x.unitName,
                            planDate = x.action_date,
                            planWeight = x.action_weight + " %",
                            planCost = x.action_cost,
                            planned_status = (x.planned_status == 1) ? "Waiting Approval" :
                                (x.planned_status == 2) ? "Confirmed" : "Declined",
                            project_kpi_id = x.project_kpi_id
                        });
                    if (UnitId != null && UnitId > 0)
                    {
                        projActionPlan = projActionPlan.Where(x => x.Unit_Id == UnitId);
                    }

                    var result = projActionPlan.ToList();


                    if (result != null)
                    {
                        return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                    }

                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
                else
                {
                    var projActionPlan = (from actionPlan in db.ProjectActionPlanCollection.Where(e =>
                                (empID == null || e.emp_id == empID) &&
                                (projectId == null || e.projectID == projectId) &&
                                (projectKpiID == null || e.project_kpi_id == projectKpiID)
                            )
                            join _proj in db.ProjectsCollection on actionPlan.projectID equals _proj.ID
                            join objKpi in db.ObjectiveKpiCollection.Where(x => x.is_obj_kpi == 2) on actionPlan
                                .project_kpi_id equals objKpi.ID
                            join emp in db.EmployeesCollection on actionPlan.emp_id equals emp.ID
                            join unit in db.UnitCollection on _proj.UnitId equals unit.ID
                            select new
                            {
                                actionPlan.ID,
                                unitId = unit.ID,
                                objKpiName = objKpi.Name,
                                unitName = unit.NAME,
                                empName = emp.name1_1 + " " + emp.name1_2,
                                actionPlan.project_kpi_id,
                                KpiName = _proj.Name,
                                actionPlan.action_cost,
                                actionPlan.action_name,
                                actionPlan.planned_status,
                                actionPlan.action_req,
                                actionPlan.action_date,
                                actionPlan.action_weight
                            })
                        .Select(x => new
                        {
                            ID = x.ID,
                            ProjectName = x.KpiName,
                            KpiName = x.objKpiName,
                            // KpiName = tt.FirstOrDefault(d => d.ID == x.project_kpi_id).Name,
                            PlanName = x.action_name,
                            req = x.action_req,
                            EmpName = x.empName,
                            Unit_Id = x.unitId,
                            Unit_Name = x.unitName,
                            planDate = x.action_date,
                            planWeight = x.action_weight + " %",
                            planCost = x.action_cost,
                            planned_status = (x.planned_status == 1) ? "Waiting Approval" :
                                (x.planned_status == 2) ? "Confirmed" : "Declined",
                            project_kpi_id = x.project_kpi_id
                        });
                    if (UnitId != null && UnitId > 0)
                    {
                        projActionPlan = projActionPlan.Where(x => x.Unit_Id == UnitId);
                    }

                    var result = projActionPlan.ToList();


                    if (result != null)
                    {
                        return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                    }

                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
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
        public IHttpActionResult SaveProjectActionPlans(
            int projectId,
            decimal? action_cost,
            string username,
            string action_date,
            string action_name,
            string action_notes,
            string action_req,
            float action_weight,
            int emp_id,
            int project_kpi_id)
        {
            try
            {
                var Weights = (from obj in db.ProjectActionPlanCollection
                        where
                            (obj.projectID == projectId)
                            && (obj.project_kpi_id == project_kpi_id)
                        select obj.action_weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + action_weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                var projActionPlan = new ProjectActionPlanEntity
                {
                    action_cost = action_cost,
                    action_date = Convert.ToDateTime(action_date),
                    action_name = action_name,
                    action_notes = action_notes,
                    action_req = action_req,
                    action_weight = action_weight,
                    CreatedBy = username,
                    CreatedDate = DateTime.Now,
                    emp_id = emp_id,
                    projectID = projectId,
                    project_kpi_id = project_kpi_id
                };
                //db.Entry(projActionPlan).State = System.Data.Entity.EntityState.Added;
                db.ProjectActionPlanCollection.Add(projActionPlan);
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
        public IHttpActionResult UpdateProjectActionPlans(
            int id,
            int projectId,
            decimal action_cost,
            string username,
            string action_date,
            string action_name,
            string action_notes,
            string action_req,
            float action_weight,
            int emp_id,
            int project_kpi_id)
        {
            try
            {
                var Weights = (from obj in db.ProjectActionPlanCollection
                        where
                            (obj.projectID == projectId) &
                            (obj.project_kpi_id == project_kpi_id) &
                            (obj.ID != id)
                        select obj.action_weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + action_weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                ProjectActionPlanEntity projActionPlan =
                    db.ProjectActionPlanCollection.Where(x => x.ID == id).FirstOrDefault();

                projActionPlan.action_cost = action_cost;
                projActionPlan.action_date = Convert.ToDateTime(action_date);
                projActionPlan.action_name = action_name;
                projActionPlan.action_notes = action_notes;
                projActionPlan.action_req = action_req;
                projActionPlan.action_weight = action_weight;
                projActionPlan.CreatedBy = username;
                projActionPlan.CreatedDate = DateTime.Now;
                projActionPlan.emp_id = emp_id;
                projActionPlan.projectID = projectId;
                projActionPlan.project_kpi_id = project_kpi_id;


                db.Entry(projActionPlan).State = System.Data.Entity.EntityState.Modified;
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
        public IHttpActionResult DeleteActionPlan(int id)
        {
            try
            {
                var actionPlan = db.ProjectActionPlanCollection.Where(e => e.ID == id).FirstOrDefault();
                if (actionPlan != null)
                {
                    db.ProjectActionPlanCollection.Remove(actionPlan);
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
        public IHttpActionResult GetProjectActionPlansByID(long id)
        {
            try
            {
                float max = 0;
                var list = db.ProjectActionPlanCollection.Where(x => x.projectID == id).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.action_weight);
                }

                var actionPlan = db.ProjectActionPlanCollection.Where(e => e.ID == id).Select(e => new
                {
                    e.ID,
                    e.action_cost,
                    e.action_date,
                    e.action_name,
                    e.action_notes,
                    e.action_req,
                    e.action_weight,
                    e.emp_id,
                    e.projectID,
                    e.project_kpi_id,
                    max = max
                }).ToList();

                var result = actionPlan.Select(e => new
                {
                    e.ID,
                    e.action_cost,
                    action_date = e.action_date.ToString("dd/MM/yyyy"),
                    e.action_name,
                    e.action_notes,
                    e.action_req,
                    e.action_weight,
                    e.emp_id,
                    e.projectID,
                    e.project_kpi_id,
                    e.max
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

        [HttpGet]
        public IHttpActionResult GetProjectPlannedCost(long? id, int? planID)
        {
            try
            {
                var plannedCost = db.ProjectsCollection.Where(e => e.ID == id).Select(e => new
                {
                    e.ID,
                    e.PlannedCost,
                    actionPlansCost = db.ProjectActionPlanCollection.Where(x => x.projectID == id)
                        .Sum(x => x.action_cost),
                    planCost = (planID == null)
                        ? 0
                        : db.ProjectActionPlanCollection.Where(x => x.ID == planID).FirstOrDefault().action_cost
                }).FirstOrDefault();

                if (plannedCost != null)
                {
                    return Ok(new { Data = plannedCost, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getActionPlanRemainingWeight(int projectID, int projKPI)
        {
            try
            {
                float max = 0;
                var list = db.ProjectActionPlanCollection
                    .Where(x => x.projectID == projectID && x.project_kpi_id == projKPI).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.action_weight);
                }

                return Ok(new { Data = max, IsError = false, ErrorMessage = string.Empty });
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

        #endregion


        [HttpGet]
        public IHttpActionResult GetProjectActionPlansForAssessment(long? projectId, long? projKPI, string languageCode)

        {
            try
            {
                var projActionPlan = db.ProjectActionPlanCollection.Where(e => e.planned_status == 2 &&
                                                                               (projectId == null ||
                                                                                   e.projectID == projectId)
                                                                               &&
                                                                               (projKPI == null ||
                                                                                   e.project_kpi_id == projKPI)
                ).Select(x => new
                {
                    x.ID,
                    ProjectName = db.ProjectsCollection.FirstOrDefault(s => s.ID == x.projectID).Name,
                    ProjectKpiName = db.ObjectiveKpiCollection
                        .FirstOrDefault(s => s.objective_id == x.projectID).Name,
                    PlanName = x.action_name,
                    req = x.action_req,
                    EmpName = db.EmployeesCollection.FirstOrDefault(s => s.ID == x.emp_id).name1_1 + " " +
                              db.EmployeesCollection.FirstOrDefault(s => s.ID == x.emp_id).name1_2,
                    planDate = x.action_date,
                    planWeight = x.action_weight + " %",
                    planWeight2 = x.action_weight,
                    planCost = x.action_cost,
                    PlanKPIs = db.ObjectiveKpiCollection.Where(s => s.objective_id == x.ID && s.is_obj_kpi == 3).Select(
                        s => new
                        {
                            ID = s.ID,
                            kpiName = s.Name,
                            kpiTarget = s.Target,
                            kpiWeight = s.Weight,
                            result = s.result,
                            resultPercentage = (s.result / s.Target) * 100
                            // resultPercentage = (s.result / s.Target) * s.Weight
                        }),
                    resultPercentageAll = db.ObjectiveKpiCollection.Where(s => s.objective_id == x.ID && s.is_obj_kpi == 3),
                    x.attachment,
                    x.assessment_status
                }).ToList();


                var result = projActionPlan.Select(x => new
                {
                    x.ID,
                    x.ProjectName,
                    x.ProjectKpiName,
                    x.PlanName,
                    x.req,
                    x.EmpName,
                    planDate = x.planDate.ToString("dd/MM/yyyy"),
                    x.planWeight,
                    x.planWeight2,
                    x.planCost,
                    x.PlanKPIs,
                    x.attachment,
                    x.assessment_status,
                    resultPercentageAll = x.PlanKPIs.ToList().Sum(xs => xs.resultPercentage),

                    //resultPercentageAll= ( x.resultPercentageAll.ToList().Sum(s => (s.result*s.Target)) <1?0: x.resultPercentageAll.ToList().Sum(s => (s.result * s.Target)/s.Weight))
                }).ToList();


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

        [HttpPost]
        public IHttpActionResult saveActionPlanAssessment()
        {
            try
            {
                var kpisData = JsonConvert.DeserializeObject<dynamic[]>(HttpContext.Current.Request.Form["kpiData"]);

                foreach (var itm in kpisData)
                {
                    foreach (var kpi in itm.PlanKPIs)
                    {
                        int id = Convert.ToInt32(kpi.ID);
                        float result = kpi.result;
                        var objKPI = db.ObjectiveKpiCollection.Where(e => e.ID == id).FirstOrDefault();
                        objKPI.result = result;

                        db.Entry(objKPI).State = EntityState.Modified;
                        db.SaveChanges();
                    }
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


        [HttpPost]
        public IHttpActionResult UploadFiles(int docID, int planID, string username)
        {
            try
            {
                var actionPlan = db.ProjectActionPlanCollection.Where(x => x.ID == planID).FirstOrDefault();

                string fileUrl = string.Empty;
                string fileName = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        fileName = docID + "_" + planID + "_" + file.FileName;
                        file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/" + fileName));
                        fileUrl = ConfigurationManager.AppSettings["DocumentURL"] + fileName;
                    }
                }

                actionPlan.attachment = fileUrl;

                db.Entry(actionPlan).State = EntityState.Modified;
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
        public IHttpActionResult RemoveEvident(int docID, string username)
        {
            try
            {
                var plan = db.ProjectActionPlanCollection.Where(e => e.ID == docID).FirstOrDefault();
                plan.attachment = string.Empty;
                plan.ModifiedBy = username;
                plan.ModifiedDate = DateTime.Now;

                db.Entry(plan).State = EntityState.Modified;

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
    }
}