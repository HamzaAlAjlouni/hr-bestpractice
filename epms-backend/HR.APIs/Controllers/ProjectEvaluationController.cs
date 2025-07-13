using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;
using HR.Entities.Models;

namespace HR.APIs.Controllers
{
    public class ProjectEvaluationController : ApiController
    {
        private HRContext _Context;

        public ProjectEvaluationController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetProjectEvaluation(
            int type,
            string name
        )
        {
            try
            {
                var evaluationValues = from x in _Context.ProjectEvaluationValuesCollection
                        select new

                        {
                            x.ID,
                            x.Name,
                            x.Weight,
                            x.EVALUATION_ID,
                        }
                    ;
                var lstCompetence = (
                    from Competence in _Context.ProjectEvaluationCollection
                    where (string.IsNullOrEmpty(name) || Competence.Name.ToUpper().Contains(name.ToUpper())
                        ) && (Competence.Type == type)
                    select new
                    {
                        Competence.ID,
                        Competence.Name,
                        Competence.Weight,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.modified_by,
                        Competence.modified_date,
                        evaluationValues = evaluationValues.Where(a => a.EVALUATION_ID == Competence.ID).ToList()
                    }).ToList();

                return Ok(new { Data = lstCompetence, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int type,
            string Name,
            float Weight,
            string CreatedBy)
        {
            try
            {
                ProjectEvaluationEntity oCompetenceEntity = new ProjectEvaluationEntity();

                oCompetenceEntity.created_by = CreatedBy;
                oCompetenceEntity.created_date = DateTime.Now;
                oCompetenceEntity.Weight = Weight;
                oCompetenceEntity.Name = Name;
                oCompetenceEntity.Type = type;

                _Context.Entry(oCompetenceEntity).State = System.Data.Entity.EntityState.Added;
                _Context.ProjectEvaluationCollection.Add(oCompetenceEntity);
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
            string Name,
            float Weight,
            string ModifiedBy)
        {
            try
            {
                ProjectEvaluationEntity oCompetenceEntity =
                    _Context.ProjectEvaluationCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    oCompetenceEntity.modified_by = ModifiedBy;
                    oCompetenceEntity.modified_date = DateTime.Now;

                    oCompetenceEntity.Name = Name;
                    oCompetenceEntity.Weight = Weight;

                    _Context.Entry(oCompetenceEntity).State = System.Data.Entity.EntityState.Modified;

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
                var oCompetenceEntity = _Context.ProjectEvaluationCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    _Context.ProjectEvaluationCollection.Remove(oCompetenceEntity);
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
        public IHttpActionResult getByID(long ID, string LanguageCode)
        {
            try
            {
                var oCompetenceEntity = (from Competence in _Context.ProjectEvaluationCollection.Where(x => x.ID == ID)
                    select new
                    {
                        Competence.ID,
                        Competence.Name,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.modified_by,
                        Competence.modified_date,
                    }).FirstOrDefault();
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
    }
}