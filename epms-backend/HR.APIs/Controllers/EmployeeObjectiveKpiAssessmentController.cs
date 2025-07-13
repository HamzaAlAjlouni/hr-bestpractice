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
    public class EmployeeObjectiveKpiAssessmentController : ApiController
    {
        private HRContext _Context;

        public EmployeeObjectiveKpiAssessmentController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectiveKpiAssessments(
            int employeeObjectiveKpiId)
        {
            try
            {
                List<EmployeeObjectiveKPIAssessmentEntity> employeeObjectiveKPIAssessments =
                    (from employeeObjectiveKPIAssessment in _Context.EmployeeObjectiveKPIAssessmentCollection
                        where employeeObjectiveKPIAssessment.EmployeeObjectiveKpiId == employeeObjectiveKpiId
                        select employeeObjectiveKPIAssessment).ToList();

                return Ok(new { Data = employeeObjectiveKPIAssessments, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int periodNo,
            int employeeObjectiveKpiId,
            float? result,
            float target,
            string createdBy
        )
        {
            try
            {
                EmployeeObjectiveKPIAssessmentEntity employeeObjectiveKPIAssessment =
                    new EmployeeObjectiveKPIAssessmentEntity();

                employeeObjectiveKPIAssessment.PeriodNo = periodNo;
                employeeObjectiveKPIAssessment.CreatedBy = createdBy;
                employeeObjectiveKPIAssessment.CreatedDate = DateTime.Now;
                employeeObjectiveKPIAssessment.EmployeeObjectiveKpiId = employeeObjectiveKpiId;
                employeeObjectiveKPIAssessment.Result = result;

                employeeObjectiveKPIAssessment.Target = target;


                _Context.Entry(employeeObjectiveKPIAssessment).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPIAssessmentCollection.Add(employeeObjectiveKPIAssessment);
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
            int employeeObjectiveKpiId,
            float? result,
            float target,
            string modifiedBy
        )
        {
            try
            {
                EmployeeObjectiveKPIAssessmentEntity employeeObjectiveKPIAssessment =
                    _Context.EmployeeObjectiveKPIAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeObjectiveKPIAssessment != null)
                {
                    employeeObjectiveKPIAssessment.ModifiedBy = modifiedBy;
                    employeeObjectiveKPIAssessment.ModifiedDate = DateTime.Now;
                    employeeObjectiveKPIAssessment.PeriodNo = periodNo;
                    employeeObjectiveKPIAssessment.Result = result;

                    employeeObjectiveKPIAssessment.Target = target;


                    _Context.Entry(employeeObjectiveKPIAssessment).State = System.Data.Entity.EntityState.Modified;

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
            long companyId,
            string ObjKPINote
        )
        {
            try
            {
                EmployeeObjectiveKPIAssessmentEntity employeeObjectiveKPIAssessment =
                    _Context.EmployeeObjectiveKPIAssessmentCollection.Where(x => x.Id == id).FirstOrDefault();

                int periodNo = employeeObjectiveKPIAssessment.PeriodNo;

                EmployeeObjectiveAssessmentEntity employeeObjectiveAssessment = null;

                if (employeeObjectiveKPIAssessment != null)
                {
                    EmployeeObjectiveKPIEntity empObjKPI = _Context.EmployeeObjectiveKPICollection
                        .Where(x => x.ID == employeeObjectiveKPIAssessment.EmployeeObjectiveKpiId).FirstOrDefault();

                    EmployeeObjectiveEntity empObj = _Context.EmployeeObjectiveCollection
                        .Where(x => x.ID == empObjKPI.emp_obj_id).FirstOrDefault();

                    EmployeeAssesmentEntity empAss = _Context.EmployeeAssesmentCollection
                        .Where(x => x.ID == empObj.emp_assesment_id).FirstOrDefault();


                    employeeObjectiveKPIAssessment.ModifiedBy = modifiedBy;
                    employeeObjectiveKPIAssessment.ModifiedDate = DateTime.Now;
                    employeeObjectiveKPIAssessment.PeriodNo = periodNo;
                    employeeObjectiveKPIAssessment.Result = result;

                    _Context.Entry(employeeObjectiveKPIAssessment).State = System.Data.Entity.EntityState.Modified;


                    if (result > 0)
                    {
                        EmployeeObjectiveKPIEntity employeeObjectiveKPI = _Context.EmployeeObjectiveKPICollection
                            .Where(x => x.ID == employeeObjectiveKPIAssessment.EmployeeObjectiveKpiId).FirstOrDefault();


                        List<EmployeeObjectiveKPIAssessmentEntity> employeeObjKPIAss =
                            _Context.EmployeeObjectiveKPIAssessmentCollection.Where(x => x.Id == id).ToList();

                        if (employeeObjKPIAss.Where(x => x.Result > 0).Count() == employeeObjKPIAss.Count)
                        {
                            if (empObjKPI != null)
                            {
                                empObjKPI.Result = (float)employeeObjKPIAss.Average(x => (x.Result * 100 / x.Target));
                                empObjKPI.Note = ObjKPINote;
                                int roundedResult = (int)Math.Round((double)empObjKPI.Result, 0);

                                List<EmployeePerformanceSegmentEntity> perfSegments = _Context
                                    .EmployeePerformanceSegmentCollection.Where(x => x.year == empAss.year_id).ToList();

                                EmployeePerformanceSegmentEntity seg = perfSegments.Where(x =>
                                        roundedResult >= x.percentage_from && roundedResult <= x.percentage_to)
                                    .FirstOrDefault();

                                if (seg != null)
                                {
                                    empObjKPI.SegmentId = seg.id;
                                    empObjKPI.Result = seg.segment;
                                    _Context.Entry(empObjKPI).State = System.Data.Entity.EntityState.Modified;
                                    _Context.SaveChanges();
                                }
                            }
                        }

                        if (employeeObjectiveKPI != null)
                        {
                            employeeObjectiveAssessment = _Context.EmployeeObjectiveAssessmentCollection.Where(x =>
                                    x.EmployeeObjectiveId == employeeObjectiveKPI.emp_obj_id && x.PeriodNo == periodNo)
                                .FirstOrDefault();

                            List<EmployeeObjectiveKPIEntity> employeeObjectiveKPIs = _Context
                                .EmployeeObjectiveKPICollection
                                .Where(x => x.emp_obj_id == employeeObjectiveKPI.emp_obj_id).ToList();

                            EmployeeObjectiveEntity employeeObjective = _Context.EmployeeObjectiveCollection
                                .Where(x => x.ID == employeeObjectiveKPI.emp_obj_id).FirstOrDefault();

                            if (employeeObjectiveAssessment != null && employeeObjective != null &&
                                employeeObjectiveKPIs != null && employeeObjectiveKPIs.Count > 0)
                            {
                                List<long> employeeObjectiveKPIIds =
                                    employeeObjectiveKPIs.Select(x => x.ID).ToList<long>();

                                List<EmployeeObjectiveKPIAssessmentEntity> employeeObjectiveKPIAssessments =
                                    _Context.EmployeeObjectiveKPIAssessmentCollection.Where(x =>
                                        employeeObjectiveKPIIds.Contains(x.EmployeeObjectiveKpiId) &&
                                        x.PeriodNo == periodNo).ToList();

                                if (employeeObjectiveKPIAssessments != null &&
                                    employeeObjectiveKPIAssessments.Count() > 0)
                                {
                                    if (employeeObjectiveKPIAssessments.Where(x => x.Result > 0).ToList().Count() > 0)
                                    {
                                        float average = 0;
                                        float totalActualResult = 0;
                                        float totalPlannedResult = 0;
                                        foreach (EmployeeObjectiveKPIAssessmentEntity empObjKpi in
                                                 employeeObjectiveKPIAssessments)
                                        {
                                            totalActualResult = totalActualResult + (float)empObjKpi.Result;
                                            totalPlannedResult = totalPlannedResult + (float)empObjKpi.Target;
                                        }

                                        if (totalPlannedResult > 0)
                                        {
                                            average = totalActualResult / totalPlannedResult * 100;
                                        }

                                        //(float)employeeObjectiveKPIAssessments.Average(x => x.Result);
                                        employeeObjectiveAssessment.ResultBeforeRound = average;
                                        employeeObjectiveAssessment.ResultAfterRound =
                                            (float)Math.Round(average, 0, MidpointRounding.AwayFromZero);
                                        if (employeeObjective.weight > 0)
                                        {
                                            employeeObjectiveAssessment.WeightResultWithoutRound =
                                                (float)(average * employeeObjective.weight / 100);
                                            employeeObjectiveAssessment.WeightResultWithoutRound =
                                                (float)(Math.Round(average, 0, MidpointRounding.AwayFromZero) *
                                                    employeeObjective.weight / 100);
                                        }

                                        _Context.Entry(employeeObjectiveAssessment).State =
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
                var employeeObjectiveKPIAssessment = _Context.EmployeeObjectiveKPIAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeObjectiveKPIAssessment != null)
                {
                    _Context.EmployeeObjectiveKPIAssessmentCollection.Remove(employeeObjectiveKPIAssessment);
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
                var employeeObjectiveKPIAssessment = _Context.EmployeeObjectiveKPIAssessmentCollection
                    .Where(x => x.Id == id).FirstOrDefault();
                if (employeeObjectiveKPIAssessment != null)
                {
                    return Ok(new
                        { Data = employeeObjectiveKPIAssessment, IsError = false, ErrorMessage = string.Empty });
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