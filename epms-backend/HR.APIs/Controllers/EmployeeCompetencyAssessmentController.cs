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
    public class EmployeeCompetencyAssessmentController : ApiController
    {
        private HRContext _Context;

        public EmployeeCompetencyAssessmentController()
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

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetencyAssessments(
            int employeeCompetencyId)
        {
            try
            {
                List<EmployeeCompetencyAssessmentEntity> employeeCompetencyAssessments =
                    (from employeeCompetencyAssessment in _Context.EmployeeCompetencyAssessmentCollection
                        where employeeCompetencyAssessment.EmployeeCompetencyId == employeeCompetencyId
                        select employeeCompetencyAssessment).ToList();

                return Ok(new { Data = employeeCompetencyAssessments, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int periodNo,
            int employeeCompetencyId,
            float? resultWithoutRound,
            float? resultAfterRound,
            float target,
            float? weightResultWithoutRound,
            float? weightResultAfterRound,
            string createdBy
        )
        {
            try
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment =
                    new EmployeeCompetencyAssessmentEntity();

                employeeCompetencyAssessment.PeriodNo = periodNo;
                employeeCompetencyAssessment.CreatedBy = createdBy;
                employeeCompetencyAssessment.CreatedDate = DateTime.Now;
                employeeCompetencyAssessment.EmployeeCompetencyId = employeeCompetencyId;
                employeeCompetencyAssessment.ResultBeforeRound = resultWithoutRound;
                employeeCompetencyAssessment.ResultAfterRound = resultAfterRound;
                employeeCompetencyAssessment.Target = target;
                employeeCompetencyAssessment.WeightResultWithoutRound = weightResultWithoutRound;
                employeeCompetencyAssessment.WeightResultAfterRound = weightResultAfterRound;

                _Context.Entry(employeeCompetencyAssessment).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyAssessmentCollection.Add(employeeCompetencyAssessment);
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
            int employeeCompetencyId,
            float? resultWithoutRound,
            float? resultAfterRound,
            float target,
            float? weightResultWithoutRound,
            float? weightResultAfterRound,
            string modifiedBy
        )
        {
            try
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment =
                    _Context.EmployeeCompetencyAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyAssessment != null)
                {
                    employeeCompetencyAssessment.ModifiedBy = modifiedBy;
                    employeeCompetencyAssessment.ModifiedDate = DateTime.Now;
                    employeeCompetencyAssessment.PeriodNo = periodNo;
                    employeeCompetencyAssessment.ResultBeforeRound = resultWithoutRound;
                    employeeCompetencyAssessment.ResultAfterRound = resultAfterRound;
                    employeeCompetencyAssessment.Target = target;
                    employeeCompetencyAssessment.WeightResultWithoutRound = weightResultWithoutRound;
                    employeeCompetencyAssessment.WeightResultAfterRound = weightResultAfterRound;

                    _Context.Entry(employeeCompetencyAssessment).State = System.Data.Entity.EntityState.Modified;

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
        public IHttpActionResult Update(
            EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment
        )
        {
            try
            {
                if (employeeCompetencyAssessment != null)
                {
                    _Context.Entry(employeeCompetencyAssessment).State = System.Data.Entity.EntityState.Modified;

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
        public IHttpActionResult GetEmployeeCompetenciesAssessments(
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

                var lstEmployeeCompetenciesAssessments = (
                    from empAss in _Context.EmployeeAssesmentCollection
                    join empComp in _Context.EmployeeCompetencyCollection on empAss.ID equals empComp
                        .EmployeeAssessmentId
                    join emp in _Context.EmployeesCollection on empAss.employee_id equals emp.ID
                    join comp in _Context.CompetenceCollection on empComp.CompetencyId equals comp.ID
                    where empAss.ID == employeeAssessmentId
                    select new
                    {
                        empAss.ID,
                        empCompId = empComp.Id,
                        competencyName = (LanguageCode == "en" ? comp.NAME : comp.name2),
                        empComp.Target,
                        empComp.ResultWithoutRound,
                        empComp.ResultAfterRound,
                        code = comp.CODE,
                        Q1_Disabled = empAss.isQuarter1 ? "false" : "true",
                        Q2_Disabled = empAss.isQuarter2 ? "false" : "true",
                        Q3_Disabled = empAss.isQuarter3 ? "false" : "true",
                        Q4_Disabled = empAss.isQuarter4 ? "false" : "true",
                    }
                ).ToList().AsQueryable();

                var join1 = (from a in lstEmployeeCompetenciesAssessments
                    select new
                    {
                        a.code,
                        a.competencyName,
                        a.empCompId,
                        a.ID,
                        a.Q1_Disabled,
                        a.Q2_Disabled,
                        a.Q3_Disabled,
                        a.Q4_Disabled,
                        a.ResultAfterRound,
                        a.ResultWithoutRound,
                        a.Target,
                        color = GetColor(assessmentsMapping, a.ResultAfterRound),
                        Q1_ID = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 1
                            select q1.Id).FirstOrDefault(),
                        Q1_P = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 1
                            select q1.Target).FirstOrDefault(),
                        Q1_A = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 1
                            select q1.ResultAfterRound).FirstOrDefault(),

                        Q2_ID = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 2
                            select q1.Id).FirstOrDefault(),
                        Q2_P = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 2
                            select q1.Target).FirstOrDefault(),
                        Q2_A = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 2
                            select q1.ResultAfterRound).FirstOrDefault(),

                        Q3_ID = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 3
                            select q1.Id).FirstOrDefault(),
                        Q3_P = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 3
                            select q1.Target).FirstOrDefault(),
                        Q3_A = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 3
                            select q1.ResultAfterRound).FirstOrDefault(),
                        Q4_ID = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 4
                            select q1.Id).FirstOrDefault(),
                        Q4_P = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 4
                            select q1.Target).FirstOrDefault(),
                        Q4_A = (from q1 in _Context.EmployeeCompetencyAssessmentCollection
                            where q1.EmployeeCompetencyId == a.empCompId
                                  && q1.PeriodNo == 4
                            select q1.ResultAfterRound).FirstOrDefault(),
                    }).ToList().AsQueryable();

                var KPIs = (from empCompKpis in _Context.EmployeeCompetencyKpiCollection
                    join compKpi in _Context.CompetenciesKpiCollection on empCompKpis.EmployeeCompetencyKpiId equals
                        compKpi.ID
                    select new
                    {
                        empCompKpis.Id,
                        empCompKpis.Note,
                        empCompKpis.Result,
                        empCompKpis.SegmentId,
                        CompKpiName = (LanguageCode == "en" ? compKpi.NAME : compKpi.name2),
                        empCompKpis.EmployeeCompetencyId,
                        empCompKpis.EmployeeCompetencyKpiId
                    }).ToList().AsQueryable();


                var join2 = (
                    from b in join1
                    select new
                    {
                        b.code,
                        b.competencyName,

                        b.empCompId,
                        b.ID,
                        b.Q1_A,
                        b.Q1_Disabled,
                        b.Q1_ID,
                        b.Q1_P,
                        b.Q2_A,
                        b.Q2_Disabled,
                        b.Q2_ID,
                        b.Q2_P,
                        b.Q3_A,
                        b.Q3_Disabled,
                        b.Q3_ID,
                        b.Q3_P,
                        b.Q4_A,
                        b.Q4_Disabled,
                        b.Q4_ID,
                        b.Q4_P,
                        b.ResultAfterRound,
                        b.ResultWithoutRound,
                        b.Target,
                        b.color,
                        KPIs = (from k in KPIs
                            where k.EmployeeCompetencyId == b.empCompId
                            select new
                            {
                                k.CompKpiName,
                                CompKPINote = (k.Note == null ? "" : k.Note),
                                CompKPIResult = (k.Result == null ? 0 : k.Result),
                                CompKPIResultDesc = getResultDesc(segments, k.SegmentId),
                                k.Id,
                                Q1_ID = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 1
                                    select q1.Id).DefaultIfEmpty(0).Sum(),
                                Q1_P = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 1
                                    select q1.Target).DefaultIfEmpty(0).Sum(),
                                Q1_A = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 1
                                    select q1.Result).DefaultIfEmpty(0).Sum(),

                                Q2_ID = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 2
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q2_P = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 2
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q2_A = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 2
                                    select q1.Result).DefaultIfEmpty().Sum(),

                                Q3_ID = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 3
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q3_P = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 3
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q3_A = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 3
                                    select q1.Result).DefaultIfEmpty().Sum(),
                                Q4_ID = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 4
                                    select q1.Id).DefaultIfEmpty().Sum(),
                                Q4_P = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 4
                                    select q1.Target).DefaultIfEmpty().Sum(),
                                Q4_A = (from q1 in _Context.EmployeeCompetencyKpiAssessmentCollection
                                    where q1.EmployeeCompetencyKpiId == k.Id
                                          && q1.PeriodNo == 4
                                    select q1.Result).DefaultIfEmpty(0).Sum()
                            }).ToList()
                    });

                return Ok(new { Data = join2, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult SaveCompetencyKpiResults()
        {
            try
            {
                string ModifiedBy = "ADMIN";
                var details =
                    JsonConvert.DeserializeObject<dynamic[]>(
                        HttpContext.Current.Request.Form["competency_kpi_results"]);
                EmployeeCompetencyKpiAssessmentController employeeCompetencyKpiAssessmentController =
                    new EmployeeCompetencyKpiAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    string compKPINote = item.CompKPINote;
                    if (id1 != 0 && result1 != null && result1 > 0)
                    {
                        employeeCompetencyKpiAssessmentController.SetResult(id1, result1, ModifiedBy, compKPINote);
                    }

                    long id2 = item.Q2_ID;
                    float? result2 = item.Q2_A;
                    if (id2 != 0 && result2 != null && result2 > 0)
                    {
                        employeeCompetencyKpiAssessmentController.SetResult(id2, result2, ModifiedBy, compKPINote);
                    }

                    long id3 = item.Q2_ID;
                    float? result3 = item.Q3_A;
                    if (id3 != 0 && result3 != null && result3 > 0)
                    {
                        employeeCompetencyKpiAssessmentController.SetResult(id3, result3, ModifiedBy, compKPINote);
                    }

                    long id4 = item.Q4_ID;
                    float? result4 = item.Q4_A;
                    if (id4 != 0 && result4 != null && result4 > 0)
                    {
                        employeeCompetencyKpiAssessmentController.SetResult(id4, result4, ModifiedBy, compKPINote);
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
        public IHttpActionResult SaveCompetenciesResults()
        {
            try
            {
                string ModifiedBy = "ADMIN";
                var details =
                    JsonConvert.DeserializeObject<dynamic[]>(HttpContext.Current.Request.Form["competencies_results"]);
                EmployeeCompetencyAssessmentController employeeCompetencyAssessmentController =
                    new EmployeeCompetencyAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    if (id1 != 0 && result1 != null && result1 > 0)
                    {
                        employeeCompetencyAssessmentController.SetResult(id1, result1, ModifiedBy);
                    }

                    long id2 = item.Q2_ID;
                    float? result2 = item.Q2_A;
                    if (id2 != 0 && result2 != null && result2 > 0)
                    {
                        employeeCompetencyAssessmentController.SetResult(id2, result2, ModifiedBy);
                    }

                    long id3 = item.Q2_ID;
                    float? result3 = item.Q3_A;
                    if (id3 != 0 && result3 != null && result3 > 0)
                    {
                        employeeCompetencyAssessmentController.SetResult(id3, result3, ModifiedBy);
                    }

                    long id4 = item.Q4_ID;
                    float? result4 = item.Q4_A;
                    if (id4 != 0 && result4 != null && result4 > 0)
                    {
                        employeeCompetencyAssessmentController.SetResult(id4, result4, ModifiedBy);
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
        public IHttpActionResult SetResult(
            long id,
            float? resultAfterRound,
            string modifiedBy
        )
        {
            try
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment =
                    _Context.EmployeeCompetencyAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyAssessment != null)
                {
                    employeeCompetencyAssessment.ModifiedBy = modifiedBy;
                    employeeCompetencyAssessment.ModifiedDate = DateTime.Now;
                    employeeCompetencyAssessment.PeriodNo = employeeCompetencyAssessment.PeriodNo;
                    employeeCompetencyAssessment.ResultAfterRound = resultAfterRound;

                    _Context.Entry(employeeCompetencyAssessment).State = System.Data.Entity.EntityState.Modified;

                    _Context.SaveChanges();

                    if (resultAfterRound > 0)
                    {
                        // Get all assessment for the Employee competency id in differnet period to calculate average
                        List<EmployeeCompetencyAssessmentEntity> employeeCompetencyAssessmentsInAllPeriods =
                            _Context.EmployeeCompetencyAssessmentCollection.Where(x =>
                                x.EmployeeCompetencyId == employeeCompetencyAssessment.EmployeeCompetencyId).ToList();

                        if (employeeCompetencyAssessmentsInAllPeriods != null &&
                            employeeCompetencyAssessmentsInAllPeriods.Count > 0)
                        {
                            List<EmployeeCompetencyAssessmentEntity>
                                employeeCompetencyAssessmentsInAllPeriodsHaveResult =
                                    employeeCompetencyAssessmentsInAllPeriods.Where(x => x.ResultAfterRound > 0)
                                        .ToList();

                            if (employeeCompetencyAssessmentsInAllPeriodsHaveResult.Count ==
                                employeeCompetencyAssessmentsInAllPeriods.Count)
                            {
                                EmployeeCompetencyEntity employeeCompetency = _Context.EmployeeCompetencyCollection
                                    .Where(x => x.Id == employeeCompetencyAssessment.EmployeeCompetencyId)
                                    .FirstOrDefault();

                                if (employeeCompetency != null)
                                {
                                    float employeeCompetencyAverageResult =
                                        (float)employeeCompetencyAssessmentsInAllPeriodsHaveResult.Average(x =>
                                            x.ResultAfterRound);
                                    employeeCompetency.ResultWithoutRound = employeeCompetencyAverageResult;
                                    employeeCompetency.ResultAfterRound =
                                        (float)Math.Round(employeeCompetencyAverageResult, 0,
                                            MidpointRounding.AwayFromZero);

                                    _Context.Entry(employeeCompetency).State = System.Data.Entity.EntityState.Modified;

                                    List<EmployeeCompetencyEntity> employeeCompetencies =
                                        _Context.EmployeeCompetencyCollection.Where(x =>
                                            x.EmployeeAssessmentId == employeeCompetency.EmployeeAssessmentId).ToList();

                                    EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                                        .Where(x => x.ID == employeeCompetency.EmployeeAssessmentId).FirstOrDefault();

                                    if (employeeAssesment != null && employeeCompetencies != null &&
                                        employeeCompetencies.Count() > 0)
                                    {
                                        //List<EmployeeCompetencyAssessmentEntity> employeeCompetencyAssessments = _Context.EmployeeCompetencyAssessmentCollection.Where(x => employeeCompetenciesIds.Contains(x.EmployeeCompetencyId)).ToList();

                                        //List<EmployeeCompetencyAssessmentEntity> employeeCompetencyAssessmentsHaveResult = 
                                        List<EmployeeCompetencyEntity> employeeCompetenciesHaveResults =
                                            employeeCompetencies.Where(x => x.ResultAfterRound > 0).ToList();

                                        if (employeeCompetenciesHaveResults != null &&
                                            employeeCompetenciesHaveResults.Count() == employeeCompetencies.Count())
                                        {
                                            float average =
                                                (float)employeeCompetencies.Average(x => x.ResultAfterRound);

                                            average = (average * (float)employeeAssesment.target) /
                                                      employeeCompetencies[0].Target;


                                            employeeAssesment.competencies_result = average;
                                            employeeAssesment.competencies_result_after_round =
                                                (float)Math.Round(average, 0, MidpointRounding.AwayFromZero);
                                            employeeAssesment.competencies_weight_result = average *
                                                (employeeAssesment.competencies_weight == null
                                                    ? 1
                                                    : employeeAssesment.competencies_weight / 100);
                                            employeeAssesment.competencies_weight_result_after_round =
                                                (float)Math.Round((float)employeeAssesment.competencies_weight_result,
                                                    0, MidpointRounding.AwayFromZero);
                                            if (employeeAssesment.objectives_result_after_round > 0)
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


        [HttpGet]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                var employeeCompetencyAssessment = _Context.EmployeeCompetencyAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyAssessment != null)
                {
                    _Context.EmployeeCompetencyAssessmentCollection.Remove(employeeCompetencyAssessment);
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
                var employeeCompetencyAssessment = _Context.EmployeeCompetencyAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyAssessment != null)
                {
                    return Ok(new
                        { Data = employeeCompetencyAssessment, IsError = false, ErrorMessage = string.Empty });
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