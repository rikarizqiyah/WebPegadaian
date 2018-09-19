using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using WebPegadaian.Models;
using WebPegadaian.ViewModel;
using System.Data.Entity.Validation;

namespace WebPegadaian.Controllers
{
    public class UsersController : Controller
    {
        private WebPegadaianEntities db = new WebPegadaianEntities();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Role).Include(u => u.Village);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            List<Province> ProvList = db.Provinces.ToList();
            ViewBag.ProvList = new SelectList(ProvList, "Id", "Name");
            ViewBag.roles = new SelectList(db.Roles.Where(x => x.Name == "Customer"), "Id", "Name");
            return View();
        }

        public JsonResult GetRegencyList(string Provinces_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Regency> KabList = db.Regencies.Where(x => x.Province_Id == Provinces_Id).ToList();
            return Json(KabList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrictList(string Regencies_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<District> KecList = db.Districts.Where(x => x.Regency_Id == Regencies_Id).ToList();
            return Json(KecList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVillageList(string Districts_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Village> VilList = db.Villages.Where(x => x.District_Id == Districts_Id).ToList();
            return Json(VilList, JsonRequestBehavior.AllowGet);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvRegViewModel provreg)
        {
            var push = new User();
            if (ModelState.IsValid)
            {
                push.Name = provreg.Name;
                push.Identity_Type = provreg.Identity_Type;
                push.Identity_Number = provreg.Identity_Number;
                push.Gender = "L";
                push.Born_Place = provreg.Born_Place;
                push.Born_Date = provreg.Born_Date;
                push.Address = provreg.Address;
                push.Village_Id = provreg.Villages_Id;
                push.Email = provreg.Email;
                push.Username = provreg.Username;
                push.Password = provreg.Password;
                push.Role_Id = provreg.Role_Id;
                push.Number_Npwp = provreg.Number_Npwp;
                push.Picture_Npwp = provreg.Picture_Npwp;
                push.Picture_Identity = provreg.Picture_Identity;

                try
                {
                    db.Users.Add(push);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ee)
                {
                    Console.Write(ee.Message);
                    Console.Write(ee.StackTrace);
                    Console.Write(ee.EntityValidationErrors);
                }                    
            }

            return View(provreg);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_Id = new SelectList(db.Roles, "Id", "Name", user.Role_Id);
            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id", user.Village_Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Identity_Type,Identity_Number,Gender,Born_Place,Born_Date,Address,Village_Id,Email,Username,Password,Role_Id,Number_Npwp,Picture_Npwp,Picture_Identity")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Role_Id = new SelectList(db.Roles, "Id", "Name", user.Role_Id);
            ViewBag.Village_Id = new SelectList(db.Villages, "Id", "District_Id", user.Village_Id);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
