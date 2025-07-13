using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities;
using Newtonsoft.Json;
using HR.Entities.Admin;
using System.Transactions;
using System.Web;

namespace HR.APIs.Controllers
{
    public class EmployeeCompetencyController : ApiController
    {
        private HRContext _Context;

        public EmployeeCompetencyController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult old_GetEmployeeCompetencies(
            int employeeAssessmentId, string languageCode)
        {
            try
            {
                var employeeCompetencies =
                    (from x in _Context.EmployeeCompetencyCollection
                        join y in _Context.CompetenceCollection on x.CompetencyId equals y.ID
                        join z in _Context.CodesCollection.Where(s => s.MAJOR_NO == 1) on y.c_nature_id equals z
                            .MINOR_NO
                        where x.EmployeeAssessmentId == employeeAssessmentId
                        select new
                        {
                            x.CompetencyId,
                            x.CompetencyLevelId,
                            x.CreatedBy,
                            x.CreatedDate,
                            x.EmployeeAssessmentId,
                            x.Id,
                            x.ModifiedBy,
                            x.ModifiedDate,
                            x.ResultAfterRound,
                            x.ResultWithoutRound,
                            x.Target,
                            x.Weight,
                            CompetencyName = y.NAME,
                            CompetenceNature = (languageCode == "en" ? z.NAME : z.name2),
                            x.project_desc
                        }).AsEnumerable().ToList();

                var result = (from d in employeeCompetencies select d).AsEnumerable().Select((d, index) => new
                {
                    d.CompetencyId,
                    d.CompetencyLevelId,
                    d.CreatedBy,
                    d.CreatedDate,
                    d.EmployeeAssessmentId,
                    d.Id,
                    d.ModifiedBy,
                    d.ModifiedDate,
                    d.ResultAfterRound,
                    d.ResultWithoutRound,
                    d.Target,
                    d.Weight,
                    d.CompetencyName,
                    CompetencyLevelName = getCompetencyLevelName(d.CompetencyLevelId, languageCode),
                    d.CompetenceNature,
                    d.project_desc,
                    code = index + 1
                }).ToList();


                return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

   

        
        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetencies(
            int employeeAssessmentId, string languageCode)
        {
            try
            {
                var comkpis = _Context.EmployeeCompetencyKpiCollection.ToList();
                var CompetenciesKpi = _Context.CompetenciesKpiCollection.ToList();
                var _EmployeeCompetencyKpiAssessment = _Context.EmployeeCompetencyKpiAssessmentCollection.ToList();
                var result1 = (from empCom in _Context.EmployeeCompetencyCollection
                        .Where(x => x.EmployeeAssessmentId == employeeAssessmentId).ToList()
                    join compName in _Context.CompetenceCollection.ToList() on empCom.CompetencyId equals compName.ID
                    select new
                    {
                        empCom.CompetencyLevelId,
                        empCom.CompetencyId,
                        empCom.CreatedBy,
                        empCom.CreatedDate,
                        empCom.EmployeeAssessmentId,
                        empCom.Id,
                        empCom.ModifiedBy,
                        empCom.ModifiedDate,
                        name = compName.NAME,
                        empCom.ResultAfterRound,
                        empCom.ResultWithoutRound,
                        empCom.Target,
                        empCom.Weight,
                        empCom.project_desc,
                        CompetencyLevelName = 1,
                        KPIs = (from compKpi in comkpis.ToList().Where(x => x.EmployeeCompetencyId == empCom.Id)
                            select new
                            {
                                name = CompetenciesKpi.Where(x => x.ID == compKpi.EmployeeCompetencyKpiId)
                                    .Select(x => x.NAME).FirstOrDefault(),
        
                                Q1_ID = (from q1 in _EmployeeCompetencyKpiAssessment
                                    where q1.EmployeeCompetencyKpiId == compKpi.Id
                                          && q1.PeriodNo == 1
                                    select q1.Id).DefaultIfEmpty().Sum(),
        
                                Q1_P = (from q1 in _EmployeeCompetencyKpiAssessment
                                    where q1.EmployeeCompetencyKpiId == compKpi.Id
                                          && q1.PeriodNo == 1
                                    select q1.Target).DefaultIfEmpty().Sum() ?? 0,
        
                                Q1_A = (from q1 in _EmployeeCompetencyKpiAssessment
                                    where q1.EmployeeCompetencyKpiId == compKpi.Id
                                          && q1.PeriodNo == 1
                                    select q1.Result).DefaultIfEmpty().Sum() ?? 0,
        
        
                                Q2_ID = (from q2 in _EmployeeCompetencyKpiAssessment
                                    where q2.EmployeeCompetencyKpiId == compKpi.Id
                                          && q2.PeriodNo == 2
                                    select q2.Id).DefaultIfEmpty().Sum(),
        
                                Q2_P = (from q2 in _EmployeeCompetencyKpiAssessment
                                    where q2.EmployeeCompetencyKpiId == compKpi.Id
                                          && q2.PeriodNo == 2
                                    select q2.Target).DefaultIfEmpty().Sum() ?? 0,
        
                                Q2_A = (from q2 in _EmployeeCompetencyKpiAssessment
                                    where q2.EmployeeCompetencyKpiId == compKpi.Id
                                          && q2.PeriodNo == 2
                                    select q2.Result).DefaultIfEmpty().Sum() ?? 0,
        
        
                                Q3_ID = (from q3 in _EmployeeCompetencyKpiAssessment
                                    where q3.EmployeeCompetencyKpiId == compKpi.Id
                                          && q3.PeriodNo == 3
                                    select q3.Id).DefaultIfEmpty().Sum(),
        
                                Q3_P = (from q3 in _EmployeeCompetencyKpiAssessment
                                    where q3.EmployeeCompetencyKpiId == compKpi.Id
                                          && q3.PeriodNo == 3
                                    select q3.Target).DefaultIfEmpty().Sum() ?? 0,
        
                                Q3_A = (from q3 in _EmployeeCompetencyKpiAssessment
                                    where q3.EmployeeCompetencyKpiId == compKpi.Id
                                          && q3.PeriodNo == 3
                                    select q3.Result).DefaultIfEmpty().Sum() ?? 0,
        
                                Q4_ID = (from q4 in _EmployeeCompetencyKpiAssessment
                                    where q4.EmployeeCompetencyKpiId == compKpi.Id
                                          && q4.PeriodNo == 4
                                    select q4.Id).DefaultIfEmpty().Sum(),
        
                                Q4_P = (from q4 in _EmployeeCompetencyKpiAssessment
                                    where q4.EmployeeCompetencyKpiId == compKpi.Id
                                          && q4.PeriodNo == 4
                                    select q4.Target).DefaultIfEmpty().Sum() ?? 0,
        
                                Q4_A = (from q4 in _EmployeeCompetencyKpiAssessment
                                    where q4.EmployeeCompetencyKpiId == compKpi.Id
                                          && q4.PeriodNo == 4
                                    select q4.Result).DefaultIfEmpty().Sum() ?? 0,
        
                                AnnualResult = (from q in _EmployeeCompetencyKpiAssessment
                                    where q.EmployeeCompetencyKpiId == compKpi.Id
                                    select q.Result).DefaultIfEmpty().Sum(),
        
        
                                AnnualTarget = (from q in _EmployeeCompetencyKpiAssessment
                                    where q.EmployeeCompetencyKpiId == compKpi.Id
                                    select q.Target).DefaultIfEmpty().Sum(),
                                resultPerc = (((from q in _EmployeeCompetencyKpiAssessment
                                                   where q.EmployeeCompetencyKpiId == compKpi.Id
                                                   select q.Result).DefaultIfEmpty().Sum() /
                                               (from q in _EmployeeCompetencyKpiAssessment
                                                   where q.EmployeeCompetencyKpiId == compKpi.Id
                                                   select q.Target).DefaultIfEmpty().Sum()) * 100),
                                resultPercOf5 = (((from q in _EmployeeCompetencyKpiAssessment
                                                      where q.EmployeeCompetencyKpiId == compKpi.Id
                                                      select q.Result).DefaultIfEmpty().Sum() /
                                                  (from q in _EmployeeCompetencyKpiAssessment
                                                      where q.EmployeeCompetencyKpiId == compKpi.Id
                                                      select q.Target).DefaultIfEmpty().Sum()) * 100) / 20.0,
        
                                ResultDesc = "",
                                Notes = compKpi.Note
                            }).ToList()
                    });
        
                var result2 = result1.Select(empCom => new
                {
                    empCom.CompetencyLevelId,
                    empCom.CompetencyId,
                    empCom.CreatedBy,
                    empCom.CreatedDate,
                    empCom.EmployeeAssessmentId,
                    empCom.Id,
                    empCom.ModifiedBy,
                    empCom.ModifiedDate,
                    empCom.name,
                    empCom.ResultAfterRound,
                    empCom.ResultWithoutRound,
                    empCom.Target,
                    empCom.Weight,
                    empCom.project_desc,
                    empCom.CompetencyLevelName,
                    empCom.KPIs,
                    q1 = (empCom.KPIs.Select(x => new { t = ((x.Q1_P == 0 ? 0 : x.Q1_A / x.Q1_P)) }).Select(x => x.t)
                        .DefaultIfEmpty().Average()) * 100,
                    q2 = (empCom.KPIs.Select(x => new { t = ((x.Q2_P == 0 ? 0 : x.Q2_A / x.Q2_P)) }).Select(x => x.t)
                        .DefaultIfEmpty().Average()) * 100,
                    q3 = (empCom.KPIs.Select(x => new { t = ((x.Q3_P == 0 ? 0 : x.Q3_A / x.Q3_P)) }).Select(x => x.t)
                        .DefaultIfEmpty().Average()) * 100,
                    q4 = (empCom.KPIs.Select(x => new { t = ((x.Q4_P == 0 ? 0 : x.Q4_A / x.Q4_P)) }).Select(x => x.t)
                        .DefaultIfEmpty().Average()) * 100,
                    Result = empCom.KPIs.Select(x => new { x.resultPerc }).Select(x => x.resultPerc).DefaultIfEmpty()
                        .Average(),
                    KPICount = empCom.KPIs.Count,
                    KPIPercent = empCom.KPIs.Sum(xw => xw.resultPercOf5).Value,
                    KPIFinal = (empCom.KPIs.Count == 0
                        ? 0
                        : Math.Round(empCom.KPIs.Sum(xw => xw.resultPercOf5 ?? 0) / empCom.KPIs.Count)),
                });
                var result = result2.Select(empCom => new
                {
                    empCom.CompetencyLevelId,
                    empCom.CompetencyId,
                    empCom.CreatedBy,
                    empCom.CreatedDate,
                    empCom.EmployeeAssessmentId,
                    empCom.Id,
                    empCom.ModifiedBy,
                    empCom.ModifiedDate,
                    empCom.name,
                    empCom.ResultAfterRound,
                    empCom.ResultWithoutRound,
                    empCom.Target,
                    empCom.Weight,
                    empCom.project_desc,
                    empCom.CompetencyLevelName,
                    empCom.KPIs,
                    empCom.q1,
                    empCom.q2,
                    empCom.q3,
                    empCom.q4,
                    empCom.Result,
                    //EmployeePerformance = _Context.EmployeePerformanceSegmentCollection.Where(x => empCom.Result < x.percentage_to && empCom.Result > x.percentage_from)
                    EmployeePerformance = _Context.EmployeePerformanceSegmentCollection
                        .Where(x => x.segment == empCom.KPIFinal)
                        .Select(x => new { Result = x.segment, ResultDesc = x.name }).DefaultIfEmpty().FirstOrDefault(),
                    //empObj.KPI
                    QPercent = ((empCom.q1 + empCom.q2 + empCom.q3 + empCom.q4) == 0
                            ? 0
                            : (empCom.q1 + empCom.q2 + empCom.q3 + empCom.q4) / ((empCom.q1 != 0 ? 1 : 0) +
                                (empCom.q2 != 0 ? 1 : 0) +
                                (empCom.q3 != 0 ? 1 : 0) +
                                (empCom.q4 != 0 ? 1 : 0)))
                        .ToString("##0.00")
                });
        
                return Ok(new { Data = result.ToList(), IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                // Build a detailed error message by traversing the exception chain
                var errorDetails = new System.Text.StringBuilder();
                errorDetails.AppendLine("Exception Details:");
        
                Exception currentException = ex;
                while (currentException != null)
                {
                    errorDetails.AppendLine($"Exception Type: {currentException.GetType().FullName}");
                    errorDetails.AppendLine($"Message: {currentException.Message}");
                    errorDetails.AppendLine($"Stack Trace: {currentException.StackTrace}");
                    errorDetails.AppendLine("--------------------------------------------------");
                    currentException = currentException.InnerException;
                }
        
                return Ok(new
                {
                    Data = errorDetails.ToString(),
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetenciesFinalResult(
            int employeeAssessmentId, string languageCode)
        {
            try
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

                var sum2 = Math.Round(sum);
                var sumComFinalResultValue = _Context.EmployeePerformanceSegmentCollection.Where(x => x.segment == sum2)
                    .Select(x => x.name).DefaultIfEmpty().FirstOrDefault();
                sumComFinalResultValue = sumComFinalResultValue == null && sum > 0
                    ? (from test in _Context.EmployeePerformanceSegmentCollection.ToList()
                            .OrderByDescending(x => x.segment)
                        select test.name).FirstOrDefault()
                    : sumComFinalResultValue;
                var lis = new Dictionary<string, object>();
                lis.Add("sumComFinalResult", sum.ToString("#,##0.00"));
                lis.Add("sumComFinalResultValue", sumComFinalResultValue);
                lis.Add("data", result);
                return Ok(new { Data = lis, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IHttpActionResult SaveCompetancyKpiResults(long companyId, string username, int empComID)
        {
            try
            {
                string ModifiedBy = username;
                var details =
                    JsonConvert.DeserializeObject<dynamic[]>(
                        HttpContext.Current.Request.Form["competency_kpi_results"]);
                EmployeeCompetencyKpiAssessmentController employeeCompetencyKpiAssessmentController =
                    new EmployeeCompetencyKpiAssessmentController();

                foreach (var item in details)
                {
                    long id1 = item.Q1_ID;
                    float? result1 = item.Q1_A;
                    string compKPINote = item.Notes;
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

                    long id3 = item.Q3_ID;
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

                var CompKpiList = _Context.EmployeeCompetencyKpiCollection
                    .Where(x => x.EmployeeCompetencyId == empComID).ToList();

                foreach (var itm in CompKpiList)
                {
                    var totalKpisAssResult = _Context.EmployeeCompetencyKpiAssessmentCollection
                        .Where(x => x.EmployeeCompetencyKpiId == itm.Id).Sum(x => x.Result);
                    var totalKpisAssTarget = _Context.EmployeeCompetencyKpiAssessmentCollection
                        .Where(x => x.EmployeeCompetencyKpiId == itm.Id).Sum(x => x.Target);

                    EmployeeCompetencyKpiEntity empCompKpi_Obj = _Context.EmployeeCompetencyKpiCollection
                        .Where(x => x.Id == itm.Id).FirstOrDefault();

                    if (empCompKpi_Obj != null)
                    {
                        empCompKpi_Obj.Result = totalKpisAssResult;

                        _Context.Entry(empCompKpi_Obj).State = System.Data.Entity.EntityState.Modified;
                        _Context.SaveChanges();
                    }
                }

                double tatolKpiResults = _Context.EmployeeCompetencyKpiCollection
                    .Where(x => x.EmployeeCompetencyId == empComID).Sum(x => x.Result) ?? 0;
                double totalKpiTarget = Convert.ToDouble(_Context.EmployeeCompetencyKpiCollection
                    .Where(x => x.EmployeeCompetencyId == empComID).Sum(x => x.Target));

                double ObjResult = (totalKpiTarget == 0 ? 0 : (double)(tatolKpiResults / totalKpiTarget) * 100);


                EmployeeCompetencyEntity empComp = _Context.EmployeeCompetencyCollection.Where(x => x.Id == empComID)
                    .FirstOrDefault();
                if (empComp != null)
                {
                    empComp.ResultAfterRound = (float)ObjResult;
                    empComp.ResultWithoutRound = (float)ObjResult;


                    _Context.Entry(empComp).State = System.Data.Entity.EntityState.Modified;
                    _Context.SaveChanges();
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
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetenciesByEmployee(
            int employeeId, int companyId, int yearId, string LanguageCode)
        {
            try
            {
                EmployeeAssesmentEntity employeeAssesmentEntity = _Context.EmployeeAssesmentCollection
                    .Where(x => x.employee_id == employeeId && x.year_id == yearId).FirstOrDefault();
                if (employeeAssesmentEntity != null)
                {
                    var employeeCompetencies =
                        (from x in _Context.EmployeeCompetencyCollection
                            join y in _Context.CompetenceCollection on x.CompetencyId equals y.ID
                            join z in _Context.CodesCollection.Where(s => s.MAJOR_NO == 1) on y.c_nature_id equals z
                                .MINOR_NO
                            where x.EmployeeAssessmentId == employeeAssesmentEntity.ID
                            select new
                            {
                                x.CompetencyId,
                                x.CompetencyLevelId,
                                x.CreatedBy,
                                x.CreatedDate,
                                x.EmployeeAssessmentId,
                                x.Id,
                                x.ModifiedBy,
                                x.ModifiedDate,
                                x.ResultAfterRound,
                                x.ResultWithoutRound,
                                x.Target,
                                x.Weight,
                                CompetencyName = (LanguageCode == "en" ? y.NAME : y.name2),
                                CompetenceNature = (LanguageCode == "en" ? z.NAME : z.name2),
                                x.project_desc
                            }).AsEnumerable().ToList();

                    var result = (from d in employeeCompetencies select d).AsEnumerable().Select((d, index) => new
                    {
                        d.CompetencyId,
                        d.CompetencyLevelId,
                        d.CreatedBy,
                        d.CreatedDate,
                        d.EmployeeAssessmentId,
                        d.Id,
                        d.ModifiedBy,
                        d.ModifiedDate,
                        d.ResultAfterRound,
                        d.ResultWithoutRound,
                        d.Target,
                        d.Weight,
                        d.CompetencyName,
                        CompetencyLevelName = getCompetencyLevelName(d.CompetencyLevelId, LanguageCode),
                        d.CompetenceNature,
                        d.project_desc,
                        code = index + 1
                    }).ToList();


                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
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

        private string getCompetencyLevelName(long? competencyLevelId, string languageCode)
        {
            string name = "";
            if (competencyLevelId != null && competencyLevelId != 0)
            {
                CodeEntity code = _Context.CodesCollection
                    .Where(x => x.MAJOR_NO == 7 && x.MINOR_NO == competencyLevelId).FirstOrDefault();

                if (code != null)
                {
                    name = languageCode == "en" ? code.NAME : code.name2;
                }
            }

            return name;
        }


        [HttpGet]
        public IHttpActionResult Save(
            int competencyId,
            int competencyLevelId,
            float weight,
            float? resultWithoutRound,
            float? resultAfterRound,
            int employeeAssessmentId,
            string createdBy,
            int kPICycle,
            int competenceLevelId
        )
        {
            try
            {
                var employeeCompetenciesCount = _Context.EmployeeCompetencyCollection.Where(x =>
                    x.EmployeeAssessmentId == employeeAssessmentId && x.CompetencyId == competencyId).Count();
                if (employeeCompetenciesCount > 0)
                {
                    return Ok(new
                        { Data = string.Empty, IsError = true, ErrorMessage = "This competency is selected" });
                }

                var Weights = (from obj in _Context.EmployeeCompetencyCollection
                        where
                            (obj.CompetencyId != competencyId) &&
                            //(obj.CompetencyLevelId== competenceLevelId)&&
                            obj.EmployeeAssessmentId == employeeAssessmentId
                        select obj.Weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    EmployeeCompetencyEntity employeeCompetency = new EmployeeCompetencyEntity();

                    employeeCompetency.CompetencyId = competencyId;
                    employeeCompetency.CreatedBy = createdBy;
                    employeeCompetency.CreatedDate = DateTime.Now;
                    employeeCompetency.CompetencyLevelId = competencyLevelId;
                    employeeCompetency.Weight = weight;
                    employeeCompetency.ResultAfterRound = resultAfterRound;
                    employeeCompetency.ResultWithoutRound = resultWithoutRound;
                    employeeCompetency.EmployeeAssessmentId = employeeAssessmentId;
                    employeeCompetency.CompetencyLevelId = competenceLevelId;
                    employeeCompetency.Target = 0;


                    _Context.Entry(employeeCompetency).State = System.Data.Entity.EntityState.Added;
                    _Context.EmployeeCompetencyCollection.Add(employeeCompetency);
                    _Context.SaveChanges();


                    List<CompetenciesKpiEntity> competencyKpis = _Context.CompetenciesKpiCollection
                        .Where(x => x.competence_id == competencyId && x.c_kpi_type_id == competenceLevelId).ToList();


                    if (competencyKpis != null && competencyKpis.Count > 0)
                    {
                        foreach (CompetenciesKpiEntity competenciesKpi in competencyKpis)
                        {
                            EmployeeCompetencyKpiController d = new EmployeeCompetencyKpiController();
                            d.Save(employeeCompetency.Id, competenciesKpi.ID, createdBy, kPICycle);
                        }
                    }

                    EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                        .Where(x => x.ID == employeeAssessmentId).FirstOrDefault();

                    SaveAssesments(employeeCompetency, kPICycle, employeeAssesment);
                    scope.Complete();
                }


                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        private void SaveAssesments(EmployeeCompetencyEntity oEmployeeCompetency, int kPICycle,
            EmployeeAssesmentEntity employeeAssesment)
        {
            if (employeeAssesment.isQuarter1)
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment1 =
                    new EmployeeCompetencyAssessmentEntity();

                employeeCompetencyAssessment1.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q1;
                employeeCompetencyAssessment1.CreatedBy = oEmployeeCompetency.CreatedBy;
                employeeCompetencyAssessment1.CreatedDate = DateTime.Now;
                employeeCompetencyAssessment1.EmployeeCompetencyId = (int)oEmployeeCompetency.Id;
                employeeCompetencyAssessment1.ResultBeforeRound = 0;
                employeeCompetencyAssessment1.ResultAfterRound = 0;
                employeeCompetencyAssessment1.Target = 5;
                employeeCompetencyAssessment1.WeightResultWithoutRound = 0;
                employeeCompetencyAssessment1.WeightResultAfterRound = 0;

                _Context.Entry(employeeCompetencyAssessment1).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyAssessmentCollection.Add(employeeCompetencyAssessment1);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter2)
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment2 =
                    new EmployeeCompetencyAssessmentEntity();

                employeeCompetencyAssessment2.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q2;
                employeeCompetencyAssessment2.CreatedBy = oEmployeeCompetency.CreatedBy;
                employeeCompetencyAssessment2.CreatedDate = DateTime.Now;
                employeeCompetencyAssessment2.EmployeeCompetencyId = (int)oEmployeeCompetency.Id;
                employeeCompetencyAssessment2.ResultBeforeRound = 0;
                employeeCompetencyAssessment2.ResultAfterRound = 0;
                employeeCompetencyAssessment2.Target = 5;
                employeeCompetencyAssessment2.WeightResultWithoutRound = 0;
                employeeCompetencyAssessment2.WeightResultAfterRound = 0;

                _Context.Entry(employeeCompetencyAssessment2).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyAssessmentCollection.Add(employeeCompetencyAssessment2);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter3)
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment3 =
                    new EmployeeCompetencyAssessmentEntity();

                employeeCompetencyAssessment3.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q3;
                employeeCompetencyAssessment3.CreatedBy = oEmployeeCompetency.CreatedBy;
                employeeCompetencyAssessment3.CreatedDate = DateTime.Now;
                employeeCompetencyAssessment3.EmployeeCompetencyId = (int)oEmployeeCompetency.Id;
                employeeCompetencyAssessment3.ResultBeforeRound = 0;
                employeeCompetencyAssessment3.ResultAfterRound = 0;
                employeeCompetencyAssessment3.Target = 5;
                employeeCompetencyAssessment3.WeightResultWithoutRound = 0;
                employeeCompetencyAssessment3.WeightResultAfterRound = 0;

                _Context.Entry(employeeCompetencyAssessment3).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyAssessmentCollection.Add(employeeCompetencyAssessment3);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter4)
            {
                EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment4 =
                    new EmployeeCompetencyAssessmentEntity();

                employeeCompetencyAssessment4.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q4;
                employeeCompetencyAssessment4.CreatedBy = oEmployeeCompetency.CreatedBy;
                employeeCompetencyAssessment4.CreatedDate = DateTime.Now;
                employeeCompetencyAssessment4.EmployeeCompetencyId = (int)oEmployeeCompetency.Id;
                employeeCompetencyAssessment4.ResultBeforeRound = 0;
                employeeCompetencyAssessment4.ResultAfterRound = 0;
                employeeCompetencyAssessment4.Target = 5;
                employeeCompetencyAssessment4.WeightResultWithoutRound = 0;
                employeeCompetencyAssessment4.WeightResultAfterRound = 0;

                _Context.Entry(employeeCompetencyAssessment4).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyAssessmentCollection.Add(employeeCompetencyAssessment4);
                _Context.SaveChanges();
            }
        }

        [HttpGet]
        public IHttpActionResult Update(
            int id,
            int competencyId,
            int competencyLevelId,
            float weight,
            float? resultWithoutRound,
            float? resultAfterRound,
            int employeeAssessmentId,
            string modifiedBy
        )
        {
            try
            {
                var Weights = (from obj in _Context.EmployeeCompetencyCollection
                        where
                            (obj.CompetencyId == competencyId) &&
                            (obj.EmployeeAssessmentId == (employeeAssessmentId > 0
                                ? employeeAssessmentId
                                : obj.EmployeeAssessmentId)) &&
                            (obj.Id != id)
                        select obj.Weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    EmployeeCompetencyEntity employeeCompetency =
                        _Context.EmployeeCompetencyCollection.Where(x => x.Id == id).FirstOrDefault();
                    if (employeeCompetency != null)
                    {
                        employeeCompetency.ModifiedBy = modifiedBy;
                        employeeCompetency.ModifiedDate = DateTime.Now;
                        employeeCompetency.CompetencyId = competencyId;
                        employeeCompetency.CompetencyLevelId = competencyLevelId;
                        employeeCompetency.Weight = weight;
                        employeeCompetency.ResultAfterRound = resultAfterRound;
                        employeeCompetency.ResultWithoutRound = resultWithoutRound;
                        employeeCompetency.Target = 0;
                        _Context.Entry(employeeCompetency).State = System.Data.Entity.EntityState.Modified;


                        List<EmployeeCompetencyAssessmentEntity> employeeCompetencyAssessments =
                            _Context.EmployeeCompetencyAssessmentCollection.Where(x => x.EmployeeCompetencyId == id)
                                .ToList();

                        EmployeeCompetencyAssessmentController employeeCompetencyAssessmentController =
                            new EmployeeCompetencyAssessmentController();

                        //foreach (EmployeeCompetencyAssessmentEntity employeeCompetencyAssessmentEntity in employeeCompetencyAssessments)
                        //{
                        //    employeeCompetencyAssessmentEntity.Target = 0;
                        //    employeeCompetencyAssessmentController.Update(employeeCompetencyAssessmentEntity);
                        //}


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
        public IHttpActionResult Delete(long ID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var employeeCompetency =
                        _Context.EmployeeCompetencyCollection.Where(x => x.Id == ID).FirstOrDefault();
                    if (employeeCompetency != null)
                    {
                        List<long> kpiIds = _Context.EmployeeCompetencyKpiCollection
                            .Where(x => x.EmployeeCompetencyId == ID).Select(x => x.Id).ToList<long>();

                        EmployeeCompetencyKpiController employeeCompetencyKpiController =
                            new EmployeeCompetencyKpiController();

                        foreach (long id in kpiIds)
                        {
                            employeeCompetencyKpiController.DeleteAction(id);
                        }


                        List<long> compAssIds = _Context.EmployeeCompetencyAssessmentCollection
                            .Where(x => x.EmployeeCompetencyId == ID).Select(x => x.Id).ToList<long>();

                        EmployeeCompetencyAssessmentController employeeCompetencyAssessmentController =
                            new EmployeeCompetencyAssessmentController();

                        foreach (long compAssId in compAssIds)
                        {
                            employeeCompetencyAssessmentController.Delete(compAssId);
                        }

                        _Context.EmployeeCompetencyCollection.Remove(employeeCompetency);
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
        public IHttpActionResult getByID(long ID)
        {
            try
            {
                //var employeeCompetency = _Context.EmployeeCompetencyCollection.Where(x => x.Id == ID).FirstOrDefault();

                var employeeCompetencyList = (
                    from empCom in _Context.EmployeeCompetencyCollection.Where(x => x.Id == ID)
                    join y in _Context.CompetenceCollection on empCom.CompetencyId equals y.ID
                    select new
                    {
                        empCom.CompetencyId,
                        empCom.CompetencyLevelId,
                        empCom.CreatedBy,
                        empCom.CreatedDate,
                        empCom.EmployeeAssessmentId,
                        empCom.Id,
                        empCom.ModifiedBy,
                        empCom.ModifiedDate,
                        empCom.ResultAfterRound,
                        empCom.ResultWithoutRound,
                        empCom.Target,
                        empCom.Weight,
                        CompetenceNature = y.c_nature_id,
                    }
                ).ToList();


                var employeeCompetency = (from y in employeeCompetencyList
                    select new
                    {
                        y.CompetenceNature,
                        y.CompetencyId,
                        y.CompetencyLevelId,
                        y.CreatedBy,
                        y.CreatedDate,
                        y.EmployeeAssessmentId,
                        y.Id,
                        y.ModifiedBy,
                        y.ModifiedDate,
                        y.ResultAfterRound,
                        y.ResultWithoutRound,
                        y.Target,
                        y.Weight
                    }).FirstOrDefault();


                if (employeeCompetency != null)
                {
                    return Ok(new { Data = employeeCompetency, IsError = false, ErrorMessage = string.Empty });
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