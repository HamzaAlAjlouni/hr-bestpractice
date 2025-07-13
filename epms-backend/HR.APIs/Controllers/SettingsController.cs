using HR.Entities;
using HR.Entities.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class SettingsController : ApiController
    {
        HRContext db;

        public SettingsController()
        {
            db = new HRContext();
        }


        #region Skills Methods

        [HttpGet]
        public IHttpActionResult SaveSkillsTypes(string code, string name, string languageCode)
        {
            try
            {
                tbl_skills_types skill = new tbl_skills_types();

                skill.code = code;
                skill.name = name;
                if (languageCode == "en")
                    skill.name = name;
                else
                    skill.name2 = name;

                skill.created_by = "ADMIN";
                skill.created_date = DateTime.Now;

                db.Entry(skill).State = System.Data.Entity.EntityState.Added;
                db.tbl_skills_typesCollection.Add(skill);
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
        public IHttpActionResult UpdateSettings(int id, string code, string name, string languageCode)
        {
            try
            {
                tbl_skills_types skill = db.tbl_skills_typesCollection.Where(x => x.id == id).FirstOrDefault();
                if (skill != null)
                {
                    skill.code = code;
                    if (languageCode == "en")
                        skill.name = name;
                    else
                        skill.name2 = name;

                    db.Entry(skill).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult LoadAllSkillsTypes(string languageCode)
        {
            try
            {
                var skills = (from skilltypes in db.tbl_skills_typesCollection
                    select new
                    {
                        skilltypes.code,
                        name = (languageCode == "en" ? skilltypes.name : skilltypes.name2),
                        skilltypes.id,
                    }).ToList();
                return Ok(new { Data = skills, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult getSkillbyID(long id, string languageCode)
        {
            try
            {
                //var skill = db.tbl_skills_typesCollection.Where(x => x.id == id).FirstOrDefault();
                var skill = (from skilltypes in db.tbl_skills_typesCollection
                    where skilltypes.id == id
                    select new
                    {
                        skilltypes.code,
                        name = (languageCode == "en" ? skilltypes.name : skilltypes.name2),
                        skilltypes.id,
                    }).FirstOrDefault();


                if (skill != null)
                {
                    return Ok(new { Data = skill, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult DeleteSkillByID(long id)
        {
            try
            {
                var skill = db.tbl_skills_typesCollection.Where(x => x.id == id).FirstOrDefault();
                if (skill != null)
                {
                    db.tbl_skills_typesCollection.Remove(skill);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult RemoveSkillsByIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.tbl_skills_typesCollection.RemoveRange(db.tbl_skills_typesCollection.Where(x => ids.Contains(x.id))
                    .ToList());
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Employee Levels Methods

        [HttpGet]
        public IHttpActionResult SaveLevels(string code, string name, int number)
        {
            try
            {
                tbl_emp_levels level = new tbl_emp_levels();

                level.lvl_code = code;
                level.lvl_name = name;
                level.lvl_number = number;

                db.Entry(level).State = System.Data.Entity.EntityState.Added;
                db.tbl_emp_levelsCollection.Add(level);
                db.SaveChanges();
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdateEmpLvl(int id, string code, string name, int number)
        {
            try
            {
                tbl_emp_levels level = db.tbl_emp_levelsCollection.Where(x => x.id == id).FirstOrDefault();
                if (level != null)
                {
                    level.lvl_code = code;
                    level.lvl_name = name;
                    level.lvl_number = number;

                    db.Entry(level).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult LoadAllEmpLevels()
        {
            try
            {
                var levels = db.tbl_emp_levelsCollection.ToList();
                return Ok(new { Data = levels, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult getEmpLvlbyID(long id)
        {
            try
            {
                var level = db.tbl_emp_levelsCollection.Where(x => x.id == id).FirstOrDefault();
                if (level != null)
                {
                    return Ok(new { Data = level, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult DeleteEmpLvlByID(long id)
        {
            try
            {
                var level = db.tbl_emp_levelsCollection.Where(x => x.id == id).FirstOrDefault();
                if (level != null)
                {
                    db.tbl_emp_levelsCollection.Remove(level);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult RemoveLevelsIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.tbl_emp_levelsCollection.RemoveRange(db.tbl_emp_levelsCollection.Where(x => ids.Contains(x.id))
                    .ToList());
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Performance Levels Methods

        [HttpGet]
        public IHttpActionResult SavePerformaceLevels(string name, int number, int percent, int year, int companyId)
        {
            try
            {
                tbl_performance_levels level = new tbl_performance_levels();

                level.lvl_code = "";
                level.lvl_name = name;
                level.lvl_number = number;
                level.lvl_percent = percent;
                level.lvl_year = year;
                level.company_id = companyId;
                db.Entry(level).State = System.Data.Entity.EntityState.Added;
                db.tbl_performance_levelsCollection.Add(level);
                db.SaveChanges();
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdatePerformanceLevel(int id, string name, int number, int percent, int year,
            int companyId)
        {
            try
            {
                tbl_performance_levels level = db.tbl_performance_levelsCollection.Where(x => x.id == id)
                    .FirstOrDefault();
                if (level != null)
                {
                    level.lvl_code = "";
                    level.lvl_name = name;
                    level.lvl_number = number;
                    level.lvl_percent = percent;
                    level.lvl_year = year;
                    level.company_id = companyId;
                    db.Entry(level).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult LoadAllPerformanceLevels(int yearId, int companyId)
        {
            try
            {
                var levels = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == yearId && x.company_id == companyId).ToList();
                return Ok(new { Data = levels, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult getPerformanceByID(long id)
        {
            try
            {
                var level = db.tbl_performance_levelsCollection.Where(x => x.id == id).FirstOrDefault();

                if (level != null)
                {
                    return Ok(new { Data = level, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult DeletePerformanceByID(long id)
        {
            try
            {
                var level = db.tbl_performance_levelsCollection.Where(x => x.id == id).FirstOrDefault();
                if (level != null)
                {
                    db.tbl_performance_levelsCollection.Remove(level);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult RemovePerformanceIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.tbl_performance_levelsCollection.RemoveRange(db.tbl_performance_levelsCollection
                    .Where(x => ids.Contains(x.id)).ToList());
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Performance Levels Quota Methods

        [HttpGet]
        public IHttpActionResult SavePerformaceLevelQuota(float fromPercentage, float toPercentage, int levelNumber,
            int quotaType, int yearId, int companyId)
        {
            try
            {
                PerformancelevelsQuota performancelevelsQuota = new PerformancelevelsQuota();

                performancelevelsQuota.FromPercentage = fromPercentage;
                performancelevelsQuota.ToPercentage = fromPercentage;

                performancelevelsQuota.LevelNumber = levelNumber;
                performancelevelsQuota.QuotaType = quotaType;
                performancelevelsQuota.YearId = yearId;
                performancelevelsQuota.CompanyId = companyId;

                db.Entry(performancelevelsQuota).State = System.Data.Entity.EntityState.Added;
                db.PerformancelevelsQuotaCollection.Add(performancelevelsQuota);
                db.SaveChanges();
                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpPost]
        public IHttpActionResult SavePerformaceQuota()
        {
            try
            {
                RangesQuota rangesQuota =
                    JsonConvert.DeserializeObject<RangesQuota>(HttpContext.Current.Request.Form["Quota"]);


                using (TransactionScope scope = new TransactionScope())
                {
                    if (rangesQuota != null)
                    {
                        foreach (Quota quota in rangesQuota.QuotaList)
                        {
                            PerformancelevelsQuota performancelevelsQuota = new PerformancelevelsQuota();

                            performancelevelsQuota.FromPercentage = rangesQuota.FromPercentage;
                            performancelevelsQuota.ToPercentage = rangesQuota.ToPercentage;

                            performancelevelsQuota.LevelNumber = quota.LevelNumber;
                            performancelevelsQuota.QuotaDirection = quota.QuotaDirection;
                            performancelevelsQuota.QuotaType = quota.QuotaType;
                            performancelevelsQuota.YearId = rangesQuota.yearId;
                            performancelevelsQuota.CompanyId = rangesQuota.companyId;

                            db.Entry(performancelevelsQuota).State = System.Data.Entity.EntityState.Added;
                            db.PerformancelevelsQuotaCollection.Add(performancelevelsQuota);
                        }

                        db.SaveChanges();
                        scope.Complete();
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }

                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpPost]
        public IHttpActionResult UpdatePerformaceQuota()
        {
            try
            {
                RangesQuota rangesQuota =
                    JsonConvert.DeserializeObject<RangesQuota>(HttpContext.Current.Request.Form["Quota"]);


                using (TransactionScope scope = new TransactionScope())
                {
                    if (rangesQuota != null)
                    {
                        foreach (Quota quota in rangesQuota.QuotaList)
                        {
                            PerformancelevelsQuota performancelevelsQuota = db.PerformancelevelsQuotaCollection
                                .Where(x => x.Id == quota.Id).FirstOrDefault();

                            if (performancelevelsQuota != null)
                            {
                                performancelevelsQuota.FromPercentage = rangesQuota.FromPercentage;
                                performancelevelsQuota.ToPercentage = rangesQuota.ToPercentage;
                                performancelevelsQuota.QuotaDirection = quota.QuotaDirection;
                                performancelevelsQuota.LevelNumber = quota.LevelNumber;
                                performancelevelsQuota.QuotaType = quota.QuotaType;
                                performancelevelsQuota.YearId = rangesQuota.yearId;
                                performancelevelsQuota.CompanyId = rangesQuota.companyId;

                                db.Entry(performancelevelsQuota).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                        scope.Complete();
                        return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                    }

                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult UpdatePerformanceLevelQuota(int Id, float fromPercentage, float toPercentage,
            int levelNumber, int quotaType, int yearId, int companyId,int? quotaDirection=0 )
        {
            try
            {
                PerformancelevelsQuota performancelevelsQuota =
                    db.PerformancelevelsQuotaCollection.Where(x => x.Id == Id).FirstOrDefault();
                if (performancelevelsQuota != null)
                {
                    performancelevelsQuota.FromPercentage = fromPercentage;
                    performancelevelsQuota.ToPercentage = fromPercentage;

                    performancelevelsQuota.LevelNumber = levelNumber;
                    performancelevelsQuota.QuotaType = quotaType;
                    performancelevelsQuota.QuotaDirection = quotaDirection ?? 0;
                    performancelevelsQuota.YearId = yearId;
                    performancelevelsQuota.CompanyId = companyId;

                    db.Entry(performancelevelsQuota).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult LoadAllPerformanceLevelQuota(int yearId, int companyId)
        {
            try
            {
                var levels = db.PerformancelevelsQuotaCollection
                    .Where(x => x.YearId == yearId && x.CompanyId == companyId).ToList();
                return Ok(new { Data = levels, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult LoadAllPerformanceLevelQuotaView(int yearId, int companyId, string languageCode)
        {
            PerformanceLevelsQuotaView perfQuota = new PerformanceLevelsQuotaView();

            perfQuota.LevelsNumbers = new List<long>();

            perfQuota.RangesQuotaList = new List<RangesQuota>();
            perfQuota.companyId = companyId;
            perfQuota.yearId = yearId;
            List<tbl_resources> resources = new List<tbl_resources>();

            try
            {
                resources = db.tbl_resourcesCollection
                    .Where(x => x.url == "global" && x.culture_name == languageCode && x.org_id == companyId).ToList();
                List<tbl_performance_levels> levels = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == yearId && x.company_id == companyId).ToList()
                    .OrderByDescending(x => x.lvl_number).ToList();
                List<PerformancelevelsQuota> levelsQuota = db.PerformancelevelsQuotaCollection
                    .Where(x => x.YearId == yearId && x.CompanyId == companyId).ToList()
                    .OrderByDescending(x => x.FromPercentage).ToList();

                List<float> fromPercentages =
                    levelsQuota.Select(x => x.FromPercentage).ToList<float>().Distinct().ToList();
                List<float> toPercentages = levelsQuota.Select(x => x.ToPercentage).ToList<float>().Distinct().ToList();

                //perfQuota.RangesQuotaList = levelsQuota.Select(x => new RangesQuota { FromPercentage = x.FromPercentage, ToPercentage = x.ToPercentage }).ToList<RangesQuota>().Distinct().ToList();


                if (levels != null && levels.Count > 0)
                {
                    foreach (tbl_performance_levels level in levels)
                    {
                        perfQuota.LevelsNumbers.Add(level.lvl_number);
                    }
                }

                if (fromPercentages != null && fromPercentages.Count > 0)
                {
                    for (int count = 0; count < fromPercentages.Count; count++)
                    {
                        RangesQuota rangesQuota = new RangesQuota();

                        rangesQuota.FromPercentage = fromPercentages[count];
                        rangesQuota.ToPercentage = toPercentages[count];

                        rangesQuota.QuotaList = new List<Quota>();


                        if (levels != null && levels.Count > 0)
                        {
                            foreach (tbl_performance_levels level in levels)
                            {
                                PerformancelevelsQuota levelsQuotaByNumber = levelsQuota
                                    .Where(x => x.LevelNumber == level.lvl_number &&
                                                x.FromPercentage == fromPercentages[count]).ToList()
                                    .OrderByDescending(x => x.FromPercentage).FirstOrDefault();

                                if (levelsQuotaByNumber != null)
                                {
                                    Quota quotaObj = new Quota();
                                    quotaObj.LevelNumber = level.lvl_number;
                                    quotaObj.QuotaDirection = levelsQuotaByNumber.QuotaDirection;
                                    quotaObj.QuotaType = levelsQuotaByNumber.QuotaType;

                                    switch (levelsQuotaByNumber.QuotaType)
                                    {
                                        case 0:
                                            quotaObj.QuotaTypeDesc = "";
                                            break;
                                        case 1:
                                            quotaObj.QuotaTypeDesc = getResourceByKey("PlannedQuotaRoundUp", resources);
                                            break;
                                        case 3:
                                            quotaObj.QuotaTypeDesc = getResourceByKey("PlannedQuotaRoundDown", resources);
                                            break;
                                        case 2:
                                            quotaObj.QuotaTypeDesc = getResourceByKey("RemainingEmployee", resources);
                                            break;
                                        default:
                                            quotaObj.QuotaTypeDesc = "";
                                            break;
                                    }

                                    rangesQuota.QuotaList.Add(quotaObj);
                                }
                            }

                            perfQuota.RangesQuotaList.Add(rangesQuota);
                        }
                    }
                }


                return Ok(new { Data = perfQuota, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult getPerformanceQuotaByID(long id)
        {
            try
            {
                var level = db.PerformancelevelsQuotaCollection.Where(x => x.Id == id).FirstOrDefault();
                if (level != null)
                {
                    return Ok(new { Data = level, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult EditPerformanceQuota(float FromPercentage, long companyId, int yearId)
        {
            try
            {
                List<tbl_performance_levels> levels = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == yearId && x.company_id == companyId).ToList()
                    .OrderByDescending(x => x.lvl_number).ToList();
                List<PerformancelevelsQuota> levelsQuota = db.PerformancelevelsQuotaCollection
                    .Where(x => x.YearId == yearId && x.CompanyId == companyId && x.FromPercentage == FromPercentage)
                    .ToList().OrderByDescending(x => x.FromPercentage).ToList();

                RangesQuota rangesQuota = new RangesQuota();

                rangesQuota.FromPercentage = levelsQuota[0].FromPercentage;
                rangesQuota.ToPercentage = levelsQuota[0].ToPercentage;
                rangesQuota.companyId = companyId;
                rangesQuota.yearId = yearId;

                rangesQuota.QuotaList = new List<Quota>();


                if (levels != null && levels.Count > 0 && levelsQuota != null && levelsQuota.Count > 0)
                {
                    foreach (tbl_performance_levels level in levels)
                    {
                        PerformancelevelsQuota levelsQuotaByNumber = levelsQuota
                            .Where(x => x.LevelNumber == level.lvl_number &&
                                        x.FromPercentage == rangesQuota.FromPercentage).ToList()
                            .OrderByDescending(x => x.FromPercentage).FirstOrDefault();

                        if (levelsQuotaByNumber != null)
                        {
                            Quota quotaObj = new Quota();
                            quotaObj.LevelNumber = levelsQuotaByNumber.LevelNumber;
                            quotaObj.QuotaType = levelsQuotaByNumber.QuotaType;
                            quotaObj.Id = levelsQuotaByNumber.Id;
                            switch (levelsQuotaByNumber.QuotaType)
                            {
                                case 0:
                                    quotaObj.QuotaTypeDesc = "";
                                    break;
                                case 1:
                                    quotaObj.QuotaTypeDesc = Enums.Enums.QuotaType.PlannedQuota.ToString();
                                    break;
                                case 2:
                                    quotaObj.QuotaTypeDesc = Enums.Enums.QuotaType.RemainingEmployee.ToString();
                                    break;
                                default:
                                    quotaObj.QuotaTypeDesc = "";
                                    break;
                            }

                            rangesQuota.QuotaList.Add(quotaObj);
                        }
                    }

                    return Ok(new { Data = rangesQuota, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult NewPerformanceQuota(long companyId, int yearId)
        {
            try
            {
                List<tbl_performance_levels> levels = db.tbl_performance_levelsCollection
                    .Where(x => x.lvl_year == yearId && x.company_id == companyId).ToList()
                    .OrderByDescending(x => x.lvl_number).ToList();

                RangesQuota rangesQuota = new RangesQuota();

                rangesQuota.FromPercentage = 0;
                rangesQuota.ToPercentage = 0;

                rangesQuota.companyId = companyId;
                rangesQuota.yearId = yearId;

                rangesQuota.QuotaList = new List<Quota>();


                if (levels != null && levels.Count > 0)
                {
                    foreach (tbl_performance_levels level in levels)
                    {
                        Quota quotaObj = new Quota();
                        quotaObj.LevelNumber = level.lvl_number;
                        quotaObj.QuotaType = 0;


                        quotaObj.QuotaTypeDesc = "";


                        rangesQuota.QuotaList.Add(quotaObj);
                    }


                    return Ok(new { Data = rangesQuota, IsError = false, ErrorMessage = string.Empty });
                }
                else
                {
                    Quota quotaObj = new Quota();
                    quotaObj.LevelNumber = 1;
                    quotaObj.QuotaType = 0;


                    quotaObj.QuotaTypeDesc = "";


                    rangesQuota.QuotaList.Add(quotaObj);
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Data = string.Empty,
                    IsError = true,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public IHttpActionResult DeletePerformanceLevelQuotaByID(long id)
        {
            try
            {
                var level = db.PerformancelevelsQuotaCollection.Where(x => x.Id == id).FirstOrDefault();
                if (level != null)
                {
                    db.PerformancelevelsQuotaCollection.Remove(level);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        public IHttpActionResult DeletePerformanceQuota(float FromPercentage, long companyId, int yearId)
        {
            try
            {
                List<PerformancelevelsQuota> performancelevelsQuotas = db.PerformancelevelsQuotaCollection
                    .Where(x => x.FromPercentage == FromPercentage && x.YearId == yearId && x.CompanyId == companyId)
                    .ToList();
                if (performancelevelsQuotas != null && performancelevelsQuotas.Count > 0)
                {
                    db.PerformancelevelsQuotaCollection.RemoveRange(performancelevelsQuotas);
                    db.SaveChanges();
                    return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found" });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult RemovePerformanceLevelQuotaByIDs(string IDs)
        {
            try
            {
                List<long> ids = IDs.Split(',').Select(Int64.Parse).ToList();
                db.PerformancelevelsQuotaCollection.RemoveRange(db.PerformancelevelsQuotaCollection
                    .Where(x => ids.Contains(x.Id)).ToList());
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion


        #region Years

        [HttpGet]
        public IHttpActionResult LoadAllYears()
        {
            try
            {
                var years = db.YearsCollection.ToList();
                return Ok(new { Data = years, IsError = false, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = ex.Message });
            }
        }

        #endregion


        private string getResourceByKey(string key, List<tbl_resources> resources)
        {
            string value = key;
            if (resources != null && resources.Count > 0)
            {
                tbl_resources res = resources.Where(x => x.resource_key == key).FirstOrDefault();
                if (res != null)
                {
                    value = res.resource_value;
                }
            }

            return value;
        }
    }


    public class PerformanceLevelsQuotaView
    {
        public List<long> LevelsNumbers { get; set; }

        public List<RangesQuota> RangesQuotaList { get; set; }

        public int yearId { get; set; }
        public long companyId { get; set; }
    }


    public class RangesQuota
    {
        public int yearId { get; set; }
        public long companyId { get; set; }

        public float FromPercentage { get; set; }
        public float ToPercentage { get; set; }

        public List<Quota> QuotaList { get; set; }
    }

    public class Quota
    {
        public long Id { get; set; }
        public long LevelNumber { get; set; }
        public int QuotaType { get; set; }
        public int QuotaDirection { get; set; }
        public string QuotaTypeDesc { get; set; }
    }
}