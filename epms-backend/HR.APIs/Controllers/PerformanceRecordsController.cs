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

namespace HR.APIs.Controllers
{
    public class PerformanceRecordsController : ApiController
    {
        private HRContext _Context;

        public PerformanceRecordsController()
        {
            _Context = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult Save(
            int EmpId, long? ObjectiveId, long? CompetencyId, string Behavior, DateTime PerformanceDate, bool Effect,
            string AgreedAction, string Comment)
        {
            try
            {
                var _performance_Records = new Performance_RecordsEntity();

                _performance_Records.EmpId = EmpId;
                _performance_Records.IsObjective = _performance_Records.ObjectiveId == null ? false : true;
                _performance_Records.ObjectiveId = ObjectiveId;
                _performance_Records.CompetencyId = CompetencyId;
                _performance_Records.Behavior = Behavior;
                _performance_Records.PerformanceDate = PerformanceDate;
                _performance_Records.Effect = Effect;
                _performance_Records.AgreedAction = AgreedAction;
                _performance_Records.Comment = Comment;

                _performance_Records.CreatedDate = DateTime.Now;

                _Context.Performance_RecordsCollection.Add(_performance_Records);
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
            int EmpId, long? ObjectiveId, long? CompetencyId, string Behavior, DateTime PerformanceDate, bool Effect,
            string AgreedAction, string Comment)
        {
            try
            {
                var _performance_Records =
                    _Context.Performance_RecordsCollection.Where(x => x.id == ID).FirstOrDefault();
                if (_performance_Records != null)
                {
                    _performance_Records.EmpId = EmpId;
                    _performance_Records.IsObjective = _performance_Records.ObjectiveId == null ? false : true;
                    _performance_Records.ObjectiveId = ObjectiveId;
                    _performance_Records.CompetencyId = CompetencyId;
                    _performance_Records.Behavior = Behavior;
                    _performance_Records.PerformanceDate = PerformanceDate;
                    _performance_Records.Effect = Effect;
                    _performance_Records.AgreedAction = AgreedAction;
                    _performance_Records.Comment = Comment;
                    _performance_Records.ModifiedDate = DateTime.Now;

                    _Context.Entry(_performance_Records).State = System.Data.Entity.EntityState.Modified;
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
                var oPerformanceEntity = _Context.Performance_RecordsCollection.Where(x => x.id == ID).FirstOrDefault();
                if (oPerformanceEntity != null)
                {
                    _Context.Performance_RecordsCollection.Remove(oPerformanceEntity);
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
                var oCompetenceEntity =
                    (from Performance_Records in _Context.Performance_RecordsCollection.Where(x => x.id == ID)
                        select new
                        {
                            Performance_Records.id,
                            Performance_Records.ObjectiveId,
                            Performance_Records.CompetencyId,
                            Performance_Records.EmpId,
                            Performance_Records.AgreedAction,
                            Performance_Records.Behavior,
                            Performance_Records.Comment,
                            Performance_Records.Effect,
                            PerformanceDate = Performance_Records.PerformanceDate.ToString("yyyy-MM-dd"),
                            Performance_Records.CreatedBy,
                            Performance_Records.CreatedDate,
                            Performance_Records.ModifiedBy,
                            Performance_Records.ModifiedDate,
                        }).ToList();
                if (oCompetenceEntity != null)
                {
                    return Ok(new { Data = oCompetenceEntity, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult getByObjectiveIdAndEmpId(long ObjectiveID, int EmpId)
        {
            try
            {
                var oCompetenceEntity =
                    (from Performance_Records in _Context.Performance_RecordsCollection.Where(x =>
                            x.ObjectiveId == ObjectiveID && x.EmpId == EmpId)
                        select new
                        {
                            Performance_Records.id,
                            Performance_Records.ObjectiveId,
                            Performance_Records.EmpId,
                            Performance_Records.AgreedAction,
                            Performance_Records.Behavior,
                            Performance_Records.Comment,
                            Performance_Records.Effect,
                            PerformanceDate = Performance_Records.PerformanceDate,
                            Performance_Records.CreatedBy,
                            Performance_Records.CreatedDate,
                            Performance_Records.ModifiedBy,
                            Performance_Records.ModifiedDate,
                        }).ToList();
                var test = oCompetenceEntity.Select(Performance_Records => new
                {
                    Performance_Records.id,
                    Performance_Records.ObjectiveId,
                    Performance_Records.EmpId,
                    Performance_Records.AgreedAction,
                    Performance_Records.Behavior,
                    Performance_Records.Comment,
                    Performance_Records.Effect,
                    PerformanceDate = Performance_Records.PerformanceDate.ToString("yyyy-MM-dd"),
                    Performance_Records.CreatedBy,
                    Performance_Records.CreatedDate,
                    Performance_Records.ModifiedBy,
                    Performance_Records.ModifiedDate,
                }).ToList();

                if (oCompetenceEntity != null)
                {
                    return Ok(new { Data = test, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult getByCompetencyIdAndEmpId(long CompetencyId, int EmpId)
        {
            try
            {
                var oCompetenceEntity =
                    (from Performance_Records in _Context.Performance_RecordsCollection.Where(x =>
                            x.CompetencyId == CompetencyId && x.EmpId == EmpId)
                        select new
                        {
                            Performance_Records.id,
                            Performance_Records.CompetencyId,
                            Performance_Records.EmpId,
                            Performance_Records.AgreedAction,
                            Performance_Records.Behavior,
                            Performance_Records.Comment,
                            Performance_Records.Effect,
                            PerformanceDate = Performance_Records.PerformanceDate,
                            Performance_Records.CreatedBy,
                            Performance_Records.CreatedDate,
                            Performance_Records.ModifiedBy,
                            Performance_Records.ModifiedDate,
                        }).ToList();
                var test = oCompetenceEntity.Select(Performance_Records => new
                {
                    Performance_Records.id,
                    Performance_Records.CompetencyId,
                    Performance_Records.EmpId,
                    Performance_Records.AgreedAction,
                    Performance_Records.Behavior,
                    Performance_Records.Comment,
                    Performance_Records.Effect,
                    PerformanceDate = Performance_Records.PerformanceDate.ToString("yyyy-MM-dd"),
                    Performance_Records.CreatedBy,
                    Performance_Records.CreatedDate,
                    Performance_Records.ModifiedBy,
                    Performance_Records.ModifiedDate,
                }).ToList();

                if (oCompetenceEntity != null)
                {
                    return Ok(new { Data = test, IsError = false, ErrorMessage = string.Empty });
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