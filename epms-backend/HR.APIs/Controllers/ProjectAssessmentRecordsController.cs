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
    public class ProjectAssessmentRecordsController : ApiController
    {
        private HRContext _Context;

        public ProjectAssessmentRecordsController()
        {
            _Context = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult Save(
            int ProjectAssessmentId, string Behavior, DateTime PerformanceDate, bool Effect,
            string AgreedAction, string Comment)
        {
            try
            {
                var _performance_Records = new ProjectAssessmentRecordsEntity();

                _performance_Records.ProjectAssessmentId = ProjectAssessmentId;
                _performance_Records.Behavior = Behavior;
                _performance_Records.PerformanceDate = PerformanceDate;
                _performance_Records.Effect = Effect;
                _performance_Records.AgreedAction = AgreedAction;
                _performance_Records.Comment = Comment;

                _performance_Records.CreatedDate = DateTime.Now;

                _Context.ProjectAssessmentRecordsCollection.Add(_performance_Records);
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
             string Behavior, DateTime PerformanceDate, bool Effect,
            string AgreedAction, string Comment)
        {
            try
            {
                var _performance_Records =
                    _Context.ProjectAssessmentRecordsCollection.Where(x => x.id == ID).FirstOrDefault();
                if (_performance_Records != null)
                {
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
                var oPerformanceEntity = _Context.ProjectAssessmentRecordsCollection.Where(x => x.id == ID).FirstOrDefault();
                if (oPerformanceEntity != null)
                {
                    _Context.ProjectAssessmentRecordsCollection.Remove(oPerformanceEntity);
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
                    (from Performance_Records in _Context.ProjectAssessmentRecordsCollection.Where(x => x.id == ID)
                        select new
                        {
                            Performance_Records.id,
                            Performance_Records.ProjectAssessmentId,
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
        public IHttpActionResult getByProjectAssessmentId(long ProjectAssessmentId)
        {
            try
            {
                var oCompetenceEntity =
                    (from Performance_Records in _Context.ProjectAssessmentRecordsCollection.Where(x =>
                            x.ProjectAssessmentId == ProjectAssessmentId)
                        select new
                        {
                            Performance_Records.id,
                            Performance_Records.ProjectAssessmentId,
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
                    Performance_Records.ProjectAssessmentId,
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