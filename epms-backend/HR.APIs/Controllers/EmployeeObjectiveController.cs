using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;
using HR.Entities;
using System.Transactions;

namespace HR.APIs.Controllers
{
    public class EmployeeObjectiveController : ApiController
    {
        private HRContext _Context;

        public EmployeeObjectiveController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult old_GetEmployeeObjective(
            int EmployeeAssesmentID, string LanguageCode)
        {
            try
            {
                var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection
                        .Where(x => x.emp_assesment_id == EmployeeAssesmentID).ToList().AsQueryable()
                    select new
                    {
                        empObj.code,
                        empObj.created_by,
                        empObj.created_date,
                        empObj.emp_assesment_id,
                        empObj.ID,
                        empObj.modified_by,
                        empObj.modified_date,
                        name = (LanguageCode == "en" ? empObj.name : empObj.name2),
                        empObj.note,
                        empObj.pos_desc_id,
                        empObj.project_id,
                        empObj.result_after_round,
                        empObj.result_without_round,
                        empObj.target,
                        empObj.weight,

                        Target1 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == empObj.ID
                                  && q1.PeriodNo == 1
                            select q1.Target).DefaultIfEmpty(0),
                        Target2 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == empObj.ID
                                  && q1.PeriodNo == 2
                            select q1.Target).DefaultIfEmpty(0),
                        Target3 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == empObj.ID
                                  && q1.PeriodNo == 3
                            select q1.Target).DefaultIfEmpty(0),
                        Target4 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == empObj.ID
                                  && q1.PeriodNo == 4
                            select q1.Target).DefaultIfEmpty(0)
                    }).ToList();


                return Ok(new { Data = lstObjs, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjective(
            int EmployeeAssesmentID, string LanguageCode, int q1Filter = 1, int q2Filter = 1, int q3Filter = 1,
            int q4Filter = 1)
        {
            try
            {
                var maxSegmentPercentage = _Context.EmployeePerformanceSegmentCollection.Max(a => a.percentage_to);
                var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection
                        .Where(x => x.emp_assesment_id == EmployeeAssesmentID).ToList().AsQueryable()
                    select new
                    {
                        empObj.code,
                        empObj.created_by,
                        empObj.created_date,
                        empObj.emp_assesment_id,
                        empObj.ID,
                        empObj.modified_by,
                        empObj.modified_date,
                        name = (LanguageCode == "en" ? empObj.name : empObj.name2),
                        empObj.note,
                        empObj.pos_desc_id,
                        empObj.project_id,
                        empObj.result_after_round,
                        empObj.result_without_round,
                        empObj.target,
                        empObj.weight,
                    }).ToList();
                var _EmployeeObjectiveKPIAssessment = _Context.EmployeeObjectiveKPIAssessmentCollection.ToList();
                var objkpis = _Context.EmployeeObjectiveKPICollection.ToList().AsQueryable();
                var result1 = (from empObj in lstObjs
                    select new
                    {
                        empObj.code,
                        empObj.created_by,
                        empObj.created_date,
                        empObj.emp_assesment_id,
                        empObj.ID,
                        empObj.modified_by,
                        empObj.modified_date,
                        empObj.name,
                        empObj.note,
                        empObj.pos_desc_id,
                        empObj.project_id,
                        empObj.result_after_round,
                        empObj.result_without_round,
                        empObj.target,
                        empObj.weight,

                        KPIs = (from objKpiID in objkpis.Where(x => x.emp_obj_id == empObj.ID)
                            select new
                            {
                                name = objKpiID.name,
                                objKpiID.BetterUpDown,

                                Q1_ID = (from q1 in _EmployeeObjectiveKPIAssessment
                                    where q1.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q1.PeriodNo == 1
                                    select q1.Id).DefaultIfEmpty().Sum(),

                                Q1_P = (from q1 in _EmployeeObjectiveKPIAssessment
                                    where q1.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q1.PeriodNo == 1
                                    select q1.Target).DefaultIfEmpty().Sum() ?? 0,

                                Q1_A = (from q1 in _EmployeeObjectiveKPIAssessment
                                    where q1.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q1.PeriodNo == 1
                                    select q1.Result).DefaultIfEmpty().Sum() ?? 0,


                                Q2_ID = (from q2 in _EmployeeObjectiveKPIAssessment
                                    where q2.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q2.PeriodNo == 2
                                    select q2.Id).DefaultIfEmpty().Sum(),

                                Q2_P = (from q2 in _EmployeeObjectiveKPIAssessment
                                    where q2.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q2.PeriodNo == 2
                                    select q2.Target).DefaultIfEmpty().Sum() ?? 0,

                                Q2_A = (from q2 in _EmployeeObjectiveKPIAssessment
                                    where q2.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q2.PeriodNo == 2
                                    select q2.Result).DefaultIfEmpty().Sum() ?? 0,


                                Q3_ID = (from q3 in _EmployeeObjectiveKPIAssessment
                                    where q3.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q3.PeriodNo == 3
                                    select q3.Id).DefaultIfEmpty().Sum(),

                                Q3_P = (from q3 in _EmployeeObjectiveKPIAssessment
                                    where q3.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q3.PeriodNo == 3
                                    select q3.Target).DefaultIfEmpty().Sum() ?? 0,

                                Q3_A = (from q3 in _EmployeeObjectiveKPIAssessment
                                    where q3.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q3.PeriodNo == 3
                                    select q3.Result).DefaultIfEmpty().Sum() ?? 0,

                                Q4_ID = (from q4 in _EmployeeObjectiveKPIAssessment
                                    where q4.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q4.PeriodNo == 4
                                    select q4.Id).DefaultIfEmpty().Sum(),

                                Q4_P = (from q4 in _EmployeeObjectiveKPIAssessment
                                    where q4.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q4.PeriodNo == 4
                                    select q4.Target).DefaultIfEmpty().Sum() ?? 0,

                                Q4_A = (from q4 in _EmployeeObjectiveKPIAssessment
                                    where q4.EmployeeObjectiveKpiId == objKpiID.ID
                                          && q4.PeriodNo == 4
                                    select q4.Result).DefaultIfEmpty().Sum() ?? 0,

                                AnnualResult = (from q in _EmployeeObjectiveKPIAssessment
                                    where q.EmployeeObjectiveKpiId == objKpiID.ID && (
                                        (q.PeriodNo == 1 && q1Filter == 1) ||
                                        (q.PeriodNo == 2 && q2Filter == 1) ||
                                        (q.PeriodNo == 3 && q3Filter == 1) ||
                                        (q.PeriodNo == 4 && q4Filter == 1)
                                    )
                                    select q.Result).DefaultIfEmpty().Sum(),


                                AnnualTarget = (from q in _EmployeeObjectiveKPIAssessment
                                    where q.EmployeeObjectiveKpiId == objKpiID.ID && (
                                        (q.PeriodNo == 1 && q1Filter == 1) ||
                                        (q.PeriodNo == 2 && q2Filter == 1) ||
                                        (q.PeriodNo == 3 && q3Filter == 1) ||
                                        (q.PeriodNo == 4 && q4Filter == 1)
                                    )
                                    select q.Target).DefaultIfEmpty().Sum(),
                                resultPerc = ((from q in _EmployeeObjectiveKPIAssessment
                                    where q.EmployeeObjectiveKpiId == objKpiID.ID &&(
                                        (q.PeriodNo==1 && q1Filter==1) ||
                                        (q.PeriodNo==2 && q2Filter==1) ||
                                        (q.PeriodNo==3 && q3Filter==1) ||
                                        (q.PeriodNo==4 && q4Filter==1) 
                                    )
                                    select q.Result).DefaultIfEmpty().Sum() / (from q in _EmployeeObjectiveKPIAssessment
                                    where q.EmployeeObjectiveKpiId == objKpiID.ID &&(
                                        (q.PeriodNo==1 && q1Filter==1) ||
                                        (q.PeriodNo==2 && q2Filter==1) ||
                                        (q.PeriodNo==3 && q3Filter==1) ||
                                        (q.PeriodNo==4 && q4Filter==1) 
                                    )
                                    select q.Target).DefaultIfEmpty().Sum()) * 100,
                                ResultDesc = "",
                                Notes = objKpiID.Note,
                            }).ToList()
                    });
                var result2 = result1.Select(empObj => new
                {
                    empObj.code,
                    empObj.created_by,
                    empObj.created_date,
                    empObj.emp_assesment_id,
                    empObj.ID,
                    empObj.modified_by,
                    empObj.modified_date,
                    empObj.name,
                    empObj.note,
                    empObj.pos_desc_id,
                    empObj.project_id,
                    empObj.result_after_round,
                    empObj.result_without_round,
                    empObj.target,
                    empObj.weight,
                    empObj.KPIs,
                    q1 = Math.Round(
                        (empObj.KPIs.Select(x => new { t = x.Q1_P == 0 ? 0 : (x.Q1_A / x.Q1_P) }).Select(x => x.t)
                            .DefaultIfEmpty().Average()) * 4, 2),
                    q2 = Math.Round(
                        (empObj.KPIs.Select(x => new { t = x.Q2_P == 0 ? 0 : (x.Q2_A / x.Q2_P) }).Select(x => x.t)
                            .DefaultIfEmpty().Average()) * 4, 2),
                    q3 = Math.Round(
                        (empObj.KPIs.Select(x => new { t = x.Q3_P == 0 ? 0 : (x.Q3_A / x.Q3_P) }).Select(x => x.t)
                            .DefaultIfEmpty().Average()) * 4, 2),
                    q4 = Math.Round(
                        (empObj.KPIs.Select(x => new { t = x.Q4_P == 0 ? 0 : (x.Q4_A / x.Q4_P) }).Select(x => x.t)
                            .DefaultIfEmpty().Average()) * 4, 2),
                    //q1 = Math.Round((empObj.KPIs.Select(x => new { t = (x.Q1_A / x.Q1_P == 0 ? 1 : x.Q1_P) }).Select(x => x.t).DefaultIfEmpty().Average()) * 5, 2),
                    //q2 = Math.Round((empObj.KPIs.Select(x => new { t = (x.Q2_A / x.Q2_P == 0 ? 1 : x.Q2_P) }).Select(x => x.t).DefaultIfEmpty().Average()) * 5, 2),
                    //q3 = Math.Round((empObj.KPIs.Select(x => new { t = (x.Q3_A / x.Q3_P == 0 ? 1 : x.Q3_P) }).Select(x => x.t).DefaultIfEmpty().Average()) * 5, 2),
                    //q4 = Math.Round((empObj.KPIs.Select(x => new { t = (x.Q4_A / x.Q4_P == 0 ? 1 : x.Q4_P) }).Select(x => x.t).DefaultIfEmpty().Average()) * 5, 2),
                    Result = empObj.KPIs.Any(a => a.resultPerc > 0)
                        ? empObj.KPIs.Select(x => x.resultPerc).DefaultIfEmpty()
                            .Average()
                        : 0,
                });
                var result = result2.Select(empObj => new
                {
                    empObj.code,
                    empObj.created_by,
                    empObj.created_date,
                    empObj.emp_assesment_id,
                    empObj.ID,
                    empObj.modified_by,
                    empObj.modified_date,
                    empObj.name,
                    empObj.note,
                    empObj.pos_desc_id,
                    empObj.project_id,
                    empObj.result_after_round,
                    empObj.result_without_round,
                    empObj.target,
                    empObj.weight,
                    empObj.KPIs,
                    empObj.q1,
                    empObj.q2,
                    empObj.q3,
                    empObj.q4,
                    Result = empObj.Result,
                    EmployeePerformance = _Context.EmployeePerformanceSegmentCollection.Where(x =>
                            (empObj.Result >= maxSegmentPercentage ? maxSegmentPercentage : empObj.Result) <= x.percentage_to &&  (empObj.Result >= maxSegmentPercentage ? maxSegmentPercentage : empObj.Result) >= x.percentage_from)
                        .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault(),
                    //empObj.KPI
                });
                return Ok(new { Data = result.ToList(), IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectiveFinalResult(
            int EmployeeAssesmentID, string LanguageCode)
        {
            try
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
                var result = result5.Select(empObj => new
                {
                    empObj.project_id,
                    empObj.name,
                    ObjFinalResult = empObj.ObjFinalResult,
                });
                var sum2 = Math.Round(sum);
                var sumComFinalResultValue2 = _Context.EmployeePerformanceSegmentCollection.ToList();
                var sumComFinalResultValue = _Context.EmployeePerformanceSegmentCollection.Where(x => sum2 == x.segment)
                    .Select(x => x.name).DefaultIfEmpty().FirstOrDefault();
                sumComFinalResultValue = sumComFinalResultValue == null && sum > 0
                    ? (from test in _Context.EmployeePerformanceSegmentCollection.ToList()
                            .OrderByDescending(x => x.segment)
                        select test.name).FirstOrDefault()
                    : sumComFinalResultValue;
                var lis = new Dictionary<string, object>();
                lis.Add("sumObjFinalResult", sum);
                //lis.Add("sumObjFinalResult", sum/cnt);
                lis.Add("sumObjFinalResultValue", sumComFinalResultValue);
                lis.Add("data", result);
                return Ok(new { Data = lis, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectiveByEmployee(
            int employeeId, int companyId, int yearId, string LanguageCode)
        {
            try
            {
                EmployeeAssesmentEntity employeeAssesmentEntity = _Context.EmployeeAssesmentCollection
                    .Where(x => x.employee_id == employeeId && x.year_id == yearId).FirstOrDefault();


                if (employeeAssesmentEntity != null)
                {
                    var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection
                            .Where(x => x.emp_assesment_id == employeeAssesmentEntity.ID).ToList().AsQueryable()
                        select new
                        {
                            empObj.code,
                            empObj.created_by,
                            empObj.created_date,
                            empObj.emp_assesment_id,
                            empObj.ID,
                            empObj.modified_by,
                            empObj.modified_date,
                            name = (LanguageCode == "en" ? empObj.name : empObj.name2),
                            empObj.note,
                            empObj.pos_desc_id,
                            empObj.project_id,
                            empObj.result_after_round,
                            empObj.result_without_round,
                            empObj.target,
                            empObj.weight,

                            Target1 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 1
                                select q1.Target).DefaultIfEmpty(0),
                            Target2 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 2
                                select q1.Target).DefaultIfEmpty(0),
                            Target3 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 3
                                select q1.Target).DefaultIfEmpty(0),
                            Target4 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 4
                                select q1.Target).DefaultIfEmpty(0)
                        }).ToList();


                    return Ok(new { Data = lstObjs, IsError = false, ErrorMessage = string.Empty });
                }
                else
                {
                    return Ok(new { Data = "", IsError = false, ErrorMessage = string.Empty });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectives(
             int yearId, string LanguageCode,int? employeeId, int? unitId)
        {
            try
            {
                 var lstObjs = (from empObj in _Context.EmployeeObjectiveCollection
                             join empAss in _Context.EmployeeAssesmentCollection on empObj.emp_assesment_id equals empAss.ID
                             join emp in _Context.EmployeesCollection on empAss.employee_id equals emp.ID
                                     where ( employeeId==null || empAss.employee_id==employeeId ) &&
                                           (unitId==null || emp.UNIT_ID==unitId)
                        select new
                        {
                            empObj.ID,
                            name = (LanguageCode == "en" ? empObj.name : empObj.name2),
                            empObj.pos_desc_id,
                            empObj.project_id,
                          

                        }).ToList();


                    return Ok(new { Data = lstObjs, IsError = false, ErrorMessage = string.Empty });
               
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult Save(
            string Code,
            int EmployeeAssesmentID,
            string CreatedBy,
            string Name,
            string Note,
            float Weight,
            long? ProjectID,
            long? PositionDescID,
            int kPICycle,
            string LanguageCode,
            int ObjectiveCompetencyID=0
            )
        {
            try
            {
                var empExist = (from obj in _Context.EmployeeObjectiveCollection
                        where
                            (obj.emp_assesment_id == EmployeeAssesmentID) && (obj.code == Code) //& 
                        // (obj.ID != id)
                        select obj.weight
                    ).FirstOrDefault();
                if (empExist != null)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "This code is exist, please add another code"
                    });
                }

                var Weights = (from obj in _Context.EmployeeObjectiveCollection
                        where
                            (obj.emp_assesment_id == EmployeeAssesmentID) //&
                        // (obj.ID != id)
                        select obj.weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + Weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                EmployeeObjectiveEntity oEmployeeObjectiveEntity = new EmployeeObjectiveEntity();

                oEmployeeObjectiveEntity.code = Code;
                oEmployeeObjectiveEntity.objective_competency_id = ObjectiveCompetencyID;
                oEmployeeObjectiveEntity.emp_assesment_id = EmployeeAssesmentID;
                oEmployeeObjectiveEntity.created_by = CreatedBy;
                oEmployeeObjectiveEntity.created_date = DateTime.Now;
                oEmployeeObjectiveEntity.project_id = ProjectID;
                oEmployeeObjectiveEntity.pos_desc_id = PositionDescID;

                if (LanguageCode == "en")
                    oEmployeeObjectiveEntity.name = Name;
                else
                    oEmployeeObjectiveEntity.name2 = Name;
                oEmployeeObjectiveEntity.note = Note;
                oEmployeeObjectiveEntity.weight = Weight;
                oEmployeeObjectiveEntity.target = 0;

                _Context.Entry(oEmployeeObjectiveEntity).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveCollection.Add(oEmployeeObjectiveEntity);
                _Context.SaveChanges();


                EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                    .Where(x => x.ID == EmployeeAssesmentID).FirstOrDefault();


                //SaveAssesments(oEmployeeObjectiveEntity, employeeAssesment, Target1, Target2, Target3, Target4);

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        private void SaveAssesments(EmployeeObjectiveEntity oEmployeeObjective,
            EmployeeAssesmentEntity employeeAssesment, float Target1,
            float Target2,
            float Target3,
            float Target4)
        {
            if (employeeAssesment.isQuarter1)
            {
                EmployeeObjectiveAssessmentEntity o1 = new EmployeeObjectiveAssessmentEntity();
                o1.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q1;
                o1.CreatedBy = oEmployeeObjective.created_by;
                o1.CreatedDate = DateTime.Now;
                o1.EmployeeObjectiveId = oEmployeeObjective.ID;
                o1.ResultBeforeRound = 0;
                o1.ResultAfterRound = 0;
                o1.Target = Target1;
                o1.WeightResultWithoutRound = 0;
                o1.WeightResultAfterRound = 0;
                _Context.Entry(o1).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveAssessmentCollection.Add(o1);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter2)
            {
                EmployeeObjectiveAssessmentEntity o2 = new EmployeeObjectiveAssessmentEntity();
                o2.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q2;
                o2.CreatedBy = oEmployeeObjective.created_by;
                o2.CreatedDate = DateTime.Now;
                o2.EmployeeObjectiveId = oEmployeeObjective.ID;
                o2.ResultBeforeRound = 0;
                o2.ResultAfterRound = 0;
                o2.Target = Target2;
                o2.WeightResultWithoutRound = 0;
                o2.WeightResultAfterRound = 0;
                _Context.Entry(o2).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveAssessmentCollection.Add(o2);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter3)
            {
                EmployeeObjectiveAssessmentEntity o3 = new EmployeeObjectiveAssessmentEntity();
                o3.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q3;
                o3.CreatedBy = oEmployeeObjective.created_by;
                o3.CreatedDate = DateTime.Now;
                o3.EmployeeObjectiveId = oEmployeeObjective.ID;
                o3.ResultBeforeRound = 0;
                o3.ResultAfterRound = 0;
                o3.Target = Target3;
                o3.WeightResultWithoutRound = 0;
                o3.WeightResultAfterRound = 0;
                _Context.Entry(o3).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveAssessmentCollection.Add(o3);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter4)
            {
                EmployeeObjectiveAssessmentEntity o4 = new EmployeeObjectiveAssessmentEntity();
                o4.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q4;
                o4.CreatedBy = oEmployeeObjective.created_by;
                o4.CreatedDate = DateTime.Now;
                o4.EmployeeObjectiveId = oEmployeeObjective.ID;
                o4.ResultBeforeRound = 0;
                o4.ResultAfterRound = 0;
                o4.Target = Target4;
                o4.WeightResultWithoutRound = 0;
                o4.WeightResultAfterRound = 0;
                _Context.Entry(o4).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveAssessmentCollection.Add(o4);
                _Context.SaveChanges();
            }
        }

        [HttpGet]
        public IHttpActionResult Update(
            int ID,
            string Code,
            string ModifiedBy,
            string Name,
            string Note,
            float Weight,
            long? ProjectID,
            long? PositionDescID,
            string LanguageCode,
            int ObjectiveCompetencyID=0

            )
        {
            try
            {
                EmployeeObjectiveEntity oEmployeeObjectiveEntity =
                    _Context.EmployeeObjectiveCollection.Where(x => x.ID == ID).FirstOrDefault();
                var Weights = (from obj in _Context.EmployeeObjectiveCollection
                    where
                        (obj.emp_assesment_id == oEmployeeObjectiveEntity.emp_assesment_id) &
                        (obj.ID != ID)
                    select obj.weight).DefaultIfEmpty().Sum();
                if ((Weights + Weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                if (oEmployeeObjectiveEntity != null)
                {
                    oEmployeeObjectiveEntity.code = Code;
                    oEmployeeObjectiveEntity.modified_by = ModifiedBy;
                    oEmployeeObjectiveEntity.modified_date = DateTime.Now;
                    oEmployeeObjectiveEntity.project_id = ProjectID;
                    oEmployeeObjectiveEntity.pos_desc_id = PositionDescID;
                    if (LanguageCode == "en")
                        oEmployeeObjectiveEntity.name = Name;
                    else
                        oEmployeeObjectiveEntity.name2 = Name;
                    oEmployeeObjectiveEntity.note = Note;
                    oEmployeeObjectiveEntity.weight = Weight;
                    oEmployeeObjectiveEntity.target = 0;
                    oEmployeeObjectiveEntity.objective_competency_id = ObjectiveCompetencyID;


                    _Context.Entry(oEmployeeObjectiveEntity).State = System.Data.Entity.EntityState.Modified;

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
                using (TransactionScope scope = new TransactionScope())
                {
                    var oEmployeeObjective =
                        _Context.EmployeeObjectiveCollection.Where(x => x.ID == ID).FirstOrDefault();
                    if (oEmployeeObjective != null)
                    {
                        List<long> kpiIds = _Context.EmployeeObjectiveKPICollection.Where(x => x.emp_obj_id == ID)
                            .Select(x => x.ID).ToList<long>();

                        EmployeeObjectiveKPIController employeeObjectiveKPIController =
                            new EmployeeObjectiveKPIController();

                        foreach (long id in kpiIds)
                        {
                            employeeObjectiveKPIController.DeleteWithoutTransScope(id);
                        }


                        List<long> compObjIds = _Context.EmployeeObjectiveAssessmentCollection
                            .Where(x => x.EmployeeObjectiveId == ID).Select(x => x.Id).ToList<long>();

                        EmployeeObjectiveAssessmentController employeeObjectiveAssessmentController =
                            new EmployeeObjectiveAssessmentController();

                        foreach (long compObjId in compObjIds)
                        {
                            employeeObjectiveAssessmentController.Delete(compObjId);
                        }


                        _Context.EmployeeObjectiveCollection.Remove(oEmployeeObjective);
                        _Context.SaveChanges();

                        scope.Complete();

                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }
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
                var oEmployeeObjective =
                    (from empObj in _Context.EmployeeObjectiveCollection.Where(x => x.ID == ID).ToList().AsQueryable()
                        select new
                        {
                            empObj.code,
                            empObj.created_by,
                            empObj.created_date,
                            empObj.emp_assesment_id,
                            empObj.ID,
                            empObj.modified_by,
                            empObj.modified_date,
                            name = (LanguageCode == "en" ? empObj.name : empObj.name2),
                            empObj.note,
                            empObj.pos_desc_id,
                            empObj.project_id,
                            empObj.result_after_round,
                            empObj.result_without_round,
                            empObj.target,
                            empObj.weight,
                            empObj.target_type,
                            empObj.objective_competency_id,
                            Target1 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 1
                                select q1.Target).DefaultIfEmpty(0),
                            Target2 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 2
                                select q1.Target).DefaultIfEmpty(0),
                            Target3 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 3
                                select q1.Target).DefaultIfEmpty(0),
                            Target4 = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                                where q1.EmployeeObjectiveId == empObj.ID
                                      && q1.PeriodNo == 4
                                select q1.Target).DefaultIfEmpty(0)
                        }).FirstOrDefault();


                if (oEmployeeObjective != null)
                {
                    return Ok(new { Data = oEmployeeObjective, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult ImportActionPlansToEmployeeObjectives(
            int EmployeeAssesmentID,
            int companyId
        )
        {
            try
            {
                EmployeeAssesmentEntity empAss = _Context.EmployeeAssesmentCollection
                    .Where(x => x.ID == EmployeeAssesmentID).FirstOrDefault();

                if (empAss != null)
                {
                    List<ProjectActionPlanEntity> actionPlans =
                        (from ap in _Context.ProjectActionPlanCollection.Where(x => x.emp_id == empAss.employee_id)
                            join pr in _Context.ProjectsCollection on ap.projectID equals pr.ID
                            select ap).ToList();

                    if (actionPlans != null && actionPlans.Count > 0)
                    {
                        foreach (ProjectActionPlanEntity actionPlan in actionPlans)
                        {
                            EmployeeObjectiveEntity oEmployeeObjectiveEntity = new EmployeeObjectiveEntity();

                            oEmployeeObjectiveEntity.code = actionPlan.ID.ToString();
                            oEmployeeObjectiveEntity.emp_assesment_id = EmployeeAssesmentID;
                            oEmployeeObjectiveEntity.created_by = actionPlan.CreatedBy;
                            oEmployeeObjectiveEntity.created_date = DateTime.Now;
                            oEmployeeObjectiveEntity.project_id = actionPlan.projectID;
                            //oEmployeeObjectiveEntity.pos_desc_id = PositionDescID;

                            //if (LanguageCode == "en")
                            oEmployeeObjectiveEntity.name = actionPlan.action_name;
                            //else
                            oEmployeeObjectiveEntity.name2 = actionPlan.action_name;
                            oEmployeeObjectiveEntity.note = actionPlan.action_notes;
                            oEmployeeObjectiveEntity.weight = actionPlan.action_weight;
                            oEmployeeObjectiveEntity.target = 0;
                            oEmployeeObjectiveEntity.target_type = 1;

                            _Context.Entry(oEmployeeObjectiveEntity).State = System.Data.Entity.EntityState.Added;
                            _Context.EmployeeObjectiveCollection.Add(oEmployeeObjectiveEntity);
                            _Context.SaveChanges();


                            EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                                .Where(x => x.ID == EmployeeAssesmentID).FirstOrDefault();


                            SaveAssesments(oEmployeeObjectiveEntity, employeeAssesment, 0, 0, 0, 0);
                        }
                    }
                }

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}