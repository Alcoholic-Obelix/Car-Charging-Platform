using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using pweb1920.DAL;
using pweb1920.Models;
using pweb1920.Models.DTO;

namespace pweb1920.Controllers
{
    public class StationsController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        public Client GetClient()
        {
            //vai buscar o ID do utilizador atual
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            var client = db.Clients.Where(m => m.IdentityId == userIdValue).FirstOrDefault();

            return client;
        }

        public Company GetCompany()
        {
            //vai buscar o ID do utilizador atual
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            var company = db.Companies.Where(m => m.IdentityId == userIdValue).FirstOrDefault();

            return company;
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var company = GetCompany();

                var myStations = db.Stations.ToList();

                var indexCompanyDTO = new IndexCompanyDTO();
                indexCompanyDTO.myStations = myStations;

                return View("../Home/IndexCompany", indexCompanyDTO);
            }
            else
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
        public ActionResult Create([Bind(Include = "Id,Name,StreetAdress,City,District")] Station station)
        {
            if (ModelState.IsValid)
            {
                station.Status = ConstantValues.PENDING;
                station.Companies = GetCompany();
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

            List<SelectListItem> dropdownList = new List<SelectListItem>();
            SelectListItem accepted = new SelectListItem { Text = "Accepted", Value = ConstantValues.ACCEPTED };
            SelectListItem pending = new SelectListItem { Text = "Pending", Value = ConstantValues.PENDING };
            dropdownList.Add(accepted);
            dropdownList.Add(pending);
            station.StatusDropDown = dropdownList;

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
