using HR.Entities;
using HR.Entities.Admin;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace HR.APIs.Controllers
{
    public class UnitsPerformanceController : ApiController
    {
        HRContext db;

        public UnitsPerformanceController()
        {
            db = new HRContext();
        }


        #region Projects Action Plans

        [HttpGet]
        public IHttpActionResult GetUnitsSearch(int Year, int? unitID, int companyID, string languageCode)
        {
            try
            {
                var unitsList = db.UnitCollection.ToList();
                var projectsList = db.ProjectsCollection.ToList().Where(s => s.Year == Year && s.CompanyId == companyID)
                    .ToList();
                var objectivesList = db.StratigicObjectivesCollection.ToList()
                    .Where(s => s.Year == Year && s.CompanyId == companyID).ToList();
                var Emplist = db.EmployeesCollection.ToList();
                var objKPIs = db.ObjectiveKpiCollection.ToList();
                var sumUnitEmp = Emplist.Where(x => x.COMPANY_ID == companyID).Count();

                //get  project calculation method
                var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                var data = unitsList.Where(x => x.COMPANY_ID == companyID && (unitID == null || x.ID == unitID)).Select(
                    x => new
                    {
                        x.ID,
                        UnitName = (languageCode == "ar") ? x.name2 : x.NAME,
                        UnitEmp = Emplist.Where(s => s.UNIT_ID == x.ID).Count(),
                        UnitPlanWeight = projectsList.Where(a => a.UnitId == x.ID).Select(a => new
                        {
                            PWeight = a.Weight * (objectivesList.FirstOrDefault(o => o.id == a.StratigicObjectiveId)
                                                   ?.Weight / 100)
                                               * (calculationWeight == 1
                                                   ? (objKPIs.FirstOrDefault(o => o.ID.ToString() == a.KPI)?.Weight /
                                                      100)
                                                   : 1
                                               )
                        }).Sum(a => a.PWeight),
                        //projectsList
                        //.Where(s => s.UnitId == x.ID && s.Year == Year && s.CompanyId == companyID)
                        //.Sum(s => s.Weight * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == Year)
                        //.Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault())
                        UnitActualWeight = projectsList.Where(s => s.UnitId == x.ID).Sum(a =>
                            (((a.ResultWeightPercentageFromObjectives.HasValue
                                 ? a.ResultWeightPercentageFromObjectives.Value
                                 : 0) / projectsList.Where(p => p.KPI == a.KPI).Sum(k => k.Weight) * 100) *
                             ((objectivesList.Where(o => o.id == a.StratigicObjectiveId)).FirstOrDefault()?.Weight /
                              100) * (calculationWeight == 1
                                 ? (objKPIs.Where(k => k.ID.ToString() == a.KPI).FirstOrDefault()?.Weight / 100)
                                 : 1)))
                    }).ToList();

                var result = (from x in data
                    select new
                    {
                        FTE = Convert.ToDecimal(
                            (x.UnitEmp - ((x.UnitPlanWeight.Value / 100) * sumUnitEmp)).ToString("#,##0.00")),
                        x.ID,
                        x.UnitName,
                        x.UnitEmp,
                        sumUnitEmp,
                        SuccessRate =
                            Convert.ToDouble(
                                ((x.UnitPlanWeight == 0 ? 0 : x.UnitActualWeight.Value / x.UnitPlanWeight.Value) * 100)
                                .ToString("##0.00")),
                        UnitActualWeight = Convert.ToDouble(x.UnitActualWeight.Value.ToString("##0.00")),
                        UnitPlanWeight = Convert.ToDouble(x.UnitPlanWeight.Value.ToString("##0.00")),
                        UnitWeightGap =
                            Convert.ToDouble((x.UnitActualWeight - x.UnitPlanWeight > 0
                                ? 0
                                : Math.Abs(x.UnitActualWeight.Value - x.UnitPlanWeight.Value)).ToString("##0.00")),
                        PlusWeight =
                            Convert.ToDouble(((x.UnitActualWeight.HasValue ? x.UnitActualWeight.Value : 0) -
                                (x.UnitPlanWeight.HasValue ? x.UnitPlanWeight.Value : 0) > 0
                                    ? Math.Abs(x.UnitActualWeight.Value - x.UnitPlanWeight.Value)
                                    : 0).ToString("##0.00")),
                    }).ToList();

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

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult GetEmployeeDistribution(int year, int companyID)
        {
            try
            {
                var result = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == year && x.company_id == companyID).Select(x => new
                    {
                        x.lvl_name,
                        x.lvl_number,
                        x.lvl_code,
                        x.lvl_percent,
                        empCount = ((x.lvl_percent / 100) *
                                    db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count() * 1.00)
                    });

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

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        //[HttpGet]
        //public IHttpActionResult GetActualEmployeeDistribution(int year, int companyID)
        //{
        //    try
        //    {
        //        // get actual qouta for all units

        //        var result1 = db.tbl_performance_levelsCollection.Where(x => x.lvl_year == year && x.company_id == companyID).Select(x => new
        //        {
        //            x.lvl_name,
        //            x.lvl_number,
        //            x.lvl_code,
        //            x.lvl_percent,
        //            empCount = ((x.lvl_percent / 100) * db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count() * 1.00)
        //        });
        //        var projectsList = db.ProjectsCollection.ToList().Where(s => s.Year == year && s.CompanyId == companyID).ToList();
        //        var objectivesList = db.StratigicObjectivesCollection.ToList().Where(s => s.Year == year && s.CompanyId == companyID).ToList();


        //        foreach (var level in db.tbl_performance_levelsCollection.Where(x => x.lvl_year == year && x.company_id == companyID))
        //        {


        //            foreach (var unit in db.UnitCollection)
        //            {

        //                var empCount = ((level.lvl_percent / 100) * db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count() * 1.00);
        //                var UnitEmp = db.EmployeesCollection.Where(s => s.UNIT_ID == unit.ID).Count();
        //                var UnitWeight = db.ProjectsCollection.Where(s => s.UnitId == unit.ID).Sum(s => s.Weight);
        //                var Result = db.ProjectsCollection.Where(s => s.UnitId == unit.ID && s.CompanyId == companyID).Sum(s => s.Result);
        //                var UnitPlanWeight = projectsList.Where(s => s.UnitId == unit.ID && s.Year == year && s.CompanyId == companyID)
        //                    .Sum(s => s.Weight * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == year)
        //                    .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault());

        //                var UnitActualWeight = db.ProjectsCollection.Where(s => s.UnitId == unit.ID && s.CompanyId == companyID && s.Year == year)
        //                .Sum(s => (s.Result == null) ? 0 : s.Result) == null ? 0 : db.ProjectsCollection
        //                .Where(s => s.UnitId == unit.ID).Sum(s => (s.Result == null) ? 0 : s.Result)
        //            / db.ProjectsCollection.Where(s => s.UnitId == unit.ID).Count();

        //                var remaing = UnitEmp;
        //                var successRate = Convert.ToDouble(((UnitPlanWeight == 0 ? 0 : UnitActualWeight / UnitPlanWeight) * 100));
        //                var lst = new List<testQuta>();
        //                List<int> lstOfRem = new List<int>();
        //                if (successRate <= 50)
        //                {
        //                    var Column1 = Convert.ToInt32(Math.Ceiling(result.ToList()[0].unitEmpQouta));
        //                    var Column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
        //                    remaing = remaing - Column1;
        //                    remaing = remaing - Column2;
        //                    var column3 = remaing;

        //                    var Column4 = 0;
        //                    var Column5 = 0;
        //                    lstOfRem = new List<int> { Column1, Column2, column3, Column4, Column5 };

        //                }
        //                else if (successRate >= 51 && successRate <= 70)
        //                {
        //                    var Column1 = Convert.ToInt32(Math.Ceiling(result.ToList()[0].unitEmpQouta));
        //                    var Column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
        //                    remaing = remaing - Column1;
        //                    remaing = remaing - Column2;
        //                    var column3 = remaing;

        //                    var Column4 = 0;
        //                    var Column5 = 0;
        //                    lstOfRem = new List<int> { Column1, Column2, column3, Column4, Column5 };

        //                }
        //                else if (successRate >= 91 && successRate <= 100)
        //                {
        //                    var column1 = 0;
        //                    var column2 = 0;
        //                    var column3 = 0;
        //                    var Remainingcolumn2 = 0;
        //                    var Column4 = Convert.ToInt32(Math.Ceiling(result.ToList()[3].unitEmpQouta));
        //                    var Column5 = Convert.ToInt32(Math.Round(result.ToList()[4].unitEmpQouta));
        //                    remaing = remaing - Column4;
        //                    remaing = remaing - Column5;
        //                    if (remaing > result.ToList()[2].unitEmpQouta)
        //                    {
        //                        column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
        //                    }
        //                    column3 = remaing - column2;
        //                    lstOfRem = new List<int> { column1, column2, column3, Column4, Column5 };
        //                }
        //                else if (successRate >= 101 && successRate <= 500)
        //                {
        //                    var column1 = 0;
        //                    var column2 = 0;
        //                    var column3 = 0;
        //                    var Column5 = Convert.ToInt32(Math.Ceiling(result.ToList()[4].unitEmpQouta));
        //                    var Column4 = Convert.ToInt32(Math.Ceiling(result.ToList()[3].unitEmpQouta));
        //                    if (Column4 >= remaing)
        //                    {
        //                        Column4 = remaing - Column5;
        //                    }
        //                    remaing = remaing - Column4;
        //                    remaing = remaing - Column5;
        //                    if (remaing < 0) remaing = 0;
        //                    column3 = remaing;
        //                    lstOfRem = new List<int> { column1, column2, column3, Column4, Column5 };
        //                }
        //            }


        //        }


        //        var result = db.tbl_performance_levelsCollection.Where(x => x.lvl_year == year && x.company_id == companyID).Select(x => new
        //        {
        //            x.lvl_name,
        //            x.lvl_number,
        //            x.lvl_code,
        //            x.lvl_percent,
        //            empCount = ((x.lvl_percent / 100) * db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count() * 1.00)
        //        });


        //        if (result != null)
        //        {
        //            return Ok(new { Data = result, IsError = false, ErrorMessage = string.Empty });
        //        }
        //        return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
        //    }
        //    catch (Exception ex)
        //    {
        //        while (ex.InnerException != null) { ex = ex.InnerException; }
        //        return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
        //    }
        //}

        public class EmployeeQuotaRequest
        {
            public int Year { get; set; }
            public int CompanyID { get; set; }
            public string Units { get; set; }
            public string SuccessRates { get; set; }
        }

        [HttpGet]
        public IHttpActionResult GetAllUnitsEmployeeQouta([FromUri] EmployeeQuotaRequest request)
        {
            int year = request.Year;
            int companyID = request.CompanyID;
            int[] units = request.Units.Split(',').Select(int.Parse).ToArray();
            ;
            double[] successRates = request.SuccessRates.Split(',').Select(double.Parse).ToArray();

            try
            {
                var projectsList = db.ProjectsCollection.ToList();
                var performance = db.PerformancelevelsQuotaCollection.ToList();
                var levels = db.tbl_performance_levelsCollection.ToList();
                var PlannedUnitWeights2 = (from obj in db.ProjectsCollection
                    where
                        (obj.CompanyId == companyID)
                    //&&(unitId <= 0 || obj.UnitId == unitId)
                    select new
                    {
                        obj.Weight,
                        obj.KPI,
                        obj.StratigicObjectiveId
                    });
                //get  project calculation method
                var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                // var soList = db.StratigicObjectivesCollection.Where(o => o.Year == year);
                //var oKpiList = db.ObjectiveKpiCollection.ToList();
                var finalResult = new List<testQuta>();
                var empsList = db.EmployeesCollection.ToList();
                var empsListCount = db.EmployeesCollection.Count(s => s.COMPANY_ID == companyID);
                for (int i = 0; i < units.Length; i++)
                {
                    var unitID = units[i];

                    var lst = new List<testQuta>();
                    var projectsA = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((unitID != 0) ? p.UnitId == unitID : p.UnitId == p.UnitId)
                            && ((companyID != 0) ? p.CompanyId == companyID : p.CompanyId == p.CompanyId)
                        ))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == year) on prj
                            .StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyID) on prj.KPI equals ok
                            .ID
                            .ToString()
                        select new
                        {
                            plannedStratigy =
                                ((prj.Weight / PlannedUnitWeights2
                                    .Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId)
                                    .Sum(a => a.Weight)) * 100) * (calculationWeight == 1 ? (ok.Weight / 100) : 1) *
                                (so.Weight / 100),
                            actualStratigy =
                                ((prj.ResultWeightPercentageFromObjectives / PlannedUnitWeights2
                                    .Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId)
                                    .Sum(a => a.Weight)) * 100) * (calculationWeight == 1 ? (ok.Weight / 100) : 1) *
                                (so.Weight / 100)
                        }).ToList();
                    var projects = projectsA.Sum(a => a.plannedStratigy);
                    var projectsActual = projectsA.Sum(a => a.actualStratigy);
                    var data = levels
                        .Where(x => x.lvl_year == year && x.company_id == companyID)
                        .Select(x => new
                        {
                            x.lvl_name,
                            x.lvl_number,
                            x.lvl_code,
                            x.lvl_percent,
                            UnitEmp = empsList.Where(s => s.UNIT_ID == unitID).Count(),
                            empCount = ((x.lvl_percent) * empsListCount * 1.00 / 100),
                            UnitWeight = projects,
                            Result = projectsList.Where(s => s.UnitId == unitID && s.CompanyId == companyID)
                                .Sum(s => s.Result),
                            UnitPlanWeight = projects,
                            UnitActualWeight = projectsActual,
                        }).ToList();
                    int successRateValue = Convert.ToInt32(successRates[i]);


                    var result = (from x in data
                        select new testQuta
                        {
                            lvl_code = x.lvl_code,
                            lvl_name = x.lvl_name,
                            lvl_number = x.lvl_number,
                            lvl_percent = x.lvl_percent,
                            UnitPlanWeight = x.UnitPlanWeight,
                            UnitActualWeight = x.UnitActualWeight,
                            empCount = x.empCount,
                            UnitWeight = x.UnitWeight,
                            plannedQuota = x.UnitWeight * x.empCount / 100,
                            unitEmpQouta =
                                Convert.ToDouble((x.empCount * (x.UnitPlanWeight / 100.00)).ToString("#,##0.00")),
                            UnitEmp = x.UnitEmp,
                            unitEmpAfterAssessment = 0,
                            QuotaType =
                                (performance.Where(s => s.CompanyId == companyID &&
                                                        s.YearId == year
                                                        && s.LevelNumber == x.lvl_number
                                                        && (successRateValue) >= s.FromPercentage &&
                                                        (successRateValue) <= s.ToPercentage)
                                    .Select(s => s.QuotaType).DefaultIfEmpty(0)).FirstOrDefault(),
                            QuotaDirection =
                                (performance.Where(s => s.CompanyId == companyID &&
                                                        s.YearId == year
                                                        && s.LevelNumber == x.lvl_number
                                                        && (successRateValue) >= s.FromPercentage &&
                                                        (successRateValue) <= s.ToPercentage)
                                    .Select(s => s.QuotaDirection).DefaultIfEmpty(0)).FirstOrDefault(),

                            Result = x.Result
                        }).ToList();
                    var limitLeft = 0;
                    var direction = result.Select(a => a.QuotaDirection).DefaultIfEmpty(0).FirstOrDefault();
                    if (direction == 0) 
                    {
                        //fill Planned Quota	
                        var normalList = result.Where(a => a.QuotaType == 1 || a.QuotaType == 3)
                            .OrderByDescending(a => a.lvl_number).ToList();
                        foreach (var x in normalList)
                        {
                            var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;
                            limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1 || a.QuotaType == 3).Sum(a => a.unitEmpAfterAssessment);
                            //add with ceiling 
                            x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                                ? (
                                    x.QuotaType == 1
                                        ? Convert.ToInt32(Math.Ceiling(unitEmpQouta))
                                        : Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                                )
                                : limitLeft;
                            lst.Add(x);
                            // if (successRateValue > 94)
                            // {
                            //     //add with ceiling 
                            //     x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            //         ? Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                            //         : limitLeft;
                            //     lst.Add(x);
                            // }
                            // else
                            // {
                            //     //add with round when 0.5 or more in 4 and 5 level 
                            //     if (successRateValue > 80 && (x.lvl_number == 5 || x.lvl_number == 4))
                            //         x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            //             ? Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                            //             : limitLeft;
                            //     else
                            //         x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            //             ? Convert.ToInt32(x.unitEmpQouta)
                            //             : limitLeft;
                            //
                            //     lst.Add(x);
                            // }
                        }

                        var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                        foreach (var x in remainingList)
                        {
                            limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                            x.unitEmpAfterAssessment = limitLeft;
                            lst.Add(x);
                        }
                    }

                    else
                    {
                        //fill Planned Quota	

                        var normalList = result.Where(a => a.QuotaType == 1 || a.QuotaType == 3)
                            .OrderBy(a => a.lvl_number)
                            .ToList();
                        foreach (var x in normalList)
                        {
                            var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;

                            limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1|| a.QuotaType == 3).Sum(a => a.unitEmpAfterAssessment);


                            x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                                ? (
                                    x.QuotaType == 1
                                        ? Convert.ToInt32(Math.Ceiling(unitEmpQouta))
                                        : Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                                )
                                : limitLeft;

                            lst.Add(x);
                        }

                        var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                        foreach (var x in remainingList)
                        {
                            limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                            x.unitEmpAfterAssessment = limitLeft;
                            lst.Add(x);
                        }
                    }

                    lst.AddRange(result.Where(a => a.QuotaType == 0));


                    finalResult.AddRange(lst);
                }


                if (finalResult != null)

                {
                    var groupedResult = finalResult
                        .GroupBy(x => x.lvl_number)
                        .Select(g => new
                        {
                            lvl_code = g.FirstOrDefault() != null ? g.FirstOrDefault().lvl_code : "",
                            lvl_number = g.Key,
                            lvl_name = g.FirstOrDefault() != null ? g.FirstOrDefault().lvl_name : "",
                            lvl_percent = g.FirstOrDefault() != null ? g.FirstOrDefault().lvl_percent : 0,
                            unitEmpAfterAssessment = g.Sum(x => x.unitEmpAfterAssessment),
                            UnitActualWeight = g.Sum(x => x.UnitActualWeight),
                            empCount = g.FirstOrDefault() != null ? g.FirstOrDefault().empCount : 0,
                            UnitWeight = g.Sum(x => x.UnitWeight),
                            plannedQuota = g.Sum(x => x.plannedQuota),
                            unitEmpQouta = g.Sum(x => x.unitEmpQouta),
                            UnitEmp = g.Sum(x => x.UnitEmp),
                            FTEQouta = g.Sum(x => x.FTEQouta)
                        })
                        .ToList();
                    return Ok(new
                    {
                        Data = groupedResult.OrderBy(x => x.lvl_number), IsError = false, ErrorMessage = string.Empty
                    });
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


        [HttpGet]
        public IHttpActionResult GetUnitEmployeeQouta(int year, int unitID, int companyID, double? successRate)
        {
            try
            {
                var PlannedUnitWeights2 = (from obj in db.ProjectsCollection
                    where
                        (obj.CompanyId == companyID)
                    //&&(unitId <= 0 || obj.UnitId == unitId)
                    select new
                    {
                        obj.Weight,
                        obj.KPI,
                        obj.StratigicObjectiveId
                    });

                //get  project calculation method
                var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;

                var projectsA = (
                    from prj in db.ProjectsCollection.Where(p => (
                        ((unitID != 0) ? p.UnitId == unitID : p.UnitId == p.UnitId)
                        && ((companyID != 0) ? p.CompanyId == companyID : p.CompanyId == p.CompanyId)
                    ))
                    join so in db.StratigicObjectivesCollection.Where(o => o.Year == year) on
                        prj.StratigicObjectiveId equals so.id
                    join unit in db.UnitCollection on prj.UnitId equals unit.ID
                    join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyID) on prj.KPI equals ok.ID
                        .ToString()
                    select new
                    {
                        plannedStratigy =
                            // prj.Weight /  (calculationWeight == 1 ? (ok.Weight / 100) : 1) *
                            //   (so.Weight / 100),
                        ((prj.Weight / PlannedUnitWeights2
                            .Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId)
                            .Sum(a => a.Weight)) * 100) * (calculationWeight == 1 ? (ok.Weight / 100) : 1) *
                        (so.Weight / 100),
                        actualStratigy =
                            ((prj.ResultWeightPercentageFromObjectives / PlannedUnitWeights2
                                .Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId)
                                .Sum(a => a.Weight)) * 100) * (calculationWeight == 1 ? (ok.Weight / 100) : 1) *
                            (so.Weight / 100)
                    }).ToList();
                var projects = projectsA.Sum(a => a.plannedStratigy);
                var projectsActual = projectsA.Sum(a => a.actualStratigy);
                // var unitsList = db.UnitCollection.ToList();
                // var projectsList = db.ProjectsCollection.ToList().Where(s => s.Year == year && s.CompanyId == companyID)
                //     .ToList();
                // var objectivesList = db.StratigicObjectivesCollection.ToList()
                //     .Where(s => s.Year == year && s.CompanyId == companyID).ToList();
                // //var UnitPlanWeight = unitsList.Where(x => x.COMPANY_ID == companyID && x.ID == unitID).Select(x => new
                //{
                //    UnitPlanWeight = projectsList.Where(s => s.UnitId == x.ID && s.Year == year && s.CompanyId == companyID)
                //            .Sum(s => s.Weight * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == year)
                //            .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault())
                //}).ToList().FirstOrDefault();


                var data = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == year && x.company_id == companyID)
                    .Select(x => new
                    {
                        x.lvl_name,
                        x.lvl_number,
                        x.lvl_code,
                        x.lvl_percent,
                        UnitEmp = db.EmployeesCollection.Where(s => s.UNIT_ID == unitID).Count(),
                        empCount = ((x.lvl_percent / 100) *
                                    db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count() * 1.00),
                        UnitWeight = projects,
                        Result = db.ProjectsCollection.Where(s => s.UnitId == unitID && s.CompanyId == companyID)
                            .Sum(s => s.Result),
                        UnitPlanWeight = projects,
                        UnitActualWeight = projectsActual,
                    }).ToList();
                int remainingIndex = 0;
                int successRateValue = Convert.ToInt32(successRate);
                var result = (from x in data
                    select new testQuta
                    {
                        lvl_code = x.lvl_code,
                        lvl_name = x.lvl_name,
                        lvl_number = x.lvl_number,
                        lvl_percent = x.lvl_percent,
                        UnitPlanWeight = x.UnitPlanWeight,
                        UnitActualWeight = x.UnitActualWeight,
                        empCount = x.empCount,
                        UnitWeight = x.UnitWeight,
                        plannedQuota = x.UnitWeight * x.empCount / 100,
                        unitEmpQouta =
                            Convert.ToDouble((x.empCount * (x.UnitPlanWeight / 100.00)).ToString("#,##0.00")),
                        UnitEmp = x.UnitEmp,
                        unitEmpAfterAssessment = 0,
                        QuotaDirection =
                            (db.PerformancelevelsQuotaCollection.Where(s => s.CompanyId == companyID && s.YearId == year
                                    && s.LevelNumber == x.lvl_number
                                    && (successRateValue) >= s.FromPercentage &&
                                    (successRateValue) <= s.ToPercentage)
                                .Select(s => s.QuotaDirection).DefaultIfEmpty(0)).FirstOrDefault(),
                        QuotaType =
                            (db.PerformancelevelsQuotaCollection.Where(s => s.CompanyId == companyID && s.YearId == year
                                    && s.LevelNumber == x.lvl_number
                                    && (successRateValue) >= s.FromPercentage &&
                                    (successRateValue) <= s.ToPercentage)
                                .Select(s => s.QuotaType).DefaultIfEmpty(0)).FirstOrDefault(),
                       

                        Result = x.Result
                    }).ToList();

                var lst = new List<testQuta>();
                bool first = true;
                var limitLeft = 0;
                var direction = result.Select(a => a.QuotaDirection).DefaultIfEmpty(0).FirstOrDefault();
                if (direction == 0)
                {
                    //fill Planned Quota	
                    var normalList = result.Where(a => a.QuotaType == 1 || a.QuotaType == 3)
                        .OrderByDescending(a => a.lvl_number).ToList();
                    foreach (var x in normalList)
                    {
                        var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;
                        limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1 || a.QuotaType == 3).Sum(a => a.unitEmpAfterAssessment);
                        //add with ceiling 
                        x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            ? (
                                x.QuotaType == 1
                                    ? Convert.ToInt32(Math.Ceiling(unitEmpQouta))
                                    : Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                            )
                            : limitLeft;
                        lst.Add(x);
                        // if (successRateValue > 94)
                        // {
                        //     //add with ceiling 
                        //     x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                        //         ? Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                        //         : limitLeft;
                        //     lst.Add(x);
                        // }
                        // else
                        // {
                        //     //add with round when 0.5 or more in 4 and 5 level 
                        //     if (successRateValue > 80 && (x.lvl_number == 5 || x.lvl_number == 4))
                        //         x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                        //             ? Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                        //             : limitLeft;
                        //     else
                        //         x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                        //             ? Convert.ToInt32(x.unitEmpQouta)
                        //             : limitLeft;
                        //
                        //     lst.Add(x);
                        // }
                    }

                    var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                    foreach (var x in remainingList)
                    {
                        limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft;
                        lst.Add(x);
                    }
                }

                else
                {
                    //fill Planned Quota	

                    var normalList = result.Where(a => a.QuotaType == 1 || a.QuotaType == 3).OrderBy(a => a.lvl_number)
                        .ToList();
                    foreach (var x in normalList)
                    {
                        var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;

                        limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1 || a.QuotaType == 3).Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            ? (
                                x.QuotaType == 1
                                    ? Convert.ToInt32(Math.Ceiling(unitEmpQouta))
                                    : Convert.ToInt32(Math.Round(unitEmpQouta, MidpointRounding.AwayFromZero))
                            )
                            : limitLeft;

                        lst.Add(x);
                    }

                    var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                    foreach (var x in remainingList)
                    {
                        limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft;
                        lst.Add(x);
                    }
                }

                lst.AddRange(result.Where(a => a.QuotaType == 0));

                if (lst != null)
                {
                    return Ok(new
                        { Data = lst.OrderBy(x => x.lvl_number), IsError = false, ErrorMessage = string.Empty });
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

        /**/
        [HttpGet]
        public IHttpActionResult GetAllEmployeeQouta(int year, int companyID)
        {
            try
            {
                var unitsList = db.UnitCollection.ToList();
                var projectsList = db.ProjectsCollection.ToList().Where(s => s.Year == year && s.CompanyId == companyID)
                    .ToList();
                var kpis = db.ObjectiveKpiCollection.ToList();
                var objectivesList = db.StratigicObjectivesCollection.ToList()
                    .Where(s => s.Year == year && s.CompanyId == companyID).ToList();
                var UnitPlanWeight = unitsList.Where(x => x.COMPANY_ID == companyID).Select(x => new
                {
                    UnitPlanWeight = projectsList.Where(s => s.Year == year && s.CompanyId == companyID)
                        .Sum(s => s.Weight
                                  * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == year)
                                      .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault() *
                                  kpis.Where(a => a.ID.ToString() == s.KPI)
                                      .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault()
                        )
                }).FirstOrDefault();
                var empCount = db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count();
                var projectW = projectsList.Sum(s =>
                    s.Weight * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == year)
                        .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault() *
                    kpis.Where(a => a.ID.ToString() == s.KPI)
                        .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault());

                var projectR = projectsList.Where(s => s.CompanyId == companyID).Sum(s => s.Result);
                var projectA = projectsList.Sum(s => (s.Result == null) ? 0 : s.Result) == null
                    ? 0
                    : db.ProjectsCollection.Sum(s => (s.Result == null) ? 0 : s.Result) / db.ProjectsCollection.Count();
                var data = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == year && x.company_id == companyID)
                    .Select(x => new
                    {
                        x.lvl_name,
                        x.lvl_number,
                        x.lvl_code,
                        x.lvl_percent,
                        UnitEmp = empCount,
                        empCount = ((x.lvl_percent / 100) * empCount * 1.00),
                        UnitWeight = projectW,
                        Result = projectR,
                        UnitPlanWeight.UnitPlanWeight,
                        UnitActualWeight = projectA,
                    }).ToList();
                int successRateValue = Convert.ToInt32(projectA);

                var result = (from x in data
                    select new testQuta
                    {
                        lvl_code = x.lvl_code,
                        lvl_name = x.lvl_name,
                        lvl_number = x.lvl_number,
                        lvl_percent = x.lvl_percent,
                        UnitPlanWeight = x.UnitPlanWeight,
                        UnitActualWeight = x.UnitActualWeight,
                        empCount = x.empCount,
                        UnitWeight = (float)x.UnitWeight,
                        plannedQuota = x.UnitWeight * x.empCount / 100,
                        unitEmpQouta =
                            Convert.ToDouble((x.empCount * (x.UnitPlanWeight / 100.00)).ToString("#,##0.00")),
                        UnitEmp = x.UnitEmp,
                        unitEmpAfterAssessment = 0,
                        QuotaType = x.lvl_number == 3 ? 2 : 1,
                        Result = x.Result
                    }).ToList();

                var lst = new List<testQuta>();
                bool first = true;
                var limitLeft = 0;

                if (successRateValue > 60)
                {
                    //fill Planned Quota	
                    var normalList = result.Where(a => a.QuotaType == 1).OrderByDescending(a => a.lvl_number).ToList();
                    foreach (var x in normalList)
                    {
                        var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;
                        limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1).Sum(a => a.unitEmpAfterAssessment);
                        if (successRateValue > 94)
                        {
                            //add with ceiling 
                            x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                                ? Convert.ToInt32(Math.Round(unitEmpQouta))
                                : limitLeft;
                            lst.Add(x);
                        }
                        else
                        {
                            //add with round when 0.5 or more in 4 and 5 level 
                            if (successRateValue > 80 && (x.lvl_number == 5 || x.lvl_number == 4))
                                x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                                    ? Convert.ToInt32(Math.Round(unitEmpQouta))
                                    : limitLeft;
                            else
                                x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                                    ? Convert.ToInt32(x.unitEmpQouta)
                                    : limitLeft;

                            lst.Add(x);
                        }
                    }

                    var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                    foreach (var x in remainingList)
                    {
                        limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft;
                        lst.Add(x);
                    }
                }

                else
                {
                    //fill Planned Quota	

                    var normalList = result.Where(a => a.QuotaType == 1).OrderBy(a => a.lvl_number).ToList();
                    foreach (var x in normalList)
                    {
                        var unitEmpQouta = (int)(x.unitEmpQouta * 10) / 10.0;

                        limitLeft = x.UnitEmp - lst.Where(a => a.QuotaType == 1).Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft > x.unitEmpQouta
                            ? Convert.ToInt32(Math.Round(unitEmpQouta))
                            : limitLeft;

                        lst.Add(x);
                    }

                    var remainingList = result.Where(a => a.QuotaType == 2).ToList();

                    foreach (var x in remainingList)
                    {
                        limitLeft = x.UnitEmp - lst.Sum(a => a.unitEmpAfterAssessment);


                        x.unitEmpAfterAssessment = limitLeft;
                        lst.Add(x);
                    }
                }

                lst.AddRange(result.Where(a => a.QuotaType == 0));


                if (result != null)
                {
                    return Ok(new
                        { Data = lst.OrderBy(x => x.lvl_number), IsError = false, ErrorMessage = string.Empty });
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


        private IOrderedEnumerable<testQuta> GetUnitEmployeeQoutaInternal(UnitEntity unit, int year, int companyID,
            double? successRate)
        {
            try
            {
                //  var unitsList = db.UnitCollection.ToList();
                var projectsList = db.ProjectsCollection.Where(s => s.Year == year && s.CompanyId == companyID)
                    .ToList();
                var objectivesList = db.StratigicObjectivesCollection.AsNoTracking()
                    .Where(s => s.Year == year && s.CompanyId == companyID).ToList();


                var UnitPlanWeight = projectsList
                    .Where(s => s.UnitId == unit.ID && s.Year == year && s.CompanyId == companyID)
                    .Sum(s => s.Weight * objectivesList.Where(a => a.id == s.StratigicObjectiveId && a.Year == year)
                        .Select(a => a.Weight / 100.00).DefaultIfEmpty(1.00).FirstOrDefault());

                var UnitEmp = db.EmployeesCollection.Where(s => s.UNIT_ID == unit.ID).Count();

                var projectWeightSum = projectsList.Sum(s => s.Weight);
                var projectResultSum = projectsList.Sum(s => s.Result);
                var resultSum = projectsList.Where(s => s.UnitId == unit.ID).Sum(s => s.Result);
                var unitProjectCount = projectsList.Where(s => s.UnitId == unit.ID).Count();
                var unitActualWeight = unitProjectCount != 0 ? resultSum / unitProjectCount : 0;

                var companyEmployee = db.EmployeesCollection.Where(s => s.COMPANY_ID == companyID).Count();


                var data = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == year && x.company_id == companyID).AsNoTracking()
                    .Select(x => new
                    {
                        x.lvl_name,
                        x.lvl_number,
                        x.lvl_code,
                        x.lvl_percent,
                        UnitEmp = UnitEmp,
                        empCount = ((x.lvl_percent / 100) * companyEmployee * 1.00),
                        UnitWeight = projectWeightSum,
                        Result = projectResultSum,
                        UnitPlanWeight = UnitPlanWeight,
                        UnitActualWeight = unitActualWeight,
                    }).ToList();

                var result = (from x in data
                    select new
                    {
                        x.lvl_code,
                        x.lvl_name,
                        x.lvl_number,
                        x.lvl_percent,
                        x.UnitPlanWeight,
                        x.UnitActualWeight,
                        x.empCount,
                        x.UnitWeight,
                        plannedQuota = x.UnitWeight * x.empCount / 100,
                        unitEmpQouta =
                            Convert.ToDouble((x.empCount * (x.UnitPlanWeight / 100.00)).ToString("#,##0.00")),
                        x.UnitEmp,
                        unitEmpAfterAssessment =
                            (db.PerformancelevelsQuotaCollection.Where(s => s.CompanyId == companyID && s.YearId == year
                                    && s.LevelNumber == x.lvl_number
                                    && (x.UnitActualWeight) >= s.FromPercentage &&
                                    (x.UnitActualWeight) <= s.ToPercentage).AsNoTracking()
                                .Select(s => s.QuotaType).DefaultIfEmpty(0)).FirstOrDefault(),
                        x.Result
                    }).OrderBy(x => x.lvl_number);
                var remaing = data.FirstOrDefault().UnitEmp;
                var countOfrows = data.Count();
                var lst = new List<testQuta>();
                List<int> lstOfRem = new List<int>();
                if (successRate <= 50)
                {
                    var Column1 = Convert.ToInt32(Math.Ceiling(result.ToList()[0].unitEmpQouta));
                    var Column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
                    remaing = remaing - Column1;
                    remaing = remaing - Column2;
                    var column3 = remaing;

                    var Column4 = 0;
                    var Column5 = 0;
                    lstOfRem = new List<int> { Column1, Column2, column3, Column4, Column5 };
                }
                else if (successRate >= 51 && successRate <= 70)
                {
                    var Column1 = Convert.ToInt32(Math.Ceiling(result.ToList()[0].unitEmpQouta));
                    var Column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
                    remaing = remaing - Column1;
                    remaing = remaing - Column2;
                    var column3 = remaing;

                    var Column4 = 0;
                    var Column5 = 0;
                    lstOfRem = new List<int> { Column1, Column2, column3, Column4, Column5 };
                }
                else if (successRate >= 91 && successRate <= 100)
                {
                    var column1 = 0;
                    var column2 = 0;
                    var column3 = 0;
                    var Remainingcolumn2 = 0;
                    var Column4 = Convert.ToInt32(Math.Ceiling(result.ToList()[3].unitEmpQouta));
                    var Column5 = Convert.ToInt32(Math.Round(result.ToList()[4].unitEmpQouta));
                    remaing = remaing - Column4;
                    remaing = remaing - Column5;
                    if (remaing > result.ToList()[2].unitEmpQouta)
                    {
                        column2 = Convert.ToInt32(Math.Ceiling(result.ToList()[1].unitEmpQouta));
                    }

                    column3 = remaing - column2;
                    lstOfRem = new List<int> { column1, column2, column3, Column4, Column5 };
                }
                else if (successRate >= 101 && successRate <= 500)
                {
                    var column1 = 0;
                    var column2 = 0;
                    var column3 = 0;
                    var Column5 = Convert.ToInt32(Math.Ceiling(result.ToList()[4].unitEmpQouta));
                    var Column4 = Convert.ToInt32(Math.Ceiling(result.ToList()[3].unitEmpQouta));
                    if (Column4 >= remaing)
                    {
                        Column4 = remaing - Column5;
                    }

                    remaing = remaing - Column4;
                    remaing = remaing - Column5;
                    if (remaing < 0) remaing = 0;
                    column3 = remaing;
                    lstOfRem = new List<int> { column1, column2, column3, Column4, Column5 };
                }

                int forCount = 0;
                foreach (var x in result)
                {
                    var unitEmpAfterAssessment = 0;
                    var emp = Convert.ToInt32(x.unitEmpQouta);
                    var lvl = x.unitEmpAfterAssessment;
                    var lxlNumber = x.lvl_number;
                    if (lvl == 1)
                    {
                        if (remaing > emp)
                        {
                            unitEmpAfterAssessment = emp;
                            remaing = remaing - emp;
                        }
                        else if (remaing != 0)
                        {
                            unitEmpAfterAssessment = remaing;
                            remaing = 0;
                        }
                    }
                    else if (lvl == 2 && remaing != 0)
                    {
                        unitEmpAfterAssessment = remaing;
                    }

                    unitEmpAfterAssessment = lstOfRem[forCount];
                    lst.Add(new testQuta
                    {
                        //  unitEmpAfterAssessment = 1
                        lvl_code = x.lvl_code,
                        lvl_name = x.lvl_name,
                        lvl_number = x.lvl_number,
                        lvl_percent = x.lvl_percent,
                        UnitPlanWeight = x.UnitPlanWeight,
                        UnitActualWeight = x.UnitActualWeight,
                        empCount = x.empCount,
                        UnitWeight = x.UnitWeight,
                        plannedQuota = Convert.ToDouble(x.plannedQuota.ToString("#,##0.00")),
                        unitEmpQouta = x.unitEmpQouta,
                        unitEmpAfterAssessment = x.unitEmpAfterAssessment,
                        UnitEmp = x.UnitEmp,
                        Result = x.Result,
                        FTEQouta = Convert.ToDouble((x.unitEmpAfterAssessment - x.unitEmpQouta).ToString("#,##0.00"))
                    });
                    forCount = forCount + 1;
                }

                if (result != null)
                {
                    return lst.OrderBy(x => x.lvl_number);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /**/

        [HttpGet]
        public IHttpActionResult GetActualUnitEmployeeQouta(int year, int companyID, double? successRate)
        {
            try
            {
                var allData = new List<testQuta>();
                var units = db.UnitCollection.AsNoTracking();
                foreach (var item in units)
                {
                    var unitResult = GetUnitEmployeeQoutaInternal(item, year, companyID, successRate);
                    if (unitResult != null)
                        allData.AddRange(unitResult);
                }

                var result = allData.GroupBy(a => a.lvl_number).Select(x => new testQuta
                {
                    lvl_code = x.FirstOrDefault().lvl_code,
                    lvl_name = x.FirstOrDefault().lvl_name,
                    lvl_number = x.FirstOrDefault().lvl_number,
                    lvl_percent = x.FirstOrDefault().lvl_percent,
                    UnitPlanWeight = x.FirstOrDefault().UnitPlanWeight,
                    UnitActualWeight = x.FirstOrDefault().UnitActualWeight,
                    empCount = x.FirstOrDefault().empCount,
                    UnitWeight = x.FirstOrDefault().UnitWeight,
                    plannedQuota = Convert.ToDouble(x.FirstOrDefault().plannedQuota.ToString("#,##0.00")),
                    unitEmpQouta = x.FirstOrDefault().unitEmpQouta,
                    unitEmpAfterAssessment = x.FirstOrDefault().unitEmpAfterAssessment,
                    UnitEmp = x.FirstOrDefault().UnitEmp,
                    Result = x.FirstOrDefault().Result,
                    FTEQouta = Convert.ToDouble(
                        (x.FirstOrDefault().unitEmpAfterAssessment - x.FirstOrDefault().unitEmpQouta).ToString(
                            "#,##0.00"))
                });


                return Ok(new
                    { Data = result.OrderBy(x => x.lvl_number), IsError = false, ErrorMessage = string.Empty });
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

        public class testQuta
        {
            public string lvl_code { get; set; }
            public string lvl_name { get; set; }
            public long lvl_number { get; set; }
            public long lvl_percent { get; set; }
            public double empCount { get; set; }
            public double UnitPlanWeight { get; set; }
            public float UnitWeight { get; set; }
            public double plannedQuota { get; set; }
            public int unitEmpAfterAssessment { get; set; }
            public int QuotaType { get; set; }
            public int QuotaDirection { get; set; }

            public float? UnitActualWeight { get; set; }
            public int UnitEmp { get; set; }
            public float? Result { get; set; }
            public double unitEmpQouta { get; set; }
            public double FTEQouta { get; set; }
        }

        [HttpGet]
        public IHttpActionResult SaveProjectActionPlans(
            int projectId,
            decimal? action_cost,
            string username,
            string action_date,
            string action_name,
            string action_notes,
            string action_req,
            float action_weight,
            int emp_id,
            int project_kpi_id)
        {
            try
            {
                ProjectActionPlanEntity projActionPlan = new ProjectActionPlanEntity();
                projActionPlan.action_cost = action_cost;
                projActionPlan.action_date = Convert.ToDateTime(action_date);
                projActionPlan.action_name = action_name;
                projActionPlan.action_notes = action_notes;
                projActionPlan.action_req = action_req;
                projActionPlan.action_weight = action_weight;
                projActionPlan.CreatedBy = username;
                projActionPlan.CreatedDate = DateTime.Now;
                projActionPlan.emp_id = emp_id;
                projActionPlan.projectID = projectId;
                projActionPlan.project_kpi_id = project_kpi_id;


                db.Entry(projActionPlan).State = System.Data.Entity.EntityState.Added;
                db.ProjectActionPlanCollection.Add(projActionPlan);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult UpdateProjectActionPlans(
            int id,
            int projectId,
            decimal action_cost,
            string username,
            string action_date,
            string action_name,
            string action_notes,
            string action_req,
            float action_weight,
            int emp_id,
            int project_kpi_id)
        {
            try
            {
                ProjectActionPlanEntity projActionPlan =
                    db.ProjectActionPlanCollection.Where(x => x.ID == id).FirstOrDefault();

                projActionPlan.action_cost = action_cost;
                projActionPlan.action_date = Convert.ToDateTime(action_date);
                projActionPlan.action_name = action_name;
                projActionPlan.action_notes = action_notes;
                projActionPlan.action_req = action_req;
                projActionPlan.action_weight = action_weight;
                projActionPlan.CreatedBy = username;
                projActionPlan.CreatedDate = DateTime.Now;
                projActionPlan.emp_id = emp_id;
                projActionPlan.projectID = projectId;
                projActionPlan.project_kpi_id = project_kpi_id;


                db.Entry(projActionPlan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult DeleteActionPlan(int id)
        {
            try
            {
                var actionPlan = db.ProjectActionPlanCollection.Where(e => e.ID == id).FirstOrDefault();
                if (actionPlan != null)
                {
                    db.ProjectActionPlanCollection.Remove(actionPlan);
                    db.SaveChanges();
                }
                else
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Cannot Delete Action Plan." });
                }

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetProjectActionPlansByID(long id)
        {
            try
            {
                float max = 0;
                var list = db.ProjectActionPlanCollection.Where(x => x.projectID == id).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.action_weight);
                }

                var actionPlan = db.ProjectActionPlanCollection.Where(e => e.ID == id).Select(e => new
                {
                    e.ID,
                    e.action_cost,
                    e.action_date,
                    e.action_name,
                    e.action_notes,
                    e.action_req,
                    e.action_weight,
                    e.emp_id,
                    e.projectID,
                    e.project_kpi_id,
                    max = max
                }).ToList();

                var result = actionPlan.Select(e => new
                {
                    e.ID,
                    e.action_cost,
                    action_date = e.action_date.ToString("dd/MM/yyyy"),
                    e.action_name,
                    e.action_notes,
                    e.action_req,
                    e.action_weight,
                    e.emp_id,
                    e.projectID,
                    e.project_kpi_id,
                    e.max
                }).FirstOrDefault();

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

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult GetProjectPlannedCost(long id, int? planID)
        {
            try
            {
                var plannedCost = db.ProjectsCollection.Where(e => e.ID == id).Select(e => new
                {
                    e.ID,
                    e.PlannedCost,
                    actionPlansCost = db.ProjectActionPlanCollection.Where(x => x.projectID == id)
                        .Sum(x => x.action_cost),
                    planCost = (planID == null)
                        ? 0
                        : db.ProjectActionPlanCollection.Where(x => x.ID == planID).FirstOrDefault().action_cost
                }).FirstOrDefault();

                if (plannedCost != null)
                {
                    return Ok(new { Data = plannedCost, IsError = false, ErrorMessage = string.Empty });
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


        [HttpGet]
        public IHttpActionResult getActionPlanRemainingWeight(int projectID, int projKPI)
        {
            try
            {
                float max = 0;
                var list = db.ProjectActionPlanCollection
                    .Where(x => x.projectID == projectID && x.project_kpi_id == projKPI).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.action_weight);
                }

                return Ok(new { Data = max, IsError = false, ErrorMessage = string.Empty });
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

        #endregion
    }
}