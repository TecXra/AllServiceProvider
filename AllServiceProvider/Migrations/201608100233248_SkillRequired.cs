namespace AllServiceProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skills", "SkillName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "SkillName", c => c.String());
        }
    }
}
