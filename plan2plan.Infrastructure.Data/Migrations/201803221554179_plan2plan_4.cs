namespace plan2plan.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plan2plan_4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSession", "Start", c => c.DateTime());
            AlterColumn("dbo.UserSession", "Finish", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSession", "Finish", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserSession", "Start", c => c.DateTime(nullable: false));
        }
    }
}
