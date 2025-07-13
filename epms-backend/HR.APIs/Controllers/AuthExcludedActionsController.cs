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
    public class AuthExcludedActionsController : ApiController
    {
        private HRContext _Context;

        public AuthExcludedActionsController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult LoadPageExcludedActions(
            string Username,
            string URL,
            int CompanyID)
        {
            try
            {
                var lstActions = (from Action in _Context.ActionsCollection
                        join Excluded in _Context.AuthExcludedActionsCollection on Action.id equals Excluded.action_id
                        join RUser in _Context.UsersRolesCollection on Excluded.role_id equals RUser.ROLE_ID
                        join Menu in _Context.MenusCollection on Action.menu_id equals Menu.ID
                        where Menu.COMPANY_ID == CompanyID
                              & Menu.URL.ToUpper() == URL.ToUpper()
                              & RUser.USERNAME.ToUpper() == Username.ToUpper()
                        select new
                        {
                            Action.id,
                            Action.name,
                            Excluded.is_invisible,
                            Excluded.is_readonly
                        }
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