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

namespace HR.APIs.Controllers
{
    public class EmployeeCompetencyKpiAssessmentController : ApiController
    {
        private HRContext _Context;

        public EmployeeCompetencyKpiAssessmentController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetencyKpiAssessments(
            int employeeCompetencyKpiId)
        {
            try
            {
                List<EmployeeCompetencyKpiAssessmentEntity> employeeCompetencyKpiAssessments =
                    (from employeeCompetencyKpiAssessment in _Context.EmployeeCompetencyKpiAssessmentCollection
                        where employeeCompetencyKpiAssessment.EmployeeCompetencyKpiId == employeeCompetencyKpiId
                        select employeeCompetencyKpiAssessment).ToList();

                return Ok(new
                    { Data = employeeCompetencyKpiAssessments, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int periodNo,
            int employeeCompetencyKpiId,
            float? result,
            float target,
            string createdBy
        )
        {
            try
            {
                EmployeeCompetencyKpiAssessmentEntity employeeCompetencyKpiAssessment =
                    new EmployeeCompetencyKpiAssessmentEntity();

                employeeCompetencyKpiAssessment.PeriodNo = periodNo;
                employeeCompetencyKpiAssessment.CreatedBy = createdBy;
                employeeCompetencyKpiAssessment.CreatedDate = DateTime.Now;
                employeeCompetencyKpiAssessment.EmployeeCompetencyKpiId = employeeCompetencyKpiId;
                employeeCompetencyKpiAssessment.Result = result;
                employeeCompetencyKpiAssessment.Target = target;


                _Context.Entry(employeeCompetencyKpiAssessment).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiAssessmentCollection.Add(employeeCompetencyKpiAssessment);
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
            EmployeeCompetencyKpiAssessmentEntity employeeCompetencyKpiAssessment
        )
        {
            try
            {
                if (employeeCompetencyKpiAssessment != null)
                {
                    _Context.Entry(employeeCompetencyKpiAssessment).State = System.Data.Entity.EntityState.Modified;

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
            int id,
            int periodNo,
            int employeeCompetencyKpiId,
            float? result,
            float target,
            string modifiedBy
        )
        {
            try
            {
                EmployeeCompetencyKpiAssessmentEntity employeeCompetencyKpiAssessment =
                    _Context.EmployeeCompetencyKpiAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpiAssessment != null)
                {
                    employeeCompetencyKpiAssessment.ModifiedBy = modifiedBy;
                    employeeCompetencyKpiAssessment.ModifiedDate = DateTime.Now;
                    employeeCompetencyKpiAssessment.PeriodNo = periodNo;
                    employeeCompetencyKpiAssessment.Result = result;
                    employeeCompetencyKpiAssessment.Target = target;


                    _Context.Entry(employeeCompetencyKpiAssessment).State = System.Data.Entity.EntityState.Modified;

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
            float? result,
            string modifiedBy,
            string compKPINote
        )
        {
            try
            {
                EmployeeCompetencyKpiAssessmentEntity employeeCompetencyKPIAssessment =
                    _Context.EmployeeCompetencyKpiAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKPIAssessment != null)
                {
                    EmployeeCompetencyKpiEntity empComKPI = _Context.EmployeeCompetencyKpiCollection
                        .Where(x => x.Id == employeeCompetencyKPIAssessment.EmployeeCompetencyKpiId).FirstOrDefault();

                    EmployeeCompetencyEntity empComp = _Context.EmployeeCompetencyCollection
                        .Where(x => x.Id == empComKPI.EmployeeCompetencyId).FirstOrDefault();

                    EmployeeAssesmentEntity empAss = _Context.EmployeeAssesmentCollection
                        .Where(x => x.ID == empComp.EmployeeAssessmentId).FirstOrDefault();


                    employeeCompetencyKPIAssessment.ModifiedBy = modifiedBy;
                    employeeCompetencyKPIAssessment.ModifiedDate = DateTime.Now;
                    employeeCompetencyKPIAssessment.PeriodNo = employeeCompetencyKPIAssessment.PeriodNo;
                    employeeCompetencyKPIAssessment.Result = result;

                    _Context.Entry(employeeCompetencyKPIAssessment).State = System.Data.Entity.EntityState.Modified;
                    _Context.SaveChanges();

                    EmployeeCompetencyAssessmentEntity employeeCompetencyAssessment = null;
                    if (result > 0)
                    {
                        List<EmployeeCompetencyKpiAssessmentEntity> employeeCompetencyKPIAss =
                            _Context.EmployeeCompetencyKpiAssessmentCollection.Where(x => x.Id == id).ToList();

                        if (employeeCompetencyKPIAss.Where(x => x.Result > 0).Count() == employeeCompetencyKPIAss.Count)
                        {
                            if (empComKPI != null)
                            {
                                empComKPI.Result = (float)employeeCompetencyKPIAss.Average(x => x.Result);

                                int roundedResult = (int)Math.Round((double)empComKPI.Result, 0);

                                List<EmployeePerformanceSegmentEntity> perfSegments = _Context
                                    .EmployeePerformanceSegmentCollection.Where(x => x.year == empAss.year_id).ToList();

                                EmployeePerformanceSegmentEntity seg =
                                    perfSegments.Where(x => x.segment == roundedResult).FirstOrDefault();

                                if (seg != null)
                                {
                                    empComKPI.SegmentId = seg.id;
                                    empComKPI.Note = compKPINote;
                                    _Context.Entry(empComKPI).State = System.Data.Entity.EntityState.Modified;
                                    _Context.SaveChanges();
                                }
                            }
                        }


                        EmployeeCompetencyKpiEntity employeeCompetencyKPI = _Context.EmployeeCompetencyKpiCollection
                            .Where(x => x.Id == employeeCompetencyKPIAssessment.EmployeeCompetencyKpiId)
                            .FirstOrDefault();


                        if (employeeCompetencyKPI != null)
                        {
                            employeeCompetencyAssessment = _Context.EmployeeCompetencyAssessmentCollection.Where(x =>
                                x.EmployeeCompetencyId == employeeCompetencyKPI.EmployeeCompetencyId &&
                                x.PeriodNo == employeeCompetencyKPIAssessment.PeriodNo).FirstOrDefault();

                            List<EmployeeCompetencyKpiEntity> employeeCompetencyKPIs =
                                _Context.EmployeeCompetencyKpiCollection.Where(x =>
                                    x.EmployeeCompetencyId == employeeCompetencyKPI.EmployeeCompetencyId).ToList();

                            EmployeeCompetencyEntity employeeCompetency = _Context.EmployeeCompetencyCollection
                                .Where(x => x.Id == employeeCompetencyKPI.EmployeeCompetencyId).FirstOrDefault();

                            if (employeeCompetencyAssessment != null && employeeCompetency != null &&
                                employeeCompetencyKPIs != null && employeeCompetencyKPIs.Count > 0)
                            {
                                List<long> employeeCompetencyKPIIds =
                                    employeeCompetencyKPIs.Select(x => x.Id).ToList<long>();

                                List<EmployeeCompetencyKpiAssessmentEntity> employeeCompetencyKPIAssessments =
                                    _Context.EmployeeCompetencyKpiAssessmentCollection.Where(x =>
                                        employeeCompetencyKPIIds.Contains(x.EmployeeCompetencyKpiId) &&
                                        x.PeriodNo == employeeCompetencyKPIAssessment.PeriodNo).ToList();

                                if (employeeCompetencyKPIAssessments != null &&
                                    employeeCompetencyKPIAssessments.Count() > 0)
                                {
                                    if (employeeCompetencyKPIAssessments.Where(x => x.Result > 0).ToList().Count() > 0)
                                    {
                                        // Calculate Employee Competency KPIs average for the give quarter (period)
                                        float average = (float)employeeCompetencyKPIAssessments.Average(x => x.Result);
                                        employeeCompetencyAssessment.ResultBeforeRound = average;
                                        employeeCompetencyAssessment.ResultAfterRound =
                                            (float)Math.Round(average, 0, MidpointRounding.AwayFromZero);
                                        if (employeeCompetency.Weight > 0)
                                        {
                                            employeeCompetencyAssessment.WeightResultWithoutRound =
                                                (float)(average * employeeCompetency.Weight / 100);
                                            employeeCompetencyAssessment.WeightResultWithoutRound =
                                                (float)(Math.Round(average, 0, MidpointRounding.AwayFromZero) *
                                                    employeeCompetency.Weight / 100);
                                        }

                                        _Context.Entry(employeeCompetencyAssessment).State =
                                            System.Data.Entity.EntityState.Modified;
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
                var employeeCompetencyKpiAssessment = _Context.EmployeeCompetencyKpiAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpiAssessment != null)
                {
                    _Context.EmployeeCompetencyKpiAssessmentCollection.Remove(employeeCompetencyKpiAssessment);
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
                var employeeCompetencyKpiAssessment = _Context.EmployeeCompetencyKpiAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpiAssessment != null)
                {
                    return Ok(new
                        { Data = employeeCompetencyKpiAssessment, IsError = false, ErrorMessage = string.Empty });
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