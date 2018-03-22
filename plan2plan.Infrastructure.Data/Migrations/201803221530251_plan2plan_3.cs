namespace plan2plan.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plan2plan_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSession", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserSession", "Finish", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSession", "Finish");
            DropColumn("dbo.UserSession", "Start");
        }
    }
}
