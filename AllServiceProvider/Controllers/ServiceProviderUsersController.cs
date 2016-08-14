using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AllServiceProvider.Models;

namespace AllServiceProvider.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ServiceProviderUsersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ServiceProviderUsers
        public ActionResult Index()
        {
            var dbSPUser = db.dbSPUser.Include(s => s.City).Include(s => s.Skill);
            return View(dbSPUser.ToList());
        }

        // GET: ServiceProviderUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProviderUser serviceProviderUser = db.dbSPUser.Find(id);
            if (serviceProviderUser == null)
            {
                return HttpNotFound();
            }
            return View(serviceProviderUser);
        }

        // GET: ServiceProviderUsers/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName");
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName");
            return View();
        }

        // POST: ServiceProviderUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,AppUserId,SkillId,CityId")] ServiceProviderUser serviceProviderUser)
        {
            if (ModelState.IsValid)
            {
                db.dbSPUser.Add(serviceProviderUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName", serviceProviderUser.CityId);
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName", serviceProviderUser.SkillId);
            return View(serviceProviderUser);
        }

        // GET: ServiceProviderUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProviderUser serviceProviderUser = db.dbSPUser.Find(id);
            if (serviceProviderUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName", serviceProviderUser.CityId);
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName", serviceProviderUser.SkillId);
            return View(serviceProviderUser);
        }

        // POST: ServiceProviderUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,AppUserId,SkillId,CityId")] ServiceProviderUser serviceProviderUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceProviderUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName", serviceProviderUser.CityId);
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName", serviceProviderUser.SkillId);
            return View(serviceProviderUser);
        }

        // GET: ServiceProviderUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProviderUser serviceProviderUser = db.dbSPUser.Find(id);
            if (serviceProviderUser == null)
            {
                return HttpNotFound();
            }
            return View(serviceProviderUser);
        }

        // POST: ServiceProviderUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceProviderUser serviceProviderUser = db.dbSPUser.Find(id);
            db.dbSPUser.Remove(serviceProviderUser);
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
