using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using pweb1920.DAL;
using pweb1920.Models.DTO;
using pweb1920.Models.ViewModels;

namespace pweb1920.Controllers
{
    [Authorize(Roles = "Admin, Company")]
    public class ChargingPointsController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

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

        // GET: ChargingPoints
        public ActionResult Index()
        {
            Company cc = GetCompany();
            List<MyChargingPointsViewModel> cp = new List<MyChargingPointsViewModel>();
            var sList = db.Stations.Where(e => e.Companies.Id == cc.Id).ToList();

            foreach (var station in sList)
            {
                var cpList = db.ChargingPoints
                .Where(e => e.Station.Id == station.Id)
                .Select(e => new {
                    Id = e.Id,
                    Status = e.Status,
                    ModeId = e.ChargingModes.FirstOrDefault().Id,
                    ModeName = e.ChargingModes.FirstOrDefault().Name,
                    ModeDescription = e.ChargingModes.FirstOrDefault().Description
                }).ToList();

                List<ChargingPointDTO> cpDtoList = new List<ChargingPointDTO>();
                foreach (var cpItem in cpList)
                {
                    cpDtoList.Add(new ChargingPointDTO(cpItem.Id, cpItem.Status, cpItem.ModeId, cpItem.ModeName, cpItem.ModeDescription));
                }

                cp.Add(new MyChargingPointsViewModel(station, cpDtoList));
            }

            return View(cp);
        }

        // GET: ChargingPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingPoint chargingPoint = db.ChargingPoints.Find(id);
            if (chargingPoint == null)
            {
                return HttpNotFound();
            }
            return View(chargingPoint);
        }

        // GET: ChargingPoints/Create
        public ActionResult Create(int station_id)
        {
            return View(new CreateChargingPointDTO(station_id, db.ChargingModes.ToList()));
        }

        // POST: ChargingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ChargingModeId, int StationId)
        {
            if (ModelState.IsValid)
            {
                var chargingMode = db.ChargingModes.Where(e => e.Id == ChargingModeId).FirstOrDefault();
                var station = db.Stations.Where(e => e.Id == StationId).FirstOrDefault();
                
                var chargingPoint = new ChargingPoint();

                chargingPoint.Status = "On";
                chargingPoint.Station = station;
                chargingPoint.ChargingModes.Add(chargingMode);

                db.ChargingPoints.Add(chargingPoint);
                db.SaveChanges();
                return RedirectToAction("Index", "Stations", new { sucess = 1 });
            }

            return View();
        }

        // GET: ChargingPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingPoint chargingPoint = db.ChargingPoints.Find(id);
            if (chargingPoint == null)
            {
                return HttpNotFound();
            }
            return View(chargingPoint);
        }

        // POST: ChargingPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] ChargingPoint chargingPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chargingPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chargingPoint);
        }

        // GET: ChargingPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingPoint chargingPoint = db.ChargingPoints.Find(id);
            if (chargingPoint == null)
            {
                return HttpNotFound();
            }
            return View(chargingPoint);
        }

        // POST: ChargingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChargingPoint chargingPoint = db.ChargingPoints.Find(id);
            db.ChargingPoints.Remove(chargingPoint);
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
