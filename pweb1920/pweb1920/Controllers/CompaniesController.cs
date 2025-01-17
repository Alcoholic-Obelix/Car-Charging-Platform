﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pweb1920.DAL;
using pweb1920.Models;

namespace pweb1920.Controllers
{
    [Authorize(Roles = "Admin, Compamy")]
    public class CompaniesController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        // GET: Companies
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,NIF,Status,IdentityId")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> dropdownList = new List<SelectListItem>();
            SelectListItem accepted = new SelectListItem {Text = "Accepted", Value = ConstantValues.ACCEPTED };
            SelectListItem pending = new SelectListItem {Text = "Pending", Value = ConstantValues.PENDING };
            dropdownList.Add(accepted);
            dropdownList.Add(pending);
            company.StatusDropDown = dropdownList;

            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                var companyToChange = db.Companies.Find(company.Id);
                companyToChange.Name = company.Name;
                companyToChange.NIF = company.NIF;
                companyToChange.Status = company.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //public ActionResult Edit([Bind(Include = "Id,Name,NIF,Status")] Company company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(company).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(company);
        //}

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
