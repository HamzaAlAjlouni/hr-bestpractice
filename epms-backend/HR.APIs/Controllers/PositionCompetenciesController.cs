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
    public class PositionCompetenciesController : ApiController
    {
        private HRContext _Context;

        public PositionCompetenciesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetPositionCompetencies(
            int PositionID, string languageCode, int? natureID)
        {
            try
            {
                //var sss = _Context.PositionCompetenciesCollection.ToList();
                //var s2ss = _Context.CompetenceCollection.ToList();
                var lstCompetence = (
                    from y in _Context.CompetenceCollection
                    join x in _Context.PositionCompetenciesCollection on y.ID equals x.competence_id
                    join z in _Context.CodesCollection on y.c_nature_id equals z.MINOR_NO
                    where y.COMPANY_ID == z.COMPANY_ID && x.position_id == PositionID
                                                       && z.MAJOR_NO == 1
                                                       && (natureID == null || z.MINOR_NO == natureID)
                    //from x in _Context.PositionCompetenciesCollection
                    //join y in _Context.CompetenceCollection on x.competence_id equals y.ID
                    //join z in _Context.CodesCollection.Where(s=>s.MAJOR_NO==1) on y.c_nature_id equals z.MINOR_NO

                    //where z.MAJOR_NO == 1
                    ////&& x.position_id == PositionID
                    //&& y.COMPANY_ID == z.COMPANY_ID
                    select new
                    {
                        y.ID,
                        //PositionID = x.position_id,
                        PositionCompetencyId = x.ID,
                        CompetenceID = y.ID,
                        CompetenceName = languageCode == "en" ? y.NAME : y.name2,
                        CompetenceCode = y.CODE,
                        //CompetenceLevel = y.competence_level,
                        CompetenceMandetory = y.is_mandetory,
                        CompetenceNature = y.c_nature_id,
                        CompetenceType = z.NAME
                    }
                ).ToList();


                return Ok(new { Data = lstCompetence, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult Save(
            int PositionID,
            int CompetenceID,
            int competenceLevel
        )
        {
            try
            {
                var projActionPlan = _Context.PositionCompetenciesCollection
                    .Where(x => x.position_id == PositionID && x.competence_id == CompetenceID).FirstOrDefault();
                if (projActionPlan != null)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "This compoetency exist" });
                }

                PositionCompetenciesEntity oCompetenceEntity = new PositionCompetenciesEntity();

                oCompetenceEntity.position_id = PositionID;
                oCompetenceEntity.competence_id = CompetenceID;
                oCompetenceEntity.competence_level = competenceLevel;
                _Context.Entry(oCompetenceEntity).State = System.Data.Entity.EntityState.Added;
                _Context.PositionCompetenciesCollection.Add(oCompetenceEntity);
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
            int PositionID,
            int CompetenceID, int competenceLevel)
        {
            try
            {
                PositionCompetenciesEntity oCompetenceEntity =
                    _Context.PositionCompetenciesCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    oCompetenceEntity.position_id = PositionID;
                    oCompetenceEntity.competence_id = CompetenceID;
                    oCompetenceEntity.competence_level = competenceLevel;
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
        public IHttpActionResult Delete(long ID, int PositionID)
        {
            try
            {
                var oCompetenceEntity = _Context.PositionCompetenciesCollection
                    .Where(x => x.competence_id == ID && x.position_id == PositionID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    _Context.PositionCompetenciesCollection.Remove(oCompetenceEntity);
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
                var oCompetenceEntity = _Context.PositionCompetenciesCollection.Where(x => x.ID == ID).FirstOrDefault();
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