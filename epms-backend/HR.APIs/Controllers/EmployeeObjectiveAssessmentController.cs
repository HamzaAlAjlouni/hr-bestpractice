using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities;
using HR.Entities.Admin;
using Newtonsoft.Json;
using System.Web;

namespace HR.APIs.Controllers
{
    public class EmployeeObjectiveAssessmentController : ApiController
    {
        private HRContext _Context;

        public EmployeeObjectiveAssessmentController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectiveAssessments(
            int employeeObjectiveId)
        {
            try
            {
                List<EmployeeObjectiveAssessmentEntity> employeeObjectiveAssessments =
                    (from employeeObjectiveAssessment in _Context.EmployeeObjectiveAssessmentCollection
                        where employeeObjectiveAssessment.EmployeeObjectiveId == employeeObjectiveId
                        select employeeObjectiveAssessment).ToList();

                return Ok(new { Data = employeeObjectiveAssessments, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int periodNo,
            int employeeObjectiveId,
            float? resultBeforeRound,
            float? resultAfterRound,
            float target,
            float? weightResultWithoutRound,
            float? weightResultAfterRound,
            string createdBy
        )
        {
            try
            {
                EmployeeObjectiveAssessmentEntity employeeObjectiveAssessment = new EmployeeObjectiveAssessmentEntity();

                employeeObjectiveAssessment.PeriodNo = periodNo;
                employeeObjectiveAssessment.CreatedBy = createdBy;
                employeeObjectiveAssessment.CreatedDate = DateTime.Now;
                employeeObjectiveAssessment.EmployeeObjectiveId = employeeObjectiveId;
                employeeObjectiveAssessment.ResultBeforeRound = resultBeforeRound;
                employeeObjectiveAssessment.ResultAfterRound = resultAfterRound;
                employeeObjectiveAssessment.Target = target;
                employeeObjectiveAssessment.WeightResultWithoutRound = weightResultWithoutRound;
                employeeObjectiveAssessment.WeightResultAfterRound = weightResultAfterRound;

                _Context.Entry(employeeObjectiveAssessment).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveAssessmentCollection.Add(employeeObjectiveAssessment);
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
            int periodNo,
            int employeeObjectiveId,
            float? resultBeforeRound,
            float? resultAfterRound,
            float target,
            float? weightResultWithoutRound,
            float? weightResultAfterRound,
            string modifiedBy
        )
        {
            try
            {
                EmployeeObjectiveAssessmentEntity employeeObjectiveAssessment =
                    _Context.EmployeeObjectiveAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeObjectiveAssessment != null)
                {
                    employeeObjectiveAssessment.ModifiedBy = modifiedBy;
                    employeeObjectiveAssessment.ModifiedDate = DateTime.Now;
                    employeeObjectiveAssessment.PeriodNo = periodNo;
                    employeeObjectiveAssessment.ResultBeforeRound = resultBeforeRound;
                    employeeObjectiveAssessment.ResultAfterRound = resultAfterRound;
                    employeeObjectiveAssessment.Target = target;
                    employeeObjectiveAssessment.WeightResultWithoutRound = weightResultWithoutRound;
                    employeeObjectiveAssessment.WeightResultAfterRound = weightResultAfterRound;

                    _Context.Entry(employeeObjectiveAssessment).State = System.Data.Entity.EntityState.Modified;

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
        public IHttpActionResult SetResult(
            long id,
            float? resultAfterRound,
            string modifiedBy,
            long companyId
        )
        {
            try
            {
                EmployeeObjectiveAssessmentEntity employeeObjectiveAssessment =
                    _Context.EmployeeObjectiveAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeObjectiveAssessment != null)
                {
                    employeeObjectiveAssessment.ModifiedBy = modifiedBy;
                    employeeObjectiveAssessment.ModifiedDate = DateTime.Now;
                    employeeObjectiveAssessment.PeriodNo = employeeObjectiveAssessment.PeriodNo;
                    employeeObjectiveAssessment.ResultAfterRound = resultAfterRound;

                    _Context.Entry(employeeObjectiveAssessment).State = System.Data.Entity.EntityState.Modified;


                    if (resultAfterRound > 0)
                    {
                        // Get all assessment for the Employee competency id in differnet period to calculate average
                        List<EmployeeObjectiveAssessmentEntity> employeeObjectiveAssessmentsInAllPeriods =
                            _Context.EmployeeObjectiveAssessmentCollection.Where(x =>
                                x.EmployeeObjectiveId == employeeObjectiveAssessment.EmployeeObjectiveId).ToList();

                        if (employeeObjectiveAssessmentsInAllPeriods != null &&
                            employeeObjectiveAssessmentsInAllPeriods.Count > 0)
                        {
                            List<EmployeeObjectiveAssessmentEntity> employeeObjectiveAssessmentsInAllPeriodsHaveResult =
                                employeeObjectiveAssessmentsInAllPeriods.Where(x => x.ResultAfterRound > 0).ToList();

                            if (employeeObjectiveAssessmentsInAllPeriodsHaveResult.Count ==
                                employeeObjectiveAssessmentsInAllPeriods.Count)
                            {
                                EmployeeObjectiveEntity employeeObjective = _Context.EmployeeObjectiveCollection
                                    .Where(x => x.ID == employeeObjectiveAssessment.EmployeeObjectiveId)
                                    .FirstOrDefault();

                                if (employeeObjective != null)
                                {
                                    //float employeeObjectiveAverageResult = (float)employeeObjectiveAssessmentsInAllPeriodsHaveResult.Average(x => x.ResultAfterRound);

                                    float employeeObjectiveAverageResult = 0;
                                    float totalActualResult = 0;
                                    float totalPlannedResult = 0;
                                    foreach (EmployeeObjectiveAssessmentEntity emoObjAss in
                                             employeeObjectiveAssessmentsInAllPeriodsHaveResult)
                                    {
                                        totalActualResult = totalActualResult + (float)emoObjAss.ResultAfterRound;
                                        totalPlannedResult = totalPlannedResult + (float)emoObjAss.Target;
                                    }


                                    if (totalPlannedResult > 0)
                                    {
                                        List<AssessmentMapping> assessmentMappingList =
                                            _Context.AssessmentMappingCollection.Where(x => x.company_id == companyId)
                                                .ToList();
                                        employeeObjectiveAverageResult = totalActualResult / totalPlannedResult * 100;

                                        if (assessmentMappingList != null && assessmentMappingList.Count > 0)
                                        {
                                            AssessmentMapping assessmentMapping = assessmentMappingList
                                                .Where(x => employeeObjectiveAverageResult >= x.from &&
                                                            employeeObjectiveAverageResult <= x.to).FirstOrDefault();

                                            if (assessmentMapping != null)
                                            {
                                                employeeObjective.final_point_result = assessmentMapping.point;
                                            }
                                        }
                                    }


                                    employeeObjective.result_without_round = employeeObjectiveAverageResult;
                                    employeeObjective.result_after_round =
                                        (float)Math.Round(employeeObjectiveAverageResult, 0,
                                            MidpointRounding.AwayFromZero);

                                    _Context.Entry(employeeObjective).State = System.Data.Entity.EntityState.Modified;

                                    List<EmployeeObjectiveEntity> employeeObjectives =
                                        _Context.EmployeeObjectiveCollection.Where(x =>
                                            x.emp_assesment_id == employeeObjective.emp_assesment_id).ToList();

                                    EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                                        .Where(x => x.ID == employeeObjective.emp_assesment_id).FirstOrDefault();

                                    if (employeeAssesment != null && employeeObjectives != null &&
                                        employeeObjectives.Count() > 0)
                                    {
                                        List<EmployeeObjectiveEntity> employeeObjectivesHaveResults =
                                            employeeObjectives.Where(x => x.final_point_result > 0).ToList();


                                        if (employeeObjectivesHaveResults != null &&
                                            employeeObjectivesHaveResults.Count() == employeeObjectives.Count())
                                        {
                                            float average =
                                                (float)employeeObjectives.Average(x => x.final_point_result);

                                            //average = (average * (float)employeeAssesment.target) / (float)employeeObjectives[0].target;

                                            employeeAssesment.objectives_result = average;
                                            employeeAssesment.objectives_result_after_round =
                                                (float)Math.Round(average, 0, MidpointRounding.AwayFromZero);


                                            employeeAssesment.objectives_weight_result = average *
                                                (employeeAssesment.objectives_weight == null
                                                    ? 1
                                                    : employeeAssesment.objectives_weight / 100);
                                            employeeAssesment.objectives_weight_result_after_round =
                                                (float)Math.Round((float)employeeAssesment.objectives_weight_result, 0,
                                                    MidpointRounding.AwayFromZero);
                                            if (employeeAssesment.competencies_result_after_round > 0)
                                            {
                                                float finalAverage =
                                                    (float)((employeeAssesment.competencies_result_after_round +
                                                             employeeAssesment.objectives_result_after_round) / 2);
                                                float finalWeightAverage =
                                                    (float)((employeeAssesment.competencies_weight_result_after_round +
                                                             employeeAssesment.objectives_weight_result_after_round) /
                                                            2);
                                                employeeAssesment.result_before_round = finalAverage;
                                                employeeAssesment.result_after_round =
                                                    (float)Math.Round((float)finalAverage, 0,
                                                        MidpointRounding.AwayFromZero);
                                            }

                                            _Context.Entry(employeeAssesment).State =
                                                System.Data.Entity.EntityState.Modified;
                                        }
                                    }
                                }
                            }
                        }
                    }

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


        private string getResultDesc(List<EmployeePerformanceSegmentEntity> segments, long? segmentId)
        {
            string resultDesc = "";

            if (segments != null && segmentId != null && segmentId > 0)
            {
                EmployeePerformanceSegmentEntity segment = segments.Where(x => x.id == segmentId).FirstOrDefault();

                if (segment != null)
                {
                    resultDesc = segment.name;
                }
            }

            return resultDesc;
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeObjectivesAssessments(
            long employeeAssessmentId, long companyId, string LanguageCode
        )
        {
            try
            {
                List<AssessmentMapping> assessmentsMapping =
                    _Context.AssessmentMappingCollection.Where(x => x.company_id == companyId).ToList();
                EmployeeAssesmentEntity ass = _Context.EmployeeAssesmentCollection
                    .Where(x => x.ID == employeeAssessmentId).FirstOrDefault();

                List<EmployeePerformanceSegmentEntity> segments = _Context.EmployeePerformanceSegmentCollection
                    .Where(x => x.year == ass.year_id).ToList();

                var lstEmployeeObjectivesAssessments = (
                    from empAss in _Context.EmployeeAssesmentCollection
                    join empObj in _Context.EmployeeObjectiveCollection on empAss.ID equals empObj.emp_assesment_id
                    join emp in _Context.EmployeesCollection on empAss.employee_id equals emp.ID
                    where empAss.ID == employeeAssessmentId
                    select new
                    {
                        empAss.ID,
                        empObjId = empObj.ID,
                        ObjectiveName = (LanguageCode == "en" ? empObj.name : empObj.name2),

                        empObj.target,
                        empObj.result_without_round,
                        empObj.result_after_round,
                        empObj.code,
                        Q1_Disabled = empAss.isQuarter1 ? "false" : "true",
                        Q2_Disabled = empAss.isQuarter2 ? "false" : "true",
                        Q3_Disabled = empAss.isQuarter3 ? "false" : "true",
                        Q4_Disabled = empAss.isQuarter4 ? "false" : "true",
                        empObj.final_point_result
                    }).ToList().AsQueryable();

                var KPIs = (from empObjKpis in _Context.EmployeeObjectiveKPICollection
                    select new
                    {
                        empObjKpis.ID,
                        empObjKpis.Note,
                        empObjKpis.Result,
                        empObjKpis.SegmentId,
                        ObjKpiName = (LanguageCode == "en" ? empObjKpis.name : empObjKpis.name2),
                        empObjKpis.emp_obj_id
                    }).ToList().AsQueryable();

                var join1 = (from a in lstEmployeeObjectivesAssessments
                    select new
                    {
                        a.ID,
                        a.empObjId,
                        a.ObjectiveName,
                        a.Q1_Disabled,
                        a.Q2_Disabled,
                        a.Q3_Disabled,
                        a.Q4_Disabled,
                        a.result_after_round,
                        a.result_without_round,
                        a.target,
                        a.code,
                        a.final_point_result,
                        color = GetColor(assessmentsMapping, a.final_point_result),
                        Q1_ID = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 1
                            select q1.Id).FirstOrDefault(),
                        Q1_P = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 1
                            select q1.Target).FirstOrDefault(),
                        Q1_A = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 1
                            select q1.ResultAfterRound).FirstOrDefault(),

                        Q2_ID = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 2
                            select q1.Id).FirstOrDefault(),
                        Q2_P = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 2
                            select q1.Target).FirstOrDefault(),
                        Q2_A = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 2
                            select q1.ResultAfterRound).FirstOrDefault(),

                        Q3_ID = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 3
                            select q1.Id).FirstOrDefault(),
                        Q3_P = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 3
                            select q1.Target).FirstOrDefault(),
                        Q3_A = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 3
                            select q1.ResultAfterRound).FirstOrDefault(),
                        Q4_ID = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 4
                            select q1.Id).FirstOrDefault(),
                        Q4_P = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 4
                            select q1.Target).FirstOrDefault(),
                        Q4_A = (from q1 in _Context.EmployeeObjectiveAssessmentCollection
                            where q1.EmployeeObjectiveId == a.empObjId
                                  && q1.PeriodNo == 4
                            select q1.ResultAfterRound).FirstOrDefault(),
                        KPIs = (from k in KPIs
                            where k.emp_obj_id == a.empObjId
                            select new
                            {
                                ObjKpiName = k.ObjKpiName,
                                ObjKPINote = (k.Note == null ? "" : k.Note),
                                ObjKPIResult = (k.Result == null ? 0 : k.Result),
                                ObjKPIResultDesc = getResultDesc(segments, k.SegmentId),
                                Q1_ID = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 1
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q1_P = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 1
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q1_A = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 1
                                    select q1.Result).DefaultIfEmpty().Sum(),

                                Q2_ID = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 2
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q2_P = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 2
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q2_A = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 2
                                    select q1.Result).DefaultIfEmpty().Sum(),

                                Q3_ID = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 3
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q3_P = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 3
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q3_A = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 3
                                    select q1.Result).DefaultIfEmpty().Sum(),
                                Q4_ID = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 4
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q4_P = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 4
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q4_A = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                                    where q1.EmployeeObjectiveKpiId == k.ID
                                          && q1.PeriodNo == 4
                                    select q1.Result).DefaultIfEmpty().Sum()
                            }).ToList()
                    }).ToList();

                return Ok(new { Data = join1, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public IHttpActionResult old_SaveObjectiveKpiResults(long companyId)
        {
            try
            {
                string ModifiedBy = "ADMIN";
                var details =
                    JsonConvert.DeserializeObject<dynamic[]>(HttpContext.Current.Request.Form["objective_kpi_results"]);
                EmployeeObjectiveKpiAssessmentController employeeObjectiveKpiAssessmentController =
                    new EmployeeObjectiveKpiAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    string ObjKPINote = item.Notes;

                    if (id1 != 0 && result1 != null && result1 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id1, result1, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id2 = item.Q2_ID;
                    float? result2 = item.Q2_A;
                    if (id2 != 0 && result2 != null && result2 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id2, result2, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id3 = item.Q3_ID;
                    float? result3 = item.Q3_A;
                    if (id3 != 0 && result3 != null && result3 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id3, result3, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id4 = item.Q4_ID;
                    float? result4 = item.Q4_A;
                    if (id4 != 0 && result4 != null && result4 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id4, result4, ModifiedBy, companyId,
                            ObjKPINote);
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
        public IHttpActionResult SaveObjectiveKpiResults(long companyId, string username, int empObjID)
        {
            try
            {
                string ModifiedBy = username;
                var details = JsonConvert.DeserializeObject<dynamic[]>(HttpContext.Current.Request.Form["KPIs_List"]);
                EmployeeObjectiveKpiAssessmentController employeeObjectiveKpiAssessmentController =
                    new EmployeeObjectiveKpiAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    string ObjKPINote = item.Notes;

                    if (id1 != 0 && result1 != null && result1 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id1, result1, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id2 = item.Q2_ID;
                    float? result2 = item.Q2_A;
                    if (id2 != 0 && result2 != null && result2 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id2, result2, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id3 = item.Q3_ID;
                    float? result3 = item.Q3_A;
                    if (id3 != 0 && result3 != null && result3 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id3, result3, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    long id4 = item.Q4_ID;
                    float? result4 = item.Q4_A;
                    if (id4 != 0 && result4 != null && result4 > 0)
                    {
                        employeeObjectiveKpiAssessmentController.SetResult(id4, result4, ModifiedBy, companyId,
                            ObjKPINote);
                    }

                    var objKpiID = _Context.EmployeeObjectiveKPICollection.Where(x => x.emp_obj_id == empObjID)
                        .Select(x => x.ID).FirstOrDefault();

                    var totalKpisAssResult = _Context.EmployeeObjectiveKPIAssessmentCollection
                        .Where(x => x.EmployeeObjectiveKpiId == objKpiID).Sum(x => x.Result);
                    var totalKpisAssTarget = _Context.EmployeeObjectiveKPIAssessmentCollection
                        .Where(x => x.EmployeeObjectiveKpiId == objKpiID).Sum(x => x.Target);

                    EmployeeObjectiveKPIEntity empObjKpi_Obj = _Context.EmployeeObjectiveKPICollection
                        .Where(x => x.ID == objKpiID).FirstOrDefault();

                    if (empObjKpi_Obj != null)
                    {
                        empObjKpi_Obj.Result = totalKpisAssResult;

                        _Context.Entry(empObjKpi_Obj).State = System.Data.Entity.EntityState.Modified;
                        _Context.SaveChanges();
                    }

                    double tatolKpiResults = _Context.EmployeeObjectiveKPICollection
                        .Where(x => x.emp_obj_id == empObjID).Sum(x => x.Result) ?? 0;
                    double totalKpiTarget = Convert.ToDouble(_Context.EmployeeObjectiveKPICollection
                        .Where(x => x.emp_obj_id == empObjID).Sum(x => x.target));

                    double ObjResult = (double)(tatolKpiResults / totalKpiTarget) * 100;


                    EmployeeObjectiveEntity empObj = _Context.EmployeeObjectiveCollection.Where(x => x.ID == empObjID)
                        .FirstOrDefault();
                    if (empObj != null)
                    {
                        empObj.final_point_result = (float)ObjResult;
                        empObj.result_after_round = (float)ObjResult;
                        empObj.result_without_round = (float)ObjResult;

                        _Context.Entry(empObj).State = System.Data.Entity.EntityState.Modified;
                        _Context.SaveChanges();
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
        public IHttpActionResult SaveObjectiveResults(long companyId)
        {
            try
            {
                string ModifiedBy = "ADMIN";
                var details =
                    JsonConvert.DeserializeObject<dynamic[]>(HttpContext.Current.Request.Form["objectives_results"]);
                EmployeeObjectiveAssessmentController employeeObjectiveAssessmentController =
                    new EmployeeObjectiveAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    if (id1 != 0 && result1 != null && result1 > 0)
                    {
                        employeeObjectiveAssessmentController.SetResult(id1, result1, ModifiedBy, companyId);
                    }

                    long id2 = item.Q2_ID;
                    float? result2 = item.Q2_A;
                    if (id2 != 0 && result2 != null && result2 > 0)
                    {
                        employeeObjectiveAssessmentController.SetResult(id2, result2, ModifiedBy, companyId);
                    }

                    long id3 = item.Q2_ID;
                    float? result3 = item.Q3_A;
                    if (id3 != 0 && result3 != null && result3 > 0)
                    {
                        employeeObjectiveAssessmentController.SetResult(id3, result3, ModifiedBy, companyId);
                    }

                    long id4 = item.Q4_ID;
                    float? result4 = item.Q4_A;
                    if (id4 != 0 && result4 != null && result4 > 0)
                    {
                        employeeObjectiveAssessmentController.SetResult(id4, result4, ModifiedBy, companyId);
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

        [HttpGet]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                var employeeObjectiveAssessment = _Context.EmployeeObjectiveAssessmentCollection.Where(x => x.Id == id)
                    .FirstOrDefault();
                if (employeeObjectiveAssessment != null)
                {
                    _Context.EmployeeObjectiveAssessmentCollection.Remove(employeeObjectiveAssessment);
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
        public IHttpActionResult getByID(long id)
        {
            try
            {
                var employeeObjectiveAssessment = _Context.EmployeeObjectiveAssessmentCollection.Where(x => x.Id == id)
                    .FirstOrDefault();
                if (employeeObjectiveAssessment != null)
                {
                    return Ok(new { Data = employeeObjectiveAssessment, IsError = false, ErrorMessage = string.Empty });
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