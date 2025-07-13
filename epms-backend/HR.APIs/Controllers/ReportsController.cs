using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static HR.APIs.Controllers.StratigicObjectivesChartController;
using System.Web.UI.WebControls;

namespace HR.APIs.Controllers
{
    public class ReportsController : ApiController
    {
        HRContext db;

        public ReportsController()
        {
            db = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult GetProjectActionPlans(int year, int unitId = 0)
        {
            try
            {
                var objectives =
                    unitId > 0
                        ? (from  x in db.ProjectsCollection 
                            where x.Year == year && x.UnitId == unitId && x.planned_status == 2
                            select x.StratigicObjectiveId
                        ).Distinct().Count()
                        : (from kpis in db.StratigicObjectivesCollection
                           where kpis.Year == year  select kpis  
                        ).Count();

                // get objective kpis
                var objectivesKpis = unitId > 0
                    ? (from x in db.ProjectsCollection 
                        where x.Year == year && x.UnitId == unitId && x.planned_status == 2
                        select x.KPI
                    ).Distinct().Count()
                    : (from kpis in db.ObjectiveKpiCollection
                        join obj in db.StratigicObjectivesCollection on kpis.objective_id equals obj.id
                        where obj.Year == year
                        select kpis
                    ).Count();

                var goals = (from kpi in db.ObjectiveKpiCollection
                    join x in db.ProjectsCollection on kpi.objective_id equals x.ID
                    where (unitId <= 0 || x.UnitId == unitId) && kpi.is_obj_kpi == 2 && x.Year==year && x.planned_status == 2
                    select kpi.ID).Distinct().Count();

                var projects = db.ProjectsCollection.Count(a => (a.planned_status == 2 && a.p_type != 2 && a.Year==year && (a.UnitId == unitId || unitId <= 0)));
                var operations =
                    db.ProjectsCollection.Count(a => (a.planned_status == 2 && a.p_type == 2 && a.Year==year && (a.UnitId == unitId || unitId <= 0)));
                var result = new
                {
                    objectives,
                    goals,
                    objectivesKpis,
                    projects,
                    operations
                };

                if (result != null)
                {
                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }

            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesStatistics(int unitId = 0)
        {
            try
            {
                var employees = db.EmployeesCollection.Count(a => unitId <= 0 || a.UNIT_ID == unitId);

                var comptencies = (from em in db.EmployeesCollection
                    join ep in db.EmployeesPostionsCollection on em.ID equals ep.EMP_ID
                    join po in db.PositionCollection on ep.POSITION_ID equals po.ID
                    join c in db.PositionCompetenciesCollection

                        on po.ID equals c.position_id
                    where em.UNIT_ID == (unitId > 0 ? unitId : em.UNIT_ID)
                    select c.competence_id).Distinct().Count();
                
                var comptenciesKPIs = (from em in db.EmployeesCollection
                    join ep in db.EmployeesPostionsCollection on em.ID equals ep.EMP_ID
                    join po in db.PositionCollection on ep.POSITION_ID equals po.ID
                    join c in db.PositionCompetenciesCollection

                        on po.ID equals c.position_id
                        join k in db.CompetenciesKpiCollection on c.competence_id equals k.competence_id
                    where em.UNIT_ID == (unitId > 0 ? unitId : em.UNIT_ID)
                    select k.ID).Distinct().Count();
                var result = new
                {
                    employees,
                    comptencies,
                    comptenciesKPIs
                };

                if (result != null)
                {
                    return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }

            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}