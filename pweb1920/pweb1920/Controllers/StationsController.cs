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

        public ActionResult Index()
        {
            ViewBag.Title = "Stations";
            return View(db.Stations.ToList());
        }

        // GET: Stations
        public ActionResult Search(int? district, int? city)
        {
            ViewBag.Title = "Search Stations";
            if (district == null && city == null)
            {

                var dto = new IndexStationDTO();
                dto.Stations = db.Stations.Where(e => e.Status == "Accepted").DistinctBy(e => e.District).ToList();

                return View("SearchByDistricts", dto);
            }
            else if(city == null)
            {

                var dto = new IndexStationDTO();
                dto.SelectedDistrict = db.Stations.Find(district);

                dto.Stations = db.Stations.Where(e => e.Status == "Accepted")
                    .Where(e => e.District == dto.SelectedDistrict.District)
                    .DistinctBy(e => e.City).ToList();

                return View("SearchByCities", dto);
            }
            else
            {

                var dto = new IndexStationDTO();
                dto.SelectedDistrict = db.Stations.Find(district);

                dto.SelectedCity = db.Stations.Find(city);

                dto.Stations = db.Stations.Where(e => e.Status == "Accepted")
                    .Where(e => e.District == dto.SelectedDistrict.District)
                    .Where(e => e.City == dto.SelectedCity.City).ToList();

                return View("SearchByStations", dto);
            }
        }

        // GET: Stations/Details/5
        public ActionResult Details(int id)
        {
            Station station = db.Stations.Find(id);
            return View(station);
        }

        // GET: Stations/Create
        [Authorize(Roles = "Admin, Company")]
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
        [Authorize(Roles = "Admin, Company")]
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
        [Authorize(Roles = "Admin, Company")]
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
