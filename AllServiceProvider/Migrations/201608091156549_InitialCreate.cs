namespace AllServiceProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FeedBack = c.String(),
                        WriterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceProviderUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SiteUsers", t => t.WriterId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WriterId);
            
            CreateTable(
                "dbo.ServiceProviderUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        AppUserId = c.String(),
                        SkillId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.SkillId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.SiteUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        AppUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactRequest",
                c => new
                    {
                        SiteUserId = c.Int(nullable: false),
                        ServiceProviderUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SiteUserId, t.ServiceProviderUserId })
                .ForeignKey("dbo.SiteUsers", t => t.SiteUserId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviderUsers", t => t.ServiceProviderUserId, cascadeDelete: true)
                .Index(t => t.SiteUserId)
                .Index(t => t.ServiceProviderUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceProviderUsers", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ContactRequest", "ServiceProviderUserId", "dbo.ServiceProviderUsers");
            DropForeignKey("dbo.ContactRequest", "SiteUserId", "dbo.SiteUsers");
            DropForeignKey("dbo.Reviews", "WriterId", "dbo.SiteUsers");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.ServiceProviderUsers");
            DropForeignKey("dbo.ServiceProviderUsers", "CityId", "dbo.Cities");
            DropIndex("dbo.ContactRequest", new[] { "ServiceProviderUserId" });
            DropIndex("dbo.ContactRequest", new[] { "SiteUserId" });
            DropIndex("dbo.ServiceProviderUsers", new[] { "CityId" });
            DropIndex("dbo.ServiceProviderUsers", new[] { "SkillId" });
            DropIndex("dbo.Reviews", new[] { "WriterId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropTable("dbo.ContactRequest");
            DropTable("dbo.Skills");
            DropTable("dbo.SiteUsers");
            DropTable("dbo.ServiceProviderUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Cities");
        }
    }
}
