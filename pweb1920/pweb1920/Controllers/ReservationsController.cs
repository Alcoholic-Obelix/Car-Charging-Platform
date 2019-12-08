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
using Microsoft.AspNet.Identity;
using pweb1920.DAL;
using pweb1920.Models.DTO;

namespace pweb1920.Controllers
{
    public class ReservationsController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        // GET: Reservations
        public ActionResult Index()
        {
            return View(db.Reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            var dto = new CreateReservationDTO();

            //Adds all Stations with distinct Districs to a List
            var stationsList = db.Stations.Where(e => e.Status == "Accepted").DistinctBy(e => e.District).ToList();

            dto.DistrictDropDown = new SelectList(stationsList, "Id", "District");
            dto.CityDropDown = new SelectList("");
            dto.StationDropDown = new SelectList("");

            return View(dto);
        }

        public JsonResult GetCities(int DistrictId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var station = db.Stations.Find(DistrictId);
            var stationsList = db.Stations.Where(e => e.District == station.District).ToList();

            return Json(stationsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStations(int CityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var station = db.Stations.Find(CityId);
            var stationsList = db.Stations
                .Where(e => e.District == station.District)
                .Where(e => e.City == station.City).ToList();

            return Json(stationsList, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetFreeReservations(int StationId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var station = db.Stations.Find(StationId);

        //    var openTime = s
        //    for

        //    var reservation = db.Stations
        //        .Where(e => e.District == station.District)
        //        .Where(e => e.City == station.City).ToList();

        //    return Json(stationsList, JsonRequestBehavior.AllowGet);
        //}

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStart,TimeFinish,ServiceCode,EstimatedCost,Status")] Reservation reservation)
        {
            //vai buscar o ID do utilizador atual
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;

            if (ModelState.IsValid)
            {
                //vai ver qual é o cliente atual com o Id do User
                reservation.Client = db.Clients.Where(m => m.IdentityId == userIdValue).FirstOrDefault();
                //estas 2 é só porque não podem ser null e assim já dá para experimentar as reservations
                reservation.ChargingMode = db.ChargingModes.Where(m => m.Id == 1).FirstOrDefault();
                reservation.ChargingPoint = db.ChargingPoints.Where(m => m.Id == 1).FirstOrDefault();

                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStart,TimeFinish,ServiceCode,EstimatedCost,Status")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
