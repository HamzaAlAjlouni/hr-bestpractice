using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using Newtonsoft.Json;

namespace HR.APIs.Controllers
{
    public class ProjectCalculationSetupController : ApiController
    {
        private HRContext _Context;

        public ProjectCalculationSetupController()
        {
            _Context = new HRContext();
        }


        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult Get()
        {
            try
            {
                var projects = _Context.ProjectsCollection.Any();
                var option = _Context.ProjectCalculationSetupCollection.Select(a=> new
                {
                    a.Id,
                    a.Calculation,
                    hasProjects= projects
                }).ToList();

                return Ok(new { Data = option, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [AcceptVerbs("Options")]
        public IHttpActionResult Update(int value)
        {
            try
            {
                var projects = _Context.ProjectsCollection.Any();
                if (projects)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "there are already added projects" });

                }

                if (value != 1 && value != 2)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "invalid value" });
                    
                }

                var option = _Context.ProjectCalculationSetupCollection.FirstOrDefault();

                if (option == null)
                {

                    option = new ProjectCalculationSetupEntity()
                    {

                        Calculation = value

                    };
                    // add new record with new value
                    _Context.ProjectCalculationSetupCollection.Add(option);

                    _Context.SaveChanges();

                }
                else
                {
                    option.Calculation = value;
                    _Context.SaveChanges();
                }


                return Ok(new { Data = option, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }
    }
}