namespace AllServiceProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "FeedBack", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "FeedBack", c => c.String());
        }
    }
}
