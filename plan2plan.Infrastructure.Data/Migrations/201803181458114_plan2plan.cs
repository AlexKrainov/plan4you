namespace plan2plan.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plan2plan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IP = c.String(nullable: false),
                        FileID = c.Guid(),
                        EmailID = c.Int(),
                        isDownload = c.Boolean(nullable: false),
                        isLike = c.Boolean(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Email", t => t.EmailID)
                .ForeignKey("dbo.File", t => t.FileID)
                .Index(t => t.FileID)
                .Index(t => t.EmailID);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        FileName = c.String(),
                        Path = c.String(),
                        PreviewPath_min = c.String(),
                        PreviewPath_avg = c.String(),
                        PreviewPath_max = c.String(),
                        isDelete = c.Boolean(nullable: false),
                        isShow = c.Boolean(nullable: false),
                        isExist = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailID = c.Int(nullable: false),
                        Message = c.String(),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Email", t => t.EmailID, cascadeDelete: true)
                .Index(t => t.EmailID);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IP = c.String(),
                        SessionID = c.String(),
                        Browser_name = c.String(),
                        Browser_version = c.String(),
                        OS_name = c.String(),
                        OS_version = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Status = c.String(),
                        isMobile = c.Boolean(nullable: false),
                        Referrer = c.String(),
                        Screen_size = c.String(),
                        Index = c.String(),
                        Location = c.String(),
                        dateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailID = c.Int(nullable: false),
                        UserTypeID = c.Int(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        dateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Email", t => t.EmailID, cascadeDelete: true)
                .ForeignKey("dbo.UserType", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.EmailID)
                .Index(t => t.UserTypeID);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserSession",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SessionID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSession", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "UserTypeID", "dbo.UserType");
            DropForeignKey("dbo.User", "EmailID", "dbo.Email");
            DropForeignKey("dbo.Feedback", "EmailID", "dbo.Email");
            DropForeignKey("dbo.Action", "FileID", "dbo.File");
            DropForeignKey("dbo.Action", "EmailID", "dbo.Email");
            DropIndex("dbo.UserSession", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "UserTypeID" });
            DropIndex("dbo.User", new[] { "EmailID" });
            DropIndex("dbo.Feedback", new[] { "EmailID" });
            DropIndex("dbo.Action", new[] { "EmailID" });
            DropIndex("dbo.Action", new[] { "FileID" });
            DropTable("dbo.UserSession");
            DropTable("dbo.UserType");
            DropTable("dbo.User");
            DropTable("dbo.Statistics");
            DropTable("dbo.Feedback");
            DropTable("dbo.File");
            DropTable("dbo.Email");
            DropTable("dbo.Action");
        }
    }
}
