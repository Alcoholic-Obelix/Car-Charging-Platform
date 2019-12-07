using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using pweb1920.DAL;
using pweb1920.Models.DTO;

namespace pweb1920.Controllers
{
    public class StationsController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        // GET: Stations
        public ActionResult Index(int? district, int? city)
        {
            if(district == null && city == null) {
                var dto = new IndexStationDTO();
                dto.Stations = db.Stations.Where(e => e.Status == "Accepted").DistinctBy(e => e.District).ToList();

                return View("IndexDistricts", dto);
            }
            else if(city == null)
            {
                var dto = new IndexStationDTO();
                dto.District = db.Stations.Where(e => e.Id == district)
                    .Select(e => e.District).FirstOrDefault().ToString();

                dto.Stations = db.Stations.Where(e => e.Status == "Accepted")
                    .Where(e => e.District == dto.District)
                    .DistinctBy(e => e.City).ToList();

                return View("IndexCities", dto);
            }
            else
            {
                var dto = new IndexStationDTO();
                dto.District = db.Stations.Where(e => e.Id == district)
                    .Select(e => e.District).FirstOrDefault().ToString();

                dto.City = db.Stations.Where(e => e.Id == city)
                    .Select(e => e.City).FirstOrDefault().ToString();

                dto.Stations = db.Stations.Where(e => e.Status == "Accepted")
                    .Where(e => e.District == dto.District)
                    .Where(e => e.City == dto.City).ToList();

                return View("IndexStations", dto);
            }
        }

        // GET: Stations/Details/5
        public ActionResult Details(int id)
        {
            Station station = db.Stations.Find(id);
            return View(station);
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StreetAdress,City,District,Status")] Station station)
        {
            if (ModelState.IsValid)
            {
                db.Stations.Add(station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(station);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StreetAdress,City,District,Status")] Station station)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Station station = db.Stations.Find(id);
            db.Stations.Remove(station);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
