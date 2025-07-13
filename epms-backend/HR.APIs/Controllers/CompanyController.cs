using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;
using System.Web;

namespace HR.APIs.Controllers
{
    public class CompanyController : ApiController
    {
        private HRContext _Context;

        public CompanyController()
        {
            _Context = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult getByID(long ID)
        {
            try
            {
                var oCompanyEntity = _Context.CompanyCollection.Where(x => x.id == ID).FirstOrDefault();
                if (oCompanyEntity != null)
                {
                    return Ok(new { Data = oCompanyEntity, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IHttpActionResult Update()
        {
            try
            {
                var companyObj = JsonConvert.DeserializeObject<dynamic>(HttpContext.Current.Request.Form["CompData"]);
                int companyID = companyObj.ID;
                CompanyEntity oCompanyEntity =
                    _Context.CompanyCollection.Where(x => x.id == companyID).FirstOrDefault();
                if (oCompanyEntity != null)
                {
                    oCompanyEntity.Address = companyObj.Address;
                    oCompanyEntity.Code = companyObj.Code;
                    oCompanyEntity.CurrencyCode = companyObj.CurrencyCode;
                    oCompanyEntity.Email = companyObj.Email;
                    oCompanyEntity.Fax = companyObj.Fax;
                    oCompanyEntity.Mission = companyObj.Mission;
                    oCompanyEntity.modified_by = companyObj.ModifiedBy;
                    oCompanyEntity.modified_date = System.DateTime.Now;
                    oCompanyEntity.Name = companyObj.Name;
                    oCompanyEntity.name2 = companyObj.Name2;
                    oCompanyEntity.Phone1 = companyObj.Phone1;
                    oCompanyEntity.Phone2 = companyObj.Phone2;
                    oCompanyEntity.PlansLink = companyObj.PlansLink;
                    oCompanyEntity.ProjectsLink = companyObj.ProjectsLink;
                    oCompanyEntity.Vision = companyObj.Vision;
                    oCompanyEntity.Website = companyObj.Website;
                    oCompanyEntity.company_values = companyObj.company_values;
                    oCompanyEntity.ObjectiveFactor = companyObj.ObjectiveFactor;
                    oCompanyEntity.CompetencyFactor = companyObj.CompetencyFactor;
                    _Context.Entry(oCompanyEntity).State = System.Data.Entity.EntityState.Modified;

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
    }
}