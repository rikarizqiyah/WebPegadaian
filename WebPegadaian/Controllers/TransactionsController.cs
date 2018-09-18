using System;
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
    public class TransactionsController : Controller
    {
        private WebPegadaianEntities db = new WebPegadaianEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Installment_type).Include(t => t.Outlet).Include(t => t.Product).Include(t => t.User).Include(t => t.User1);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.Installment_Type_Id = new SelectList(db.Installment_type, "Id", "Name");
            ViewBag.Outlet_Id = new SelectList(db.Outlets, "Id", "Name");
            ViewBag.Product_Id = new SelectList(db.Products, "Id", "Name");
            ViewBag.Cust_Id = new SelectList(db.Users, "Id", "Name");
            ViewBag.User_Create = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_Id,Installment_Type_Id,Outlet_Id,Transaction_Date,Administrative_Cost,Loan_Amount,Approved_Date,Approved_Status,Accepted_Date,Accepted_Status,Remaining_Amount,User_Create,Cust_Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Installment_Type_Id = new SelectList(db.Installment_type, "Id", "Name", transaction.Installment_Type_Id);
            ViewBag.Outlet_Id = new SelectList(db.Outlets, "Id", "Name", transaction.Outlet_Id);
            ViewBag.Product_Id = new SelectList(db.Products, "Id", "Name", transaction.Product_Id);
            ViewBag.Cust_Id = new SelectList(db.Users, "Id", "Name", transaction.Cust_Id);
            ViewBag.User_Create = new SelectList(db.Users, "Id", "Name", transaction.User_Create);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Installment_Type_Id = new SelectList(db.Installment_type, "Id", "Name", transaction.Installment_Type_Id);
            ViewBag.Outlet_Id = new SelectList(db.Outlets, "Id", "Name", transaction.Outlet_Id);
            ViewBag.Product_Id = new SelectList(db.Products, "Id", "Name", transaction.Product_Id);
            ViewBag.Cust_Id = new SelectList(db.Users, "Id", "Name", transaction.Cust_Id);
            ViewBag.User_Create = new SelectList(db.Users, "Id", "Name", transaction.User_Create);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_Id,Installment_Type_Id,Outlet_Id,Transaction_Date,Administrative_Cost,Loan_Amount,Approved_Date,Approved_Status,Accepted_Date,Accepted_Status,Remaining_Amount,User_Create,Cust_Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Installment_Type_Id = new SelectList(db.Installment_type, "Id", "Name", transaction.Installment_Type_Id);
            ViewBag.Outlet_Id = new SelectList(db.Outlets, "Id", "Name", transaction.Outlet_Id);
            ViewBag.Product_Id = new SelectList(db.Products, "Id", "Name", transaction.Product_Id);
            ViewBag.Cust_Id = new SelectList(db.Users, "Id", "Name", transaction.Cust_Id);
            ViewBag.User_Create = new SelectList(db.Users, "Id", "Name", transaction.User_Create);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
