namespace plan2plan.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plan2plan_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSession", "IP", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSession", "IP");
        }
    }
}
