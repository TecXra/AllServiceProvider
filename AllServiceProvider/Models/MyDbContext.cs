using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace AllServiceProvider.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MyDbContext")
        {

        }

        public DbSet<Skill> dbSkill { set; get; }
        public DbSet<Review> dbReview { set; get; }
        public DbSet<SiteUser> dbSiteUser { set; get; }
        public DbSet<ServiceProviderUser> dbSPUser { set; get; }
        public DbSet<City> dbCity { set; get; }







        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SiteUser>()
                        .HasMany<ServiceProviderUser>(s => s.ServiceProviderUser)
                        .WithMany(c => c.SiteUser)

                        .Map(cs =>
                        {
                            
                            cs.MapLeftKey("SiteUserId");
                            cs.MapRightKey("ServiceProviderUserId");
                            cs.ToTable("ContactRequest");

                        });


        }















    }
}