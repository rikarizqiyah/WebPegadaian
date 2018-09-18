using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPegadaian.Models;

namespace WebPegadaian.Controllers
{
    public class ItemsController : Controller
    {
        private WebPegadaianEntities db = new WebPegadaianEntities();

        public ActionResult GetView()
        {
            return PartialView("MyPartialView");
        }

        // GET: Items
        public async Task<ActionResult> Index()
        {
            var type_Laptop = db.Type_Laptop.Include(t => t.Transaction);
            return View(await type_Laptop.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Laptop type_Laptop = await db.Type_Laptop.FindAsync(id);
            if (type_Laptop == null)
            {
                return HttpNotFound();
            }
            return View(type_Laptop);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.Transaction_Id = new SelectList(db.Transactions, "Id", "Approved_Status");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Brand,Type,Harddisk_Capacity,Years_Of_Buy,Selling_Price,Condition,Picture,Transaction_Id")] Type_Laptop type_Laptop)
        {
            if (ModelState.IsValid)
            {
                db.Type_Laptop.Add(type_Laptop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Transaction_Id = new SelectList(db.Transactions, "Id", "Approved_Status", type_Laptop.Transaction_Id);
            return View(type_Laptop);
        }

        // GET: Items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Laptop type_Laptop = await db.Type_Laptop.FindAsync(id);
            if (type_Laptop == null)
            {
                return HttpNotFound();
            }
            ViewBag.Transaction_Id = new SelectList(db.Transactions, "Id", "Approved_Status", type_Laptop.Transaction_Id);
            return View(type_Laptop);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Brand,Type,Harddisk_Capacity,Years_Of_Buy,Selling_Price,Condition,Picture,Transaction_Id")] Type_Laptop type_Laptop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type_Laptop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Transaction_Id = new SelectList(db.Transactions, "Id", "Approved_Status", type_Laptop.Transaction_Id);
            return View(type_Laptop);
        }

        // GET: Items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Laptop type_Laptop = await db.Type_Laptop.FindAsync(id);
            if (type_Laptop == null)
            {
                return HttpNotFound();
            }
            return View(type_Laptop);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Type_Laptop type_Laptop = await db.Type_Laptop.FindAsync(id);
            db.Type_Laptop.Remove(type_Laptop);
            await db.SaveChangesAsync();
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
