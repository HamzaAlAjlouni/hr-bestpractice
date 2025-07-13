using HR.Entities;
using HR.Entities.Admin;
using HR.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HR.Busniess;
using Newtonsoft.Json;
using System.Web;
using System.Transactions;
using System.Data.Entity;
using System.Configuration;
using HR.APIs.Untilities;
using HR.Entities.Models;

namespace HR.APIs.Controllers
{
    public class StratigicObjectivesController : ApiController
    {
        HRContext db;

        public StratigicObjectivesController()
        {
            db = new HRContext();
        }


        #region stratigic Objective Methods

        [HttpGet]
        public IHttpActionResult SaveStratigicObjective(string name, long companyId, float weight, int year,
            string description, string CreatedBy, string LanguageCode, int bsc)
        {
            try
            {
                var Weights = (from obj in db.StratigicObjectivesCollection
                        where
                            (obj.CompanyId == companyId) &&
                            (obj.Year == year) &&
                            (bsc == 1 || obj.bsc == bsc)
                        select obj.Weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                //var IsExist = (from obj in db.StratigicObjectivesCollection
                //               where
                //               (obj.CompanyId == companyId) &&
                //               (obj.Year == year) && (obj.Name==name || obj.Name2==name)
                //               select obj
                //    ).Count() >0;
                //if (IsExist)
                //{
                //    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "This Stratigic Objective is already exist" });
                //}
                StratigicObjectivesEntity stratigicObjectiveEntity = new StratigicObjectivesEntity();

                stratigicObjectiveEntity.Code = "";
                stratigicObjectiveEntity.Name = name;
                stratigicObjectiveEntity.CompanyId = companyId;
                stratigicObjectiveEntity.Order = 1;
                stratigicObjectiveEntity.Weight = weight;
                stratigicObjectiveEntity.Year = year;
                stratigicObjectiveEntity.Description = description;
                stratigicObjectiveEntity.bsc = bsc;

                if (LanguageCode == "ar")
                    stratigicObjectiveEntity.Name2 = name;
                else
                    stratigicObjectiveEntity.Name = name;

                stratigicObjectiveEntity.CreatedBy = CreatedBy;
                stratigicObjectiveEntity.CreatedDate = DateTime.Now;

                db.Entry(stratigicObjectiveEntity).State = System.Data.Entity.EntityState.Added;
                db.StratigicObjectivesCollection.Add(stratigicObjectiveEntity);
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
        public IHttpActionResult UpdateStratigicObjective(long id, string name, long companyId,
            float weight, int year, string description, string ModifiedBy, string LanguageCode, int bsc)
        {
            try
            {
                var Weights = (from obj in db.StratigicObjectivesCollection
                        where
                            (obj.CompanyId == companyId) &&
                            (obj.Year == year) &&
                            (bsc == 1 || obj.bsc == bsc) && obj.id != id
                        select obj.Weight
                    ).DefaultIfEmpty().Sum();
                if ((Weights + weight) > 100)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                    });
                }

                StratigicObjectivesEntity stratigicObjectiveEntity =
                    db.StratigicObjectivesCollection.Where(x => x.id == id).FirstOrDefault();

                if (stratigicObjectiveEntity != null)
                {
                    stratigicObjectiveEntity.Code = "";
                    stratigicObjectiveEntity.Name = name;
                    stratigicObjectiveEntity.CompanyId = companyId;
                    stratigicObjectiveEntity.Order = 1;
                    stratigicObjectiveEntity.Weight = weight;
                    stratigicObjectiveEntity.Year = year;
                    stratigicObjectiveEntity.Description = description;
                    stratigicObjectiveEntity.bsc = bsc;

                    if (LanguageCode == "ar")
                        stratigicObjectiveEntity.Name2 = name;
                    else
                        stratigicObjectiveEntity.Name = name;
                    stratigicObjectiveEntity.ModifiedBy = ModifiedBy;
                    stratigicObjectiveEntity.ModifiedDate = DateTime.Now;

                    db.Entry(stratigicObjectiveEntity).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult SearchAllStratigicObjectives(long companyId, int year, string languageCode,
            int? bsc = null)
        {
            try
            {
                if (year <= 0 || companyId == -1)
                {
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                var stratigicObjectives = (from obj in db.StratigicObjectivesCollection
                        where
                            (obj.CompanyId == companyId) &&
                            (obj.Year == year) &&
                            (bsc == null || obj.bsc == bsc)
                        orderby obj.id descending
                        select new
                        {
                            serial = 1,
                            obj.id,
                            name = ((languageCode == "ar") ? obj.Name2 : obj.Name),
                            obj.CompanyId,
                            KpiSCount = db.ObjectiveKpiCollection.Count(a => a.objective_id == obj.id),
                            obj.Code,
                            obj.CreatedBy,
                            obj.CreatedDate,
                            obj.ModifiedBy,
                            obj.ModifiedDate,
                            obj.Order,
                            obj.ResultPercentage,
                            obj.ResultWeightPercentage,
                            obj.Weight,
                            obj.Year,
                            BSC_Name =
                                (obj.bsc == 1) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCFinancialOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 2) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCCustomersOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 3) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCInternalProcessOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 4) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCLearninggrowthOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() : "",


                            WeightDesc = Math.Round(obj.Weight, 0) + " " + "%",
                            nameShortcut = ((languageCode == "ar")
                                ? (obj.Name2.Length > 20 ? obj.Name2.Substring(0, 30) + "..." : obj.Name2)
                                : (obj.Name.Length > 20 ? obj.Name.Substring(0, 30) + "..." : obj.Name)),
                        }
                    ).ToList().OrderBy(x => x.id).Select((obj, i) => new
                    {
                        serial = i + 1,
                        obj.id,
                        obj.name,
                        obj.CompanyId,
                        obj.Code,
                        obj.KpiSCount,
                        obj.CreatedBy,
                        obj.CreatedDate,
                        obj.ModifiedBy,
                        obj.ModifiedDate,
                        obj.Order,
                        obj.ResultPercentage,
                        obj.ResultWeightPercentage,
                        obj.Weight,
                        obj.Year,
                        obj.BSC_Name,
                        obj.WeightDesc,
                        obj.nameShortcut
                    });

                return Ok(new { Data = stratigicObjectives, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetPlannedWeightForEachObjective(long companyId, int? branchId, int yearId,
            int? unitId, string languageCode, int? projectId, int? bsc = null)
        {
            try
            {
                var resultList = new List<dynamic>();
                //Modified by yousef sleit
                unitId = (unitId.HasValue ? unitId : -1);
                projectId = (projectId.HasValue ? projectId : -1);
                using (HRContext xxx = new HRContext())
                {
                    if (yearId <= 0 || companyId == -1)
                    {
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }


                    var stratigicObjectives = (from obj in db.StratigicObjectivesCollection
                            where
                                (obj.CompanyId == companyId) &&
                                (obj.Year == yearId) &&
                                (bsc == null || obj.bsc == bsc)
                            orderby obj.id descending
                            select new
                            {
                                obj.id,
                                obj.Name
                            }
                        ).ToList().OrderBy(x => x.id).Select((obj, i) => new
                        {
                            serial = i + 1,
                            obj.id,
                            obj.Name
                        });
                    //get  project calculation method
                    var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                    var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;

                    var PrjKpis = (from kpi in db.ObjectiveKpiCollection.Where(s => s.is_obj_kpi == 2) select kpi)
                        .ToList().AsQueryable();

                    var KPIs = (from kpi in PrjKpis
                        select new
                        {
                            kpi.ID,
                            kpi.Weight,
                            kpi.objective_id,
                            KPI_name = kpi.Name,
                            kpi.Target,
                            kpi.BetterUpDown,
                            kpi.result,
                        }).ToList().AsQueryable();


                    foreach (var item in stratigicObjectives)
                    {
                        var projects = (
                                from prj in db.ProjectsCollection
                                where (
                                    ((item.id != 0) ? prj.StratigicObjectiveId == item.id : 1 == 1)
                                    && ((unitId != -1) ? prj.UnitId == unitId : prj.UnitId == prj.UnitId)
                                    && ((projectId != -1) ? prj.ID == projectId : prj.ID == prj.ID)
                                    && ((companyId != 0) ? prj.CompanyId == companyId : prj.CompanyId == prj.CompanyId)
                                    && ((branchId != null) ? prj.BranchId == branchId : prj.BranchId == prj.BranchId)
                                )
                                join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on prj
                                    .StratigicObjectiveId equals so.id
                                join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI
                                    equals ok.ID.ToString()
                                join unit in db.UnitCollection on prj.UnitId equals unit.ID
                                select new
                                {
                                    prj,
                                    so,
                                    ok,
                                    unit
                                }
                            ).AsEnumerable()
                            .Select(result => new
                                {
                                    plannedStratigy = calculationWeight == 1
                                        ? (result.prj.Weight) * (result.ok.Weight / 100) * (result.so.Weight / 100)
                                        : (result.prj.Weight) * (result.so.Weight / 100),
                                    ActualWeights =
                                        KPIs
                                            .Where(a => a.objective_id == result.prj.ID)
                                            .Sum(d => d.Weight *
                                                      (d.BetterUpDown == 2
                                                          ? (d.result == 0 ? 1.20 :
                                                              d.Target / d.result > 1.20 ? 1.20 : d.Target / d.result)
                                                          : (d.result / d.Target > 1.20 ? 1.20 : d.result / d.Target)
                                                      )
                                            )
                                        * (calculationWeight == 1
                                            ? (result.prj.Weight) * (result.ok.Weight / 100) * (result.so.Weight / 100)
                                            : (result.prj.Weight) * (result.so.Weight / 100)) / 100,
                                    result.prj.p_type
                                }
                            ).ToList().AsQueryable();

                        var xvalue = (from res in projects
                            select new
                            {
                                res.plannedStratigy,
                                res.ActualWeights,
                            }).ToList();


                        var test = new
                        {
                            Id = item.id, Name = item.Name,
                            PlannedWeights = xvalue.Sum(x => x.plannedStratigy),
                            ActualWeights = xvalue.Sum(x => x.ActualWeights)
                        };


                        resultList.Add(test);
                    }


                    return Ok(new { Data = resultList, IsError = false, ErrorMessage = string.Empty });
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


        [HttpGet]
        public IHttpActionResult GetPlannedWeightForAll(long companyId, int? branchId, int yearId, int? unitId,
            int stratigicObjectiveId, string languageCode, int? projectId)
        {
            try
            {
                //Modified by yousef sleit
                unitId = (unitId.HasValue ? unitId : -1);
                unitId = -1;
                projectId = (projectId.HasValue ? projectId : -1);
                using (HRContext DB = new HRContext())
                {
                    var PlannedUnitWeights = (from obj in db.ProjectsCollection
                            where
                                (obj.CompanyId == companyId) &&
                                (stratigicObjectiveId <= 0 || obj.StratigicObjectiveId == stratigicObjectiveId) &&
                                (unitId <= 0 || obj.UnitId == unitId)
                            select obj.Weight
                        ).Sum();
                    var PlannedWeights = (from obj in db.StratigicObjectivesCollection
                            where
                                (obj.CompanyId == companyId) &&
                                (stratigicObjectiveId <= 0 || obj.id == stratigicObjectiveId)
                            select obj.Weight
                        ).Sum();
                    //get  project calculation method
                    var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                    var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;

                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != -1) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((projectId != -1) ? p.ID == projectId : p.ID == p.ID)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != null) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI equals ok
                            .ID
                            .ToString()
                        select new
                        {
                            plannedStratigy = calculationWeight == 1
                                ? (prj.Weight) * (ok.Weight / 100) * (so.Weight / 100)
                                : (prj.Weight) * (so.Weight / 100),
                            PlannedWeights = stratigicObjectiveId > 0 || unitId > 0
                                ? (PlannedUnitWeights / 100) * (PlannedWeights)
                                : 0,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.WeightFromObjective,
                            projectPercentageFromEntireStratigic = (
                                prj.ResultWeightPercentageFromObjectives *
                                db.StratigicObjectivesCollection
                                    .FirstOrDefault(a => a.id == prj.StratigicObjectiveId).Weight) / 100,

                            prj.UnitId,
                            prj.PlannedCost,
                          
                        }
                    ).ToList().AsQueryable();
                    var result = (from res in projects
                        select new
                        {
                            res.plannedStratigy,
                            res.PlannedWeights,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.ResultWeightPercentageFromObjectives,
                            res.projectPercentageFromEntireStratigic,
                        }).ToList();
                    return Ok(new
                    {
                        Data = new
                        {
                            PlannedWeights = result.Sum(x => x.plannedStratigy),
                            ActualWeights = result.Sum(x => x.projectPercentageFromEntireStratigic)
                        },
                        IsError = false, ErrorMessage = string.Empty
                    });
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

        [HttpGet]
        public IHttpActionResult getStratigicObjectivebyID(long id, string languageCode)
        {
            try
            {
                float max = 0;
                StratigicObjectivesEntity so = db.StratigicObjectivesCollection.Where(x => x.id == id).FirstOrDefault();
                if (so != null)
                {
                    List<StratigicObjectivesEntity> stratigicObjectives = db.StratigicObjectivesCollection
                        .Where(x => x.CompanyId == so.CompanyId && x.Year == so.Year).ToList();
                    if (stratigicObjectives != null)
                    {
                        max = 100 - stratigicObjectives.Sum(x => x.Weight) + so.Weight;
                    }
                }

                var stratigicObjective = (
                    from obj in db.StratigicObjectivesCollection.Where(x => x.id == id)
                    select new
                    {
                        obj.id,
                        name = ((languageCode == "ar") ? obj.Name2 : obj.Name),
                        obj.CompanyId,
                        obj.Code,
                        obj.CreatedBy,
                        obj.CreatedDate,
                        obj.ModifiedBy,
                        obj.ModifiedDate,
                        obj.Order,
                        obj.ResultPercentage,
                        obj.ResultWeightPercentage,
                        obj.Weight,
                        obj.Year,
                        max = max,
                        obj.bsc
                    }
                ).ToList().AsQueryable().FirstOrDefault();


                if (stratigicObjective != null)
                {
                    return Ok(new { Data = stratigicObjective, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getObjectivesRemainingWeight(int company, int year)
        {
            try
            {
                float max = 0;
                List<StratigicObjectivesEntity> list = db.StratigicObjectivesCollection
                    .Where(x => x.Year == year && x.CompanyId == company).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.Weight);
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

        [HttpGet]
        public IHttpActionResult getObjectiveKPIRemainingWeight(int ObjID, int isObjKpi)
        {
            try
            {
                float max = 0;
                var list = db.ObjectiveKpiCollection.Where(x => x.objective_id == ObjID && x.is_obj_kpi == isObjKpi)
                    .ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.Weight);
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


        [HttpGet]
        public IHttpActionResult DeleteStratigicObjectiveByID(long id)
        {
            try
            {
                var stratigicObjective = db.StratigicObjectivesCollection.Where(x => x.id == id).FirstOrDefault();
                if (stratigicObjective != null)
                {
                    db.StratigicObjectivesCollection.Remove(stratigicObjective);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult RemoveStratigicObjectivesByIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.StratigicObjectivesCollection.RemoveRange(db.StratigicObjectivesCollection
                    .Where(x => ids.Contains(x.id)).ToList());
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

        #endregion

        #region Project Documents

        /// <summary>
        /// GetEvidanceByObjectiveKPI_id
        /// </summary>
        /// <param name="id">ObjectiveKPI_id</param> 
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetEvidanceByObjectiveKPI_id(long id)
        {
            try
            {
                var projEvidence = db.ProjectEvidenceCollection.Where(e => e.ObjectiveKPI_id == id).ToList();


                if (projEvidence != null)
                {
                    return Ok(new { Data = projEvidence, IsError = false, ErrorMessage = string.Empty });
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

        /// <summary>
        /// SaveEvidanceForObjectiveKPI
        /// </summary>
        /// <param name="ObjectiveKPI_id">ObjectiveKPI_id</param>
        /// <param name="documentName">Evidance Name</param>
        /// <param name="CreatedBy">CreatedBy</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SaveEvidanceForObjectiveKPI(int ObjectiveKPI_id, string documentName, string username)
        {
            try
            {
                ProjectEvidenceEntity projEvidence = new ProjectEvidenceEntity();
                projEvidence.ObjectiveKPI_id = ObjectiveKPI_id;
                projEvidence.doc_name = documentName;
                projEvidence.CreatedBy = username;
                projEvidence.CreatedDate = DateTime.Now;

                db.Entry(projEvidence).State = System.Data.Entity.EntityState.Added;
                db.ProjectEvidenceCollection.Add(projEvidence);
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
        public IHttpActionResult UpdateEvidance(string EvidanceName, int Evidance_id, string UpdatedBy)
        {
            try
            {
                var projEvidence = db.ProjectEvidenceCollection.Where(x => x.ID == Evidance_id).FirstOrDefault();
                if (projEvidence != null)
                {
                    projEvidence.doc_name = EvidanceName;
                    projEvidence.ModifiedBy = UpdatedBy;
                    projEvidence.ModifiedDate = DateTime.Now;

                    db.Entry(projEvidence).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
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
        public IHttpActionResult DeleteEvidanceForObjectiveKPI(int Evidance_id)
        {
            try
            {
                var projEvidence = db.ProjectEvidenceCollection.Where(e => e.ID == Evidance_id).FirstOrDefault();
                db.ProjectEvidenceCollection.Remove(projEvidence);
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
        public IHttpActionResult GetProjectDocuments(long id, string languageCode, string IsObjective)
        {
            try
            {
                var projEvidence = db.ProjectEvidenceCollection
                    .Where(e => (IsObjective == "0") ? e.ProjectId == id : e.objective_id == id).ToList();


                if (projEvidence != null)
                {
                    return Ok(new { Data = projEvidence, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult SaveProjectDocuments(int projectId, string documentName, string username,
            string IsObjective)
        {
            try
            {
                ProjectEvidenceEntity projEvidence = new ProjectEvidenceEntity();
                projEvidence.ProjectId = (IsObjective == "0") ? projectId : (Nullable<int>)null;
                projEvidence.objective_id = (IsObjective == "1") ? projectId : (Nullable<int>)null;
                projEvidence.doc_name = documentName;
                projEvidence.CreatedBy = username;
                projEvidence.CreatedDate = DateTime.Now;

                db.Entry(projEvidence).State = System.Data.Entity.EntityState.Added;
                db.ProjectEvidenceCollection.Add(projEvidence);
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
        public IHttpActionResult UpdateProjectDocuments(int id, int projectId, string documentName, string username,
            string IsObjective)
        {
            try
            {
                ProjectEvidenceEntity projEvidence =
                    db.ProjectEvidenceCollection.Where(e => e.ID == id).FirstOrDefault();
                if (projEvidence != null)
                {
                    projEvidence.ProjectId = (IsObjective == "0") ? projectId : (Nullable<int>)null;
                    projEvidence.objective_id = (IsObjective == "1") ? projectId : (Nullable<int>)null;

                    projEvidence.doc_name = documentName;
                    projEvidence.CreatedBy = username;
                    projEvidence.CreatedDate = DateTime.Now;

                    db.Entry(projEvidence).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
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
        public IHttpActionResult DeleteProjectDocuments(int id)
        {
            try
            {
                ProjectEvidenceEntity projEvidence = db.ProjectEvidenceCollection
                    .Where(e => e.ID == id && string.IsNullOrEmpty(e.FileName)).FirstOrDefault();
                if (projEvidence != null)
                {
                    db.ProjectEvidenceCollection.Remove(projEvidence);
                    db.SaveChanges();
                }
                else
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "Cannot Delete Document - Already Evident Provided"
                    });
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
        public IHttpActionResult GetProjectDocumentByID(long id)
        {
            try
            {
                var projEvidence = db.ProjectEvidenceCollection.Where(e => e.ID == id).Select(e => new
                {
                    e.ID,
                    e.doc_name
                }).FirstOrDefault();

                if (projEvidence != null)
                {
                    return Ok(new { Data = projEvidence, IsError = false, ErrorMessage = string.Empty });
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

        #endregion

        #region Projects Methods

        [HttpGet]
        public IHttpActionResult GetProjectsCategories()
        {
            var cateogries = from cats in db.ProjectCategoryCollection
                select new
                {
                    cats.ID,
                    cats.Name
                };

            return Ok(new { Data = cateogries.ToList(), IsError = false, ErrorMessage = "" });
        }

        [HttpGet]
        public IHttpActionResult SaveProject([FromUri] ProjectModel ProjectEntity)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ProjectEntity == null)
                    {
                        return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Bad Request" });
                    }
                    else
                    {
                        ProjectEntity Project = new ProjectEntity
                        {
                            Year = ProjectEntity.Year,
                            BranchId = ProjectEntity.BranchId,
                            Category = ProjectEntity.Category,
                            Code = "1",
                            CompanyId = ProjectEntity.CompanyId,
                            CreatedBy = ProjectEntity.CreatedBy,
                            CreatedDate = DateTime.Now,
                            Description = ProjectEntity.Description,
                            KPI = ProjectEntity.KPI,
                            KPICycleId = ProjectEntity.KPICycleId,
                            KPITypeId = ProjectEntity.BranchId,
                            Name = (ProjectEntity.languageCode == "en" ? ProjectEntity.Name : ""),
                            Name2 = (ProjectEntity.languageCode == "ar" ? ProjectEntity.Name : ""),
                            Order = 1,
                            StratigicObjectiveId = ProjectEntity.StratigicObjectiveId,
                            UnitId = ProjectEntity.UnitId,
                            Weight = ProjectEntity.Weight,
                            Target = ProjectEntity.Target,
                            Q1_Target = ProjectEntity.Q1_Target,
                            Q2_Target = ProjectEntity.Q2_Target,
                            Q3_Target = ProjectEntity.Q3_Target,
                            Q4_Target = ProjectEntity.Q4_Target,
                            PlannedCost = ProjectEntity.PlannedCost,
                            p_type = ProjectEntity.p_type
                        };
                        if (string.IsNullOrEmpty(Project.Name))
                            Project.Name = Project.Name2;
                        Project.planned_status = 1;
                        db.Entry(Project).State = System.Data.Entity.EntityState.Added;
                        db.ProjectsCollection.Add(Project);
                        db.SaveChanges();
                        // add approval history
                        var projectApprovalHistoryEntity = new ProjectApprovalHistoryEntity()
                        {
                            CreatedBy = ProjectEntity.CreatedBy,
                            CreatedDate = DateTime.Now,
                            Note = "",
                            Status = 1,
                            ProjectId = Project.ID
                        };
                        db.Entry(projectApprovalHistoryEntity).State = EntityState.Added;
                        db.SaveChanges();

                        //check evaluations
                        if (ProjectEntity.evaluations != null && ProjectEntity.evaluations.Length > 0)
                        {
                            foreach (var item in ProjectEntity.evaluations)
                            {
                                var eva = new ProjectEvaluatedValuesEntity
                                {
                                    EVALUATION_VALUE_ID = item,
                                    PROJECT_ID = Project.ID
                                };
                                db.ProjectEvaluatedValuesCollection.Add(eva);
                                db.Entry(eva).State = System.Data.Entity.EntityState.Added;
                                db.SaveChanges();
                            }
                        }

                        scope.Complete();
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }
                }
            }
            catch (TransactionAbortedException ex)
            {
                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
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
        public IHttpActionResult SavePlannedKPIs(
            string CreatedBy,
            int ProjectID,
            float? Q1_Target,
            float? Q2_Target,
            float? Q3_Target,
            float? Q4_Target,
            int KPI_ID)
        {
            //delete old kpis result first

            List<ProjectResultEntity> projectResults =
                db.ProjectResultCollection.Where(x => x.ProjectId == ProjectID && x.kpi_id == KPI_ID).ToList();


            if (Q1_Target > 0)
            {
                ProjectResultEntity projectResultEntity1 = new ProjectResultEntity();

                projectResultEntity1.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q1;
                projectResultEntity1.PlannedResult = Q1_Target.Value;
                var result = projectResults.FirstOrDefault(a => a.PeriodNo == (int)Enums.Enums.ResultPeriods.Q1);
                projectResultEntity1.ActualResult = result != null ? result.ActualResult : 0;
                projectResultEntity1.CreatedBy = CreatedBy;
                projectResultEntity1.CreatedDate = DateTime.Now;
                projectResultEntity1.ProjectId = ProjectID;
                projectResultEntity1.kpi_id = KPI_ID;

                db.Entry(projectResultEntity1).State = System.Data.Entity.EntityState.Added;
                db.ProjectResultCollection.Add(projectResultEntity1);
                db.SaveChanges();
            }

            if (Q2_Target > 0)
            {
                ProjectResultEntity projectResultEntity2 = new ProjectResultEntity();
                projectResultEntity2.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q2;
                projectResultEntity2.PlannedResult = Q2_Target.Value;
                var result = projectResults.FirstOrDefault(a => a.PeriodNo == (int)Enums.Enums.ResultPeriods.Q2);
                projectResultEntity2.ActualResult = result != null ? result.ActualResult : 0;
                projectResultEntity2.CreatedBy = CreatedBy;
                projectResultEntity2.CreatedDate = DateTime.Now;
                projectResultEntity2.ProjectId = ProjectID;
                projectResultEntity2.kpi_id = KPI_ID;

                db.Entry(projectResultEntity2).State = System.Data.Entity.EntityState.Added;
                db.ProjectResultCollection.Add(projectResultEntity2);
                db.SaveChanges();
            }

            if (Q3_Target > 0)
            {
                ProjectResultEntity projectResultEntity3 = new ProjectResultEntity();

                projectResultEntity3.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q3;
                projectResultEntity3.PlannedResult = Q3_Target.Value;
                var result = projectResults.FirstOrDefault(a => a.PeriodNo == (int)Enums.Enums.ResultPeriods.Q3);
                projectResultEntity3.ActualResult = result != null ? result.ActualResult : 0;
                projectResultEntity3.CreatedBy = CreatedBy;
                projectResultEntity3.CreatedDate = DateTime.Now;
                projectResultEntity3.ProjectId = ProjectID;
                projectResultEntity3.kpi_id = KPI_ID;

                db.Entry(projectResultEntity3).State = System.Data.Entity.EntityState.Added;
                db.ProjectResultCollection.Add(projectResultEntity3);
                db.SaveChanges();
            }

            if (Q4_Target > 0)
            {
                ProjectResultEntity projectResultEntity4 = new ProjectResultEntity();

                projectResultEntity4.PeriodNo = (int)HR.Enums.Enums.ResultPeriods.Q4;
                projectResultEntity4.PlannedResult = Q4_Target.Value;
                var result = projectResults.FirstOrDefault(a => a.PeriodNo == (int)Enums.Enums.ResultPeriods.Q4);
                projectResultEntity4.ActualResult = result != null ? result.ActualResult : 0;
                projectResultEntity4.CreatedBy = CreatedBy;
                projectResultEntity4.CreatedDate = DateTime.Now;
                projectResultEntity4.ProjectId = ProjectID;
                projectResultEntity4.kpi_id = KPI_ID;

                db.Entry(projectResultEntity4).State = System.Data.Entity.EntityState.Added;
                db.ProjectResultCollection.Add(projectResultEntity4);
                db.SaveChanges();
            }

            if (projectResults != null && projectResults.Count > 0)
            {
                db.ProjectResultCollection.RemoveRange(projectResults);

                db.SaveChanges();
            }

            return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
        }

        [HttpGet]
        public IHttpActionResult UpdatePlannedKPIs(
            string CreatedBy,
            int ProjectID,
            float? Q1_Target,
            float? Q2_Target,
            float? Q3_Target,
            float? Q4_Target,
            int KPI_ID)
        {
            var projectResultEntityAll =
                db.ProjectResultCollection.Where(x => x.kpi_id == KPI_ID && x.ProjectId == ProjectID);

            if (Q1_Target > 0)
            {
                var projectResultEntity1 =
                    projectResultEntityAll.FirstOrDefault(x => x.PeriodNo == (int)HR.Enums.Enums.ResultPeriods.Q1);
                projectResultEntity1.PlannedResult = Q1_Target.Value;
                //projectResultEntity1.ActualResult = 0;
                projectResultEntity1.ModifiedBy = CreatedBy;
                projectResultEntity1.ModifiedDate = DateTime.Now;

                db.Entry(projectResultEntity1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            if (Q2_Target > 0)
            {
                var projectResultEntity2 =
                    projectResultEntityAll.FirstOrDefault(x => x.PeriodNo == (int)HR.Enums.Enums.ResultPeriods.Q2);
                projectResultEntity2.PlannedResult = Q2_Target.Value;
                // projectResultEntity2.ActualResult = 0;
                projectResultEntity2.ModifiedBy = CreatedBy;
                projectResultEntity2.ModifiedDate = DateTime.Now;

                db.Entry(projectResultEntity2).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            if (Q3_Target > 0)
            {
                var projectResultEntity3 =
                    projectResultEntityAll.FirstOrDefault(x => x.PeriodNo == (int)HR.Enums.Enums.ResultPeriods.Q3);
                projectResultEntity3.PlannedResult = Q3_Target.Value;
                //projectResultEntity3.ActualResult = 0;
                projectResultEntity3.ModifiedBy = CreatedBy;
                projectResultEntity3.ModifiedDate = DateTime.Now;

                db.Entry(projectResultEntity3).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            if (Q4_Target > 0)
            {
                var projectResultEntity4 =
                    projectResultEntityAll.FirstOrDefault(x => x.PeriodNo == (int)HR.Enums.Enums.ResultPeriods.Q4);
                projectResultEntity4.PlannedResult = Q4_Target.Value;
                //projectResultEntity4.ActualResult = 0;
                projectResultEntity4.ModifiedBy = CreatedBy;
                projectResultEntity4.ModifiedDate = DateTime.Now;

                db.Entry(projectResultEntity4).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectEntity"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult UpdateProject([FromUri] ProjectModel ProjectEntity)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Modified by yousef sleit
                    if (ProjectEntity != null)
                    {
                        ProjectEntity Project = db.ProjectsCollection.Where(x => x.ID == ProjectEntity.ID)
                            .FirstOrDefault();
                        if (Project != null)
                        {
                            //Project.target_type = ProjectEntity.target_type;
                            Project.BranchId = ProjectEntity.BranchId;
                            Project.Code = "1";
                            Project.CompanyId = ProjectEntity.CompanyId;
                            Project.planned_status = 1;
                            // add approval history
                            var projectApprovalHistoryEntity = new ProjectApprovalHistoryEntity()
                            {
                                CreatedBy = ProjectEntity.CreatedBy,
                                CreatedDate = DateTime.Now,
                                Note = "",
                                Status = 1,
                                ProjectId = Project.ID
                            };
                            db.Entry(projectApprovalHistoryEntity).State = EntityState.Added;
                            Project.Category = ProjectEntity.Category;
                            Project.Description = ProjectEntity.Description;
                            Project.KPI = ProjectEntity.KPI;
                            Project.KPICycleId = ProjectEntity.KPICycleId;
                            Project.KPITypeId = ProjectEntity.BranchId;
                            Project.Name = (ProjectEntity.languageCode == "en" ? ProjectEntity.Name : "");
                            Project.Name2 = (ProjectEntity.languageCode == "ar" ? ProjectEntity.Name : "");
                            Project.Order = 1;
                            Project.StratigicObjectiveId = ProjectEntity.StratigicObjectiveId;
                            Project.UnitId = ProjectEntity.UnitId;
                            Project.Weight = ProjectEntity.Weight;
                            Project.Year = ProjectEntity.Year;
                            Project.Target = ProjectEntity.Target;
                            Project.ModifiedDate = DateTime.Now;
                            Project.ModifiedBy = ProjectEntity.ModifiedBy;
                            Project.PlannedCost = ProjectEntity.PlannedCost;
                            if (string.IsNullOrEmpty(Project.Name))
                                Project.Name = Project.Name2;
                            Project.p_type = ProjectEntity.p_type;
                            db.Entry(Project).State = EntityState.Modified;
                            db.Entry(Project).Property(x => x.CreatedBy).IsModified = false;
                            db.Entry(Project).Property(x => x.CreatedDate).IsModified = false;
                            //check if project has evaluations 
                            if (ProjectEntity.evaluations != null && ProjectEntity.evaluations.Count() > 0)
                            {
                                var projectEvaluations = db.ProjectEvaluatedValuesCollection
                                    .Where(a => a.PROJECT_ID == ProjectEntity.ID).ToList();
                                if (projectEvaluations.Count > 0)
                                {
                                    db.ProjectEvaluatedValuesCollection.RemoveRange(projectEvaluations);
                                }

                                db.SaveChanges();
                                if (ProjectEntity.evaluations.Length > 0)
                                {
                                    foreach (var item in ProjectEntity.evaluations)
                                    {
                                        var eva = new ProjectEvaluatedValuesEntity
                                        {
                                            EVALUATION_VALUE_ID = item,
                                            PROJECT_ID = Project.ID
                                        };
                                        db.ProjectEvaluatedValuesCollection.Add(eva);
                                        db.Entry(eva).State = System.Data.Entity.EntityState.Added;
                                        db.SaveChanges();
                                    }
                                }
                            }

                            db.SaveChanges();
                            scope.Complete();
                        }

                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }
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

        // project model

        public class ProjectModel
        {
            public int ID { set; get; }
            public int[] evaluations { get; set; }

            public string Code { set; get; }

            public string Name { set; get; }


            public string Name2 { set; get; }


            public long CompanyId { set; get; }


            public int Order { set; get; }


            public float Weight { set; get; }


            public int UnitId { set; get; }

            public int Target { set; get; }


            public int KPICycleId { set; get; }

            public int KPITypeId { set; get; }

            public int ResultUnitId { set; get; }


            public int StratigicObjectiveId { set; get; }


            public int BranchId { set; get; }

            public string Description { set; get; }


            public string KPI { set; get; }


            public string CreatedBy { set; get; }


            public DateTime CreatedDate { set; get; }


            public string ModifiedBy { set; get; }


            public DateTime? ModifiedDate { set; get; }


            public float? WeightFromObjective { set; get; }


            public float? Result { set; get; }


            public float? ResultPercentage { set; get; }


            public float? ResultWeightPercentage { set; get; }


            public float? ResultWeightPercentageFromObjectives { set; get; }


            public float? ActualCost { set; get; }


            public float? PlannedCost { set; get; }


            public int? p_type { set; get; }


            public int planned_status { set; get; }


            public int assessment_status { set; get; }


            public string StratigicObjectiveName { set; get; }

            public string UnitName { set; get; }

            public int? Year { set; get; }

            public int? Category { set; get; }

            public virtual StratigicObjectivesEntity stratigyObject { set; get; }


            public float Q1_Target { set; get; }


            public float Q2_Target { set; get; }

            public float Q3_Target { set; get; }

            public float Q4_Target { set; get; }

            public string languageCode { set; get; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="branchId"></param>
        /// <param name="yearId"></param>
        /// <param name="unitId"></param>
        /// <param name="stratigicObjectiveId"></param>
        /// <param name="languageCode"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SearchAllProjectsAndKPIs(long companyId, int branchId, int yearId, int unitId,
            int stratigicObjectiveId, string languageCode, int category)
        {
            try
            {
                using (HRContext DB = new HRContext())
                {
                    var categories = from cat in db.ProjectCategoryCollection
                        select new
                        {
                            cat.ID,
                            cat.Name
                        };
                    var PlannedUnitWeights2 = (from obj in db.ProjectsCollection
                            where
                                (obj.CompanyId == companyId) &&
                                (stratigicObjectiveId <= 0 || obj.StratigicObjectiveId == stratigicObjectiveId)
                            //&&(unitId <= 0 || obj.UnitId == unitId)
                            select new
                            {
                                obj.Weight,
                                obj.KPI,
                                obj.StratigicObjectiveId
                            }
                        );
                    float PlannedUnitWeights = 0;
                    if (PlannedUnitWeights2.Count() > 0)
                    {
                        PlannedUnitWeights = PlannedUnitWeights2.Sum(a => a.Weight);
                    }

                    var PlannedWeights = (from obj in db.StratigicObjectivesCollection
                            where
                                (obj.CompanyId == companyId) &&
                                (stratigicObjectiveId <= 0 || obj.id == stratigicObjectiveId)
                            select obj.Weight
                        ).Sum();

                    //get  project calculation method
                    var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                    var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != 0) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != 0) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI equals ok
                            .ID.ToString()
                        select new
                        {
                            plannedStratigy =
                                calculationWeight == 1
                                    ? (prj.Weight) * (ok.Weight / 100) * (so.Weight / 100)
                                    : (prj.Weight) * (so.Weight / 100),

                            prj.ID,
                            prj.Category,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                            prj.CompanyId,
                            prj.Code,
                            prj.CreatedBy,
                            KPIWeight = ok.Weight,
                            SoWeight = so.Weight,
                            prj.KPI,
                            prj.CreatedDate,
                            prj.ModifiedBy,
                            prj.ModifiedDate,
                            prj.Order,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            // WeigthValue = ((prj.Weight / PlannedUnitWeights2.Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId).Sum(a => a.Weight)) * 100),
                            WeigthValue = prj.Weight,
                            prj.ActualCost,
                            prj.BranchId,
                            prj.Description,
                            prj.KPICycleId,
                            prj.KPITypeId,
                            prj.ResultUnitId,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.StratigicObjectiveId,
                            prj.WeightFromObjective,
                            prj.Target,
                            so.Year,
                            //KPI_name = kpi.Name,
                            prj.UnitId,
                            prj.PlannedCost,
                            StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                            StratigicObjectiveShortName = ((languageCode == "ar")
                                ? (so.Name2.Length > 30 ? so.Name2.Substring(0, 30) + "..." : so.Name2)
                                : (so.Name.Length > 30 ? so.Name.Substring(0, 30) + "..." : so.Name)),
                            UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                            WeightDesc = Math.Round(prj.Weight, 0) + " %",
                            prj.p_type,
                            planned_status = (prj.planned_status == 1) ? "Waiting Approval" :
                                (prj.planned_status == 2) ? "Confirmed" : "Declined",
                        }
                    ).ToList();
                    var KPIs = (from kpi in db.ObjectiveKpiCollection
                            where kpi.is_obj_kpi == 2
                            join projResult in db.ProjectResultCollection on kpi.ID equals projResult.kpi_id
                            select new
                            {
                                kpi.ID,
                                kpi.objective_id,
                                KPI_name = kpi.Name,
                                Q1_Target = projResult.PeriodNo == 1 ? projResult.PlannedResult : 0,
                                Q2_Target = projResult.PeriodNo == 2 ? projResult.PlannedResult : 0,
                                Q3_Target = projResult.PeriodNo == 3 ? projResult.PlannedResult : 0,
                                Q4_Target = projResult.PeriodNo == 4 ? projResult.PlannedResult : 0,
                                Target = kpi.Target,
                                kpi.BetterUpDown
                            }
                        ).ToList();
                    var result = (from res in projects
                        select new
                        {
                            category = res.Category,
                            //PlannedUnitWeights = PlannedUnitWeights,
                            //PlannedStratigicWeights= PlannedWeights,
                            PlannedWeights = (PlannedUnitWeights / 100) * (PlannedWeights),
                            res.plannedStratigy,
                            res.ID,
                            res.Name,
                            res.CompanyId,
                            res.Code,
                            res.CreatedBy,
                            res.CreatedDate,
                            res.ModifiedBy,
                            res.ModifiedDate,
                            res.Order,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.Weight,
                            res.WeigthValue,
                            res.KPIWeight,
                            res.SoWeight,
                            res.ActualCost,
                            res.BranchId,
                            res.Description,
                            res.KPICycleId,
                            res.KPITypeId,
                            res.KPI,
                            res.ResultUnitId,
                            res.ResultWeightPercentageFromObjectives,
                            res.StratigicObjectiveId,
                            res.WeightFromObjective,
                            res.Target,
                            res.Year,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.StratigicObjectiveShortName,
                            res.WeightDesc,
                            res.UnitName,
                            style = (res.p_type == 2) ? "service" : "",
                            KPIs = (from k in KPIs where k.objective_id == res.ID select k).GroupBy(x => x.ID).Select(
                                k => new
                                {
                                    k.First().ID,
                                    k.First().objective_id,
                                    k.First().KPI_name,
                                    Q1_Target = k.FirstOrDefault(q => q.Q1_Target != 0)?.Q1_Target ?? 0,
                                    Q2_Target = k.FirstOrDefault(q => q.Q2_Target != 0)?.Q2_Target ?? 0,
                                    Q3_Target = k.FirstOrDefault(q => q.Q3_Target != 0)?.Q3_Target ?? 0,
                                    Q4_Target = k.FirstOrDefault(q => q.Q4_Target != 0)?.Q4_Target ?? 0,
                                    k.First().Target,
                                    BetterUpDown = k.First().BetterUpDown
                                }).ToList(),
                            Evidences_files = (from evid in db.ProjectEvidenceCollection
                                where evid.ProjectId == res.ID
                                select evid.ID).Count(),
                            res.planned_status,
                            Approvals = db.ProjectApprovalHistoryCollection.Where(a => a.ProjectId == res.ID)
                                .OrderByDescending(a => a.ID).ToList()
                        }).ToList();
                    if (result.Count > 0)
                    {
                        result = result.OrderByDescending(x => x.ID).ToList();
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
        [HttpGet]
        public IHttpActionResult SearchAllProjectsAndEvaluations(long companyId, int branchId, int yearId, int unitId,
            int stratigicObjectiveId, string languageCode, int category)
        {
            try
            {
                using (HRContext DB = new HRContext())
                {
                    var projectEvaluationsValues = db.ProjectEvaluatedValuesCollection.ToList() ;
                    var projectEvaluations = db.ProjectEvaluationValuesCollection.ToList() ;
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != 0) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != 0) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI equals ok
                            .ID.ToString()
                        select new
                        {
                            prj.ID,
                            prj.Category,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                            prj.CompanyId,
                            prj.Code,
                            prj.CreatedBy,
                            prj.CreatedDate,
                            prj.ModifiedBy,
                            prj.ModifiedDate,
                            prj.Order,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            // WeigthValue = ((prj.Weight / PlannedUnitWeights2.Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId).Sum(a => a.Weight)) * 100),
                            WeigthValue = prj.Weight,
                            prj.ActualCost,
                            prj.BranchId,
                            prj.Description,
                            prj.KPICycleId,
                            prj.KPITypeId,
                            prj.ResultUnitId,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.StratigicObjectiveId,
                            prj.WeightFromObjective,
                            prj.Target,
                            so.Year,
                            //KPI_name = kpi.Name,
                            prj.UnitId,
                            prj.PlannedCost,
                            StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                            StratigicObjectiveShortName = ((languageCode == "ar")
                                ? (so.Name2.Length > 30 ? so.Name2.Substring(0, 30) + "..." : so.Name2)
                                : (so.Name.Length > 30 ? so.Name.Substring(0, 30) + "..." : so.Name)),
                            UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                            WeightDesc = Math.Round(prj.Weight, 0) + " %",
                            prj.p_type,
                            planned_status = (prj.planned_status == 1) ? "Waiting Approval" :
                                (prj.planned_status == 2) ? "Confirmed" : "Declined",
                        }
                    ).ToList();
                    
                    var result = (from res in projects
                        select new
                        {
                            res.ID,
                            res.Name,
                            res.CompanyId,
                            res.Code,
                            res.CreatedBy,
                            res.CreatedDate,
                            res.ModifiedBy,
                            res.ModifiedDate,
                            res.Order,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.Weight,
                            res.WeigthValue,
                            res.ActualCost,
                            res.BranchId,
                            res.Description,
                            res.KPICycleId,
                            res.KPITypeId,
                            res.ResultUnitId,
                            res.ResultWeightPercentageFromObjectives,
                            res.StratigicObjectiveId,
                            res.WeightFromObjective,
                            res.Target,
                            res.Year,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.StratigicObjectiveShortName,
                            res.WeightDesc,
                            res.UnitName,
                            evaluations= projectEvaluationsValues.Where(a=>a.PROJECT_ID==res.ID).Select(a=> new
                            {
                                a.ID,
                                a.PROJECT_ID,
                                a.EVALUATION_VALUE_ID,
                                EVALUATION_ID= projectEvaluations.FirstOrDefault(x=>x.ID==a.EVALUATION_VALUE_ID)?.EVALUATION_ID??0,
                                EVALUATION_VALUE= projectEvaluations.FirstOrDefault(x=>x.ID==a.EVALUATION_VALUE_ID)?.Weight??0
                                
                            }),
                            style = (res.p_type == 2) ? "service" : "",
                            Evidences_files = (from evid in db.ProjectEvidenceCollection
                                where evid.ProjectId == res.ID
                                select evid.ID).Count(),
                            res.planned_status,
                            Approvals = db.ProjectApprovalHistoryCollection.Where(a => a.ProjectId == res.ID)
                                .OrderByDescending(a => a.ID).ToList()
                        }).ToList();
                    if (result.Count > 0)
                    {
                        result = result.OrderByDescending(x => x.ID).ToList();
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


        [HttpGet]
        public IHttpActionResult SearchAllProjectsList(long companyId, int branchId, int yearId, int unitId,
            int stratigicObjectiveId, string languageCode)
        {
            try
            {
                //Modified by yousef sleit
                if (yearId == 0)
                {
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                using (HRContext DB = new HRContext())
                {
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != 0) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != 0) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        select new
                        {
                            prj.ID,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                            prj.CompanyId,
                            prj.Code,
                            prj.CreatedBy,
                            prj.CreatedDate,
                            prj.ModifiedBy,
                            prj.ModifiedDate,
                            prj.Order,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            prj.ActualCost,
                            prj.BranchId,
                            prj.Description,
                            prj.KPICycleId,
                            prj.KPITypeId,
                            prj.ResultUnitId,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.StratigicObjectiveId,
                            prj.WeightFromObjective,
                            prj.Target,
                            so.Year,
                            prj.KPI,
                            prj.UnitId,
                            prj.PlannedCost
                        }
                    ).ToList().AsQueryable();

                    var result = (from res in projects
                        select new
                        {
                            res.ID,
                            res.Name,
                            res.CompanyId,
                            res.Code,
                            res.CreatedBy,
                            res.CreatedDate,
                            res.ModifiedBy,
                            res.ModifiedDate,
                            res.Order,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.Weight,
                            res.ActualCost,
                            res.BranchId,
                            res.Description,
                            res.KPICycleId,
                            res.KPITypeId,
                            res.ResultUnitId,
                            res.ResultWeightPercentageFromObjectives,
                            res.StratigicObjectiveId,
                            res.WeightFromObjective,
                            res.Target,
                            res.Year,
                            res.KPI,
                            res.UnitId,
                            res.PlannedCost
                        }).ToList();
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


        [HttpGet]
        public IHttpActionResult getProjectbyID(long id, string languageCode)
        {
            try
            {
                float maxWeights = 0;
                var projectEvaluations = from eval in db.ProjectEvaluatedValuesCollection
                    where eval.PROJECT_ID == id
                    select eval.EVALUATION_VALUE_ID;
                ProjectEntity proj = db.ProjectsCollection.Where(x => x.ID == id).FirstOrDefault();
                var categories = from cat in db.ProjectCategoryCollection
                    where cat.ID == proj.Category
                    select new
                    {
                        cat.ID,
                        cat.Name
                    };
                if (proj != null)
                {
                    var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();

                    int calculationMethod = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                    List<ProjectEntity> prjs = db.ProjectsCollection.Where(x =>
                        (calculationMethod == 1 && x.KPI == proj.KPI) ||
                        (calculationMethod == 2 && x.StratigicObjectiveId == proj.StratigicObjectiveId)).ToList();
                    if (prjs != null)
                    {
                        maxWeights = 100 - prjs.Sum(x => x.Weight) + proj.Weight;
                    }
                }

                var projects = (
                    from prj in db.ProjectsCollection.Where(p =>
                        p.ID == id
                    )
                    join so in db.StratigicObjectivesCollection on
                        prj.StratigicObjectiveId equals so.id
                    join unit in db.UnitCollection on prj.UnitId equals unit.ID
                    select new
                    {
                        prj.ID,
                        Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                        prj.CompanyId,
                        prj.Code,
                        prj.CreatedBy,
                        prj.CreatedDate,
                        prj.ModifiedBy,
                        prj.ModifiedDate,
                        prj.Order,
                        prj.ResultPercentage,
                        prj.ResultWeightPercentage,
                        prj.Weight,
                        prj.ActualCost,
                        prj.BranchId,
                        prj.Description,
                        prj.KPICycleId,
                        prj.KPITypeId,
                        prj.ResultUnitId,
                        prj.ResultWeightPercentageFromObjectives,
                        prj.StratigicObjectiveId,
                        prj.WeightFromObjective,
                        prj.Target,
                        so.Year,
                        prj.KPI,
                        prj.UnitId,
                        prj.PlannedCost,
                        StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                        UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                        prj.p_type,
                        max = maxWeights
                    }
                ).ToList().AsQueryable();

                var result = (from res in projects
                    select new
                    {
                        cateogryId = categories != null && categories.Count() > 0 ? categories.FirstOrDefault().ID : 0,
                        cateogry = categories != null && categories.Count() > 0 ? categories.FirstOrDefault().Name : "",
                        res.ID,
                        res.Name,
                        res.CompanyId,
                        res.Code,
                        res.CreatedBy,
                        res.CreatedDate,
                        res.ModifiedBy,
                        res.ModifiedDate,
                        res.Order,
                        res.ResultPercentage,
                        res.ResultWeightPercentage,
                        res.Weight,
                        res.ActualCost,
                        res.BranchId,
                        res.Description,
                        res.KPICycleId,
                        res.KPITypeId,
                        res.ResultUnitId,
                        res.ResultWeightPercentageFromObjectives,
                        res.StratigicObjectiveId,
                        res.WeightFromObjective,
                        res.Target,
                        res.Year,
                        res.KPI,
                        res.UnitId,
                        res.PlannedCost,
                        Approvals = db.ProjectApprovalHistoryCollection.Where(a => a.ProjectId == res.ID)
                            .OrderByDescending(a => a.ID).ToList(),


                        Q1_Target = db.ProjectResultCollection.Where(q1 =>
                            q1.ProjectId == res.ID
                            && q1.PeriodNo == 1).Select(q1 => q1.PlannedResult).DefaultIfEmpty().Sum(),

                        Q2_Target = (from q2 in db.ProjectResultCollection
                            where q2.ProjectId == res.ID
                                  && q2.PeriodNo == 2
                            select q2.PlannedResult).DefaultIfEmpty().Sum(),
                        Q3_Target = (from q3 in db.ProjectResultCollection
                            where q3.ProjectId == res.ID
                                  && q3.PeriodNo == 3
                            select q3.PlannedResult).DefaultIfEmpty().Sum(),
                        Q4_Target = (from q4 in db.ProjectResultCollection
                            where q4.ProjectId == res.ID
                                  && q4.PeriodNo == 4
                            select q4.PlannedResult).DefaultIfEmpty().Sum(),
                        Evidences_files = (from evid in db.ProjectEvidenceCollection
                            where evid.ProjectId == res.ID
                            select evid.ID).Count(),
                        res.p_type,
                        res.max,
                        res.StratigicObjectiveName,
                        evaluations = projectEvaluations.ToList()
                    }).ToList().AsQueryable().FirstOrDefault();
                ;


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
        public IHttpActionResult DeleteProjectByID(long id)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<ProjectResultEntity> projectResults =
                        db.ProjectResultCollection.Where(x => x.ProjectId == id).ToList();


                    if (projectResults != null && projectResults.Count > 0)
                    {
                        db.ProjectResultCollection.RemoveRange(projectResults);
                        //foreach (ProjectResultEntity pr in projectResults)
                        //{
                        //    db.ProjectResultCollection.Remove(pr);
                        //    //db.Entry(pr).State = EntityState.Deleted;
                        //}

                        db.SaveChanges();
                    }

                    ProjectEntity Project = db.ProjectsCollection.Where(x => x.ID == id).FirstOrDefault();
                    if (Project != null)
                    {
                        db.ProjectsCollection.Remove(Project);
                        db.SaveChanges();
                    }

                    scope.Complete();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult RemoveProjectsByIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.ProjectsCollection.RemoveRange(db.ProjectsCollection.Where(x => ids.Contains(x.ID)).ToList());
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
        public IHttpActionResult getProjectbyEmployeeID(long EmployeeID, string languageCode,int yearId)
        {
            try
            {
                var Project = (
                    from x in db.ProjectsCollection
                    join y in db.EmployeesCollection on x.UnitId equals y.UNIT_ID
                    where y.ID == EmployeeID && x.Year == yearId
                    select new
                    {
                        x.ID,
                        Name = ((languageCode == "ar") ? x.Name2 : x.Name),
                        x.CompanyId,
                        x.Code,
                        x.CreatedBy,
                        x.CreatedDate,
                        x.ModifiedBy,
                        x.ModifiedDate,
                        x.Order,
                        x.ResultPercentage,
                        x.ResultWeightPercentage,
                        x.Weight,
                        x.ActualCost,
                        x.BranchId,
                        x.Description,
                        x.KPICycleId,
                        x.KPITypeId,
                        x.ResultUnitId,
                        x.ResultWeightPercentageFromObjectives,
                        x.StratigicObjectiveId,
                        x.WeightFromObjective,
                        x.Target,
                        x.KPI,
                        x.UnitId,
                        x.PlannedCost,
                    }
                ).ToList();

                return Ok(new { Data = Project, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getProjectbyUnit(long? unitId, string languageCode)
        {
            try
            {
                var Project = (
                    from x in db.ProjectsCollection
                    join y in db.UnitCollection on x.UnitId equals y.ID
                    where   (unitId == null || y.ID == unitId) 
                    select new
                    {
                        x.ID,
                        Name = ((languageCode == "ar") ? x.Name2 : x.Name),
                        x.CompanyId,
                        x.Code,
                        x.CreatedBy,
                        x.CreatedDate,
                        x.ModifiedBy,
                        x.ModifiedDate,
                        x.Order,
                        x.ResultPercentage,
                        x.ResultWeightPercentage,
                        x.Weight,
                        x.ActualCost,
                        x.BranchId,
                        x.Description,
                        x.KPICycleId,
                        x.KPITypeId,
                        x.ResultUnitId,
                        x.ResultWeightPercentageFromObjectives,
                        x.StratigicObjectiveId,
                        x.WeightFromObjective,
                        x.Target,
                        x.KPI,
                        x.UnitId,
                        x.PlannedCost,
                    }
                ).ToList();

                return Ok(new { Data = Project, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getProjectsRemainingWeight(string kpiId)
        {
            try
            {
                //get  project calculation method
                var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                float max = 0;
                int calculationMethod = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                List<ProjectEntity> list = db.ProjectsCollection.Where(x =>
                    (calculationMethod == 1 && x.KPI == kpiId) ||
                    (calculationMethod == 2 && x.StratigicObjectiveId.ToString() == kpiId)).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.Weight);
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


        #region Project Evidences

        [HttpGet]
        public IHttpActionResult getProjectEvidencesbyProjectId(long id)
        {
            try
            {
                var ProjectEvid = db.ProjectEvidenceCollection.Where(x => x.ProjectId == id).ToList();


                if (ProjectEvid != null)
                {
                    return Ok(new { Data = ProjectEvid, IsError = false, ErrorMessage = string.Empty });
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

        #endregion

        #region Project Result

        [HttpGet]
        public IHttpActionResult SetProjectResult(long id, float actualResult, string modifiedBy)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ProjectResultEntity projectResultEntity =
                        db.ProjectResultCollection.Where(x => x.Id == id).FirstOrDefault();
                    if (projectResultEntity != null)
                    {
                        projectResultEntity.ActualResult = actualResult;
                        projectResultEntity.ModifiedBy = modifiedBy;
                        projectResultEntity.ModifiedDate = DateTime.Now;

                        db.Entry(projectResultEntity).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();

                        ProjectEntity project = db.ProjectsCollection.Where(x => x.ID == projectResultEntity.ProjectId)
                            .FirstOrDefault();
                        if (project != null)
                        {
                            StratigicObjectivesEntity stratigicObjective = db.StratigicObjectivesCollection
                                .Where(x => x.id == project.StratigicObjectiveId).FirstOrDefault();
                            if (stratigicObjective != null)
                            {
                                CalculateAllPerformance(project.CompanyId, stratigicObjective.Year, project.ID);
                            }
                        }

                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }

                    scope.Complete();


                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
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

        #endregion


        [HttpGet]
        public IHttpActionResult CalculateAllPerformance(long companyId, int year, long projectId)
        {
            try
            {
                ProjectEntity project = db.ProjectsCollection.Where(p => p.ID == projectId).FirstOrDefault();

                if (project == null)
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No project Found" });

                List<ProjectResultEntity> projectResults =
                    db.ProjectResultCollection.Where(x => x.ProjectId == projectId).ToList();

                if (projectResults != null && projectResults.Count > 0)
                {
                    float projectResultValue = 0;
                    switch (project.KPITypeId)
                    {
                        case (int)Enums.Enums.KPYType.Accumulative:
                            projectResultValue = (float)projectResults.Where(pr => pr.ActualResult > 0)
                                .Sum(s => s.ActualResult);
                            break;
                        case (int)Enums.Enums.KPYType.Average:
                            projectResultValue = (float)projectResults.Where(pr => pr.ActualResult > 0)
                                .Average(s => s.ActualResult);
                            break;
                        case (int)Enums.Enums.KPYType.Last:
                            projectResultValue = (float)projectResults.Where(pr => pr.ActualResult > 0)
                                .OrderBy(s => s.PeriodNo).Last().ActualResult;
                            break;
                        case (int)Enums.Enums.KPYType.Maximum:
                            projectResultValue =
                                (float)projectResults.Where(pr => pr.ActualResult > 0).Max().ActualResult;
                            break;
                        case (int)Enums.Enums.KPYType.Minimum:
                            projectResultValue =
                                (float)projectResults.Where(pr => pr.ActualResult > 0).Min().ActualResult;
                            break;
                    }


                    StratigicObjectivesEntity stratigicObjective = db.StratigicObjectivesCollection
                        .Where(s => s.id == project.StratigicObjectiveId).FirstOrDefault();

                    if (stratigicObjective == null)
                        return Ok(new
                            { Data = string.Empty, IsError = true, ErrorMessage = "No Stratigic objective Found" });

                    // Update Project Result

                    project.Result = projectResultValue;
                    project.WeightFromObjective = (stratigicObjective.Weight / 100 * project.Weight / 100) * 100;
                    project.ResultPercentage = (projectResultValue / project.Target) * 100;
                    project.ResultWeightPercentage = project.ResultPercentage / 100 * (project.Weight / 100) * 100;
                    project.ResultWeightPercentageFromObjectives = project.ResultWeightPercentage / 100 *
                                                                   (stratigicObjective.Weight / 100) * 100;

                    db.Entry(project).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    List<ProjectEntity> projects = db.ProjectsCollection
                        .Where(p => p.StratigicObjectiveId == stratigicObjective.id).ToList();

                    int totalProjects = projects.Count();
                    int calculatedProjects = projects.Where(p => p.Result > 0).ToList().Count();

                    if (totalProjects == calculatedProjects)
                    {
                        stratigicObjective.ResultPercentage = projects.Sum(p => p.ResultWeightPercentage);
                        stratigicObjective.ResultWeightPercentage =
                            projects.Sum(p => p.ResultWeightPercentageFromObjectives);
                        stratigicObjective.ActualCost = projects.Sum(p => p.ActualCost);
                        stratigicObjective.PlannedCost = projects.Sum(p => p.PlannedCost);
                    }


                    db.Entry(stratigicObjective).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    List<EmployeesEntity> employees = db.EmployeesCollection.ToList();

                    int totalemployees = employees.Count();

                    CompanyObjectivesPerformanceEntity companyObjectivesPerformance =
                        db.CompanyObjectivesPerformanceCollection.Where(c => c.CompanyId == companyId && c.Year == year)
                            .FirstOrDefault();

                    List<tbl_performance_levels> levels = db.tbl_performance_levelsCollection.ToList();

                    if (companyObjectivesPerformance == null)
                    {
                        companyObjectivesPerformance = new CompanyObjectivesPerformanceEntity();
                        companyObjectivesPerformance.CompanyId = companyId;
                        companyObjectivesPerformance.Year = year;

                        db.Entry(companyObjectivesPerformance).State = System.Data.Entity.EntityState.Added;
                        db.CompanyObjectivesPerformanceCollection.Add(companyObjectivesPerformance);
                    }
                    else
                    {
                        db.Entry(companyObjectivesPerformance).State = System.Data.Entity.EntityState.Modified;
                    }

                    companyObjectivesPerformance.TotalEmployee = totalemployees;

                    List<StratigicObjectivesEntity> objectives = db.StratigicObjectivesCollection
                        .Where(x => x.CompanyId == companyId && x.Year == year).ToList();

                    if (objectives != null && objectives.Count > 0)
                    {
                        companyObjectivesPerformance.ActualCost = (float)objectives.Sum(p => p.ActualCost);
                        companyObjectivesPerformance.PlannedCost = (float)objectives.Sum(p => p.PlannedCost);
                        companyObjectivesPerformance.Result_Percentage =
                            (float)objectives.Sum(p => p.ResultWeightPercentage);
                    }


                    if (levels != null && levels.Count > 0)
                    {
                        tbl_performance_levels level = levels.Where(l => l.lvl_number == 1).FirstOrDefault();
                        if (level != null)
                        {
                            companyObjectivesPerformance.Level1EmployeeCount =
                                (float)(totalemployees * (level.lvl_percent / 100.00));
                        }


                        level = levels.Where(l => l.lvl_number == 2).FirstOrDefault();
                        if (level != null)
                        {
                            companyObjectivesPerformance.Level2EmployeeCount =
                                (float)(totalemployees * (level.lvl_percent / 100.00));
                        }


                        level = levels.Where(l => l.lvl_number == 3).FirstOrDefault();
                        if (level != null)
                        {
                            companyObjectivesPerformance.Level3EmployeeCount =
                                (float)(totalemployees * (level.lvl_percent / 100.00));
                        }


                        level = levels.Where(l => l.lvl_number == 4).FirstOrDefault();
                        if (level != null)
                        {
                            companyObjectivesPerformance.Level4EmployeeCount =
                                (float)(totalemployees * (level.lvl_percent / 100.00));
                        }

                        level = levels.Where(l => l.lvl_number == 5).FirstOrDefault();
                        if (level != null)
                        {
                            companyObjectivesPerformance.Level5EmployeeCount =
                                (float)(totalemployees * (level.lvl_percent / 100.00));
                        }
                    }

                    db.SaveChanges();

                    UnitEntity unit = db.UnitCollection.Where(u => u.COMPANY_ID == companyId && u.ID == project.UnitId)
                        .FirstOrDefault();

                    if (unit != null)
                    {
                        List<StratigicObjectivesEntity> stratigicObjectives =
                            db.StratigicObjectivesCollection.Where(s => s.Year == year).ToList();

                        List<int> ids = stratigicObjectives.Select(s => s.id).ToList();

                        List<ProjectEntity> unitProjects = db.ProjectsCollection.Where(p => p.UnitId == unit.ID &&
                            p.BranchId == project.BranchId
                            && ids.Contains(p.StratigicObjectiveId) && p.CompanyId == companyId).ToList();

                        int unitCalculatedProjectsCount = unitProjects.Where(p => p.Result > 0).Count();

                        if (unitCalculatedProjectsCount == unitProjects.Count)
                        {
                            UnitProjectsPerformanceEntity unitProjectsPerformance = db.UnitProjectsPerformanceCollection
                                .Where(u => u.Year == year && u.UnitId == unit.ID
                                                           && u.CompanyId == companyId &&
                                                           u.BranchId == project.BranchId).FirstOrDefault();

                            if (unitProjectsPerformance == null)
                            {
                                unitProjectsPerformance = new UnitProjectsPerformanceEntity();
                                unitProjectsPerformance.CompanyId = companyId;
                                unitProjectsPerformance.Year = year;
                                unitProjectsPerformance.BranchId = project.BranchId;
                                unitProjectsPerformance.UnitId = project.UnitId;

                                db.Entry(unitProjectsPerformance).State = System.Data.Entity.EntityState.Added;
                                db.UnitProjectsPerformanceCollection.Add(unitProjectsPerformance);
                            }
                            else
                            {
                                db.Entry(unitProjectsPerformance).State = System.Data.Entity.EntityState.Modified;
                            }


                            List<EmployeesEntity> unitEmployees = db.EmployeesCollection
                                .Where(e => e.UNIT_ID == unit.ID && e.BRANCH_ID == project.BranchId).ToList();

                            if (unitEmployees != null && unitEmployees.Count > 0)
                            {
                                if (levels != null && levels.Count > 0)
                                {
                                    tbl_performance_levels level = levels.Where(l => l.lvl_number == 1)
                                        .FirstOrDefault();
                                    if (level != null)
                                    {
                                        unitProjectsPerformance.Level1EmployeeCount =
                                            (float)(unitEmployees.Count() * (level.lvl_percent / 100.00));
                                    }


                                    level = levels.Where(l => l.lvl_number == 2).FirstOrDefault();
                                    if (level != null)
                                    {
                                        unitProjectsPerformance.Level2EmployeeCount =
                                            (float)(unitEmployees.Count() * (level.lvl_percent / 100.00));
                                    }


                                    level = levels.Where(l => l.lvl_number == 3).FirstOrDefault();
                                    if (level != null)
                                    {
                                        unitProjectsPerformance.Level3EmployeeCount =
                                            (float)(unitEmployees.Count() * (level.lvl_percent / 100.00));
                                    }


                                    level = levels.Where(l => l.lvl_number == 4).FirstOrDefault();
                                    if (level != null)
                                    {
                                        unitProjectsPerformance.Level4EmployeeCount =
                                            (float)(unitEmployees.Count() * (level.lvl_percent / 100.00));
                                    }

                                    level = levels.Where(l => l.lvl_number == 5).FirstOrDefault();
                                    if (level != null)
                                    {
                                        unitProjectsPerformance.Level5EmployeeCount =
                                            (float)(unitEmployees.Count() * (level.lvl_percent / 100.00));
                                    }


                                    /*
                                    project.Result = projectResultValue;
                                    project.WeightFromObjective = stratigicObjective.Weight * project.Weight;
                                    project.ResultPercentage = projectResultValue / project.Target;
                                    project.ResultWeightPercentage = project.ResultPercentage * project.Weight;
                                    project.ResultWeightPercentageFromObjectives = project.ResultWeightPercentage * stratigicObjective.Weight;
                                    */


                                    unitProjectsPerformance.TotalEmployee = unitEmployees.Count();


                                    float totalWeightFromObjective =
                                        (float)unitProjects.Sum(p => p.WeightFromObjective);
                                    float totalResultWeightPercentageFromObjectives =
                                        (float)unitProjects.Sum(p => p.ResultWeightPercentageFromObjectives);

                                    unitProjectsPerformance.Result_Percentage =
                                        (totalResultWeightPercentageFromObjectives / totalWeightFromObjective) * 100;

                                    unitProjectsPerformance.ProjectsWeightPercentageFromObjectives =
                                        totalWeightFromObjective;
                                    unitProjectsPerformance.ResultWeightPercentageFromObjectives =
                                        totalResultWeightPercentageFromObjectives;

                                    unitProjectsPerformance.Level1ResultEmployee =
                                        totalResultWeightPercentageFromObjectives / 100 *
                                        companyObjectivesPerformance.Level1EmployeeCount;
                                    unitProjectsPerformance.Level2ResultEmployee =
                                        totalResultWeightPercentageFromObjectives / 100 *
                                        companyObjectivesPerformance.Level2EmployeeCount;
                                    unitProjectsPerformance.Level3ResultEmployee =
                                        totalResultWeightPercentageFromObjectives / 100 *
                                        companyObjectivesPerformance.Level3EmployeeCount;
                                    unitProjectsPerformance.Level4ResultEmployee =
                                        totalResultWeightPercentageFromObjectives / 100 *
                                        companyObjectivesPerformance.Level4EmployeeCount;
                                    unitProjectsPerformance.Level5ResultEmployee =
                                        totalResultWeightPercentageFromObjectives / 100 *
                                        companyObjectivesPerformance.Level5EmployeeCount;

                                    unitProjectsPerformance.PrjectsLevel1Employee = totalWeightFromObjective / 100 *
                                        companyObjectivesPerformance.Level1EmployeeCount;
                                    unitProjectsPerformance.PrjectsLevel2Employee = totalWeightFromObjective / 100 *
                                        companyObjectivesPerformance.Level2EmployeeCount;
                                    unitProjectsPerformance.PrjectsLevel3Employee = totalWeightFromObjective / 100 *
                                        companyObjectivesPerformance.Level3EmployeeCount;
                                    unitProjectsPerformance.PrjectsLevel4Employee = totalWeightFromObjective / 100 *
                                        companyObjectivesPerformance.Level4EmployeeCount;
                                    unitProjectsPerformance.PrjectsLevel5Employee = totalWeightFromObjective / 100 *
                                        companyObjectivesPerformance.Level5EmployeeCount;
                                    unitProjectsPerformance.ActualCost = (float)unitProjects.Sum(p => p.ActualCost);
                                    unitProjectsPerformance.PlannedCost = (float)unitProjects.Sum(p => p.PlannedCost);


                                    db.SaveChanges();
                                }
                            }
                        }
                    }


                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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


        //Added by yousef sleit
        private string GetUnitById(int id)
        {
            using (HRContext DB = new HRContext())
            {
                return DB.UnitCollection.SingleOrDefault(m => m.ID == id).NAME;
            }
        }

        //Added by yousef sleit
        private StratigicObjectivesEntity GetStrtigicObjById(int id)
        {
            using (HRContext DB = new HRContext())
            {
                return DB.StratigicObjectivesCollection.SingleOrDefault(m => m.id == id);
            }
        }

        [HttpGet]
        public IHttpActionResult SearchAllProjects(long companyId, int branchId, int yearId, int unitId,
            int stratigicObjectiveId, string languageCode)
        {
            try
            {
                //Modified by yousef sleit

                using (HRContext DB = new HRContext())
                {
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != 0) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != 0) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join kpi in db.ObjectiveKpiCollection.Where(s => s.is_obj_kpi == 2) on prj.ID equals kpi
                            .objective_id
                        select new
                        {
                            prj.ID,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                            prj.CompanyId,
                            prj.Code,
                            prj.CreatedBy,
                            prj.CreatedDate,
                            prj.ModifiedBy,
                            prj.ModifiedDate,
                            prj.Order,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            prj.ActualCost,
                            prj.BranchId,
                            prj.Description,
                            prj.KPICycleId,
                            prj.KPITypeId,
                            prj.ResultUnitId,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.StratigicObjectiveId,
                            prj.WeightFromObjective,
                            prj.Target,
                            so.Year,
                            KPI_name = kpi.Name,
                            prj.UnitId,
                            prj.PlannedCost,
                            StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                            StratigicObjectiveShortName = ((languageCode == "ar")
                                ? (so.Name2.Length > 30 ? so.Name2.Substring(0, 30) + "..." : so.Name2)
                                : (so.Name.Length > 30 ? so.Name.Substring(0, 30) + "..." : so.Name)),
                            UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                            WeightDesc = Math.Round(prj.Weight, 0) + " %",
                            prj.p_type,
                            kpi_id = kpi.ID
                        }
                    ).ToList().AsQueryable();

                    var result = (from res in projects
                        select new
                        {
                            res.ID,
                            res.Name,
                            res.CompanyId,
                            res.Code,
                            res.CreatedBy,
                            res.CreatedDate,
                            res.ModifiedBy,
                            res.ModifiedDate,
                            res.Order,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.Weight,
                            res.ActualCost,
                            res.BranchId,
                            res.Description,
                            res.KPICycleId,
                            res.KPITypeId,
                            res.ResultUnitId,
                            res.ResultWeightPercentageFromObjectives,
                            res.StratigicObjectiveId,
                            res.WeightFromObjective,
                            res.Target,
                            res.Year,
                            res.KPI_name,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.StratigicObjectiveShortName,
                            res.WeightDesc,
                            res.UnitName,
                            style = (res.p_type == 2) ? "{\"background-color\":\"#959595\"}" : "",

                            Q1_Target = db.ProjectResultCollection.Where(q1 =>
                                    q1.ProjectId == res.ID && res.kpi_id == q1.kpi_id
                                                           && q1.PeriodNo == 1).Select(q1 => q1.PlannedResult)
                                .DefaultIfEmpty().Sum(),

                            Q2_Target = (from q2 in db.ProjectResultCollection
                                where q2.ProjectId == res.ID && res.kpi_id == q2.kpi_id
                                                             && q2.PeriodNo == 2
                                select q2.PlannedResult).DefaultIfEmpty().Sum(),
                            Q3_Target = (from q3 in db.ProjectResultCollection
                                where q3.ProjectId == res.ID && res.kpi_id == q3.kpi_id
                                                             && q3.PeriodNo == 3
                                select q3.PlannedResult).DefaultIfEmpty().Sum(),
                            Q4_Target = (from q4 in db.ProjectResultCollection
                                where q4.ProjectId == res.ID
                                      && q4.PeriodNo == 4 && res.kpi_id == q4.kpi_id
                                select q4.PlannedResult).DefaultIfEmpty().Sum(),
                            Evidences_files = (from evid in db.ProjectEvidenceCollection
                                where evid.ProjectId == res.ID
                                select evid.ID).Count(),
                        }).ToList();


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


        [HttpGet]
        public IHttpActionResult GetProjectsAssessment(
            long CompanyID,
            int YearID,
            int UnitID,
            int ObjectiveID,
            string languageCode,
            int? projectID
        )
        {
            try
            {
                var lstProjectsAssessment = (
                    from obj in db.StratigicObjectivesCollection
                    join prj in db.ProjectsCollection on obj.id equals prj.StratigicObjectiveId
                    join unit in db.UnitCollection on prj.UnitId equals unit.ID
                    join kpi in db.ObjectiveKpiCollection.Where(s => s.is_obj_kpi == 2)
                        on prj.ID equals kpi.objective_id
                    where prj.CompanyId == CompanyID
                          && (obj.Year == YearID || YearID == -1)
                          && (prj.UnitId == UnitID || UnitID == -1)
                          && (obj.id == ObjectiveID || ObjectiveID == -1)
                          && (projectID == null || prj.ID == projectID)
                    select new
                    {
                        ProjectID = prj.ID,
                        ProjectName = prj.Name,
                        UnitName = unit.NAME,
                        prj.Weight,
                        WeightDesc = Math.Round(prj.Weight, 0) + " %",
                        prj.KPI,
                        prj.Target,
                        prj.KPICycleId,
                        FinalResult = prj.Result,
                        ProjectWeightFromobjective = prj.WeightFromObjective,
                        ResultPercentage = prj.ResultPercentage,
                        ProjectResultPercentage = prj.ResultWeightPercentage,
                        ResultPercentageFromObjective = prj.ResultWeightPercentageFromObjectives,
                        obj.Year,
                        stratigicObjectiveId = obj.id,
                        obj.CompanyId,
                        Q1_Disabled = prj.KPICycleId == (int)HR.Enums.Enums.KPYCycle.Quarterly ? "false" : "true",
                        Q3_Disabled = prj.KPICycleId == (int)HR.Enums.Enums.KPYCycle.Quarterly ? "false" : "true",

                        Q2_Disabled = prj.KPICycleId != (int)HR.Enums.Enums.KPYCycle.Yearly ? "false" : "true",
                        Q4_Disabled = "false",
                        prj.ActualCost,
                        prj.PlannedCost,
                        KPI_name = kpi.Name,
                        kpi_id = kpi.ID,
                        //Evidences_Files_Count = (from evid in db.ProjectEvidenceCollection
                        //                         where evid.ProjectId == prj.ID
                        //                         select evid.ID).Count(),
                        //Evidences_Files = (from evid in db.ProjectEvidenceCollection
                        //                   where evid.ProjectId == prj.ID
                        //                   select evid).ToList(),

                        RequiredsDocumnets = (from doc in db.ProjectEvidenceCollection
                            where doc.ProjectId == prj.ID && string.IsNullOrEmpty(doc.FileName)
                            select new
                            {
                                doc.ID,
                                doc.doc_name,
                                doc.FileName
                            }),

                        UploadedDocuments = (from doc in db.ProjectEvidenceCollection
                            where doc.ProjectId == prj.ID && !string.IsNullOrEmpty(doc.FileName)
                            select new
                            {
                                doc.ID,
                                doc.doc_name,
                                doc.FileName,
                                doc.FileUrl
                            }),
                        Unit = (from units in db.UnitCollection
                            where units.ID == prj.UnitId
                            select (languageCode == "en" ? units.NAME : units.name2)).DefaultIfEmpty(),
                    }
                ).ToList().AsQueryable();

                var res = (from x in lstProjectsAssessment
                    select new
                    {
                        x.ProjectID,
                        x.ProjectName,
                        x.UnitName,
                        x.Weight,
                        x.WeightDesc,
                        x.KPI,
                        x.Target,
                        x.KPICycleId,
                        x.FinalResult,
                        x.ProjectWeightFromobjective,
                        x.ResultPercentage,
                        x.ProjectResultPercentage,
                        x.ResultPercentageFromObjective,

                        x.Q1_Disabled,
                        x.Q3_Disabled,

                        x.Q2_Disabled,
                        x.Q4_Disabled,
                        x.ActualCost,
                        x.Year,
                        x.stratigicObjectiveId,
                        x.CompanyId,
                        x.RequiredsDocumnets,
                        x.UploadedDocuments,
                        x.PlannedCost,
                        x.Unit,
                        Q1_ID = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 1
                            select q1.Id).DefaultIfEmpty().Sum(),
                        Q1_P = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 1
                            select q1.PlannedResult).DefaultIfEmpty().Sum(),
                        Q1_A = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 1
                            select q1.ActualResult).DefaultIfEmpty().Sum(),
                        Q2_ID = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 2
                            select q1.Id).DefaultIfEmpty().Sum(),
                        Q2_P = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 2
                            select q1.PlannedResult).DefaultIfEmpty().Sum(),
                        Q2_A = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 2
                            select q1.ActualResult).DefaultIfEmpty().Sum(),

                        Q3_ID = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 3
                            select q1.Id).DefaultIfEmpty().Sum(),
                        Q3_P = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 3
                            select q1.PlannedResult).DefaultIfEmpty().Sum(),
                        Q3_A = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 3
                            select q1.ActualResult).DefaultIfEmpty().Sum(),
                        Q4_ID = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 4
                            select q1.Id).DefaultIfEmpty().Sum(),
                        Q4_P = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 4
                            select q1.PlannedResult).DefaultIfEmpty().Sum(),
                        Q4_A = (from q1 in db.ProjectResultCollection
                            where q1.ProjectId == x.ProjectID
                                  && q1.PeriodNo == 4
                            select q1.ActualResult).DefaultIfEmpty().Sum(),

                        Style = ((from doc in db.ProjectEvidenceCollection
                            where doc.ProjectId == x.ProjectID && string.IsNullOrEmpty(doc.FileName)
                            select doc.ID).Count() > 0)
                            ? "{\"background-color\":\"#959595\"}"
                            : ""
                    }).ToList();

                return Ok(new { Data = res, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult GetProjectsAssessmentWithKpis(long companyId, int? branchId, int yearId, int? unitId,
            int stratigicObjectiveId, string languageCode)
        {
            try
            {
                //Modified by yousef sleit

                using (HRContext DB = new HRContext())
                {
                    //get  project calculation method
                    var calculationWeightMethod = db.ProjectCalculationSetupCollection.FirstOrDefault();
                    var calculationWeight = calculationWeightMethod == null ? 1 : calculationWeightMethod.Calculation;
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != -1 && unitId != null) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != null) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI equals ok
                            .ID.ToString()
                        select new
                        {
                            plannedStratigy =
                                calculationWeight == 1
                                    ? (prj.Weight) * (ok.Weight / 100) * (so.Weight / 100)
                                    : (prj.Weight) * (so.Weight / 100),
                            prj.ID,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),
                            prj.CompanyId,
                            prj.Code,
                            prj.CreatedBy,
                            KPIWeight = ok.Weight,
                            SoWeight = so.Weight,
                            prj.KPI,
                            prj.CreatedDate,
                            prj.ModifiedBy,
                            prj.ModifiedDate,
                            prj.Order,
                            prj.ResultPercentage,
                            prj.ResultWeightPercentage,
                            prj.Weight,
                            // WeigthValue = ((prj.Weight / PlannedUnitWeights2.Where(a => a.KPI == prj.KPI && a.StratigicObjectiveId == prj.StratigicObjectiveId).Sum(a => a.Weight)) * 100),
                            WeigthValue = prj.Weight,
                            prj.ActualCost,
                            prj.BranchId,
                            prj.Description,
                            prj.KPICycleId,
                            prj.KPITypeId,
                            prj.ResultUnitId,
                            prj.ResultWeightPercentageFromObjectives,
                            prj.StratigicObjectiveId,
                            prj.WeightFromObjective,
                            prj.Target,
                            so.Year,
                            projectPercentageFromEntireStratigic = (
                                prj.ResultWeightPercentageFromObjectives *
                                db.StratigicObjectivesCollection.Where(a => a.id == prj.StratigicObjectiveId)
                                    .FirstOrDefault().Weight) / 100,
                            prj.UnitId,
                            prj.PlannedCost,
                            StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                            StratigicObjectiveShortName = ((languageCode == "ar")
                                ? (so.Name2.Length > 30 ? so.Name2.Substring(0, 30) + "..." : so.Name2)
                                : (so.Name.Length > 30 ? so.Name.Substring(0, 30) + "..." : so.Name)),
                            UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                            WeightDesc = Math.Round(prj.Weight, 0) + " %",
                            prj.p_type,
                            planned_status = (prj.planned_status == 1) ? "Waiting Approval" :
                                (prj.planned_status == 2) ? "Confirmed" : "Declined",
                            prj.Result,
                            prj.assessment_status,
                            plannedStatus = prj.planned_status,
                            assessment_status_text = (prj.assessment_status == 2) ? "Confirmed" :
                                (prj.assessment_status == 3) ? "Declined" : "Waiting Approval",
                            resultColor = db.TrafficLightCollection.Where(s => s.year == yearId &&
                                                                               s.company_id == companyId &&
                                                                               (prj.Result >= s.perc_from &&
                                                                                   prj.Result <= s.perc_to))
                                .Select(s => s.color).DefaultIfEmpty("white")
                        }
                    ).ToList().AsQueryable();
                    var PrjKpis = (from kpi in db.ObjectiveKpiCollection.Where(s => s.is_obj_kpi == 2) select kpi)
                        .ToList().AsQueryable();
                    // var  PrjKpis = PrjKpis1.GroupBy(x => x.Name).Select(x => x.FirstOrDefault());
                    var _ProjectResultCollection = db.ProjectResultCollection.ToList();
                    var KPIs = (from kpi in PrjKpis
                        select new
                        {
                            kpi.ID,
                            kpi.Weight,
                            kpiType = kpi.C_KPI_TYPE_ID,
                            kpi.objective_id,
                            kpi.BetterUpDown,
                            KPI_name = kpi.Name,
                            Q1_ID = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 1 && q1.kpi_id == kpi.ID
                                select q1.Id).DefaultIfEmpty().Sum(),
                            Q1_P = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 1 && q1.kpi_id == kpi.ID
                                select q1.PlannedResult).DefaultIfEmpty().Sum(),
                            Q1_A = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 1 && q1.kpi_id == kpi.ID
                                select q1.ActualResult).DefaultIfEmpty().Sum(),
                            Q2_ID = (from q2 in _ProjectResultCollection
                                where q2.ProjectId == kpi.objective_id
                                      && q2.PeriodNo == 2 && q2.kpi_id == kpi.ID
                                select q2.Id).DefaultIfEmpty().Sum(),
                            Q2_P = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 2 && q1.kpi_id == kpi.ID
                                select q1.PlannedResult).DefaultIfEmpty().FirstOrDefault(),
                            Q2_A = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 2 && q1.kpi_id == kpi.ID
                                select q1.ActualResult).DefaultIfEmpty().Sum(),

                            Q3_ID = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 3 && q1.kpi_id == kpi.ID
                                select q1.Id).DefaultIfEmpty().Sum(),
                            Q3_P = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 3 && q1.kpi_id == kpi.ID
                                select q1.PlannedResult).DefaultIfEmpty().Sum(),
                            Q3_A = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 3 && q1.kpi_id == kpi.ID
                                select q1.ActualResult).DefaultIfEmpty().Sum(),
                            Q4_ID = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 4 && q1.kpi_id == kpi.ID
                                select q1.Id).DefaultIfEmpty().Sum(),
                            Q4_P = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 4 && q1.kpi_id == kpi.ID
                                select q1.PlannedResult).DefaultIfEmpty().FirstOrDefault(),
                            Q4_A = (from q1 in _ProjectResultCollection
                                where q1.ProjectId == kpi.objective_id
                                      && q1.PeriodNo == 4 && q1.kpi_id == kpi.ID
                                select q1.ActualResult).DefaultIfEmpty().Sum(),
                            Target = kpi.Target,
                            AnnualResult = kpi.result
                        }).ToList().AsQueryable();
                    //on prj.ID equals kpi.objective_id
                    var _ProjectEvidenceCollection = db.ProjectEvidenceCollection.ToList();
                    var result = (from res in projects
                        select new
                        {
                            res.plannedStratigy,
                            res.WeigthValue,
                            res.ID,
                            res.Name,
                            res.CompanyId,
                            res.Code,
                            res.CreatedBy,
                            res.CreatedDate,
                            res.ModifiedBy,
                            res.ModifiedDate,
                            res.Order,
                            res.ResultPercentage,
                            res.ResultWeightPercentage,
                            res.Weight,
                            res.ActualCost,
                            res.BranchId,
                            res.Description,
                            res.KPICycleId,
                            res.KPITypeId,
                            res.ResultUnitId,
                            res.ResultWeightPercentageFromObjectives,
                            res.StratigicObjectiveId,
                            res.WeightFromObjective,
                            res.Target,
                            res.Year,
                            res.projectPercentageFromEntireStratigic,
                            //res.KPI_name,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.StratigicObjectiveShortName,
                            res.WeightDesc,
                            res.UnitName,
                            res.Result,
                            res.KPIWeight,
                            res.SoWeight,
                            res.resultColor,
                            style = (res.p_type == 2) ? "{\"background-color\":\"#959595\"}" : "",
                            KPIs = (from k in KPIs
                                where k.objective_id == res.ID
                                select new
                                {
                                    k.ID,
                                    k.Weight,
                                    k.objective_id,
                                    k.KPI_name,
                                    k.BetterUpDown,
                                    k.kpiType,
                                    k.Q1_A,
                                    k.Q1_ID,
                                    k.Q1_P,
                                    k.Q2_A,
                                    k.Q2_ID,
                                    k.Q2_P,
                                    k.Q3_A,
                                    k.Q3_ID,
                                    k.Q3_P,
                                    k.Q4_A,
                                    k.Q4_ID,
                                    k.Q4_P,
                                    k.Target,
                                    k.AnnualResult,
                                    Q1_Disabled = false,
                                    Q2_Disabled = false,
                                    Q3_Disabled = false,
                                    Q4_Disabled = false,
                                }).ToList(),
                            RequiredsDocumnets = (from doc in _ProjectEvidenceCollection
                                where doc.ProjectId == res.ID && string.IsNullOrEmpty(doc.FileName)
                                select new
                                {
                                    doc.ID,
                                    doc.doc_name,
                                    doc.FileName
                                }),

                            UploadedDocuments = (from doc in _ProjectEvidenceCollection
                                where doc.ProjectId == res.ID && !string.IsNullOrEmpty(doc.FileName)
                                select new
                                {
                                    doc.ID,
                                    doc.doc_name,
                                    doc.FileName,
                                    doc.FileUrl
                                }),
                            res.planned_status,
                            res.assessment_status,
                            res.assessment_status_text,
                            res.plannedStatus,
                            Approvals = db.ProjectApprovalHistoryCollection.Where(a => a.ProjectId == res.ID)
                                .OrderByDescending(a => a.ID).ToList()
                        }).ToList();
                    if (result.Count > 0)
                    {
                        result = result.OrderByDescending(x => x.ID).ToList();
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

        [HttpGet]
        public IHttpActionResult GetProjectsAssessmentWithEmployeeAssessment(long companyId, int? branchId, int yearId,
            int? unitId,
            int stratigicObjectiveId, string languageCode)
        {
            try
            {
                using (HRContext DB = new HRContext())
                {
                    var projects = (
                        from prj in db.ProjectsCollection.Where(p => (
                            ((stratigicObjectiveId != 0) ? p.StratigicObjectiveId == stratigicObjectiveId : 1 == 1)
                            && ((unitId != -1 && unitId != null) ? p.UnitId == unitId : p.UnitId == p.UnitId)
                            && ((companyId != 0) ? p.CompanyId == companyId : p.CompanyId == p.CompanyId)
                            && ((branchId != null) ? p.BranchId == branchId : p.BranchId == p.BranchId)))
                        join so in db.StratigicObjectivesCollection.Where(o => o.Year == yearId) on
                            prj.StratigicObjectiveId equals so.id
                        join unit in db.UnitCollection on prj.UnitId equals unit.ID
                        join ok in db.ObjectiveKpiCollection.Where(a => a.CompanyId == companyId) on prj.KPI equals ok
                            .ID.ToString()
                        select new
                        {
                            empObjectives = (from empO in db.EmployeeObjectiveCollection
                                join empAss in db.EmployeeAssesmentCollection on empO.emp_assesment_id equals empAss.ID
                                join emp in db.EmployeesCollection on empAss.employee_id equals emp.ID
                                where empO.project_id == prj.ID
                                select new
                                {
                                    empO.name,
                                    empO.name2,
                                    empO.final_point_result,
                                    empO.result_without_round,
                                    empAss.final_result,
                                    empAss.objectives_result,
                                    empAss.result_before_round,
                                    empO.emp_assesment_id,
                                    emp.name1_1,
                                    emp.ID,
                                    emp.name1_4,
                                    KPIs = (from objKpiId in db.EmployeeObjectiveKPICollection
                                        where objKpiId.emp_obj_id == empO.ID
                                        select new
                                        {
                                            objKpiId.Result,
                                            objKpiId.name,
                                            objKpiId.name2,
                                            resultPerc = ((from q1 in db.EmployeeObjectiveKPIAssessmentCollection
                                                              where q1.EmployeeObjectiveKpiId == objKpiId.ID &&
                                                                    q1.Result > 0
                                                              select q1.Result).Sum() /
                                                          (from q2 in db.EmployeeObjectiveKPIAssessmentCollection
                                                              where q2.EmployeeObjectiveKpiId == objKpiId.ID &&
                                                                    q2.Target > 0
                                                              select q2.Target).Sum()) * 100,
                                        }).ToList()
                                }).ToList(),
                            prj.ID,
                            Name = ((languageCode == "ar") ? prj.Name2 : prj.Name),


                            prj.UnitId,
                            prj.PlannedCost,
                            StratigicObjectiveName = ((languageCode == "ar") ? so.Name2 : so.Name),
                            StratigicObjectiveShortName = ((languageCode == "ar")
                                ? (so.Name2.Length > 30 ? so.Name2.Substring(0, 30) + "..." : so.Name2)
                                : (so.Name.Length > 30 ? so.Name.Substring(0, 30) + "..." : so.Name)),
                            UnitName = ((languageCode == "ar") ? unit.name2 : unit.NAME),
                            WeightDesc = Math.Round(prj.Weight, 0) + " %",
                            prj.p_type,
                            planned_status = (prj.planned_status == 1) ? "Waiting Approval" :
                                (prj.planned_status == 2) ? "Confirmed" : "Declined",
                            prj.Result,
                            prj.assessment_status,
                        }
                    );
                    var result1 = (from res in projects
                        select new
                        {
                            empObjectives = (from r in res.empObjectives
                                select new
                                {
                                    r.name,
                                    r.name2,
                                    r.emp_assesment_id,
                                    r.name1_1,
                                    r.name1_4,
                                    r.ID,
                                    Result = (from t in r.KPIs select t.resultPerc).Average()
                                }),
                            res.ID,
                            res.Name,
                            //res.KPI_name,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.StratigicObjectiveShortName,
                            res.WeightDesc,
                            res.UnitName,
                            style = (res.p_type == 2) ? "{\"background-color\":\"#959595\"}" : "",
                            res.planned_status,
                            res.assessment_status
                        });
                    var result = (from res in result1
                        select new
                        {
                            empObjectives = (from r in res.empObjectives
                                select new
                                {
                                    r.name,
                                    r.name2,
                                    r.ID,
                                    r.emp_assesment_id,
                                    r.name1_1,
                                    r.name1_4,
                                    r.Result
                                }),
                            EmpAvg = (from i in res.empObjectives select i.Result).Average(),
                            res.ID,
                            res.Name,
                            //res.KPI_name,
                            res.UnitId,
                            res.PlannedCost,
                            res.StratigicObjectiveName,
                            res.UnitName,
                            res.style,
                            res.assessment_status
                        }).ToList();

                    if (result.Count > 0)
                    {
                        result = result.Where(a => a.empObjectives.Any()).OrderByDescending(x => x.ID).ToList();
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


        //UploadFiles?docID=" + docID + "&projectID="
        [HttpPost]
        public IHttpActionResult UploadFiles(int docID, int projectID, string username)
        {
            try
            {
                string fileUrl = string.Empty;
                string fileName = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        fileName = docID + "_" + projectID + "_" + file.FileName;
                        file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/" + fileName));
                        fileUrl = ConfigurationManager.AppSettings["DocumentURL"] + fileName;
                    }
                }

                ProjectEvidenceEntity evident = db.ProjectEvidenceCollection.Where(e => e.ID == docID).FirstOrDefault();

                evident.ModifiedBy = username;
                evident.ModifiedDate = DateTime.Now;
                evident.FileName = fileName;
                evident.FileUrl = fileUrl;
                evident.ProjectId = projectID;

                db.Entry(evident).State = EntityState.Modified;
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
        public IHttpActionResult RemoveEvident(int docID, string username)
        {
            try
            {
                ProjectEvidenceEntity evident = db.ProjectEvidenceCollection.Where(e => e.ID == docID).FirstOrDefault();
                evident.FileName = string.Empty;
                evident.ModifiedBy = username;
                evident.ModifiedDate = DateTime.Now;

                db.Entry(evident).State = EntityState.Modified;

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


        [HttpPost]
        public IHttpActionResult UpdateProjectsAssessment(string username, string lang)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var projectsAssessmentDataData =
                        JsonConvert.DeserializeObject<dynamic[]>(
                            HttpContext.Current.Request.Form["ProjectsAssessmentData"]);
                    string ModifiedBy = username;
                    int iKPICycleId;
                    string Errors = string.Empty;

                    for (int i = 0; i < projectsAssessmentDataData.Length; i++)
                    {
                        //int prjID = Convert.ToInt32(projectsAssessmentDataData[i].ProjectID);
                        //int hasDocumentsNeeded = db.ProjectEvidenceCollection.Where(e => e.ProjectId == prjID).Count();
                        //if (hasDocumentsNeeded > 0)
                        //{
                        //    int unUploadedDoc = db.ProjectEvidenceCollection.Where(e => e.ProjectId == prjID && string.IsNullOrEmpty(e.FileName)).Count();
                        //    if (unUploadedDoc > 0)
                        //    {
                        //        Errors = db.tbl_resourcesCollection.Where(e => e.url == "BackEnd" && e.resource_key == "projAssessmentNeededDocs" && e.culture_name == lang).FirstOrDefault().resource_value;
                        //        continue;
                        //    }
                        //}
                        // loop in kpis list
                        foreach (var itm in projectsAssessmentDataData[i].KPIs)
                        {
                            long kpiID = itm.ID;
                            int kpi_prj_id = itm.objective_id;
                            int kpiType = itm.kpiType;
                            float? Q1_A = (float?)itm.Q1_A;
                            float? Q2_A = (float?)itm.Q2_A;
                            float? Q3_A = (float?)itm.Q3_A;
                            float? Q4_A = (float?)itm.Q4_A;
                            float? Q1_P = (float?)itm.Q1_P;
                            float? Q2_P = (float?)itm.Q2_P;
                            float? Q3_P = (float?)itm.Q3_P;
                            float? Q4_P = (float?)itm.Q4_P;

                            ProjectResultEntity prjAssQ1 = db.ProjectResultCollection
                                .FirstOrDefault(x => x.kpi_id == kpiID && x.ProjectId == kpi_prj_id && x.PeriodNo == 1);
                            if (prjAssQ1 != null)
                            {
                                prjAssQ1.ActualResult = Q1_A;
                                db.Entry(prjAssQ1).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            ProjectResultEntity prjAssQ2 = db.ProjectResultCollection
                                .FirstOrDefault(x => x.kpi_id == kpiID && x.ProjectId == kpi_prj_id && x.PeriodNo == 2);
                            if (prjAssQ2 != null)
                            {
                                prjAssQ2.ActualResult = Q2_A;
                                db.Entry(prjAssQ2).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            ProjectResultEntity prjAssQ3 = db.ProjectResultCollection
                                .FirstOrDefault(x => x.kpi_id == kpiID && x.ProjectId == kpi_prj_id && x.PeriodNo == 3);
                            if (prjAssQ3 != null)
                            {
                                prjAssQ3.ActualResult = Q3_A;
                                db.Entry(prjAssQ3).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            ProjectResultEntity prjAssQ4 = db.ProjectResultCollection
                                .FirstOrDefault(x => x.kpi_id == kpiID && x.ProjectId == kpi_prj_id && x.PeriodNo == 4);
                            if (prjAssQ4 != null)
                            {
                                prjAssQ4.ActualResult = Q4_A;
                                db.Entry(prjAssQ4).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            float totalKpiPerc =
                                CalculateResultBasedonKPI_Type(kpiType,
                                    Q1_P == 0 || Q1_P == null ? -1 : Q1_A ?? 0,
                                    Q2_P == 0 || Q1_P == null ? -1 : Q2_A ?? 0,
                                    Q3_P == 0 || Q1_P == null ? -1 : Q3_A ?? 0,
                                    Q4_P == 0 || Q1_P == null ? -1 : Q4_A ?? 0);
                            var PrjKPI = db.ObjectiveKpiCollection.FirstOrDefault(x => x.ID == kpiID);

                            if (PrjKPI != null)
                            {
                                PrjKPI.result = totalKpiPerc;
                                // PrjKPI.Target = db.ProjectResultCollection
                                //     .Where(x => x.kpi_id == kpiID && x.ProjectId == kpi_prj_id)
                                //     .Sum(x => x.PlannedResult);
                                // db.Entry(PrjKPI).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                        int projectID = projectsAssessmentDataData[i].ID;
                        float? actualCost = (float?)projectsAssessmentDataData[i].ActualCost;
                        var objectiveKPIs = db.ObjectiveKpiCollection.Where(x => x.objective_id == projectID);
                        float totalProjectPerc = objectiveKPIs.Any()
                            ? objectiveKPIs.Sum(x =>
                                (x.result / x.Target)
                                * (x.Weight / 100))
                            : 0;
                        var project = db.ProjectsCollection.Where(x => x.ID == projectID).FirstOrDefault();
                        project.Result = totalProjectPerc * 100;
                        project.ResultPercentage = totalProjectPerc;
                        project.ResultWeightPercentageFromObjectives = (project.Result * project.Weight) / 100;
                        project.ActualCost = actualCost;
                        db.Entry(project).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    scope.Complete();

                    return Ok(new { Data = projectsAssessmentDataData, IsError = false, ErrorMessage = Errors });
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

        #region Objectives KPIs Methods

        [HttpGet]
        public IHttpActionResult SaveObjectiveKpi(
            string name,
            string name2,
            string description,
            int objective_id,
            float weight,
            float target,
            int bsc,
            int measurement,
            int company_id,
            int branch_id,
            string username,
            string LanguageCode,
            int isObjKpi,
            int ReviewCycle,
            int kpi_type,
            int? resultUint,
            int betterUpDown = 1
        )
        {
            try
            {
                ObjectiveKPIEntity obj_kpi = new ObjectiveKPIEntity();
                if (betterUpDown != 2 && betterUpDown != 1)
                {
                    return Ok(new
                    {
                        Data = string.Empty, IsError = true,
                        ErrorMessage = "KPI better Up/Down value is invalid"
                    });
                }

                obj_kpi.BetterUpDown = betterUpDown;
                obj_kpi.Name = name;
                if (LanguageCode == "ar")
                    obj_kpi.Name2 = name;
                else
                    obj_kpi.Name = name;

                obj_kpi.Description = description;
                obj_kpi.objective_id = objective_id;
                obj_kpi.Weight = weight;
                obj_kpi.Target = target;
                obj_kpi.bsc = bsc;
                obj_kpi.measurement = measurement;

                obj_kpi.CreatedBy = username;
                obj_kpi.CreatedDate = DateTime.Now;
                obj_kpi.CompanyId = company_id;
                obj_kpi.BranchId = branch_id;
                obj_kpi.result = 0;
                obj_kpi.is_obj_kpi = isObjKpi;

                if (isObjKpi == 1)
                {
                    obj_kpi.C_KPI_CYCLE_ID = 1;
                    obj_kpi.C_KPI_TYPE_ID = kpi_type;
                    obj_kpi.C_RESULT_UNIT_ID = resultUint ?? 0;
                }
                else
                {
                    obj_kpi.C_KPI_CYCLE_ID = ReviewCycle;
                    obj_kpi.C_KPI_TYPE_ID = kpi_type;
                    obj_kpi.C_RESULT_UNIT_ID = resultUint ?? 0;
                }

                db.Entry(obj_kpi).State = System.Data.Entity.EntityState.Added;
                db.ObjectiveKpiCollection.Add(obj_kpi);
                db.SaveChanges();
                return Ok(new { Data = obj_kpi.ID, IsError = false, ErrorMessage = string.Empty });
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

        public class UpdateObjectiveKpiModel
        {
            public int obj_kpi_id { get; set; }
            public string name { get; set; }
            public string name2 { get; set; }
            public string description { get; set; }
            public int objective_id { get; set; }
            public float weight { get; set; }
            public float target { get; set; }
            public int bsc { get; set; }
            public int measurement { get; set; }
            public int company_id { get; set; }
            public int branch_id { get; set; }
            public string username { get; set; }
            public string LanguageCode { get; set; }
            public int ReviewCycle { get; set; }
            public int kpi_type { get; set; }
            public int resultUint { get; set; }
        }

        [HttpGet]
        public IHttpActionResult UpdateObjectiveKpi(
            int obj_kpi_id,
            string name,
            string name2,
            string description,
            int objective_id,
            float weight,
            float target,
            int bsc,
            int measurement,
            int company_id,
            int branch_id,
            string username,
            string LanguageCode,
            int ReviewCycle,
            int kpi_type,
            int resultUint,
            int betterUpDown = 1
        )
        {
            try
            {
                ObjectiveKPIEntity obj_kpi = db.ObjectiveKpiCollection.Where(e => e.ID == obj_kpi_id).FirstOrDefault();
                if (obj_kpi != null)
                {
                    var Weights = (from obj in db.ObjectiveKpiCollection
                            where
                                (obj.CompanyId == company_id) &&
                                (obj.objective_id == objective_id) &&
                                (obj.ID != obj_kpi_id)
                            select obj.Weight
                        ).DefaultIfEmpty().Sum();
                    if ((Weights + weight) > 100)
                    {
                        return Ok(new
                        {
                            Data = string.Empty, IsError = true,
                            ErrorMessage = "Sum of weights greater than 100!, please add another weight value"
                        });
                    }

                    if (betterUpDown != 2 && betterUpDown != 1)
                    {
                        return Ok(new
                        {
                            Data = string.Empty, IsError = true,
                            ErrorMessage = "KPI better Up/Down value is invalid"
                        });
                    }

                    obj_kpi.BetterUpDown = betterUpDown;
                    obj_kpi.Name = name;

                    if (LanguageCode == "ar")
                        obj_kpi.Name2 = name;
                    else
                        obj_kpi.Name = name;

                    obj_kpi.Description = description;
                    obj_kpi.objective_id = objective_id;
                    obj_kpi.Weight = weight;
                    obj_kpi.Target = target;
                    obj_kpi.bsc = bsc;
                    obj_kpi.measurement = measurement;

                    obj_kpi.CreatedBy = username;
                    obj_kpi.CreatedDate = DateTime.Now;
                    obj_kpi.CompanyId = company_id;
                    obj_kpi.BranchId = branch_id;

                    if (obj_kpi.is_obj_kpi == 1)
                    {
                        obj_kpi.C_KPI_CYCLE_ID = 1;
                        obj_kpi.C_KPI_TYPE_ID = kpi_type;
                        obj_kpi.C_RESULT_UNIT_ID = resultUint;
                    }
                    else
                    {
                        obj_kpi.C_KPI_CYCLE_ID = ReviewCycle;
                        obj_kpi.C_KPI_TYPE_ID = kpi_type;
                        obj_kpi.C_RESULT_UNIT_ID = resultUint;
                    }


                    db.Entry(obj_kpi).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
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
        public float CalculateTargetBasedonKPI_Type(
            int KPITypeId,
            float Q1_Target = 0,
            float Q2_Target = 0,
            float Q3_Target = 0,
            float Q4_Target = 0)
        {
            var Q_Targetslst = new List<float>() { Q1_Target, Q2_Target, Q3_Target, Q4_Target };
            Q_Targetslst.RemoveAll(i => i == 0);
            if (Q_Targetslst.Any() == false)
            {
                return 0;
            }

            float Target = 0;
            switch (KPITypeId)
            {
                case (int)Enums.Enums.KPYType.Accumulative:
                    Target = Q_Targetslst.Sum();
                    break;
                case (int)Enums.Enums.KPYType.Average:
                    Target = Q_Targetslst.Average();
                    break;
                case (int)Enums.Enums.KPYType.Last:
                    Target = Q_Targetslst.Last();
                    break;
                case (int)Enums.Enums.KPYType.Maximum:
                    Target = Q_Targetslst.Max();
                    break;
                case (int)Enums.Enums.KPYType.Minimum:
                    Target = Q_Targetslst.Min();
                    break;
                default:
                    break;
            }

            return Target;
        }


        public float CalculateResultBasedonKPI_Type(
            int KPITypeId,
            float Q1_Result = 0,
            float Q2_Result = 0,
            float Q3_Result = 0,
            float Q4_Result = 0)
        {
            var Q_Resultslst = new List<float>() { Q1_Result, Q2_Result, Q3_Result, Q4_Result };
            Q_Resultslst.RemoveAll(a => a == -1);
            if (Q_Resultslst.Any() == false)
            {
                return 0;
            }

            float Target = 0;
            switch (KPITypeId)
            {
                case (int)Enums.Enums.KPYType.Accumulative:
                    Target = Q_Resultslst.Sum();
                    break;
                case (int)Enums.Enums.KPYType.Average:
                    Target = Q_Resultslst.Average();
                    break;
                case (int)Enums.Enums.KPYType.Last:
                    Target = Q_Resultslst.Last();
                    break;
                case (int)Enums.Enums.KPYType.Maximum:
                    Target = Q_Resultslst.Max();
                    break;
                case (int)Enums.Enums.KPYType.Minimum:
                    Target = Q_Resultslst.Min();
                    break;
                default:
                    break;
            }

            return Target;
        }
        //KPYType

        public IHttpActionResult GetUnuitTargetVsActualResult(int yearId, int companyId, string languageCode)
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
                    List<UnitProjectsPerformanceEntity> unitsPerformance2 =
                        db.UnitProjectsPerformanceCollection.ToList();
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

                return Ok(new { Data = obj, IsError = false, ErrorMessage = string.Empty });
                //return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult DeleteObjectiveKpi(long id)
        {
            try
            {
                var objectiveKpi = db.ObjectiveKpiCollection.Where(x => x.ID == id).FirstOrDefault();
                if (objectiveKpi != null)
                {
                    db.ObjectiveKpiCollection.Remove(objectiveKpi);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getObjectiveKpiByObjective(int objective_id, string languageCode, int? BSC)
        {
            try
            {
                // convert to foreach using StratigicObjectivesCollection table 
                var objecitves_kpis = (
                    from x in db.ObjectiveKpiCollection
                    where x.objective_id == objective_id && (BSC == null || x.bsc == BSC)
                    select new
                    {
                        x.ID,
                        Name = ((languageCode == "ar") ? x.Name2 : x.Name),
                        x.CompanyId,
                        x.CreatedBy,
                        x.CreatedDate,
                        x.ModifiedBy,
                        x.ModifiedDate,
                        x.Weight,
                        WeightDesc = x.Weight + " %",
                        x.BranchId,
                        x.BetterUpDown,
                        x.Description,
                        x.Target,
                        TargetDesc = (x.measurement == 1 ? x.Target + " %" : x.Target.ToString()),
                        x.objective_id,
                        x.bsc,
                        BSC_Name =
                            (x.bsc == 1) ? db.tbl_resourcesCollection
                                .Where(t => t.resource_key == "lblKpiBSCFinancialOption" &&
                                            t.url == "#/stratigicobjectives").Select(t => t.resource_value)
                                .FirstOrDefault() :
                            (x.bsc == 2) ? db.tbl_resourcesCollection
                                .Where(t => t.resource_key == "lblKpiBSCCustomersOption" &&
                                            t.url == "#/stratigicobjectives").Select(t => t.resource_value)
                                .FirstOrDefault() :
                            (x.bsc == 3) ? db.tbl_resourcesCollection
                                .Where(t => t.resource_key == "lblKpiBSCInternalProcessOption" &&
                                            t.url == "#/stratigicobjectives").Select(t => t.resource_value)
                                .FirstOrDefault() :
                            (x.bsc == 4) ? db.tbl_resourcesCollection
                                .Where(t => t.resource_key == "lblKpiBSCLearninggrowthOption" &&
                                            t.url == "#/stratigicobjectives").Select(t => t.resource_value)
                                .FirstOrDefault() : "",

                        x.measurement,
                        ResultPercent = ((x.result / x.Target) * 100),
                        // result = (((x.result / x.Target) * 100) < 120 ? x.result : (1.20 * x.Target)),
                        // result =  ( x.BetterUpDown==2
                        //         ? 
                        //         ( x.result==0 ? 1.20 * x.Target : x.Target / x.result >1.20 ? 1.20 * x.Target :  x.result )
                        //         : 
                        //         (x.result / x.Target > 1.20 ? 1.20 * x.Target : x.result )
                        //     ),
                        x.result,

                        resultColor = db.TrafficLightCollection.Where(s =>
                                s.year == db.StratigicObjectivesCollection.Where(a =>
                                    a.id == objective_id).Select(a => a.Year).FirstOrDefault() &&
                                s.company_id == x.CompanyId &&
                                (
                                    (x.BetterUpDown == 2
                                        ? (x.result == 0 ? 1.20 :
                                            x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                        : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                    ) * 100 >= s.perc_from
                                    &&
                                    (x.BetterUpDown == 2
                                        ? (x.result == 0 ? 1.20 :
                                            x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                        : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                    ) * 100 <= s.perc_to
                                )
                            )
                            .Select(s => s.color).DefaultIfEmpty("white"),


                        //ActulleWiegth = (((x.Weight * (from obj in db.StratigicObjectivesCollection
                        //                               where
                        //                               (obj.CompanyId == x.CompanyId) &&
                        //                               (obj.id == x.objective_id)
                        //                               select obj.Weight
                        //).Sum()) * x.result) / x.Target)
                        ActulleWiegth =
                            x.Weight * (db.StratigicObjectivesCollection
                                         .Where(objs => objs.CompanyId == x.CompanyId)
                                         .Select(objs => objs.Weight).Sum())
                                     *
                                     (x.BetterUpDown == 2
                                         ? (x.result == 0 ? 1.20 :
                                             x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                         : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                     )
                            / 100,


                        PlannedWeights = ((from obj in db.StratigicObjectivesCollection
                                where
                                    (obj.id == x.objective_id)
                                select obj.Weight
                            ).Count() > 0
                                ? (from obj in db.StratigicObjectivesCollection
                                    where
                                        (obj.CompanyId == x.CompanyId) &&
                                        (obj.id == x.objective_id)
                                    select obj.Weight
                                ).Sum()
                                : 0)
                    }
                ).ToList();

                var data = (from x in objecitves_kpis
                    select new
                    {
                        x.ID,
                        x.Name,
                        x.CompanyId,
                        x.CreatedBy,
                        x.CreatedDate,
                        x.ModifiedBy,
                        x.ModifiedDate,
                        x.Weight,
                        x.BetterUpDown,
                        x.WeightDesc,
                        x.BranchId,
                        x.Description,
                        x.Target,
                        x.TargetDesc,
                        x.objective_id,
                        x.bsc,
                        x.measurement,
                        x.ResultPercent,
                        result = Convert.ToDecimal(x.result.ToString("#,##0.00")),
                        x.resultColor,
                        x.BSC_Name,
                        StrategyWiegth =
                            Convert.ToDecimal(((x.PlannedWeights * x.Weight) / 100.00).ToString("#,##0.00")),
                        ActulleWiegth =
                            Convert.ToDecimal((((((x.PlannedWeights * x.Weight) / 100.00)))
                                               *
                                               (x.BetterUpDown == 2
                                                   ? (x.result == 0 ? 1.20 :
                                                       x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                                   : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                               )
                                )
                                .ToString("#,##0.000")),
                    }).ToList();
                var TotalActuleWiegth = data.Sum(x => x.ActulleWiegth);
                var TotalStrategyWiegth = data.Sum(x => x.StrategyWiegth);
                return Ok(new
                {
                    TotalActuleWiegth = Convert.ToDecimal(TotalActuleWiegth.ToString("#,##0.00")),
                    TotalStrategyWiegth = Convert.ToDecimal(TotalStrategyWiegth.ToString("#,##0.00")),
                    Data = data,
                    IsError = false,
                    ErrorMessage = string.Empty
                });
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
        public IHttpActionResult getObjectiveCollectionSummary(long companyId, string languageCode, int year, int? BSC)
        {
            try
            {
                ////

                if (year <= 0 || companyId == -1)
                {
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                var stratigicObjectives = (from obj in db.StratigicObjectivesCollection
                        where
                            (obj.CompanyId == companyId) &&
                            (obj.Year == year)
                        orderby obj.id descending
                        select new
                        {
                            serial = 1,
                            obj.id,
                            name = ((languageCode == "ar") ? obj.Name2 : obj.Name),
                            obj.CompanyId,
                            obj.Code,
                            obj.CreatedBy,
                            obj.CreatedDate,
                            obj.ModifiedBy,
                            obj.ModifiedDate,
                            obj.Order,
                            obj.ResultPercentage,
                            obj.ResultWeightPercentage,
                            obj.Weight,
                            obj.Year,
                            BSC_Name =
                                (obj.bsc == 1) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCFinancialOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 2) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCCustomersOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 3) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCInternalProcessOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() :
                                (obj.bsc == 4) ? db.tbl_resourcesCollection
                                    .Where(x => x.resource_key == "lblKpiBSCLearninggrowthOption" &&
                                                x.url == "#/stratigicobjectives").Select(x => x.resource_value)
                                    .FirstOrDefault() : "",


                            WeightDesc = Math.Round(obj.Weight, 0) + " " + "%",
                            nameShortcut = ((languageCode == "ar")
                                ? (obj.Name2.Length > 20 ? obj.Name2.Substring(0, 30) + "..." : obj.Name2)
                                : (obj.Name.Length > 20 ? obj.Name.Substring(0, 30) + "..." : obj.Name)),
                        }
                    ).ToList().OrderBy(x => x.id).Select((obj, i) => new
                    {
                        serial = i + 1,
                        obj.id,
                        obj.name,
                        obj.CompanyId,
                        obj.Code,
                        obj.CreatedBy,
                        obj.CreatedDate,
                        obj.ModifiedBy,
                        obj.ModifiedDate,
                        obj.Order,
                        obj.ResultPercentage,
                        obj.ResultWeightPercentage,
                        obj.Weight,
                        obj.Year,
                        obj.BSC_Name,
                        obj.WeightDesc,
                        obj.nameShortcut
                    });


                ///

                var finalResult = new List<object>();

                foreach (var item in stratigicObjectives)
                {
                    int objective_id = item.id;

                    var objecitves_kpis = (
                        from x in db.ObjectiveKpiCollection
                        where x.objective_id == objective_id && (BSC == null || x.bsc == BSC)
                        select new
                        {
                            x.ID,
                            Name = ((languageCode == "ar") ? x.Name2 : x.Name),
                            x.CompanyId,
                            x.Weight,
                            WeightDesc = x.Weight + " %",
                            x.Target,
                            x.objective_id,
                            x.measurement,
                            x.BetterUpDown,
                            ResultPercent = ((x.result / x.Target) * 100),
                            x.result,
                            resultColor = db.TrafficLightCollection.Where(s =>
                                    s.year == db.StratigicObjectivesCollection.Where(a =>
                                        a.id == objective_id).Select(a => a.Year).FirstOrDefault() &&
                                    s.company_id == x.CompanyId &&
                                    (
                                        (x.BetterUpDown == 2
                                            ? (x.result == 0 ? 1.20 :
                                                x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                            : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                        ) * 100 >= s.perc_from
                                        &&
                                        (x.BetterUpDown == 2
                                            ? (x.result == 0 ? 1.20 :
                                                x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                            : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                        ) * 100 <= s.perc_to
                                    )
                                )
                                .Select(s => s.color).DefaultIfEmpty("white"),

                            ActulleWiegth = x.Weight *
                                            (db.StratigicObjectivesCollection
                                                .Where(objs => objs.CompanyId == x.CompanyId)
                                                .Select(objs => objs.Weight).Sum())
                                            *
                                            (x.BetterUpDown == 2
                                                ? (x.result == 0 ? 1.20 :
                                                    x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                                : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                            )
                                            / 100,


                            PlannedWeights = ((from obj in db.StratigicObjectivesCollection
                                    where
                                        (obj.CompanyId == x.CompanyId) &&
                                        (obj.id == x.objective_id)
                                    select obj.Weight
                                ).Count() > 0
                                    ? (from obj in db.StratigicObjectivesCollection
                                        where
                                            (obj.CompanyId == x.CompanyId) &&
                                            (obj.id == x.objective_id)
                                        select obj.Weight
                                    ).Sum()
                                    : 0)
                        }
                    ).ToList();

                    var data = (from x in objecitves_kpis
                        select new
                        {
                            x.ID,
                            x.Name,
                            x.CompanyId,
                            x.Weight,
                            x.WeightDesc,
                            x.Target,
                            x.objective_id,
                            x.measurement,
                            x.ResultPercent,
                            result = Convert.ToDecimal(x.result.ToString("#,##0.00")),
                            x.resultColor,
                            StrategyWiegth =
                                Convert.ToDecimal(((x.PlannedWeights * x.Weight) / 100.00).ToString("#,##0.00")),
                            ActulleWiegth =
                                Convert.ToDecimal((((((x.PlannedWeights * x.Weight) / 100.00)))
                                                   *
                                                   (x.BetterUpDown == 2
                                                       ? (x.result == 0 ? 1.20 :
                                                           x.Target / x.result > 1.20 ? 1.20 : x.Target / x.result)
                                                       : (x.result / x.Target > 1.20 ? 1.20 : x.result / x.Target)
                                                   )
                                    )
                                    .ToString("#,##0.000")),
                        }).ToList();
                    var TotalActuleWiegth = data.Sum(x => x.ActulleWiegth);
                    var TotalStrategyWiegth = data.Sum(x => x.StrategyWiegth);


                    finalResult.Add(
                        new
                        {
                            item.id,
                            item.name,
                            TotalActuleWiegth,
                            TotalStrategyWiegth
                        }
                    );
                }

                // convert to foreach using StratigicObjectivesCollection table 
                return Ok(new
                {
                    Data = finalResult,
                    IsError = false,
                    ErrorMessage = string.Empty
                });
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
        public IHttpActionResult getObjectiveKpiByID(int ID, string languageCode)
        {
            try
            {
                float maxWeights = 0;
                ObjectiveKPIEntity obj = db.ObjectiveKpiCollection.Where(x => x.ID == ID).FirstOrDefault();
                if (obj != null)
                {
                    List<ObjectiveKPIEntity> obkKpis = db.ObjectiveKpiCollection
                        .Where(x => x.objective_id == obj.objective_id).ToList();
                    if (obkKpis != null)
                    {
                        maxWeights = 100 - obkKpis.Sum(x => x.Weight) + obj.Weight;
                    }
                }


                var objecitves_kpis = (
                    from x in db.ObjectiveKpiCollection
                    where x.ID == ID
                    select new
                    {
                        x.ID,
                        Name = ((languageCode == "ar") ? x.Name2 : x.Name),
                        x.CompanyId,
                        x.CreatedBy,
                        x.CreatedDate,
                        x.ModifiedBy,
                        x.ModifiedDate,
                        x.Weight,
                        x.BetterUpDown,
                        x.BranchId,
                        x.Description,
                        x.Target,
                        x.objective_id,
                        x.bsc,
                        x.measurement,
                        x.result,
                        x.C_KPI_CYCLE_ID,
                        x.C_KPI_TYPE_ID,
                        x.C_RESULT_UNIT_ID,
                        max = maxWeights,
                        q1_traget = db.ProjectResultCollection.Where(s => s.kpi_id == x.ID && s.PeriodNo == 1)
                            .Select(s => s.PlannedResult).DefaultIfEmpty(),
                        q2_traget = db.ProjectResultCollection.Where(s => s.kpi_id == x.ID && s.PeriodNo == 2)
                            .Select(s => s.PlannedResult).DefaultIfEmpty(),
                        q3_traget = db.ProjectResultCollection.Where(s => s.kpi_id == x.ID && s.PeriodNo == 3)
                            .Select(s => s.PlannedResult).DefaultIfEmpty(),
                        q4_traget = db.ProjectResultCollection.Where(s => s.kpi_id == x.ID && s.PeriodNo == 4)
                            .Select(s => s.PlannedResult).DefaultIfEmpty()
                    }
                ).FirstOrDefault();

                return Ok(new { Data = objecitves_kpis, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult saveObjectivesKPIsAssessment()
        {
            try
            {
                var kpisData = JsonConvert.DeserializeObject<dynamic>(HttpContext.Current.Request.Form["kpiData"]);

                foreach (var itm in kpisData)
                {
                    int id = Convert.ToInt32(itm.ID);
                    float result = itm.result;
                    var objKPI = db.ObjectiveKpiCollection.Where(e => e.ID == id).FirstOrDefault();
                    objKPI.result = result;
                    db.Entry(objKPI).State = EntityState.Modified;
                    db.SaveChanges();
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
        public IHttpActionResult getObjectivesKPIRemainingWeight(int objective_id, int isObjKpi)
        {
            try
            {
                float max = 0;
                List<ObjectiveKPIEntity> list = db.ObjectiveKpiCollection.Where(x =>
                    x.objective_id == objective_id && x.is_obj_kpi == isObjKpi).ToList();

                if (list != null)
                {
                    max = 100 - list.Sum(x => x.Weight);
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

    public class ProjectsAssessment
    {
        public long ProjectID { get; set; }
        public string ProjectName { get; set; }

        public string UnitName { get; set; }

        public float Weight { get; set; }
        public string KPI { get; set; }
        public float FinalResult { get; set; }
        public float ProjectWeightFromobjective { get; set; }
        public float ResultPercentage { get; set; }
        public float ProjectResultPercentage { get; set; }
        public float ResultPercentageFromObjective { get; set; }
        public long Q1_ID { get; set; }
        public float Q1_P { get; set; }
        public float? Q1_A { get; set; }
        public long Q2_ID { get; set; }
        public float Q2_P { get; set; }
        public float? Q2_A { get; set; }
        public long Q3_ID { get; set; }
        public float Q3_P { get; set; }
        public float? Q3_A { get; set; }
        public long Q4_ID { get; set; }
        public float Q4_P { get; set; }
        public float? Q4_A { get; set; }
    }
}