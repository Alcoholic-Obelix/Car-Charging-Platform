using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pweb1920.DAL;

namespace pweb1920.Controllers
{
    public class ChargingPointsController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        // GET: ChargingPoints
        public ActionResult Index()
        {
            return View(db.ChargingPoints.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChargingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] ChargingPoint chargingPoint)
        {
            if (ModelState.IsValid)
            {
                db.ChargingPoints.Add(chargingPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chargingPoint);
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
