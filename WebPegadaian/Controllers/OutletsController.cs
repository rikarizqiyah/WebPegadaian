﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPegadaian.Models;

namespace WebPegadaian.Controllers
{
    public class OutletsController : Controller
    {
        private WebPegadaianEntities db = new WebPegadaianEntities();

        // GET: Outlets
        public ActionResult Index()
        {
            var outlets = db.Outlets.Include(o => o.Village);
            return View(outlets.ToList());
        }

        // GET: Outlets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // GET: Outlets/Create
        public ActionResult Create()
        {
            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id");
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Village_Id,Address")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Outlets.Add(outlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id", outlet.Village_Id);
            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id", outlet.Village_Id);
            return View(outlet);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Village_Id,Address")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id", outlet.Village_Id);
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outlet outlet = db.Outlets.Find(id);
            db.Outlets.Remove(outlet);
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
