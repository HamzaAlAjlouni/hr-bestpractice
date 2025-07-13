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
    public class ActionsController : ApiController
    {
        private HRContext _Context;

        public ActionsController()
        {
            _Context = new HRContext();
        }


        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult LoadPageActions(
            string URL,
            int CompanyID)
        {
            try
            {
                List<ActionsEntity> lstActions = (from Action in _Context.ActionsCollection
                        join Menu in _Context.MenusCollection on Action.menu_id equals Menu.ID
                        where Menu.COMPANY_ID == CompanyID
                              & Menu.URL.ToUpper() == URL.ToUpper()
                        select Action
                    ).ToList();

                return Ok(new { Data = lstActions, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}