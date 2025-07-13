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
    public class BranchesController : ApiController
    {
        private HRContext _Context;
        private APIResult _apiResult;

        public BranchesController()
        {
            _Context = new HRContext();
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public APIResult GetBranches(
            int CompanyID)
        {
            _apiResult = new APIResult();
            try
            {
                List<BranchesEntity> lstBranches = (from branch in _Context.BranchesCollection
                    where branch.COMPANY_ID == CompanyID
                    select branch).ToList();

                var json = JsonConvert.SerializeObject(lstBranches.ToArray());

                _apiResult.Data = json;
                _apiResult.IsError = false;

                return _apiResult;
            }
            catch (Exception ex)
            {
                _apiResult.ErrorMessage = ex.InnerException.StackTrace;
                _apiResult.IsError = true;
                return _apiResult;
            }
        }
    }
}