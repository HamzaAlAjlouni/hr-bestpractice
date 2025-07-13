using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.Entities.Infrastructure;
using HR.Entities.Admin;
using System.Configuration;
using HR.Entities;
using System.Globalization;
using System.Threading;
using HR.APIs.Untilities;
using System.Data.Entity;
using System.Web.Http.Results;
using static HR.APIs.Controllers.StratigicObjectivesChartController;

namespace HR.APIs.Controllers
{
    public class DashBoardController : ApiController
    {
        public HttpResponseMessage GetObjectiveWeightPerYear(long companyId, long yearId, string languageCode,
            int unitId)
        {
            try
            {
                List<tbl_resources> resources = new List<tbl_resources>();


                DashBoardEntity obj = new DashBoardEntity();
                using (HRContext DB = new HRContext())
                {
                    var projects = DB.ProjectsCollection.Where(a => a.Year == yearId);
                    var projectsInner = DB.ProjectsCollection.Where(a => a.Year == yearId);
                    var projectsKpis = DB.ObjectiveKpiCollection;
                    resources = DB.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();


                    obj.LEVEL1 = DB.StratigicObjectivesCollection.Where(
                        m => m.Year == yearId && m.CompanyId == companyId
                    ).ToList().Select(m => new LEVEL1
                    {
                        drilldown = m.id.ToString(),
                        name = (languageCode == "en" ? m.Name : m.Name2),
                        y = unitId > 0
                            ? projects.Count(px =>
                                px.UnitId == unitId && px.Year == yearId &&
                                px.StratigicObjectiveId.ToString() == m.id.ToString()) > 0
                                ? projects.Where(px =>
                                    px.UnitId == unitId && px.Year == yearId &&
                                    px.StratigicObjectiveId.ToString() == m.id.ToString()).Select(pp =>
                                    new
                                    {
                                        unitWeight =
                                            (pp.Weight / projects.Where(y => y.KPI == pp.KPI).Sum(c => c.Weight) * 100)
                                            *
                                            (
                                                projectsKpis.Where(k => k.ID.ToString() == pp.KPI).FirstOrDefault() ==
                                                null
                                                    ? 0
                                                    : projectsKpis.Where(k => k.ID.ToString() == pp.KPI)
                                                        .FirstOrDefault().Weight / 100)
                                            *
                                            (m.Weight / 100)
                                    }
                                ).Sum(c => c.unitWeight)
                                : 0
                            : (float)Math.Round(m.Weight, 3, MidpointRounding.AwayFromZero),
                    }).Where(a => a.y > 0).ToList();


                    foreach (var item in obj.LEVEL1)
                    {
                        if (unitId > 0)
                        {
                            item.y = (float)Math.Round(item.y.HasValue ? item.y.Value : 0, 3,
                                MidpointRounding.AwayFromZero);
                        }

                        long sId = Convert.ToInt64(item.drilldown);
                        LEVEL2 level2 = new LEVEL2();
                        LEVEL2Data unitsObj = new LEVEL2Data();
                        var units = projects
                            .Where(m => m.StratigicObjectiveId == sId && (unitId <= 0 || m.UnitId == unitId))
                            .Select(m => m.UnitId).Distinct().ToList();
                        foreach (int unitID in units)
                        {
                            unitsObj = DB.UnitCollection.Where(m => m.ID == unitID
                            ).Select(u => new LEVEL2Data
                            {
                                name = (languageCode == "en" ? u.NAME : u.name2),
                                y = projectsInner.Where(p =>
                                    p.UnitId == unitID && p.StratigicObjectiveId == sId &&
                                    (unitId <= 0 || p.UnitId == unitId)).Sum(p => p.Weight)
                            }).SingleOrDefault();
                            level2.name = Common.getResourceByKey("lblUnitWeight", resources);
                            level2.id = item.drilldown;
                            if (unitsObj.y < 1)
                            {
                                unitsObj.y = unitsObj.y * 100;
                            }

                            level2.data.Add(unitsObj);
                            obj.LEVEL2.Add(level2);
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public IHttpActionResult GetUnitContributionsPerYear(long yearId, int unitId = 0)
        {
            try
            {
                using (HRContext db = new HRContext())
                {
                    var units = db.UnitCollection.Where(a => unitId <= 0 || a.ID == unitId).AsNoTracking().ToList();

                    var objectives = db.StratigicObjectivesCollection.Where(a => a.Year == yearId);
                    var projectsKpis = db.ObjectiveKpiCollection;
                    var projects = db.ProjectsCollection.Where(a => a.Year == yearId);
                    var projectsInner = db.ProjectsCollection.Where(a => a.Year == yearId);
                    List<UnitContributionResult> result = new List<UnitContributionResult>();

                    foreach (var item in units)
                    {
                        result.Add(new UnitContributionResult
                        {
                            Name = item.NAME,
                            UnitId = item.ID,
                            Weight = projects.Count(px => px.UnitId == item.ID && px.Year == yearId) > 0
                                ? projects.Where(px => px.UnitId == item.ID && px.Year == yearId).Select(pp =>
                                    new
                                    {
                                        unitWeight =
                                            (pp.Weight / projectsInner.Where(y => y.KPI == pp.KPI).Sum(c => c.Weight) *
                                             100)
                                            *
                                            (
                                                projectsKpis.FirstOrDefault(k => k.ID.ToString() == pp.KPI) ==
                                                null
                                                    ? 0
                                                    : projectsKpis
                                                        .FirstOrDefault(k => k.ID.ToString() == pp.KPI).Weight / 100)
                                            *
                                            (objectives.FirstOrDefault(o => o.id == pp.StratigicObjectiveId) ==
                                             null
                                                ? 0
                                                : objectives
                                                    .FirstOrDefault(o => o.id == pp.StratigicObjectiveId).Weight / 100)
                                    }
                                ).Sum(c => c.unitWeight)
                                : 0
                        });
                    }


                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }
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


        public IHttpActionResult GetObjectiveKPIContributionsPerYear(long yearId)
        {
            try
            {
                using (HRContext db = new HRContext())
                {
                    var units = db.UnitCollection.AsNoTracking().ToList();

                    var objectives = db.StratigicObjectivesCollection.Where(a => a.Year == yearId);
                    var projectsKpis = db.ObjectiveKpiCollection.ToList();
                    var projects = db.ProjectsCollection.Where(a => a.Year == yearId).ToList();
                    var projectsInner = db.ProjectsCollection.Where(a => a.Year == yearId).ToList();
                    List<KPIContributionResult> result = new List<KPIContributionResult>();

                    var kpisList = from
                            objective in db.StratigicObjectivesCollection
                        join
                            kpi in db.ObjectiveKpiCollection
                            on objective.id equals kpi.objective_id
                        select kpi;


                    foreach (var item in kpisList)
                    {
                        result.Add(new KPIContributionResult
                        {
                            Name = item.Name,
                            Id = item.ID,
                            Weight = projects.Count(px => px.KPI == item.ID.ToString() && px.Year == yearId) > 0
                                ? projects.Where(px => px.UnitId == item.ID && px.Year == yearId).Select(pp =>
                                    new
                                    {
                                        unitWeight =
                                            (pp.Weight / projectsInner.Where(y => y.KPI == pp.KPI).Sum(c => c.Weight) *
                                             100)
                                            *
                                            (
                                                projectsKpis.Where(k => k.ID.ToString() == pp.KPI).FirstOrDefault() ==
                                                null
                                                    ? 0
                                                    : projectsKpis.Where(k => k.ID.ToString() == pp.KPI)
                                                        .FirstOrDefault().Weight / 100)
                                            *
                                            (objectives.Where(o => o.id == pp.StratigicObjectiveId).FirstOrDefault() ==
                                             null
                                                ? 0
                                                : objectives.Where(o => o.id == pp.StratigicObjectiveId)
                                                    .FirstOrDefault().Weight / 100)
                                    }
                                ).Sum(c => c.unitWeight)
                                : 0
                        });
                    }


                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }
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


        public class UnitContributionResult
        {
            public int UnitId { get; set; }
            public string Name { get; set; }
            public float Weight { get; set; }
        }

        public class KPIContributionResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public float Weight { get; set; }
        }


        public HttpResponseMessage GetObjectiveResultWeightPercentage(int year, int companyId, string languageCode,
            int unitId = 0)
        {
            try
            {
                List<tbl_resources> resources = new List<tbl_resources>();

                DashBoardEntity obj = new DashBoardEntity();
                using (HRContext DB = new HRContext())
                {
                    resources = DB.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                    obj.LEVEL1 = DB.StratigicObjectivesCollection.Where(m => m.Year == year && m.CompanyId == companyId
                    ).AsEnumerable().Select(m => new LEVEL1
                    {
                        drilldown = m.id.ToString(),
                        fullName = (languageCode == "ar" ? m.Name2 : m.Name),
                        name = (languageCode == "ar" ? getShortcut(m.Name2) : getShortcut(m.Name)),
                        y = m.ResultPercentage,
                    }).ToList();
                    foreach (var item in obj.LEVEL1)
                    {
                        long sId = Convert.ToInt64(item.drilldown);
                        LEVEL2 level2 = new LEVEL2();
                        List<LEVEL2Data> projects = new List<LEVEL2Data>();
                        projects = DB.ProjectsCollection.Where(
                            m => m.StratigicObjectiveId == sId && (unitId <= 0 || m.UnitId == unitId)
                        ).AsEnumerable().Select(p => new LEVEL2Data
                        {
                            fullName = (languageCode == "ar" ? p.Name2 : p.Name),
                            name = (languageCode == "ar" ? getShortcut(p.Name2) : getShortcut(p.Name)),
                            y = p.ResultPercentage,
                        }).ToList();
                        level2.name = Common.getResourceByKey("lblProjectResult", resources);
                        level2.id = item.drilldown;
                        level2.data = projects;
                        obj.LEVEL2.Add(level2);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string getShortcut(string value)
        {
            if (value != null && value.Length > 30)
                return value.Substring(0, 30) + "...";
            else
                return value;
        }

        public HttpResponseMessage GetUnitsPercentageData(int year, int companyId, string languageCode, int unitId = 0)
        {
            try
            {
                List<tbl_resources> resources = new List<tbl_resources>();

                DashBoardEntity obj = new DashBoardEntity();
                var strtegicsIds = new List<int>();
                var units = new List<int>();
                List<ProjectEntity> projects = new List<ProjectEntity>();
                using (HRContext db = new HRContext())
                {
                    resources = db.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                    projects = (from prj in db.ProjectsCollection
                        join st in db.StratigicObjectivesCollection
                            on prj.StratigicObjectiveId equals st.id
                        where st.Year == year
                              && st.CompanyId == companyId
                        select prj).ToList();

                    if (projects != null && projects.Count > 0)
                    {
                        units = projects.Where(a => unitId <= 0 || a.ID == unitId).Select(m => m.UnitId).Distinct()
                            .ToList();


                        foreach (int unitID in units)
                        {
                            var unit = db.UnitCollection.Where(m => m.ID == unitID
                            ).AsEnumerable().Select(m => new LEVEL1
                            {
                                drilldown = m.ID.ToString(),
                                name = (languageCode == "en" ? m.NAME : m.name2),
                                y = CalculateUnitsWeightPercentage(m.ID, projects)
                            }).SingleOrDefault();
                            obj.LEVEL1.Add(unit);
                        }
                    }


                    foreach (var item2 in obj.LEVEL1)
                    {
                        using (HRContext DB = new HRContext())
                        {
                            int uId = Convert.ToInt32(item2.drilldown);
                            LEVEL2 level2 = new LEVEL2();
                            List<LEVEL2Data> projectsData = new List<LEVEL2Data>();
                            projectsData = projects.Where(m => m.UnitId == uId
                            ).Select(p => new LEVEL2Data
                            {
                                name = (languageCode == "ar" ? p.Name2 : p.Name),
                                y = (item2.y == 0 ? 0 : ((p.WeightFromObjective) / item2.y) * 100)
                            }).ToList();
                            level2.name = Common.getResourceByKey("lblProjectResultPercentage", resources);
                            level2.id = item2.drilldown;
                            level2.data = projectsData;
                            obj.LEVEL2.Add(level2);
                        }
                    }
                }


                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private float? CalculateUnitsWeightPercentage(int unitId, List<ProjectEntity> projects)
        {
            try
            {
                using (HRContext DB = new HRContext())
                {
                    return projects.Where(p => p.UnitId == unitId).Sum(p => p.WeightFromObjective);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public HttpResponseMessage GetActualCostVsPlannedData(long year, long companyId, string languageCode)
        {
            try
            {
                actualCostsData obj = new actualCostsData();
                using (HRContext db = new HRContext())
                {
                    List<ProjectEntity> projects = (from prj in db.ProjectsCollection
                        join st in db.StratigicObjectivesCollection
                            on prj.StratigicObjectiveId equals st.id
                        where st.Year == year
                              && st.CompanyId == companyId
                        select prj).ToList();

                    projects = projects.Where(x => x.PlannedCost != null && x.ActualCost != null).ToList();

                    obj.categories = projects.Select(x => (languageCode == "ar" ? x.Name2 : x.Name)).ToList<string>();
                    obj.costs = projects.Select(x => (float)x.PlannedCost).ToList<float>();
                    obj.actualCosts = projects.Select(x => (float)x.ActualCost).ToList<float>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetActualCostStatisticsData(long year, long companyId, string languageCode)
        {
            try
            {
                List<actualCostsStatistic> actualCostsStatistics = new List<actualCostsStatistic>();

                using (HRContext db = new HRContext())
                {
                    List<ProjectEntity> projects = (from prj in db.ProjectsCollection
                        join st in db.StratigicObjectivesCollection
                            on prj.StratigicObjectiveId equals st.id
                        where st.Year == year
                              && st.CompanyId == companyId
                        select prj).ToList();

                    projects = projects.Where(x => x.PlannedCost != null && x.ActualCost != null).ToList();

                    List<StratigicObjectivesEntity> stratigicObjectives = db.StratigicObjectivesCollection.Where(st =>
                        st.Year == year
                        && st.CompanyId == companyId
                    ).ToList();


                    foreach (StratigicObjectivesEntity st in stratigicObjectives)
                    {
                        List<ProjectEntity> stProjects = projects.Where(x =>
                            x.PlannedCost != null && x.ActualCost != null && x.StratigicObjectiveId == st.id).ToList();

                        if (stProjects != null && stProjects.Count > 0)
                        {
                            actualCostsStatistic actualCostsStatisticObj = new actualCostsStatistic();
                            actualCostsStatisticObj.fullName = (languageCode == "ar" ? st.Name2 : st.Name);
                            if (languageCode == "ar")
                            {
                                actualCostsStatisticObj.name =
                                    (st.Name2.Length > 15 ? st.Name2.Substring(0, 15) + "..." : st.Name2);
                            }
                            else
                            {
                                actualCostsStatisticObj.name =
                                    (st.Name.Length > 15 ? st.Name.Substring(0, 15) + "..." : st.Name);
                            }

                            actualCostsStatisticObj.name = (languageCode == "ar" ? st.Name2 : st.Name);
                            actualCostsStatisticObj.data = new List<actualCostsStatisticData>();


                            foreach (ProjectEntity prj in stProjects)
                            {
                                actualCostsStatisticData data = new actualCostsStatisticData();

                                data.fullName = (languageCode == "ar" ? st.Name2 : st.Name);
                                if (languageCode == "ar")
                                {
                                    data.name = (prj.Name2.Length > 15
                                        ? prj.Name2.Substring(0, 15) + "..."
                                        : prj.Name2);
                                }
                                else
                                {
                                    data.name = (prj.Name.Length > 15 ? prj.Name.Substring(0, 15) + "..." : prj.Name);
                                }

                                data.value = (float)(prj.ActualCost == null ? 0 : prj.ActualCost);

                                actualCostsStatisticObj.data.Add(data);
                            }

                            actualCostsStatistics.Add(actualCostsStatisticObj);
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, actualCostsStatistics);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetUnuitTargetVsActualResult(int yearId, int companyId, string languageCode)
        {
            try
            {
                List<tbl_resources> resources = new List<tbl_resources>();

                DashboardEntityColumn obj = new DashboardEntityColumn();
                obj.categories = new List<string>();


                series targets = new series();

                series results = new series();

                targets.data = new List<float?>();
                results.data = new List<float?>();

                using (HRContext db = new HRContext())
                {
                    resources = db.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                    targets.name = Common.getResourceByKey("lblTarget", resources);
                    results.name = Common.getResourceByKey("lblResult", resources);

                    List<UnitProjectsPerformanceEntity> unitsPerformance = db.UnitProjectsPerformanceCollection
                        .Where(u => u.Year == yearId && u.CompanyId == companyId).ToList();
                    List<UnitEntity> units = db.UnitCollection.ToList();


                    if (units != null && units.Count > 0)
                    {
                        foreach (UnitEntity u in units)
                        {
                            UnitProjectsPerformanceEntity up = unitsPerformance.Where(x => x.UnitId == u.ID)
                                .FirstOrDefault();

                            if (up != null)
                            {
                                if (up.ProjectsWeightPercentageFromObjectives > 0 &&
                                    up.ResultWeightPercentageFromObjectives > 0)
                                {
                                    obj.categories.Add((languageCode == "en" ? u.NAME : u.name2));


                                    targets.data.Add(up.ProjectsWeightPercentageFromObjectives);
                                    results.data.Add(up.ResultWeightPercentageFromObjectives);
                                }
                            }
                        }

                        obj.series = new List<series>();
                        obj.series.Add(targets);
                        obj.series.Add(results);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Employees Dadshboard

        public HttpResponseMessage GetNumberEmpForUnitsVsNeedNumber(int yearId, int companyId, string languageCode)
        {
            try
            {
                List<tbl_resources> resources = new List<tbl_resources>();

                DashboardEntityColumn obj = new DashboardEntityColumn();
                using (HRContext DB = new HRContext())
                {
                    resources = DB.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                    int? totalEmployeeInCompany = 0;
                    if (DB.CompanyObjectivesPerformanceCollection.Any(m => m.Year == yearId))
                    {
                        totalEmployeeInCompany = DB.CompanyObjectivesPerformanceCollection
                            .SingleOrDefault(c => c.Year == yearId && c.CompanyId == companyId).TotalEmployee;
                    }

                    obj.categories = new List<string>();
                    obj.series = new List<series>();
                    var lst = DB.UnitProjectsPerformanceCollection
                        .Where(u => u.Year == yearId && u.CompanyId == companyId).ToList();
                    series seriesTarget = new series();
                    seriesTarget.name = Common.getResourceByKey("lblNoOfEmployee", resources);
                    series seriesResult = new series();
                    seriesResult.name = Common.getResourceByKey("lblNeeded", resources);
                    seriesResult.data = new List<float?>();
                    seriesTarget.data = new List<float?>();
                    foreach (var item in lst)
                    {
                        seriesTarget.data.Add(item.TotalEmployee);
                        seriesResult.data.Add(
                            item.ProjectsWeightPercentageFromObjectives / 100 * totalEmployeeInCompany);
                        string unitName = languageCode == "en"
                            ? DB.UnitCollection.SingleOrDefault(m => m.ID == item.UnitId).NAME
                            : DB.UnitCollection.SingleOrDefault(m => m.ID == item.UnitId).name2;
                        obj.categories.Add(unitName);
                    }

                    obj.series.Add(seriesTarget);
                    obj.series.Add(seriesResult);
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion

        public HttpResponseMessage GetEmployeeAssessment(int yearId, string languageCode, int unitId = 0)
        {
            try
            {
                EmployeeAssessmentsChartData obj = new EmployeeAssessmentsChartData();
                using (HRContext DB = new HRContext())
                {
                    obj.categories = new List<string>();
                    obj.Targets = new List<float>();
                    obj.Results = new List<float>();

                    var lst = DB.EmployeeAssesmentCollection.Where(u => u.year_id == yearId).ToList();

                    foreach (var item in lst)
                    {
                        //seriesResult.data.Add((item.result_after_round < 1) ? item.result_after_round * 100 : item.result_after_round);
                        var emp = DB.EmployeesCollection.SingleOrDefault(m => m.ID == item.employee_id);
                        string empName = (languageCode == "en"
                            ? emp.name1_1 + " " + emp.name1_4
                            : emp.name2_1 + " " + emp.name2_4);
                        if (unitId == 0 || unitId == emp.UNIT_ID)
                        {
                            obj.categories.Add(empName);
                            obj.Targets.Add((float)item.target);
                            obj.Results.Add((float)(item.final_result ?? 0));
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public HttpResponseMessage GetEmployeeRank(int yearId, int companyId, int unitId, string languageCode)
        {
            try
            {
                DashboardEntityColumn obj = new DashboardEntityColumn();
                List<tbl_resources> resources = new List<tbl_resources>();

                using (HRContext DB = new HRContext())
                {
                    resources = DB.tbl_resourcesCollection.Where(x =>
                        x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                    obj.categories = new List<string>();
                    obj.categoriesLevels = new List<long>();

                    obj.series = new List<series>();
                    //var lst = DB.CompanyObjectivesPerformanceCollection.Where(u => u.Year == yearId && u.CompanyId == companyId).ToList();
                    series seriesTarget = new series();
                    seriesTarget.name = Common.getResourceByKey("lblPlanned", resources);
                    series seriesResult = new series();
                    seriesResult.name = Common.getResourceByKey("lblRealEmployeeRank", resources);
                    seriesResult.data = new List<float?>();
                    seriesTarget.data = new List<float?>();

                    series seriesProjectWeight = new series();
                    seriesProjectWeight.data = new List<float?>();
                    seriesProjectWeight.name = "Based on Unit's Projects Weights";

                    List<tbl_performance_levels> performanceLevels = DB.tbl_performance_levelsCollection
                        .Where(x => x.company_id == companyId && x.lvl_year == yearId).ToList();

                    if (performanceLevels != null && performanceLevels.Count > 0)
                    {
                        performanceLevels = performanceLevels.OrderBy(x => x.lvl_number).ToList();
                        foreach (tbl_performance_levels performanceLevel in performanceLevels)
                        {
                            obj.categories.Add(Common.getResourceByKey("lblLevel", resources) + " " +
                                               performanceLevel.lvl_number);
                            obj.categoriesLevels.Add(performanceLevel.lvl_number);
                        }
                    }


                    /*
                    List<EmployeeAssesmentEntity> empAssessments = (from empAss in DB.EmployeeAssesmentCollection.Where(x => x.result_after_round > 0 && x.year_id == yearId) join
                            emp in DB.EmployeesCollection.Where(x => x.COMPANY_ID == companyId) on empAss.employee_id equals emp.ID select empAss).ToList();

                    int level1Emp = empAssessments.Where(x => Math.Round((double)x.result_after_round * 5 / (double)x.target, 0, MidpointRounding.AwayFromZero) == 1).Count();
                    int level2Emp = empAssessments.Where(x => Math.Round((double)x.result_after_round * 5 / (double)x.target, 0, MidpointRounding.AwayFromZero) == 2).Count();
                    int level3Emp = empAssessments.Where(x => Math.Round((double)x.result_after_round * 5 / (double)x.target, 0, MidpointRounding.AwayFromZero) == 3).Count();
                    int level4Emp = empAssessments.Where(x => Math.Round((double)x.result_after_round * 5 / (double)x.target, 0, MidpointRounding.AwayFromZero) == 4).Count();
                    int level5Emp = empAssessments.Where(x => Math.Round((double)x.result_after_round * 5 / (double)x.target, 0, MidpointRounding.AwayFromZero) == 5).Count();
                    */

                    List<PerformancelevelsQuota> performancelevelsQuotaList = DB.PerformancelevelsQuotaCollection
                        .Where(x => x.YearId == yearId && x.CompanyId == companyId).ToList();
                    UnitProjectsPerformanceEntity unitProjectsPerformanceEntity = DB.UnitProjectsPerformanceCollection
                        .Where(x => x.UnitId == unitId && x.CompanyId == companyId &&
                                    x.Year == yearId).FirstOrDefault();
                    if (unitProjectsPerformanceEntity != null && performancelevelsQuotaList != null &&
                        performancelevelsQuotaList.Count > 0)
                    {
                        int totalEmp = (int)(unitProjectsPerformanceEntity.TotalEmployee != null
                            ? unitProjectsPerformanceEntity.TotalEmployee
                            : 0);

                        int remaining = totalEmp;

                        performancelevelsQuotaList = performancelevelsQuotaList.Where(x =>
                            x.FromPercentage <=
                            Math.Round(
                                (float)(unitProjectsPerformanceEntity.Result_Percentage != null
                                    ? unitProjectsPerformanceEntity.Result_Percentage
                                    : 0), 0, MidpointRounding.AwayFromZero) &&
                            x.ToPercentage >= Math.Round((float)(unitProjectsPerformanceEntity.Result_Percentage != null
                                ? unitProjectsPerformanceEntity.Result_Percentage
                                : 0))).ToList();


                        if (performancelevelsQuotaList != null && performancelevelsQuotaList.Count > 0)
                        {
                            List<PerformancelevelsQuota> performancelevelsQuotaListPlanned = performancelevelsQuotaList
                                .Where(x => x.QuotaType == (int)Enums.Enums.QuotaType.PlannedQuota).ToList();

                            foreach (PerformancelevelsQuota quota in performancelevelsQuotaListPlanned)
                            {
                                switch (quota.LevelNumber)
                                {
                                    case 1:

                                        remaining = remaining - (int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level1EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level1EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero);
                                        break;

                                    case 2:

                                        remaining = remaining - (int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level2EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level2EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case 3:

                                        remaining = remaining - (int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level3EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level3EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case 4:

                                        remaining = remaining - (int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level4EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level4EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case 5:

                                        remaining = remaining - (int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level5EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level5EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }

                            foreach (var lvl in obj.categoriesLevels)
                            {
                                PerformancelevelsQuota performancelevelsQuota;
                                switch (lvl)
                                {
                                    case 1:
                                        seriesTarget.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level1EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level1EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        seriesProjectWeight.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.PrjectsLevel1Employee != null
                                                ? unitProjectsPerformanceEntity.PrjectsLevel1Employee
                                                : 0), 0, MidpointRounding.AwayFromZero));

                                        performancelevelsQuota = performancelevelsQuotaList
                                            .Where(x => x.LevelNumber == 1).FirstOrDefault();
                                        if (performancelevelsQuota == null || performancelevelsQuota.QuotaType ==
                                            (int)Enums.Enums.QuotaType.None)
                                            seriesResult.data.Add(0);
                                        else if (performancelevelsQuota != null && performancelevelsQuota.QuotaType ==
                                                 (int)Enums.Enums.QuotaType.PlannedQuota)
                                        {
                                            seriesResult.data.Add((float)Math.Round(
                                                (float)(unitProjectsPerformanceEntity.Level1EmployeeCount != null
                                                    ? unitProjectsPerformanceEntity.Level1EmployeeCount
                                                    : 0), 0, MidpointRounding.AwayFromZero));
                                        }
                                        else
                                            seriesResult.data.Add(remaining);

                                        break;
                                    case 2:
                                        seriesTarget.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level2EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level2EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        seriesProjectWeight.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.PrjectsLevel2Employee != null
                                                ? unitProjectsPerformanceEntity.PrjectsLevel2Employee
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        performancelevelsQuota = performancelevelsQuotaList
                                            .Where(x => x.LevelNumber == 2).FirstOrDefault();
                                        if (performancelevelsQuota == null || performancelevelsQuota.QuotaType ==
                                            (int)Enums.Enums.QuotaType.None)
                                            seriesResult.data.Add(0);
                                        else if (performancelevelsQuota != null && performancelevelsQuota.QuotaType ==
                                                 (int)Enums.Enums.QuotaType.PlannedQuota)
                                        {
                                            seriesResult.data.Add((float)Math.Round(
                                                (float)(unitProjectsPerformanceEntity.Level2EmployeeCount != null
                                                    ? unitProjectsPerformanceEntity.Level2EmployeeCount
                                                    : 0), 0, MidpointRounding.AwayFromZero));
                                        }
                                        else
                                            seriesResult.data.Add(remaining);

                                        break;
                                    case 3:
                                        seriesTarget.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level3EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level3EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        seriesProjectWeight.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.PrjectsLevel3Employee != null
                                                ? unitProjectsPerformanceEntity.PrjectsLevel3Employee
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        performancelevelsQuota = performancelevelsQuotaList
                                            .Where(x => x.LevelNumber == 3).FirstOrDefault();
                                        if (performancelevelsQuota == null || performancelevelsQuota.QuotaType ==
                                            (int)Enums.Enums.QuotaType.None)
                                            seriesResult.data.Add(0);
                                        else if (performancelevelsQuota != null && performancelevelsQuota.QuotaType ==
                                                 (int)Enums.Enums.QuotaType.PlannedQuota)
                                        {
                                            seriesResult.data.Add((float)Math.Round(
                                                (float)(unitProjectsPerformanceEntity.Level3EmployeeCount != null
                                                    ? unitProjectsPerformanceEntity.Level3EmployeeCount
                                                    : 0), 0, MidpointRounding.AwayFromZero));
                                        }
                                        else
                                            seriesResult.data.Add(remaining);

                                        break;
                                    case 4:
                                        seriesTarget.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level4EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level4EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        seriesProjectWeight.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.PrjectsLevel4Employee != null
                                                ? unitProjectsPerformanceEntity.PrjectsLevel4Employee
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        performancelevelsQuota = performancelevelsQuotaList
                                            .Where(x => x.LevelNumber == 4).FirstOrDefault();
                                        if (performancelevelsQuota == null || performancelevelsQuota.QuotaType ==
                                            (int)Enums.Enums.QuotaType.None)
                                            seriesResult.data.Add(0);
                                        else if (performancelevelsQuota != null && performancelevelsQuota.QuotaType ==
                                                 (int)Enums.Enums.QuotaType.PlannedQuota)
                                        {
                                            seriesResult.data.Add((float)Math.Round(
                                                (float)(unitProjectsPerformanceEntity.Level4EmployeeCount != null
                                                    ? unitProjectsPerformanceEntity.Level4EmployeeCount
                                                    : 0), 0, MidpointRounding.AwayFromZero));
                                        }
                                        else
                                            seriesResult.data.Add(remaining);

                                        break;
                                    case 5:
                                        seriesTarget.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.Level5EmployeeCount != null
                                                ? unitProjectsPerformanceEntity.Level5EmployeeCount
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        seriesProjectWeight.data.Add((int)Math.Round(
                                            (float)(unitProjectsPerformanceEntity.PrjectsLevel5Employee != null
                                                ? unitProjectsPerformanceEntity.PrjectsLevel5Employee
                                                : 0), 0, MidpointRounding.AwayFromZero));
                                        performancelevelsQuota = performancelevelsQuotaList
                                            .Where(x => x.LevelNumber == 5).FirstOrDefault();
                                        if (performancelevelsQuota == null || performancelevelsQuota.QuotaType ==
                                            (int)Enums.Enums.QuotaType.None)
                                            seriesResult.data.Add(0);
                                        else if (performancelevelsQuota != null && performancelevelsQuota.QuotaType ==
                                                 (int)Enums.Enums.QuotaType.PlannedQuota)
                                        {
                                            seriesResult.data.Add((float)Math.Round(
                                                (float)(unitProjectsPerformanceEntity.Level5EmployeeCount != null
                                                    ? unitProjectsPerformanceEntity.Level5EmployeeCount
                                                    : 0), 0, MidpointRounding.AwayFromZero));
                                        }
                                        else
                                            seriesResult.data.Add(remaining);

                                        break;
                                }
                            }
                        }
                    }

                    //foreach (var item in lst)
                    // {

                    //}
                    obj.series.Add(seriesTarget);
                    obj.series.Add(seriesResult);
                    obj.series.Add(seriesProjectWeight);
                }

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    #region DrillDown

    public class DashBoardEntity1
    {
        public string NAME { set; get; }
        public string VALUE { set; get; }
    }

    public class DashBoardEntity
    {
        List<LEVEL1> lst = new List<LEVEL1>();
        List<LEVEL2> lst2 = new List<LEVEL2>();

        public List<LEVEL1> LEVEL1
        {
            set { lst = value; }
            get { return lst; }
        }

        public List<LEVEL2> LEVEL2
        {
            set { lst2 = value; }
            get { return lst2; }
        }
    }

    public class LEVEL1
    {
        public float? y { set; get; }
        public string name { set; get; }
        public string drilldown { set; get; }
        public string color { set; get; }
        public string fullName { set; get; }
    }

    public class LEVEL2Data
    {
        public float? y { set; get; }
        public string name { set; get; }
        public string fullName { set; get; }
    }

    public class LEVEL2
    {
        public string name { set; get; }
        public string id { set; get; }
        public string color { set; get; }
        List<LEVEL2Data> lst = new List<LEVEL2Data>();

        public List<LEVEL2Data> data
        {
            set { lst = value; }
            get { return lst; }
        }
    }

    #endregion

    #region Vertical Multiple Columns

    public class DashboardEntityColumn
    {
        private List<string> lst = new List<string>();
        private List<long> lst2 = new List<long>();

        public List<string> categories
        {
            set { lst = value; }
            get { return lst; }
        }

        public List<long> categoriesLevels
        {
            set { lst2 = value; }
            get { return lst2; }
        }

        public List<series> series { set; get; }
    }

    public class categories
    {
        private List<string> lst = new List<string>();

        public List<string> categoriesLst
        {
            set { lst = value; }
            get { return lst; }
        }
    }

    public class series
    {
        public string name { set; get; }
        public List<float?> data = new List<float?>();
    }

    #endregion


    public class actualCostsData
    {
        public List<string> categories = new List<string>();
        public List<float> costs = new List<float>();
        public List<float> actualCosts = new List<float>();
    }


    public class EmployeeAssessmentsChartData
    {
        public List<string> categories = new List<string>();
        public List<float> Targets = new List<float>();
        public List<float> Results = new List<float>();
    }

    public class actualCostsStatistic
    {
        public string name { set; get; }
        public string fullName { set; get; }

        public List<actualCostsStatisticData> data = new List<actualCostsStatisticData>();
    }


    public class actualCostsStatisticData
    {
        public string name { set; get; }
        public string fullName { set; get; }
        public float value { set; get; }
    }
}