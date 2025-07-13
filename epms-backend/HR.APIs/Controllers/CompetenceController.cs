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
    public class CompetenceController : ApiController
    {
        private HRContext _Context;

        public CompetenceController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult GetCompetences(
            int CompanyID,
            string CompetenceName,
            string LanguageCode,
            int natureId)
        {
            try
            {
                var lstCompetence = (
                    from Competence in _Context.CompetenceCollection
                    join x in _Context.CodesCollection on Competence.c_nature_id equals x.MINOR_NO
                    where Competence.COMPANY_ID == CompanyID
                          && x.COMPANY_ID == Competence.COMPANY_ID
                          && x.MAJOR_NO == 1
                          && (string.IsNullOrEmpty(CompetenceName) ||
                              Competence.NAME.ToUpper().Contains(CompetenceName.ToUpper()))
                          && (natureId == -1 || Competence.c_nature_id == natureId)
                    select new
                    {
                        Competence.CODE,
                        Competence.COMPANY_ID,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.c_nature_id,
                        Competence.ID,
                        Competence.is_mandetory,
                        Competence.modified_by,
                        Competence.modified_date,
                        NAME = LanguageCode == "en" ? Competence.NAME : Competence.name2,
                        IsMandetory = Competence.is_mandetory == 1 ? "Yes" : "No",
                        NatureName = x.NAME
                        //,
                        // NatureName = (
                        // from x in _Context.CodesCollection where x.COMPANY_ID == CompanyID &&  x.MAJOR_NO == 1 && x.MINOR_NO == Competence.c_nature_id
                        // select x.NAME
                        // ).Max().DefaultIfEmpty()
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
            string Code,
            int CompanyID,
            string CreatedBy,
            int NatureID,
            string Name,
            int IsMandetory,
            string LanguageCode,
            string notes)
        {
            try
            {
                CompetenceEntity oCompetenceEntity = new CompetenceEntity();

                oCompetenceEntity.CODE = Code;
                oCompetenceEntity.COMPANY_ID = CompanyID;
                oCompetenceEntity.created_by = CreatedBy;
                oCompetenceEntity.created_date = DateTime.Now;
                oCompetenceEntity.c_nature_id = NatureID;
                if (LanguageCode == "en")
                    oCompetenceEntity.NAME = Name;
                else
                    oCompetenceEntity.name2 = Name;
                oCompetenceEntity.is_mandetory = IsMandetory;
                oCompetenceEntity.notes = notes;

                _Context.Entry(oCompetenceEntity).State = System.Data.Entity.EntityState.Added;
                _Context.CompetenceCollection.Add(oCompetenceEntity);
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
            string Code,
            string ModifiedBy,
            int NatureID,
            string Name,
            int IsMandetory,
            string LanguageCode,
            string notes)
        {
            try
            {
                CompetenceEntity oCompetenceEntity =
                    _Context.CompetenceCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    oCompetenceEntity.CODE = Code;
                    oCompetenceEntity.modified_by = ModifiedBy;
                    oCompetenceEntity.modified_date = DateTime.Now;
                    oCompetenceEntity.c_nature_id = NatureID;
                    oCompetenceEntity.NAME = Name;
                    oCompetenceEntity.is_mandetory = IsMandetory;
                    if (LanguageCode == "en")
                        oCompetenceEntity.NAME = Name;
                    else
                        oCompetenceEntity.name2 = Name;
                    oCompetenceEntity.notes = notes;
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
                var oCompetenceEntity = _Context.CompetenceCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (oCompetenceEntity != null)
                {
                    _Context.CompetenceCollection.Remove(oCompetenceEntity);
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
                var oCompetenceEntity = (from Competence in _Context.CompetenceCollection.Where(x => x.ID == ID)
                    select new
                    {
                        Competence.CODE,
                        Competence.COMPANY_ID,
                        Competence.created_by,
                        Competence.created_date,
                        Competence.c_nature_id,
                        Competence.ID,
                        Competence.is_mandetory,
                        Competence.modified_by,
                        Competence.modified_date,
                        Competence.notes,
                        NAME = LanguageCode == "en" ? Competence.NAME : Competence.name2
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