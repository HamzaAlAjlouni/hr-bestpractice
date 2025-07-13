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
    public class ProjectEvaluationValuesController : ApiController
    {
        private HRContext _Context;

        public ProjectEvaluationValuesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetProjectEvaluationValues(
            int id,
            string name
        )
        {
            try
            {
                var lstCompetence = (
                    from Competence in _Context.ProjectEvaluationValuesCollection
                    where (string.IsNullOrEmpty(name) || Competence.Name.ToUpper().Contains(name.ToUpper())) &&
                          (id < 1 || Competence.EVALUATION_ID == id)
                    select new
                    {
                        Competence.ID,
                        Competence.Weight,
                        Competence.Name,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.modified_by,
                        Competence.modified_date,
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
            int id,
            string Name,
            float Weight,
            string CreatedBy)
        {
            try
            {
                ProjectEvaluationValuesEntity oCompetenceEntity = new ProjectEvaluationValuesEntity();

                oCompetenceEntity.EVALUATION_ID = id;
                oCompetenceEntity.Weight = Weight;
                oCompetenceEntity.created_by = CreatedBy;
                oCompetenceEntity.created_date = DateTime.Now;
                oCompetenceEntity.Name = Name;


                _Context.Entry(oCompetenceEntity).State = System.Data.Entity.EntityState.Added;
                _Context.ProjectEvaluationValuesCollection.Add(oCompetenceEntity);
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
            float Weight,
            string Name,
            string ModifiedBy)
        {
            try
            {
                ProjectEvaluationValuesEntity oCompetenceEntity =
                    _Context.ProjectEvaluationValuesCollection.Where(x => x.ID == ID).FirstOrDefault();
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
                var oCompetenceEntity =
                    _Context.ProjectEvaluationValuesCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    _Context.ProjectEvaluationValuesCollection.Remove(oCompetenceEntity);
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