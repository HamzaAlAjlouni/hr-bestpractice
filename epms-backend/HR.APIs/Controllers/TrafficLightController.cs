using HR.Entities;
using HR.Entities.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HR.APIs.Controllers
{
    public class TrafficLightController : ApiController
    {
        HRContext db;

        public TrafficLightController()
        {
            db = new HRContext();
        }


        [HttpGet]
        public IHttpActionResult LoadTrafficLights(int companyId, int yearId)
        {
            try
            {
                var list = db.TrafficLightCollection.Where(x => x.company_id == companyId && x.year == yearId)
                    .OrderBy(a => a.perc_to).ToList();

                return Ok(new { Data = list, IsError = false, ErrorMessage = string.Empty });
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
        public IHttpActionResult LoadTrafficLightsByID(int id)
        {
            try
            {
                var list = db.TrafficLightCollection.Where(x => x.ID == id).FirstOrDefault();

                if (list != null)
                {
                    return Ok(new { Data = list, IsError = false, ErrorMessage = string.Empty });
                }

                return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "No Data Found." });
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
        public IHttpActionResult SaveTrafficLight(string name, float perc_from, float perc_to, string color,
            int companyID, int year)
        {
            try
            {
                var checkList = db.TrafficLightCollection
                    .Where(a => a.year==year) // Exclude the record being updated
                    .Any(a =>
                        (perc_to <= a.perc_to && perc_to >= a.perc_from)
                        ||
                        (perc_from >= a.perc_from && perc_from <= a.perc_to)
                    );


                if (checkList)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Invalid Percentage Range Value" });
                }

                TrafficLightEntity traf = new TrafficLightEntity();
                traf.name = name;
                traf.perc_from = perc_from;
                traf.perc_to = perc_to;
                traf.color = color;
                traf.company_id = companyID;
                traf.year = year;

                db.Entry(traf).State = EntityState.Added;
                db.TrafficLightCollection.Add(traf);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                ;
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
        public IHttpActionResult UpdateTrafficLight(int id, string name, float perc_from, float perc_to, string color,
            int companyID, int year)
        {
            try
            {
                var checkList = db.TrafficLightCollection
                    .Where(a => a.ID != id && a.year==year) // Exclude the record being updated
                    .Any(a =>
                        (perc_to <= a.perc_to && perc_to >= a.perc_from)
                        ||
                        (perc_from >= a.perc_from && perc_from <= a.perc_to)
                    );


                if (checkList)
                {
                    return Ok(new { Data = string.Empty, IsError = true, ErrorMessage = "Invalid Percentage Range Value" });
                }

                TrafficLightEntity traf = db.TrafficLightCollection.Where(x => x.ID == id).FirstOrDefault();

                traf.name = name;
                traf.perc_from = perc_from;
                traf.perc_to = perc_to;
                traf.color = color;
                traf.company_id = companyID;
                traf.year = year;

                db.Entry(traf).State = EntityState.Modified;
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
        public IHttpActionResult DeleteTrafficLight(int id)
        {
            try
            {
                TrafficLightEntity traf = db.TrafficLightCollection.Where(s => s.ID == id).FirstOrDefault();

                db.TrafficLightCollection.Remove(traf);
                db.SaveChanges();

                return Ok(new { Data = string.Empty, IsError = false, ErrorMessage = string.Empty });
                ;
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