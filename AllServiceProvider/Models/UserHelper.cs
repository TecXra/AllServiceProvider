using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllServiceProvider.Models
{
    public class UserHelper
    {
        static string authUserId = "";
        static string name = "";
        static  int count = 0;
        public static  string Name {

            set {

                authUserId = value;
            }


            get
            {

                MyDbContext db = new MyDbContext();
                var siteUser = db.dbSiteUser.FirstOrDefault(c => c.AppUserId.Equals(authUserId));
                var spUser = db.dbSPUser.FirstOrDefault(c => c.AppUserId.Equals(authUserId));

                if (siteUser != null)
                {

                    return (siteUser.Name == null) ? "" : siteUser.Name;

                }
                else if (spUser != null)
                {
                    return (spUser.Name == null) ? "" : spUser.Name;
                }
                else {
                    return "";
                }
              


            }



        }
         static string profile ="";
        public static string Profile {

            set {
                profile = value;

            }



            get {


                return profile;

            }



        }

        public static int Count {
            get
            {
                count = 0;
                MyDbContext db = new MyDbContext();
                var list = db.dbSiteUser.Where(t => t.ServiceProviderUser.Count > 0).ToList();



                foreach (var item in list)
                {
                   count += item.ServiceProviderUser.Count;
                }
                return count;
            }
            set
            {
                count = value;
            }


        }
    }
}