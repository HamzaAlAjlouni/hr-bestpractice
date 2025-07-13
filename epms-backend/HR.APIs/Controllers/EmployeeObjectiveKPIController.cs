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
    public class EmployeeObjectiveKPIController : ApiController
    {
        private HRContext _Context;

        public EmployeeObjectiveKPIController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetEmployeeObjectiveKPI(
            int EmployeeObjectiveID)
        {
            try
            {
                var lstEmployeeObjectiveKPIs = (from employeeObjectivekpi in _Context.EmployeeObjectiveKPICollection
                            join emp_obj in _Context.EmployeeObjectiveCollection on employeeObjectivekpi.emp_obj_id
                                equals emp_obj.ID
                            where employeeObjectivekpi.emp_obj_id == EmployeeObjectiveID
                            select new
                            {
                                employeeObjectivekpi.created_by,
                                employeeObjectivekpi.created_date,
                                employeeObjectivekpi.emp_obj_id,
                                employeeObjectivekpi.ID,
                                employeeObjectivekpi.modified_by,
                                employeeObjectivekpi.modified_date,
                                employeeObjectivekpi.name,
                                employeeObjectivekpi.name2,
                                employeeObjectivekpi.target,
                                employeeObjectivekpi.BetterUpDown,
                                emp_obj.target_type,
                                emp_obj.KPI_type,

                                emp_obj.weight
                            }).ToList().AsQueryable()
                        .Select((kpi, index) => new
                        {
                            kpi.created_by,
                            kpi.created_date,
                            kpi.emp_obj_id,
                            kpi.ID,
                            kpi.modified_by,
                            kpi.modified_date,
                            kpi.name,
                            kpi.name2,
                            kpi.target,
                            kpi.target_type,
                            kpi.weight,
                            kpi.KPI_type,
                            code = index + 1,
                            kpi.BetterUpDown
                        }).ToList()
                    ;

                var d = (from a in lstEmployeeObjectiveKPIs
                    select new
                    {
                        a.KPI_type,
                        a.weight,
                        a.code,
                        a.created_by,
                        a.created_date,
                        a.emp_obj_id,
                        a.ID,
                        a.modified_by,
                        a.modified_date,
                        a.name,
                        a.name2,
                        a.target,
                        a.BetterUpDown,
                        Target1 = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q1.PeriodNo == 1 && q1.EmployeeObjectiveKpiId == a.ID
                            select q1.Target).DefaultIfEmpty().Sum(),
                        Target2 = (from q2 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q2.PeriodNo == 2 && q2.EmployeeObjectiveKpiId == a.ID
                            select q2.Target).DefaultIfEmpty().Sum(),
                        Target3 = (from q3 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q3.PeriodNo == 3 && q3.EmployeeObjectiveKpiId == a.ID
                            select q3.Target).DefaultIfEmpty().Sum(),
                        Target4 = (from q4 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q4.PeriodNo == 4 && q4.EmployeeObjectiveKpiId == a.ID
                            select q4.Target).DefaultIfEmpty().Sum()
                    }).ToList();


                return Ok(new { Data = d, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int EmployeeObjectiveID,
            string CreatedBy,
            string Name,
            decimal Target,
            int kPICycle,
            float Target1,
            float Target2,
            float Target3,
            float Target4,
            float? Weight,
            int target_type,
            int kpi_type,
            string LanguageCode,
            int betterUpDown = 1
            )
        {
            try
            {
                EmployeeObjectiveKPIEntity oEmployeeObjectiveEntity = new EmployeeObjectiveKPIEntity();

                oEmployeeObjectiveEntity.emp_obj_id = EmployeeObjectiveID;
                oEmployeeObjectiveEntity.created_by = CreatedBy;
                oEmployeeObjectiveEntity.created_date = DateTime.Now;
                oEmployeeObjectiveEntity.BetterUpDown = betterUpDown;
                oEmployeeObjectiveEntity.target = Target;
                if (LanguageCode == "en")
                    oEmployeeObjectiveEntity.name = Name;
                else
                    oEmployeeObjectiveEntity.name2 = Name;

                _Context.Entry(oEmployeeObjectiveEntity).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPICollection.Add(oEmployeeObjectiveEntity);
                _Context.SaveChanges();

                EmployeeObjectiveEntity employeeObjective = _Context.EmployeeObjectiveCollection
                    .Where(x => x.ID == EmployeeObjectiveID).FirstOrDefault();
                int selectOper = 0;
                if (employeeObjective == null)
                {
                    selectOper = 0;
                    employeeObjective.ID = EmployeeObjectiveID;
                }
                else
                {
                    selectOper = 1;
                }

                employeeObjective.target_type = target_type;
                employeeObjective.KPI_type = kpi_type;
                if (!Weight.HasValue)
                    employeeObjective.weight = 0;
                //employeeObjective.weight = Weight;
                if (selectOper == 0)
                {
                    _Context.Entry(employeeObjective).State = System.Data.Entity.EntityState.Added;
                    _Context.EmployeeObjectiveCollection.Add(employeeObjective);
                }

                if (selectOper == 1)
                    _Context.Entry(employeeObjective).State = System.Data.Entity.EntityState.Modified;

                _Context.SaveChanges();
                if (employeeObjective != null)
                {
                    EmployeeAssesmentEntity employeeAssesment = _Context.EmployeeAssesmentCollection
                        .Where(x => x.ID == employeeObjective.emp_assesment_id).FirstOrDefault();

                    SaveAssesments(oEmployeeObjectiveEntity, kPICycle, Target1, Target2, Target3, Target4,
                        employeeAssesment);
                }

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        private void SaveAssesments(EmployeeObjectiveKPIEntity oEmployeeObjective, int kPICycle, float Target1,
            float Target2,
            float Target3,
            float Target4, EmployeeAssesmentEntity employeeAssesment)
        {
            if (employeeAssesment.isQuarter1)
            {
                EmployeeObjectiveKPIAssessmentEntity o1 = new EmployeeObjectiveKPIAssessmentEntity();
                o1.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q1;
                o1.CreatedBy = oEmployeeObjective.created_by;
                o1.CreatedDate = DateTime.Now;
                o1.EmployeeObjectiveKpiId = oEmployeeObjective.ID;
                o1.Result = 0;
                o1.Target = Target1;
                _Context.Entry(o1).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPIAssessmentCollection.Add(o1);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter2)
            {
                EmployeeObjectiveKPIAssessmentEntity o2 = new EmployeeObjectiveKPIAssessmentEntity();
                o2.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q2;
                o2.CreatedBy = oEmployeeObjective.created_by;
                o2.CreatedDate = DateTime.Now;
                o2.EmployeeObjectiveKpiId = oEmployeeObjective.ID;
                o2.Result = 0;
                o2.Target = Target2;
                _Context.Entry(o2).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPIAssessmentCollection.Add(o2);

                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter3)
            {
                EmployeeObjectiveKPIAssessmentEntity o3 = new EmployeeObjectiveKPIAssessmentEntity();
                o3.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q3;
                o3.CreatedBy = oEmployeeObjective.created_by;
                o3.CreatedDate = DateTime.Now;
                o3.EmployeeObjectiveKpiId = oEmployeeObjective.ID;
                o3.Result = 0;
                o3.Target = Target3;
                _Context.Entry(o3).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPIAssessmentCollection.Add(o3);
                _Context.SaveChanges();
            }

            if (employeeAssesment.isQuarter4)
            {
                EmployeeObjectiveKPIAssessmentEntity o4 = new EmployeeObjectiveKPIAssessmentEntity();
                o4.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q4;
                o4.CreatedBy = oEmployeeObjective.created_by;
                o4.CreatedDate = DateTime.Now;
                o4.EmployeeObjectiveKpiId = oEmployeeObjective.ID;
                o4.Result = 0;
                o4.Target = Target4;
                _Context.Entry(o4).State = System.Data.Entity.EntityState.Added;
                _Context.EmployeeObjectiveKPIAssessmentCollection.Add(o4);
                _Context.SaveChanges();
            }
        }

        [HttpGet]
        public IHttpActionResult Update(
            int ID,
            string ModifiedBy,
            string Name,
            decimal Target,
            float Weight,
            int target_type,
            int kpi_type,
            string LanguageCode,
            int betterUpDown = 1
            )
        {
            try
            {
                EmployeeObjectiveKPIEntity oEmployeeObjectiveEntity =
                    _Context.EmployeeObjectiveKPICollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oEmployeeObjectiveEntity != null)
                {
                    oEmployeeObjectiveEntity.modified_by = ModifiedBy;
                    oEmployeeObjectiveEntity.modified_date = DateTime.Now;
                    oEmployeeObjectiveEntity.BetterUpDown = betterUpDown;
                    if (LanguageCode == "en")
                        oEmployeeObjectiveEntity.name = Name;
                    else
                        oEmployeeObjectiveEntity.name2 = Name;
                    oEmployeeObjectiveEntity.target = Target;
                    EmployeeObjectiveEntity employeeObjective = _Context.EmployeeObjectiveCollection
                        .Where(x => x.ID == oEmployeeObjectiveEntity.emp_obj_id).FirstOrDefault();
                    employeeObjective.target_type = target_type;
                    employeeObjective.KPI_type = kpi_type;
                    employeeObjective.weight = Weight;
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


        public IHttpActionResult DeleteWithoutTransScope(long ID)
        {
            try
            {
                var oEmployeeObjectiveKpi =
                    _Context.EmployeeObjectiveKPICollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oEmployeeObjectiveKpi != null)
                {
                    List<long> kpiAssIds = _Context.EmployeeObjectiveKPIAssessmentCollection
                        .Where(x => x.EmployeeObjectiveKpiId == ID).Select(x => x.Id).ToList<long>();

                    EmployeeObjectiveKpiAssessmentController employeeObjectiveKpiAssessmentController =
                        new EmployeeObjectiveKpiAssessmentController();

                    foreach (long kpiAssId in kpiAssIds)
                    {
                        employeeObjectiveKpiAssessmentController.Delete(kpiAssId);
                    }


                    _Context.EmployeeObjectiveKPICollection.Remove(oEmployeeObjectiveKpi);
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
                    var oEmployeeObjectiveKpi = _Context.EmployeeObjectiveKPICollection
                        .FirstOrDefault(x => x.ID == ID);

                    if (oEmployeeObjectiveKpi != null)
                    {
                        List<long> kpiAssIds = _Context.EmployeeObjectiveKPIAssessmentCollection
                            .Where(x => x.EmployeeObjectiveKpiId == ID)
                            .Select(x => x.Id)
                            .ToList();

                        EmployeeObjectiveKpiAssessmentController employeeObjectiveKpiAssessmentController =
                            new EmployeeObjectiveKpiAssessmentController();

                        foreach (long kpiAssId in kpiAssIds)
                        {
                            employeeObjectiveKpiAssessmentController.Delete(kpiAssId);
                        }

                        _Context.EmployeeObjectiveKPICollection.Remove(oEmployeeObjectiveKpi);
                        _Context.SaveChanges();

                        scope.Complete(); // ✅ COMMIT the transaction BEFORE return
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }

                    // If no KPI found
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
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
                //_Context.EmployeeObjectiveCollection.Select(x=>x.)
                var lstEmployeeObjectiveKPIs = (from employeeObjectivekpi in _Context.EmployeeObjectiveKPICollection
                            where employeeObjectivekpi.ID == ID
                            select employeeObjectivekpi).ToList().AsQueryable()
                        .Select((kpi, index) => new
                        {
                            kpi.created_by,
                            kpi.BetterUpDown,
                            kpi.created_date,
                            kpi.emp_obj_id,
                            kpi.ID,
                            kpi.modified_by,
                            kpi.modified_date,
                            kpi.name,
                            kpi.name2,
                            kpi.target,
                            code = index + 1
                        }).ToList()
                    ;

                var d = (from a in lstEmployeeObjectiveKPIs
                    select new
                    {
                        a.code,
                        a.created_by,
                        a.created_date,
                        a.emp_obj_id,
                        a.ID,
                        a.modified_by,
                        a.modified_date,
                        a.name,
                        a.name2,
                        a.target,
                        a.BetterUpDown,
                        Target1 = (from q1 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q1.PeriodNo == 1 && q1.EmployeeObjectiveKpiId == a.ID
                            select q1.Target).DefaultIfEmpty().Sum(),
                        Target2 = (from q2 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q2.PeriodNo == 2 && q2.EmployeeObjectiveKpiId == a.ID
                            select q2.Target).DefaultIfEmpty().Sum(),
                        Target3 = (from q3 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q3.PeriodNo == 3 && q3.EmployeeObjectiveKpiId == a.ID
                            select q3.Target).DefaultIfEmpty().Sum(),
                        Target4 = (from q4 in _Context.EmployeeObjectiveKPIAssessmentCollection
                            where q4.PeriodNo == 4 && q4.EmployeeObjectiveKpiId == a.ID
                            select q4.Target).DefaultIfEmpty().Sum(),
                        KPI_type = (from q4 in _Context.EmployeeObjectiveCollection
                            where q4.ID == a.emp_obj_id
                            select q4.KPI_type).DefaultIfEmpty().FirstOrDefault(),
                        weight = (from q4 in _Context.EmployeeObjectiveCollection
                            where q4.ID == a.emp_obj_id
                            select q4.weight).DefaultIfEmpty().FirstOrDefault(),
                    }).FirstOrDefault();


                if (d != null)
                {
                    return Ok(new { Data = d, IsError = false, ErrorMessage = string.Empty });
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