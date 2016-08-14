using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AllServiceProvider.Models;
using Microsoft.AspNet.Identity;

namespace AllServiceProvider.Controllers
{
    public class SiteController : Controller
    {

        // GET: Site
        private MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {

       
            return View();
        }
        public ActionResult CreateSpUserProfile()
        {

            Session["profile"] = "EditSPUserProfile";
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName");

            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName");

            var skillWorker = new ServiceProviderUser();
            return View(skillWorker);
        }
        public ActionResult CreateSiteUserProfile()
        {
            Session["profile"] = "EditSiteUserProfile";
            //        var siteUser = new SiteUser();
            return View();
        }

        public ActionResult Search()
        {
            var bg = db.dbSkill.ToList<Skill>();
            var city = db.dbCity.ToList<City>();
            // SearchViewModel searchVm = new SearchViewModel();
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName");

            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName");
            //@model SaviourRedDrop.Models.SearchViewModel
            //return View();
            
            return View();
        }


        public ActionResult SearchResult(int CityId , int SkillId)
        {
        //    var users = db.dbSPUser.Where(x => x.Area == Area && x.BGId == BGId).ToList();
            var users = db.dbSPUser.Where(x => x.CityId == CityId && x.SkillId == SkillId).ToList();
            // var user = db.dbUser.Where(x => x.BGId == BGId).ToList();

            //  dd(users);


            return View(users);
        }
        public ActionResult Detail(int id/*, int justContact = 0*/)
        {
            var spUser = db.dbSPUser.Where(u => u.Id == id).FirstOrDefault();


            bool alreadyContacted = false;
            if (User.Identity.IsAuthenticated)
            {
                string uId = User.Identity.GetUserId();

                if (UserType(uId).Equals("siteUser"))
                {
                    alreadyContacted = spUser.SiteUser.Any(x => x.AppUserId.Equals(uId));
                    ViewBag.userType = "siteUser";
                    /* if site user has contacted the service provider and is redirected after being added to the contact list*/
                    if (TempData["justContact"] != null && alreadyContacted)
                    {
                        TempData["FlashMessage"] = "Thank you. Your Contact Request has been sent through admin";
                    }
                    /* if site user has alsready contacted the service provider and is seeing his detail*/

                    else if (alreadyContacted && TempData["justContact"] == null && TempData["reviewed"] == null)
                    {
                        TempData["WarningMessage"] = "Your Contact Request has  already been sent through admin";

                    }
                    else if (TempData["reviewed"] != null)
                    {
                        TempData["FlashMessage"] = "Thank you for giving your feedback";
                    }
                    /* variable to disable button in the view if site user has already contacted the service provider user*/
                    ViewBag.AlreadyContacted = alreadyContacted;
                }
                else { // athunticated use is service provider user

                    ViewBag.userType = "spUser";

                }
            }

            


            return View(spUser);
        }






