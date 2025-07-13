using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;
using HR.Busniess;

namespace HR.APIs.Controllers
{
    public class EmployeeAssesmentController : ApiController
    {
        private HRContext _Context;

        public EmployeeAssesmentController()
        {
            _Context = new HRContext();
        }


        private string GetColor(List<AssessmentMapping> assessmentsMapping, float? mark)
        {
            string color = "white";
            if (mark != null && assessmentsMapping != null && assessmentsMapping.Count > 0)
            {
                AssessmentMapping assessmentMapping = assessmentsMapping.Where(x => mark == x.point).FirstOrDefault();
                if (assessmentMapping != null)
                {
                    color = assessmentMapping.color;
                }
            }

            return color;
        }

        public IHttpActionResult GetFinalResult(float? num)
        {
            try
            {
                var final_result_desc = (from result in _Context.EmployeePerformanceSegmentCollection.ToList()
                        .Where(x => x.segment == Math.Round(num.Value))
                    //.Where(x => (empAss.final_result*100) < x.percentage_to && (empAss.final_result * 100) > x.percentage_from )
                    select result.name).FirstOrDefault();
                return Ok(new { Data = final_result_desc, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeAssesment(
            int EmployeeID,
            int YearID,
            int? unitID)
        {
            try
            {
                var lstCompetence = (from empAss in _Context.EmployeeAssesmentCollection
                    join emp in _Context.EmployeesCollection on empAss.employee_id equals emp.ID
                    join unit in _Context.UnitCollection on emp.UNIT_ID equals unit.ID
                    join pos in _Context.PositionCollection on empAss.emp_position_id equals pos.ID
                    where (empAss.employee_id == EmployeeID || EmployeeID == -1)
                          && (empAss.year_id == YearID || YearID == -1) &&
                          (unitID == null || unit.ID == unitID)
                    select new
                    {
                        final_result_desc = (from result in _Context.EmployeePerformanceSegmentCollection.ToList()
                                .Where(x => x.segment == Math.Round(empAss.final_result.Value))
                            //.Where(x => (empAss.final_result*100) < x.percentage_to && (empAss.final_result * 100) > x.percentage_from )
                            select result.name).FirstOrDefault(),
                        empAss.objectives_weight,
                        empAss.competencies_weight,
                        empAss.final_result,
                        empAss.agreement_date,
                        empAss.attachment,
                        empAss.created_by,
                        empAss.created_date,
                        empAss.c_kpi_cycle,
                        empAss.employee_id,
                        empAss.emp_position_id,
                        empAss.emp_reviewer_id,
                        empAss.ID,
                        pos.NAME,
                        empAss.modified_by,
                        empAss.modified_date,
                        empAss.year_id,
                        empAss.target,
                        EmployeeName = emp.name1_1 + " " + emp.name1_4,
                        Unit = unit.NAME,
                        empAss.isQuarter1,
                        empAss.isQuarter2,
                        empAss.isQuarter3,
                        empAss.isQuarter4,
                        empAss.emp_manager_id,
                        empAss.status
                    }).ToList();
                var lstCompetence1 = (from empAss in lstCompetence
                    select new
                    {
                        final_result_desc = empAss.final_result_desc == null
                            ? (from result in _Context.EmployeePerformanceSegmentCollection.ToList()
                                    .OrderByDescending(x => x.segment)
                                select result.name).FirstOrDefault()
                            : empAss.final_result_desc,
                        empAss.objectives_weight,
                        empAss.competencies_weight,
                        empAss.final_result,
                        empAss.agreement_date,
                        empAss.attachment,
                        empAss.created_by,
                        empAss.created_date,
                        empAss.c_kpi_cycle,
                        empAss.employee_id,
                        empAss.emp_position_id,
                        positionName = empAss.NAME,
                        empAss.emp_reviewer_id,
                        empAss.ID,
                        empAss.modified_by,
                        empAss.modified_date,
                        empAss.year_id,
                        empAss.target,
                        empAss.EmployeeName,
                        empAss.Unit,
                        empAss.isQuarter1,
                        empAss.isQuarter2,
                        empAss.isQuarter3,
                        empAss.isQuarter4,
                        empAss.emp_manager_id,
                        empAss.status
                    }).ToList();
                return Ok(new { Data = lstCompetence1, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeProjectCompetency(
            int EmployeeID,
            int YearID,
            int? unitID,
            int projectId = -1,
            int competencyId = -1,
            int objectiveId = -1
        )
        {
            try
            {
                var lstCompetence = (
                    from obj in _Context.EmployeeObjectiveCollection
                    join comb in _Context.CompetenceCollection on obj.objective_competency_id equals comb.ID
                    join proj in _Context.ProjectsCollection on obj.project_id equals proj.ID
                    join empAss in _Context.EmployeeAssesmentCollection on obj.emp_assesment_id equals empAss.ID
                    join emp in _Context.EmployeesCollection on empAss.employee_id equals emp.ID
                    join unit in _Context.UnitCollection on emp.UNIT_ID equals unit.ID
                    where (empAss.employee_id == EmployeeID || EmployeeID == -1)
                          && (empAss.year_id == YearID || YearID == -1) &&
                          (unitID == null || unit.ID == unitID) &&
                          (projectId == -1 || proj.ID == projectId) &&
                          (objectiveId == -1 || obj.ID == objectiveId) &&
                          (competencyId == -1 || comb.ID == competencyId)
                    select new
                    {
                        empAss.employee_id,
                        empAss.emp_position_id,
                        empAss.emp_reviewer_id,
                        empAss.ID,
                        EmployeeName = emp.name1_1 + " " + emp.name1_4,
                        Unit = unit.NAME,
                        empAss.emp_manager_id,
                        empAss.status,
                        projectName = proj.Name,
                        objectiveName = obj.name,
                        competencyName = comb.NAME
                    }).ToList();
                var lstCompetence1 = (from empAss in lstCompetence
                    select new
                    {
                        empAss.employee_id,
                        empAss.emp_position_id,
                        empAss.emp_reviewer_id,
                        empAss.ID,
                        empAss.EmployeeName,
                        empAss.Unit,
                        empAss.emp_manager_id,
                        empAss.objectiveName,
                        empAss.projectName,
                        empAss.competencyName,
                        empAss.status
                    }).ToList();
                return Ok(new { Data = lstCompetence1, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdateWeight(
            int ID, float? objectives_weight, float? competencies_weight)
        {
            try
            {
                var empAsslist = _Context.EmployeeAssesmentCollection.Where(x => x.ID == ID).ToList();
                if (empAsslist.Count() > 0)
                {
                    foreach (var empAss in empAsslist)
                    {
                        empAss.objectives_weight = objectives_weight;
                        empAss.competencies_weight = competencies_weight;
                        empAss.final_result = (GetObjectiveResult(ID) * objectives_weight / 100) +
                                              (GetCompetencyResult(ID) * competencies_weight / 100);
                        empAss.modified_date = DateTime.Now;
                        _Context.Entry(empAss).State = System.Data.Entity.EntityState.Modified;
                        _Context.SaveChanges();
                    }


                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        public float GetObjectiveResult(long EmployeeAssesmentID)
        {
            var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection
                    .Where(x => x.emp_assesment_id == EmployeeAssesmentID).ToList().AsQueryable()
                select new
                {
                    empObj.name,
                    empObj.ID,
                    empObj.project_id,
                    empObj.weight,
                }).ToList();
            var _EmployeeObjectiveKPIAssessment = _Context.EmployeeObjectiveKPIAssessmentCollection.ToList();
            var objkpis = _Context.EmployeeObjectiveKPICollection.ToList().AsQueryable();
            var result1 = (from empObj in lstObjs
                select new
                {
                    empObj.name,
                    empObj.project_id,
                    empObj.weight,

                    KPIs = (from objKpiID in objkpis.Where(x => x.emp_obj_id == empObj.ID)
                        select new
                        {
                            name = objKpiID.name,
                            resultPerc = ((from q in _EmployeeObjectiveKPIAssessment
                                where q.EmployeeObjectiveKpiId == objKpiID.ID
                                select q.Result).DefaultIfEmpty().Sum() / (from q in _EmployeeObjectiveKPIAssessment
                                where q.EmployeeObjectiveKpiId == objKpiID.ID
                                select q.Target).DefaultIfEmpty().Sum()) * 100,
                        }).ToList()
                });
            var result2 = result1.Select(empObj => new
            {
                empObj.name,
                empObj.project_id,
                empObj.weight,
                Result = empObj.KPIs.Any(a => a.resultPerc > 0)
                    ? empObj.KPIs.Select(x => x.resultPerc).DefaultIfEmpty()
                        .Average()
                    : 0,
            });
            var result3 = result2.Select(empObj => new
            {
                empObj.name,
                empObj.project_id,
                empObj.weight,
                EmployeePerformance = _Context.EmployeePerformanceSegmentCollection.Where(x =>
                        empObj.Result < x.percentage_to && empObj.Result > x.percentage_from)
                    .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault(),
                // ObjFinalResult = empObj.weight * empObj.Result,
            });
            //var ssss = result3.FirstOrDefault().EmployeePerformance.Result;
            var result4 = result3.Select(empObj => new
            {
                empObj.project_id,
                empObj.name,
                empObj.weight,
                ObjFinalResult = (empObj.EmployeePerformance?.Result ?? 0) * (empObj.weight ?? 0) / 100,
            });

            var result5 = result4.Select(empObj => new
            {
                empObj.project_id,
                empObj.name,
                empObj.ObjFinalResult,
                sum = 0,
            });
            float sum = 0;
            result5.ToList().ForEach(x => sum += x == null || x.ObjFinalResult == null ? 0 : x.ObjFinalResult);

            return sum;
        }
        //
        // public float GetObjectiveResult(long Id)
        // {
        //     var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection.Where(x => x.emp_assesment_id == Id)
        //             .ToList().AsQueryable()
        //         select new
        //         {
        //             empObj.ID,
        //             empObj.weight,
        //         }).ToList();
        //     var _EmployeeObjectiveKPIAssessment = _Context.EmployeeObjectiveKPIAssessmentCollection.ToList();
        //     var objkpis = _Context.EmployeeObjectiveKPICollection.ToList().AsQueryable();
        //     var result1 = (from empObj in lstObjs
        //         select new
        //         {
        //             empObj.weight,
        //             KPIs = (from objKpiID in objkpis.Where(x => x.emp_obj_id == empObj.ID)
        //                 select new
        //                 {
        //                     resultPerc = ((from q in _EmployeeObjectiveKPIAssessment
        //                         where q.EmployeeObjectiveKpiId == objKpiID.ID
        //                         select q.Result).DefaultIfEmpty().Sum() / (from q in _EmployeeObjectiveKPIAssessment
        //                         where q.EmployeeObjectiveKpiId == objKpiID.ID
        //                         select q.Target).DefaultIfEmpty().Sum()) * 100,
        //                 }).ToList()
        //         });
        //     var result3 = result1.Select(empObj => new
        //     {
        //         ObjFinalResult = empObj.weight * _Context.EmployeePerformanceSegmentCollection.ToList().Where(x =>
        //                 empObj.KPIs.Select(y => new { y.resultPerc }).Select(y => y.resultPerc).DefaultIfEmpty()
        //                     .Average() < x.percentage_to &&
        //                 empObj.KPIs.Select(y => new { y.resultPerc }).Select(y => y.resultPerc).DefaultIfEmpty()
        //                     .Average() > x.percentage_from)
        //             .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault()
        //             ?.Result ?? 0,
        //     });
        //     float sum = result3.Select(x => x.ObjFinalResult).DefaultIfEmpty().ToList().Sum();
        //
        //     return sum / 100;
        // }

        public float GetCompetencyResult(long employeeAssessmentId)
        {
            var comkpis = _Context.EmployeeCompetencyKpiCollection.ToList();
            var _EmployeeCompetencyKpiAssessment = _Context.EmployeeCompetencyKpiAssessmentCollection.ToList();
            var result1 = (from empCom in _Context.EmployeeCompetencyCollection
                    .Where(x => x.EmployeeAssessmentId == employeeAssessmentId).ToList()
                join compName in _Context.CompetenceCollection.ToList() on empCom.CompetencyId equals compName.ID
                select new
                {
                    empCom.CompetencyId,
                    empCom.Id,
                    name = compName.NAME,
                    empCom.Weight,
                    CompetencyLevelName = 1,
                    KPIs = (from compKpi in comkpis.ToList().Where(x => x.EmployeeCompetencyId == empCom.Id)
                        select new
                        {
                            resultPerc = ((from q in _EmployeeCompetencyKpiAssessment
                                              where q.EmployeeCompetencyKpiId == compKpi.Id
                                              select q.Result).DefaultIfEmpty().Sum() /
                                          (from q in _EmployeeCompetencyKpiAssessment
                                              where q.EmployeeCompetencyKpiId == compKpi.Id
                                              select q.Target).DefaultIfEmpty().Sum()) * 100,
                            resultPercOf5 = (((from q in _EmployeeCompetencyKpiAssessment
                                                  where q.EmployeeCompetencyKpiId == compKpi.Id
                                                  select q.Result).DefaultIfEmpty().Sum() /
                                              (from q in _EmployeeCompetencyKpiAssessment
                                                  where q.EmployeeCompetencyKpiId == compKpi.Id
                                                  select q.Target).DefaultIfEmpty().Sum()) * 100) / 20.0,
                        }).ToList()
                });

            var result2 = result1.Select(empCom => new
            {
                empCom.CompetencyId,
                empCom.name,
                empCom.Weight,
                empCom.KPIs,
                Result = empCom.KPIs.Select(x => new { x.resultPerc }).Select(x => x.resultPerc).DefaultIfEmpty()
                    .Average(),
                KPIFinal = (empCom.KPIs.Count == 0
                    ? 0
                    : Math.Round(empCom.KPIs.Sum(xw => xw.resultPercOf5 ?? 0) / empCom.KPIs.Count)),
            });
            var result3 = result2.Select(empCom => new
            {
                empCom.CompetencyId,
                empCom.name,
                empCom.Weight,
                EmployeePerformance = _Context.EmployeePerformanceSegmentCollection
                    .Where(x => x.segment == empCom.KPIFinal)
                    .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault(),
                // ObjFinalResult = empObj.weight * empObj.Result,
            });
            var result4 = result3.Select(empCom => new
            {
                empCom.CompetencyId,
                empCom.name,
                empCom.Weight,
                CompFinalResult = (empCom.EmployeePerformance?.Result ?? 0),
            });

            var result5 = result4.Select(empObj => new
            {
                empObj.CompetencyId,
                empObj.name,
                empObj.CompFinalResult,
                empObj.Weight,
                sum = 0,
            });
            float sum = 0;
            int cnt = result5.Count();
            var result = result5.Select(empObj => new
            {
                empObj.CompetencyId,
                empObj.name,
                CompFinalResult = (empObj.CompFinalResult * empObj.Weight) / 100,
            });
            result.ToList().ForEach(x => sum += x.CompFinalResult.Value);

            return sum;
        }
        //
        // public float GetCompetencyResult(long Id)
        // {
        //     var comkpis = _Context.EmployeeCompetencyKpiCollection.ToList();
        //     var _EmployeeCompetencyKpiAssessment = _Context.EmployeeCompetencyKpiAssessmentCollection.ToList();
        //     var result1 = (from empCom in _Context.EmployeeCompetencyCollection.Where(x => x.EmployeeAssessmentId == Id)
        //             .ToList()
        //         select new
        //         {
        //             empCom.Weight,
        //             KPIs = (from compKpi in comkpis.ToList().Where(x => x.EmployeeCompetencyId == empCom.Id)
        //                 select new
        //                 {
        //                     resultPerc = ((from q in _EmployeeCompetencyKpiAssessment
        //                         where q.EmployeeCompetencyKpiId == compKpi.Id
        //                         select q.Result).DefaultIfEmpty().Sum() / (from q in _EmployeeCompetencyKpiAssessment
        //                         where q.EmployeeCompetencyKpiId == compKpi.Id
        //                         select q.Target).DefaultIfEmpty().Sum()) * 100,
        //                 }).ToList()
        //         });
        //
        //     var result2 = result1.Select(empCom => new
        //     {
        //         ObjFinalResult = empCom.Weight * _Context.EmployeePerformanceSegmentCollection.ToList().Where(x =>
        //                 empCom.KPIs.Select(y => new { y.resultPerc }).Select(y => y.resultPerc).DefaultIfEmpty()
        //                     .Average() < x.percentage_to &&
        //                 empCom.KPIs.Select(y => new { y.resultPerc }).Select(y => y.resultPerc).DefaultIfEmpty()
        //                     .Average() > x.percentage_from)
        //             .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault()
        //             ?.Result ?? 0,
        //     });
        //     float sum = result2.Select(x => x.ObjFinalResult).DefaultIfEmpty().ToList().Sum();
        //
        //     return sum / 100;
        // }

        [HttpGet]
        public IHttpActionResult Save(
            DateTime AgreementDate,
            string Attachment,
            string CreatedBy,
            int KpiCycle,
            int YearID,
            int EmpPositionID,
            int EmployeeID,
            float Target,
            Boolean isQuarter1,
            Boolean isQuarter2,
            Boolean isQuarter3,
            Boolean isQuarter4,
            long emp_manager_id,
            long companyId
        )
        {
            try
            {
                long? ReviewerID = (from d in _Context.EmployeesCollection where d.ID == EmployeeID select d.PARENT_ID)
                    .DefaultIfEmpty(null).Max();
                EmpPositionID = _Context.EmployeesPostionsCollection.Where(x => x.EMP_ID == EmployeeID)
                    .OrderByDescending(x => x.created_date).Select(x => x.POSITION_ID).FirstOrDefault();

                EmployeeAssesmentEntity employeeAssesmentEntity = new EmployeeAssesmentEntity();

                employeeAssesmentEntity.agreement_date = AgreementDate;
                employeeAssesmentEntity.attachment = Attachment;
                employeeAssesmentEntity.created_by = CreatedBy;
                employeeAssesmentEntity.created_date = DateTime.Now;
                employeeAssesmentEntity.c_kpi_cycle = KpiCycle;
                employeeAssesmentEntity.employee_id = EmployeeID;
                employeeAssesmentEntity.emp_position_id = EmpPositionID;
                employeeAssesmentEntity.emp_reviewer_id = ReviewerID;
                employeeAssesmentEntity.year_id = YearID;
                employeeAssesmentEntity.target = Target;
                employeeAssesmentEntity.isQuarter1 = isQuarter1;
                employeeAssesmentEntity.isQuarter2 = isQuarter2;
                employeeAssesmentEntity.isQuarter3 = isQuarter3;
                employeeAssesmentEntity.isQuarter4 = isQuarter4;
                employeeAssesmentEntity.status = 0;
                employeeAssesmentEntity.emp_manager_id = emp_manager_id;


                if (companyId > 0)
                {
                    CompanyEntity company = _Context.CompanyCollection.Where(x => x.id == companyId).FirstOrDefault();
                    if (company != null)
                    {
                        employeeAssesmentEntity.competencies_weight = (float)company.CompetencyFactor;
                        employeeAssesmentEntity.objectives_weight = (float)company.ObjectiveFactor;
                    }
                }

                _Context.Entry(employeeAssesmentEntity).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeAssesmentCollection.Add(employeeAssesmentEntity);
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
            DateTime AgreementDate,
            string Attachment,
            int KpiCycle,
            int YearID,
            int EmpPositionID,
            int EmployeeID,
            float Target,
            Boolean isQuarter1,
            Boolean isQuarter2,
            Boolean isQuarter3,
            Boolean isQuarter4,
            long emp_manager_id,
            long companyId
        )
        {
            try
            {
                EmployeeAssesmentEntity employeeAssesment =
                    _Context.EmployeeAssesmentCollection.Where(x => x.ID == ID).FirstOrDefault();

                EmpPositionID = _Context.EmployeesPostionsCollection.Where(x => x.EMP_ID == EmployeeID)
                    .OrderByDescending(x => x.created_date).Select(x => x.POSITION_ID).FirstOrDefault();


                if (employeeAssesment != null)
                {
                    employeeAssesment.modified_by = ModifiedBy;
                    employeeAssesment.modified_date = DateTime.Now;

                    employeeAssesment.agreement_date = AgreementDate;
                    employeeAssesment.attachment = Attachment;
                    employeeAssesment.created_date = DateTime.Now;
                    employeeAssesment.c_kpi_cycle = KpiCycle;
                    employeeAssesment.employee_id = EmployeeID;
                    employeeAssesment.emp_position_id = EmpPositionID;
                    employeeAssesment.year_id = YearID;
                    employeeAssesment.target = Target;
                    employeeAssesment.isQuarter1 = isQuarter1;
                    employeeAssesment.isQuarter2 = isQuarter2;
                    employeeAssesment.isQuarter3 = isQuarter3;
                    employeeAssesment.isQuarter4 = isQuarter4;
                    employeeAssesment.emp_manager_id = emp_manager_id;


                    if (companyId > 0)
                    {
                        CompanyEntity company =
                            _Context.CompanyCollection.Where(x => x.id == companyId).FirstOrDefault();
                        if (company != null)
                        {
                            employeeAssesment.competencies_weight = (float)company.CompetencyFactor;
                            employeeAssesment.objectives_weight = (float)company.ObjectiveFactor;
                        }
                    }

                    _Context.Entry(employeeAssesment).State = System.Data.Entity.EntityState.Modified;

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
        public IHttpActionResult UpdateEmployeeAssessmentStatus(
            int ID,
            string ModifiedBy,
            int status
        )
        {
            try
            {
                var employeeAssesment =
                    _Context.EmployeeAssesmentCollection.FirstOrDefault(x => x.ID == ID);
                if (employeeAssesment != null)
                {
                    // if (status == Convert.ToInt32(Enums.Enums.EmployeeAssessmentStatus.Final))
                    //     employeeAssesment.final_date = DateTime.Now;
                    employeeAssesment.status = status;
                    _Context.Entry(employeeAssesment).State = System.Data.Entity.EntityState.Modified;

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
                var oEmployeeObjective = _Context.EmployeeAssesmentCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oEmployeeObjective != null)
                {
                    _Context.EmployeeAssesmentCollection.Remove(oEmployeeObjective);
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
        public IHttpActionResult getByID(long ID)
        {
            try
            {
                List<EmployeeAssesmentEntity> oEmployeeAssesment =
                    _Context.EmployeeAssesmentCollection.Where(f => f.ID == ID).ToList();

                int empId = oEmployeeAssesment[0].employee_id;


                EmployeesEntity emp = _Context.EmployeesCollection.Where(e => e.ID == empId).FirstOrDefault();


                List<AssessmentMapping> assessmentsMapping = _Context.AssessmentMappingCollection
                    .Where(d => d.company_id == emp.COMPANY_ID).ToList();

                var empAss = (from a in oEmployeeAssesment
                    select new
                    {
                        a.agreement_date,
                        a.attachment,
                        a.competencies_result,
                        a.competencies_result_after_round,
                        a.competencies_weight,
                        a.competencies_weight_result,
                        a.competencies_weight_result_after_round,
                        a.created_by,
                        a.created_date,
                        a.c_kpi_cycle,
                        a.employee_id,
                        a.emp_manager_id,
                        a.emp_position_id,
                        a.emp_reviewer_id,
                        a.final_date,
                        a.ID,
                        a.isQuarter1,
                        a.isQuarter2,
                        a.isQuarter3,
                        a.isQuarter4,
                        a.modified_by,
                        a.modified_date,
                        a.objectives_result,
                        a.objectives_result_after_round,
                        a.objectives_weight,
                        a.objectives_weight_result,
                        a.objectives_weight_result_after_round,
                        a.result_after_round,
                        a.result_before_round,
                        a.status,
                        a.target,
                        a.year_id,
                        color = GetColor(assessmentsMapping, a.result_after_round)
                    }).FirstOrDefault();

                if (oEmployeeAssesment != null)
                {
                    return Ok(new { Data = empAss, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult LoadEmployeeManager(long id)
        {
            try
            {
                var employee = _Context.EmployeesCollection.Where(x => x.ID == id).Select(x => x.PARENT_ID)
                    .FirstOrDefault();
                // try to get employee director


                if (employee != null)
                {
                    var employeeDirector = _Context.EmployeesCollection.Where(x => x.ID == employee)
                        .Select(x => x.PARENT_ID)
                        .FirstOrDefault();
                    if (employeeDirector != null)
                        return Ok(new
                        {
                            Data = new { manager = employee, director = employeeDirector }, IsError = false,
                            ErrorMessage = string.Empty
                        });

                    return Ok(new
                    {
                        Data = new { manager = employee, director = employee }, IsError = false, ErrorMessage = string.Empty
                    });
                }

                return Ok(new
                    { Data = new { manager = "", director = "" }, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}