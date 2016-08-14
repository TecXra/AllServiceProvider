namespace AllServiceProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceProviderUsers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ServiceProviderUsers", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.SiteUsers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SiteUsers", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SiteUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.SiteUsers", "Name", c => c.String());
            AlterColumn("dbo.ServiceProviderUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.ServiceProviderUsers", "Name", c => c.String());
        }
    }
}
