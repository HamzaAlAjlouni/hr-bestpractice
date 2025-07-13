using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;

namespace HR.APIs.Controllers
{
    public class CompetenceKpiController : ApiController
    {
        private HRContext _Context;

        public CompetenceKpiController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetCompetencesKPI(
            int CompetenceID,
            string LanguageCode)
        {
            try
            {
                var lstCompetenceKpi = (from Competence in _Context.CompetenciesKpiCollection
                    where Competence.competence_id == CompetenceID
                    select new
                    {
                        Competence.competence_id,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.c_kpi_type_id,
                        Competence.ID,
                        Competence.modified_by,
                        Competence.modified_date,
                        NAME = LanguageCode == "en" ? Competence.NAME : Competence.name2
                    }).ToList();

                return Ok(new { Data = lstCompetenceKpi, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetCompetencesKPIByLevel(
            int CompetenceID,
            int CompetenceLevel,
            string LanguageCode)
        {
            try
            {
                var lstCompetenceKpi = (from Competence in _Context.CompetenciesKpiCollection
                    where Competence.competence_id == CompetenceID
                          && (Competence.c_kpi_type_id == CompetenceLevel || Competence.c_kpi_type_id == 0)
                    select new
                    {
                        Competence.competence_id,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.c_kpi_type_id,
                        Competence.ID,
                        Competence.modified_by,
                        Competence.modified_date,
                        NAME = LanguageCode == "en" ? Competence.NAME : Competence.name2,
                        style = (Competence.c_kpi_type_id == 0) ? "{\"background-color\":\"#e8afaf\"}" : "",
                    }).OrderBy(x => x.c_kpi_type_id).OrderBy(x => x.NAME).ToList();

                return Ok(new { Data = lstCompetenceKpi, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int CompetenceID,
            string CreatedBy,
            int KpiTypeID,
            string Name,
            string LanguageCode
        )
        {
            try
            {
                CompetenciesKpiEntity oCompetenceKPIEntity = new CompetenciesKpiEntity();

                oCompetenceKPIEntity.competence_id = CompetenceID;
                oCompetenceKPIEntity.c_kpi_type_id = KpiTypeID;
                oCompetenceKPIEntity.created_by = CreatedBy;
                oCompetenceKPIEntity.created_date = DateTime.Now;
                if (LanguageCode == "en")
                    oCompetenceKPIEntity.NAME = Name;
                else
                    oCompetenceKPIEntity.name2 = Name;

                _Context.Entry(oCompetenceKPIEntity).State = System.Data.Entity.EntityState.Added;
                _Context.CompetenciesKpiCollection.Add(oCompetenceKPIEntity);
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
            string ModifiedBy,
            int KpiTypeID,
            string Name,
            string LanguageCode)
        {
            try
            {
                CompetenciesKpiEntity oCompetenceKPIEntity =
                    _Context.CompetenciesKpiCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceKPIEntity != null)
                {
                    oCompetenceKPIEntity.modified_by = ModifiedBy;
                    oCompetenceKPIEntity.modified_date = DateTime.Now;
                    oCompetenceKPIEntity.c_kpi_type_id = KpiTypeID;
                    if (LanguageCode == "en")
                        oCompetenceKPIEntity.NAME = Name;
                    else
                        oCompetenceKPIEntity.name2 = Name;

                    _Context.Entry(oCompetenceKPIEntity).State = System.Data.Entity.EntityState.Modified;

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
                var oCompetenceKPIEntity = _Context.CompetenciesKpiCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceKPIEntity != null)
                {
                    _Context.CompetenciesKpiCollection.Remove(oCompetenceKPIEntity);
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
                var oCompetenciesKpiEntity =
                    (from Competence in _Context.CompetenciesKpiCollection.Where(x => x.ID == ID)
                        select new
                        {
                            Competence.competence_id,
                            Competence.created_by,
                            Competence.created_date,
                            Competence.c_kpi_type_id,
                            Competence.ID,
                            Competence.modified_by,
                            Competence.modified_date,
                            NAME = LanguageCode == "en" ? Competence.NAME : Competence.name2
                        })
                    .FirstOrDefault();
                if (oCompetenciesKpiEntity != null)
                {
                    return Ok(new { Data = oCompetenciesKpiEntity, IsError = false, ErrorMessage = string.Empty });
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