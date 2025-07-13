using HR.Entities.Admin;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities;

namespace HR.APIs.Controllers
{
    public class StratigicObjectivesChartController : ApiController
    {
        HRContext db;

        public StratigicObjectivesChartController()
        {
            db = new HRContext();
        }

        [HttpGet]
        public IHttpActionResult loadStratigicObjectivesByOrg(int companyID, int year)
        {
            try
            {
                var org = db.CompanyCollection.Where(x => x.id == companyID).Select(x => new
                {
                    id = x.id.ToString() + ".1",
                    pid = 0,
                    name = x.Name,
                    level = 1
                }).ToList();

                var objc = db.StratigicObjectivesCollection.Where(x => x.CompanyId == companyID && x.Year == year)
                    .Select(x => new
                    {
                        id = x.id.ToString() + ".2",
                        pid = x.CompanyId.ToString() + ".1",
                        name = x.Name,
                        level = 2
                    }).ToList();

                var projects = db.ProjectsCollection
                    .Where(x => x.CompanyId == companyID && x.stratigyObject.Year == year).Select(x => new
                    {
                        id = x.ID.ToString() + ".3",
                        pid = x.stratigyObject.id.ToString() + ".2",
                        name = x.Name,
                        level = 3
                    }).ToList();

                List<object> tree = new List<object>();
                for (int i = 0; i < org.Count(); i++)
                {
                    tree.Add(org[i]);
                }

                for (int i = 0; i < objc.Count(); i++)
                {
                    tree.Add(objc[i]);
                }

                for (int i = 0; i < projects.Count(); i++)
                {
                    tree.Add(projects[i]);
                }


                return Ok(new { Data = tree, IsError = false, ErrorMessage = string.Empty });
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


        public class ObjectiveChart
        {
            public string id { get; set; }
            public string name { get; set; }
            public string shortName { get; set; }
            public int level { get; set; }
            public float plannedCost { get; set; }
            public float weight { get; set; }
            public float result { get; set; }
            public List<Objectives> objectives { get; set; }
        }


        public class Objectives
        {
            public string id { get; set; }
            public string pid { get; set; }
            public string name { get; set; }
            public string shortName { get; set; }
            public int level { get; set; }
            public float? plannedCost { get; set; }
            public float? weight { get; set; }
            public float? result { get; set; }

            public List<Projects> Projects { get; set; }
        }


        public class Projects
        {
            public string id { get; set; }
            public string pid { get; set; }

            public string name { get; set; }
            public string shortName { get; set; }
            public int level { get; set; }
            public float? plannedCost { get; set; }
            public float? weight { get; set; }
            public float? result { get; set; }
        }


        private string getShortName(string lang, string name)
        {
            if (name != null)
            {
                string shortName = (name.Length > 30 ? name.Substring(0, 30) + "..." : name);
                return shortName;
            }
            else
                return "";
        }


        [HttpGet]
        public IHttpActionResult LoadAll(int companyId, int year, string languageCode)
        {
            try
            {
                List<ObjectiveChart> all = new List<ObjectiveChart>();
                ObjectiveChart d = new ObjectiveChart();

                CompanyEntity x = db.CompanyCollection.Where(n => n.id == companyId).FirstOrDefault();

                if (x != null)
                {
                    d.id = x.id.ToString() + "_1";
                    d.name = (languageCode == "en" ? x.Name : x.name2);
                    d.shortName = (languageCode == "en"
                        ? getShortName(languageCode, x.Name)
                        : getShortName(languageCode, x.name2));
                    d.level = 1;
                    d.plannedCost = 0;
                    d.weight = 0;
                    d.result = 0;
                    d.objectives = new List<Objectives>();

                    List<StratigicObjectivesEntity> objectives = db.StratigicObjectivesCollection
                        .Where(a => a.CompanyId == companyId && a.Year == year).ToList();

                    if (objectives != null)
                    {
                        foreach (StratigicObjectivesEntity a in objectives)
                        {
                            Objectives objective = new Objectives();
                            objective.id = a.id.ToString() + "_2";
                            objective.pid = a.CompanyId + "_1";
                            objective.name = (languageCode == "en" ? a.Name : a.Name2);

                            objective.shortName = (languageCode == "en"
                                ? getShortName(languageCode, a.Name)
                                : getShortName(languageCode, a.Name2));
                            objective.level = 2;
                            objective.plannedCost = a.PlannedCost;
                            objective.weight = a.Weight;
                            objective.result = a.ResultPercentage;
                            //objective.plannedCost = a.ResultPercentage;

                            Projects project = new Projects();
                            List<ProjectEntity> projects = db.ProjectsCollection
                                .Where(s => s.StratigicObjectiveId == a.id).ToList();
                            objective.Projects = new List<Projects>();
                            if (projects != null)
                            {
                                float sumPlannedCost = 0;
                                foreach (ProjectEntity s in projects)
                                {
                                    Projects pr = new Projects();
                                    pr.id = s.ID + "_3";
                                    pr.pid = s.StratigicObjectiveId + "_2";
                                    pr.name = (languageCode == "en" ? s.Name : s.Name2);
                                    pr.shortName = (languageCode == "en"
                                        ? getShortName(languageCode, s.Name)
                                        : getShortName(languageCode, s.Name2));
                                    pr.level = 3;
                                    pr.plannedCost = s.PlannedCost;
                                    pr.weight = s.Weight;
                                    pr.result = s.ResultPercentage;
                                    sumPlannedCost += s.PlannedCost ?? 0;
                                    objective.Projects.Add(pr);
                                }

                                objective.plannedCost = sumPlannedCost;
                            }

                            d.objectives.Add(objective);
                        }
                    }

                    all.Add(d);
                }
                //{
                //    id = x.id.ToString() + "_1",
                //    name = x.Name,
                //    shortName = (x.Name.Length > 30 ? x.Name.Substring(0, 30) + "..." : x.Name),
                //    level = 1,
                //    plannedCost = 0,
                //    weight = 0,
                //    result = 0,
                //    objectives = db.StratigicObjectivesCollection.Where(a => a.CompanyId == x.id && a.Year == year).ToList().Distinct().Select(a => new
                //    {
                //        id = a.id.ToString() + "_2",
                //        pid = a.CompanyId + "_1",
                //        name = a.Name,

                //        shortName = (a.Name.Length > 30 ? a.Name.Substring(0, 30) + "..." : a.Name),
                //        level = 2,
                //        plannedCost = a.PlannedCost,
                //        weight = a.Weight * 100,
                //        result = a.ResultPercentage,
                //    })
                //});


                /*
                  Projects = db.ProjectsCollection.Where(s => s.StratigicObjectiveId == a.id).ToList().OrderBy(m=>m.ID).Select(s => new
                    {
                        id = s.ID + "_3",
                        pid = s.StratigicObjectiveId + "_2",
                        name = s.Name,
                        shortName = (s.Name.Length > 30 ? s.Name.Substring(0, 30) + "..." : s.Name),
                        level = 3,
                        plannedCost = s.PlannedCost,
                        weight = s.Weight * 100 ,
                        result = s.ResultPercentage
                    }),
                 */


                return Ok(new { Data = all, IsError = false, ErrorMessage = string.Empty });
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


        [HttpGet]
        public IHttpActionResult LoadOrganizations(int companyID, int year)
        {
            try
            {
                var org = db.CompanyCollection.Where(x => x.id == companyID).Select(x => new
                {
                    id = x.id.ToString() + "_1",
                    pid = 0,
                    name = x.Name,
                    level = 1
                }).ToList();


                return Ok(new { Data = org, IsError = false, ErrorMessage = string.Empty });
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

        [HttpGet]
        public IHttpActionResult LoadObjectives(int companyID, int year)
        {
            try
            {
                var objc = db.StratigicObjectivesCollection.Where(x => x.CompanyId == companyID && x.Year == year)
                    .Select(x => new
                    {
                        id = x.id.ToString() + "_2",
                        pid = x.CompanyId.ToString() + "_1",
                        name = x.Name,
                        level = 2
                    }).ToList();

                return Ok(new { Data = objc, IsError = false, ErrorMessage = string.Empty });
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

        [HttpGet]
        public IHttpActionResult LoadProjects(int companyID, int year)
        {
            try
            {
                var projects = db.ProjectsCollection
                    .Where(x => x.CompanyId == companyID && x.stratigyObject.Year == year).Select(x => new
                    {
                        id = x.ID.ToString() + "_3",
                        pid = x.stratigyObject.id.ToString() + "_2",
                        name = x.Name,
                        level = 3
                    }).ToList();


                return Ok(new { Data = projects, IsError = false, ErrorMessage = string.Empty });
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
    }
}