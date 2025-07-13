using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities;
using Newtonsoft.Json;
using System.Transactions;
using HR.Entities.Admin;

namespace HR.APIs.Controllers
{
    public class EmployeeCompetencyKpiController : ApiController
    {
        private HRContext _Context;

        public EmployeeCompetencyKpiController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeCompetencyKpis(
            int employeeCompetencyId)
        {
            try
            {
                var employeeCompetencyKpis =
                    (from x in _Context.EmployeeCompetencyKpiCollection
                        join y in _Context.CompetenciesKpiCollection on x.EmployeeCompetencyKpiId equals y.ID
                        where x.EmployeeCompetencyId == employeeCompetencyId
                        select new
                        {
                            x.Id,
                            x.EmployeeCompetencyKpiId,
                            x.EmployeeCompetencyId,
                            x.CreatedDate,
                            x.CreatedBy,
                            CompetencyKpiName = y.NAME,
                            Target = x.Target
                        }).ToList();

                var list = (from m in employeeCompetencyKpis select m).AsEnumerable()
                        .Select((kpi, index) => new
                        {
                            kpi.CompetencyKpiName,
                            kpi.CreatedBy,
                            kpi.CreatedDate,
                            kpi.EmployeeCompetencyId,
                            kpi.EmployeeCompetencyKpiId,
                            kpi.Id,
                            kpi.Target,
                            code = index + 1
                        }).ToList()
                    ;

                return Ok(new { Data = list, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            long employeeCompetencyId,
            long employeeCompetencyKpiId,
            string createdBy,
            int kPICycle
        )
        {
            try
            {
                EmployeeCompetencyKpiEntity employeeCompetencyKpi = new EmployeeCompetencyKpiEntity();

                employeeCompetencyKpi.EmployeeCompetencyId = employeeCompetencyId;
                employeeCompetencyKpi.EmployeeCompetencyKpiId = employeeCompetencyKpiId;
                employeeCompetencyKpi.CreatedBy = createdBy;
                employeeCompetencyKpi.CreatedDate = DateTime.Now;
                employeeCompetencyKpi.Target = 0;


                _Context.Entry(employeeCompetencyKpi).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiCollection.Add(employeeCompetencyKpi);
                _Context.SaveChanges();


                EmployeeCompetencyEntity employeeCompetency = _Context.EmployeeCompetencyCollection
                    .Where(x => x.Id == employeeCompetencyId).FirstOrDefault();
                if (employeeCompetency != null)
                {
                    EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                        .Where(x => x.ID == employeeCompetency.EmployeeAssessmentId).FirstOrDefault();

                    SaveAssesments(employeeCompetencyKpi, kPICycle, employeeAssesment);
                }

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        public void SaveAssesments(EmployeeCompetencyKpiEntity oEmployeeCompetencyKPI, int kPICycle,
            EmployeeAssesmentEntity employeeAssesment)
        {
            if (employeeAssesment.isQuarter1)
            {
                EmployeeCompetencyKpiAssessmentEntity o1 = new EmployeeCompetencyKpiAssessmentEntity();
                o1.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q1;
                o1.CreatedBy = oEmployeeCompetencyKPI.CreatedBy;
                o1.CreatedDate = DateTime.Now;
                o1.EmployeeCompetencyKpiId = (int)oEmployeeCompetencyKPI.Id;
                o1.Result = 0;
                o1.Target = 5;
                _Context.Entry(o1).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiAssessmentCollection.Add(o1);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter2)
            {
                EmployeeCompetencyKpiAssessmentEntity o2 = new EmployeeCompetencyKpiAssessmentEntity();
                o2.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q2;
                o2.CreatedBy = oEmployeeCompetencyKPI.CreatedBy;
                o2.CreatedDate = DateTime.Now;
                o2.EmployeeCompetencyKpiId = (int)oEmployeeCompetencyKPI.Id;
                o2.Result = 0;
                o2.Target = 5;
                _Context.Entry(o2).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiAssessmentCollection.Add(o2);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter3)
            {
                EmployeeCompetencyKpiAssessmentEntity o3 = new EmployeeCompetencyKpiAssessmentEntity();
                o3.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q3;
                o3.CreatedBy = oEmployeeCompetencyKPI.CreatedBy;
                o3.CreatedDate = DateTime.Now;
                o3.EmployeeCompetencyKpiId = (int)oEmployeeCompetencyKPI.Id;
                o3.Result = 0;
                o3.Target = 5;
                _Context.Entry(o3).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiAssessmentCollection.Add(o3);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter4)
            {
                EmployeeCompetencyKpiAssessmentEntity o4 = new EmployeeCompetencyKpiAssessmentEntity();
                o4.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q4;
                o4.CreatedBy = oEmployeeCompetencyKPI.CreatedBy;
                o4.CreatedDate = DateTime.Now;
                o4.EmployeeCompetencyKpiId = (int)oEmployeeCompetencyKPI.Id;
                o4.Result = 0;
                o4.Target = 5;
                _Context.Entry(o4).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeCompetencyKpiAssessmentCollection.Add(o4);
                _Context.SaveChanges();
            }
        }

        [HttpGet]
        public IHttpActionResult Update(
            int id,
            int employeeCompetencyId,
            int employeeCompetencyKpiId,
            float Target
        )
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    EmployeeCompetencyKpiEntity employeeCompetency =
                        _Context.EmployeeCompetencyKpiCollection.Where(x => x.Id == id).FirstOrDefault();
                    if (employeeCompetency != null)
                    {
                        employeeCompetency.EmployeeCompetencyId = employeeCompetencyId;
                        employeeCompetency.EmployeeCompetencyKpiId = employeeCompetencyKpiId;
                        employeeCompetency.Target = Target;


                        List<EmployeeCompetencyKpiAssessmentEntity> employeeCompetencyKpiAssessments =
                            _Context.EmployeeCompetencyKpiAssessmentCollection
                                .Where(x => x.EmployeeCompetencyKpiId == id).ToList();

                        EmployeeCompetencyKpiAssessmentController employeeCompetencyKpiAssessmentController =
                            new EmployeeCompetencyKpiAssessmentController();

                        foreach (EmployeeCompetencyKpiAssessmentEntity employeeCompetencyKpiAssessment in
                                 employeeCompetencyKpiAssessments)
                        {
                            employeeCompetencyKpiAssessment.Target = Target;
                            employeeCompetencyKpiAssessmentController.Update(employeeCompetencyKpiAssessment);
                        }

                        _Context.Entry(employeeCompetency).State = System.Data.Entity.EntityState.Modified;

                        _Context.SaveChanges();

                        scope.Complete();
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }

                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        public IHttpActionResult DeleteAction(long id)
        {
            try
            {
                var employeeCompetencyKpi =
                    _Context.EmployeeCompetencyKpiCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpi != null)
                {
                    List<long> kpiAssIds = _Context.EmployeeCompetencyKpiAssessmentCollection
                        .Where(x => x.EmployeeCompetencyKpiId == id).Select(x => x.Id).ToList<long>();

                    EmployeeCompetencyKpiAssessmentController employeeCompetencyKpiAssessmentController =
                        new EmployeeCompetencyKpiAssessmentController();

                    foreach (long kpiAssId in kpiAssIds)
                    {
                        employeeCompetencyKpiAssessmentController.Delete(kpiAssId);
                    }

                    _Context.EmployeeCompetencyKpiCollection.Remove(employeeCompetencyKpi);
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
                var employeeCompetencyKpi =
                    _Context.EmployeeCompetencyKpiCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpi != null)
                {
                    List<long> kpiAssIds = _Context.EmployeeCompetencyKpiAssessmentCollection
                        .Where(x => x.EmployeeCompetencyKpiId == id).Select(x => x.Id).ToList<long>();

                    EmployeeCompetencyKpiAssessmentController employeeCompetencyKpiAssessmentController =
                        new EmployeeCompetencyKpiAssessmentController();

                    foreach (long kpiAssId in kpiAssIds)
                    {
                        employeeCompetencyKpiAssessmentController.Delete(kpiAssId);
                    }

                    _Context.EmployeeCompetencyKpiCollection.Remove(employeeCompetencyKpi);
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
                var employeeCompetencyKpi =
                    _Context.EmployeeCompetencyKpiCollection.Where(x => x.Id == id).FirstOrDefault();
                if (employeeCompetencyKpi != null)
                {
                    return Ok(new { Data = employeeCompetencyKpi, IsError = false, ErrorMessage = string.Empty });
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