        public ActionResult EditSiteUserProfile(string authUserId)
        {
            if (authUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteUser siteUser = db.dbSiteUser.FirstOrDefault(a=>a.AppUserId.Equals(authUserId));
            if (siteUser == null)
            {
                return RedirectToAction("CreateSiteUserProfile");
            }
            return View(siteUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSiteUserProfile([Bind(Include = "Id,Name,PhoneNumber,AppUserId")] SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteUser);
        }



        // GET: ServiceProviderUsers/Edit/5
        public ActionResult EditSPUserProfile(string authUserId)
        {
            if (authUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProviderUser serviceProviderUser = db.dbSPUser.FirstOrDefault(a => a.AppUserId.Equals(authUserId));
            if (serviceProviderUser == null)
            {
                return RedirectToAction("CreateSpUserProfile");
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
        public ActionResult EditSPUserProfile([Bind(Include = "Id,Name,PhoneNumber,AppUserId,SkillId,CityId")] ServiceProviderUser serviceProviderUser)
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


        [Authorize]
        public ActionResult ContactPerson(int id)
        {
             
            // get athunticated user 

            string uId = User.Identity.GetUserId();
            var authUser = db.dbSiteUser.FirstOrDefault(c => c.AppUserId == uId);
           


            // getting service provider user
            var spUser = db.dbSPUser.FirstOrDefault(c=> c.Id ==id);

            //bool alreadyContacted = spUser.SiteUser.Any(x => x.AppUserId.Equals(uId));

            //if (!alreadyContacted)
            //{
            //    justContact = 1;
            //}

            authUser.ServiceProviderUser.Add(spUser);

            db.Entry(authUser).State = EntityState.Modified;

            db.SaveChanges();

            //        
            TempData["justContact"] = "yes";

            return RedirectToAction("Detail", new { id = id/*, justContact= justContact*/ });
        }
        public string CreateReview()
        {

            return "";
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReview([Bind(Include = "UserId,FeedBack")] Review review)
        {
            // get the current logged in site user  and assign the id to review.UserId
            string uId = User.Identity.GetUserId();
            var authUser = db.dbSiteUser.FirstOrDefault(c => c.AppUserId == uId);
            review.WriterId = authUser.Id;
            if (ModelState.IsValid)
            {
                TempData["reviewed"] = "yes";
                db.dbReview.Add(review);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = review.UserId });
            }
            
            return RedirectToAction("Detail", new { id = review.UserId });
        }
    //    [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSiteUserProfile([Bind(Include = "Id,Name,PhoneNumber,AppUserId")] SiteUser siteUser)
        {
            siteUser.AppUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                System.Web.HttpContext.Current.Session["Name"] = siteUser.Name;
                System.Web.HttpContext.Current.Session["profile"] = "EditSiteUserProfile";
                db.dbSiteUser.Add(siteUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteUser);
        }

 //       [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpUserProfile([Bind(Include = "Id,Name,PhoneNumber,AppUserId,SkillId,CityId")] ServiceProviderUser serviceProviderUser)
        {
            serviceProviderUser.AppUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {

                System.Web.HttpContext.Current.Session["Name"] = serviceProviderUser.Name;
                System.Web.HttpContext.Current.Session["profile"] = "EditSPUserProfile";
                db.dbSPUser.Add(serviceProviderUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.dbCity, "Id", "CityName", serviceProviderUser.CityId);
            ViewBag.SkillId = new SelectList(db.dbSkill, "Id", "SkillName", serviceProviderUser.SkillId);
            return View(serviceProviderUser);
        }
        public ActionResult DeleteContact(int id)
        {

            // get athunticated user 
            var sUser = db.dbSiteUser.FirstOrDefault(c => c.Id == 1);


            var spUser = db.dbSPUser.FirstOrDefault(c => c.Id == id);

            if (spUser == null)
            {
                ViewBag.Error = "Service Provider do not exist";
                return RedirectToAction("Detail", new { id = id });

            }
            sUser.ServiceProviderUser.Remove(spUser);

            db.Entry(sUser).State = EntityState.Modified;

            db.SaveChanges();


            return RedirectToAction("Detail", new { id = id });
        }

        public string test()
        {
            //var p = Session["profile"];
            //var testUser = db.dbSiteUser.FirstOrDefault(c => c.Id == 1);
            //System.Collections.Generic.ICollection<ServiceProviderUser> list = testUser.ServiceProviderUser;
            //var member = list.Any(x => x.Id == 2);

            //   String name = System.Web.HttpContext.Current.Session["Name"] as string;




            //   var firstName = Session["profile"];

            //var list = db.dbSiteUser.Where(t => t.ServiceProviderUser.Count >0).ToList();


            //int count = 0;
            //foreach (var item in list)
            //{
            //    count += item.ServiceProviderUser.Count;
            //}

            //   UserHelper h = new UserHelper();

            //UserHelper.Count
            
            return "" + UserHelper.Name;
            
        }

        private string  UserType( string authUserId)
        {
            //  string uId = User.Identity.GetUserId();
            var siteUser = db.dbSiteUser.FirstOrDefault(c => c.AppUserId.Equals(authUserId)  );
           // var spUser = db.dbSPUser.FirstOrDefault(c => c.AppUserId == authUserId);

            if (siteUser!=null)
            {
                    return "siteUser";
                
            }
            else
            {
                return "spUser";
            }


        }

    }
}

















