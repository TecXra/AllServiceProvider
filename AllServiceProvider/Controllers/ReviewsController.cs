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
    public class ReviewsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var dbReview = db.dbReview.Include(r => r.ServiceProviderUser).Include(r => r.SiteUser);
            return View(dbReview.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.dbReview.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.dbSPUser, "Id", "Name");
            ViewBag.WriterId = new SelectList(db.dbSiteUser, "Id", "Name");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FeedBack,WriterId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.dbReview.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.dbSPUser, "Id", "Name", review.UserId);
            ViewBag.WriterId = new SelectList(db.dbSiteUser, "Id", "Name", review.WriterId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.dbReview.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.dbSPUser, "Id", "Name", review.UserId);
            ViewBag.WriterId = new SelectList(db.dbSiteUser, "Id", "Name", review.WriterId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FeedBack,WriterId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.dbSPUser, "Id", "Name", review.UserId);
            ViewBag.WriterId = new SelectList(db.dbSiteUser, "Id", "Name", review.WriterId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.dbReview.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.dbReview.Find(id);
            db.dbReview.Remove(review);
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
