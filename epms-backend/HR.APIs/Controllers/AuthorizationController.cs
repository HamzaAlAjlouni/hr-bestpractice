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
    public class AuthorizationController : ApiController
    {
        private HRContext _Context;
        private APIResult _apiResult;

        public AuthorizationController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult LoadAuthorizedMenus(
            string Username,
            int CompanyID,
            string ApplicationCode,
            string SystemCode)
        {
            try
            {
                List<MenusEntity> lstAuthorizedMenus = (from Auth in _Context.AuthorizationCollection
                        join Menu in _Context.MenusCollection on Auth.MENU_ID equals Menu.ID
                        join RUser in _Context.UsersRolesCollection on Auth.ROLE_ID equals RUser.ROLE_ID
                        where RUser.USERNAME.ToUpper() == Username.ToUpper()
                              & Menu.COMPANY_ID == CompanyID
                              & (string.IsNullOrEmpty(ApplicationCode) || Menu.application_code == ApplicationCode)
                              & (string.IsNullOrEmpty(SystemCode) || Menu.system_code == SystemCode)
                        select Menu
                    ).OrderBy(x => x.order).ToList();

                return Ok(new { Data = lstAuthorizedMenus, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}