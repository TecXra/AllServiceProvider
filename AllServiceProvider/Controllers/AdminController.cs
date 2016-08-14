using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllServiceProvider.Models;
using System.Data.Entity;

namespace AllServiceProvider.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        MyDbContext db = new MyDbContext();

        [AllowAnonymous]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult ContactRequests()
        {

            //   var siteUsers = db.dbSiteUser.Where(a => a.ServiceProviderUser ==null).ToList();
            //var categories = db.dbSiteUser
            //                .Where(x => x.ServiceProviderUser.Any(y => y.SiteUser != null))
            //                                            .ToList();

            return View(db.dbSiteUser.ToList());
        }
        

        public ActionResult CompleteRequest(int siteUserId, int spUserId)
        {

            // get athunticated user 
            var sUser = db.dbSiteUser.FirstOrDefault(c => c.Id == siteUserId);


            var spUser = db.dbSPUser.FirstOrDefault(c => c.Id == spUserId);

            if (spUser == null)
            {
                ViewBag.Error = "Service Provider do not exist";
                return RedirectToAction("Index");

            }
            sUser.ServiceProviderUser.Remove(spUser);

            db.Entry(sUser).State = EntityState.Modified;

            db.SaveChanges();





            return RedirectToAction("ContactRequests");

        }
        public string test()
        {

            //   var siteUsers = db.dbSiteUser.Where(a => a.ServiceProviderUser ==null).ToList();
            var categories = db.dbSiteUser
                            .Where(x => x.ServiceProviderUser.Any(y => y.SiteUser != null))
                                                        .ToList();

            return "test";//View(db.dbSiteUser.ToList());
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
