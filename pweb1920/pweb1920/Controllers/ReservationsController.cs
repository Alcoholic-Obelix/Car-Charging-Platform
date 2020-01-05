using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using pweb1920.DAL;
using pweb1920.Models;
using pweb1920.Models.DTO;
using pweb1920.Models.ViewModels;

namespace pweb1920.Controllers
{
    public class ReservationsController : Controller
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

        // GET: Reservations
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                List<ReservationDetailsViewModel> reservations = new List<ReservationDetailsViewModel>();

                db.Reservations.Join(db.ChargingPoints,
                        reser => reser.ChargingPoint,
                        charg => charg,
                        (reser, charg) => new
                        {
                            Reser = reser,
                            Charg = charg
                        }
                    )
                    .Join(db.Stations,
                        joined => joined.Charg.Station,
                        stat => stat,
                        (joined, stat) => new
                        {
                            Joined = joined,
                            Stat = stat
                        }
                    )
                    .OrderByDescending(e => e.Joined.Reser.Date)
                    .Select(e => new
                    {
                        Reservation = e.Joined.Reser,
                        StationName = e.Stat.Name
                    }).ToList().ForEach(e => reservations.Add(new ReservationDetailsViewModel(e.Reservation, e.StationName)));

                return View(reservations);
            }
            else
            {
                var client = GetClient();
                List<ReservationDetailsViewModel> reservations = new List<ReservationDetailsViewModel>();

                db.Reservations.Join(db.ChargingPoints,
                        reser => reser.ChargingPoint,
                        charg => charg,
                        (reser, charg) => new
                        {
                            Reser = reser,
                            Charg = charg
                        }
                    )
                    .Join(db.Stations,
                        joined => joined.Charg.Station,
                        stat => stat,
                        (joined, stat) => new
                        {
                            Joined = joined,
                            Stat = stat
                        }
                    )
                    .Where(e => e.Joined.Reser.Client.Id == client.Id)
                    .OrderByDescending(e => e.Joined.Reser.Date)
                    .Select(e => new
                    {
                        Reservation = e.Joined.Reser,
                        StationName = e.Stat.Name
                    }).ToList().ForEach(e => reservations.Add(new ReservationDetailsViewModel(e.Reservation, e.StationName)));

                return View(reservations);
            }
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = db.Reservations
                .Join(db.ChargingPoints,
                    reser => reser.ChargingPoint,
                    charg => charg,
                    (reser, charg) => new
                    {
                        Reser = reser,
                        Charg = charg
                    }
                )
                .Join(db.Stations,
                    joined => joined.Charg.Station,
                    stat => stat,
                    (joined, stat) => new
                    {
                        Joined = joined,
                        Stat = stat
                    }
                )
                .Where(e => e.Joined.Reser.Id == id)
                .Select(e => new
                {
                    Reservation = e.Joined.Reser,
                    StationName = e.Stat.Name
                }).First();

            ReservationDetailsViewModel reservation = new ReservationDetailsViewModel(result.Reservation, result.StationName);

            if (reservation == null)
            {
                return HttpNotFound();
            }
            return PartialView(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            var dto = new StartCreateDTO();

            //Adds all Stations with distinct Districs to a List
            var stationsList = db.Stations.Where(e => e.Status == "Accepted").DistinctBy(e => e.District).ToList();

            dto.DistrictDropDown = new SelectList(stationsList, "Id", "District");

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

        //[HttpPost]
        public ActionResult ShowFreeSlots(StartCreateDTO startDto)
        {
            var station = db.Stations.Find(startDto.Station);
            var reservationsOfTheDay = db.Reservations
                .Where(e => e.ChargingPoint.Station.Id == station.Id)
                .Where(e => e.Date == startDto.Date).ToList();
            var listOfCargingPoints = station.ChargingPoints.Where(e => e.Station == station).ToList();

            var reservationsList = new List<Reservation>();

            foreach (var item in listOfCargingPoints)
            {
                var openTime = station.OpenTime;
                var closeTime = station.CloseTime;
                var timeSpan = TimeSpan.FromMinutes(30);

                while (openTime <= closeTime)
                {
                    if (reservationsOfTheDay
                        .Where(e => e.ChargingPoint.Id == item.Id)
                        .Any(e => e.TimeStart == openTime))
                    {
                        openTime = openTime.Add(timeSpan);
                        continue;
                    }
                    else
                    {
                        var reservation = new Reservation() { TimeStart = openTime, TimeFinish = openTime.Add(timeSpan),
                            ChargingPoint = item, Date = startDto.Date, Status = "Not Selected"};
                        openTime = openTime.Add(timeSpan);
                        reservationsList.Add(reservation);
                    }
                }
            }                        

            var showSlotsDto = new ShowFreeSlotsDTO();
            showSlotsDto.Reservations = reservationsList;
            showSlotsDto.Station = station;

            return View(showSlotsDto);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowFreeSlotsDTO dto)
        {
            var client = GetClient();

            foreach (var item in dto.Reservations)
            {
                if (item.Selected)
                {
                    var reservation = new Reservation()
                    {
                        Date = item.Date,
                        TimeStart = item.TimeStart,
                        TimeFinish = item.TimeFinish,
                        ServiceCode = 123,
                        EstimatedCost = 123,
                        Status = ConstantValues.READY,
                        ChargingPoint = db.ChargingPoints.Find(item.ChargingPoint.Id),
                        ChargingMode = db.ChargingModes.Find(item.ChargingMode.Id),
                        Client = client
                    };

                    try
                    {
                        db.Reservations.Add(reservation);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin, Company")]
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
