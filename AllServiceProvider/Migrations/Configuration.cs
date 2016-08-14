namespace AllServiceProvider.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AllServiceProvider.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<AllServiceProvider.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AllServiceProvider.Models.MyDbContext context)
        {
            //context.dbCity.ToList();
            context.dbCity.AddOrUpdate(
                   p => p.CityName,
               new City { CityName = "Lahore" },
               new City { CityName = "Karachi" },
               new City { CityName = "Sialkot" },
               new City { CityName = "Faisalabad" }

               );
            context.SaveChanges();

            context.dbSkill.AddOrUpdate(
                p => p.SkillName,
              new Skill { SkillName = "Carpenter" },
              new Skill { SkillName = "Construction" },
              new Skill { SkillName = "Plumber" },
              new Skill { SkillName = "Driver" },
              new Skill { SkillName = "Electritian" },
              new Skill { SkillName = "Tutor" }

              );
            context.SaveChanges();

            context.dbSPUser.AddOrUpdate(
           p => p.Name,
                new ServiceProviderUser { Name = "Najam Sethi", PhoneNumber = "03214567893", CityId = 1, SkillId = 1 },
                new ServiceProviderUser { Name = "Afzaal Ahmad", PhoneNumber = "03214567893", CityId = 1, SkillId = 1 },
           new ServiceProviderUser { Name = "Junaid Jamsheed", PhoneNumber = "03214567893", CityId = 1, SkillId = 1 },

                new ServiceProviderUser { Name = "Waseem Akhtar", PhoneNumber = "03214567893", CityId = 2, SkillId = 4 },
                 new ServiceProviderUser { Name = "Shaoib Anwar", PhoneNumber = "03214567893", CityId = 3, SkillId = 2 }

                /*           new ServiceProviderUser { Name = "Shaoib Anwar", PhoneNumber = "03214567893", City = context.dbCity.FirstOrDefault(a => a.CityName.Equals("Karachi")), Skill = context.dbSkill.FirstOrDefault(a => a.SkillName.Equals("Plumber")) }
              */

                );
            context.SaveChanges();


            context.dbSiteUser.AddOrUpdate(
              p => p.Name,
            new SiteUser { Name = "AR Malik", PhoneNumber = "03335678904" }

            );

            context.SaveChanges();





            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
