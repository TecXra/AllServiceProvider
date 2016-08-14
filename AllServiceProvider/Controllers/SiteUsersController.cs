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
    public class SiteUsersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: SiteUsers
        public ActionResult Index()
        {
            return View(db.dbSiteUser.ToList());
        }

        // GET: SiteUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteUser siteUser = db.dbSiteUser.Find(id);
            if (siteUser == null)
            {
                return HttpNotFound();
            }
            return View(siteUser);
        }

        // GET: SiteUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,AppUserId")] SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                db.dbSiteUser.Add(siteUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteUser);
        }

        // GET: SiteUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteUser siteUser = db.dbSiteUser.Find(id);
            if (siteUser == null)
            {
                return HttpNotFound();
            }
            return View(siteUser);
        }

        // POST: SiteUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,appUserId")] SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteUser);
        }

        // GET: SiteUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteUser siteUser = db.dbSiteUser.Find(id);
            if (siteUser == null)
            {
                return HttpNotFound();
            }
            return View(siteUser);
        }

        // POST: SiteUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteUser siteUser = db.dbSiteUser.Find(id);
            db.dbSiteUser.Remove(siteUser);
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
