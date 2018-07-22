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
                        isEmailConfirmed = c.Boolean(nullable: false),
                        subscribedToNewsletters = c.Boolean(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Order = c.Int(),
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
                "dbo.Letter",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailID = c.Int(nullable: false),
                        LetterTypeID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Email", t => t.EmailID, cascadeDelete: true)
                .ForeignKey("dbo.LetterType", t => t.LetterTypeID, cascadeDelete: true)
                .Index(t => t.EmailID)
                .Index(t => t.LetterTypeID);
            
            CreateTable(
                "dbo.LetterType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        FullReferrer = c.String(),
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
                        ID = c.Guid(nullable: false, identity: true),
                        EmailID = c.Int(nullable: false),
                        UserTypeID = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(),
                        dateTime = c.DateTime(nullable: false),
                        SurName = c.String(),
                        Phone = c.String(),
                        City = c.String(),
                        Address = c.String(),
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
                        UserID = c.Guid(nullable: false),
                        SessionID = c.String(nullable: false),
                        IP = c.String(nullable: false),
                        Start = c.DateTime(),
                        Finish = c.DateTime(),
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
            DropForeignKey("dbo.Letter", "LetterTypeID", "dbo.LetterType");
            DropForeignKey("dbo.Letter", "EmailID", "dbo.Email");
            DropForeignKey("dbo.Feedback", "EmailID", "dbo.Email");
            DropForeignKey("dbo.Action", "FileID", "dbo.File");
            DropForeignKey("dbo.Action", "EmailID", "dbo.Email");
            DropIndex("dbo.UserSession", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "UserTypeID" });
            DropIndex("dbo.User", new[] { "EmailID" });
            DropIndex("dbo.Letter", new[] { "LetterTypeID" });
            DropIndex("dbo.Letter", new[] { "EmailID" });
            DropIndex("dbo.Feedback", new[] { "EmailID" });
            DropIndex("dbo.Action", new[] { "EmailID" });
            DropIndex("dbo.Action", new[] { "FileID" });
            DropTable("dbo.UserSession");
            DropTable("dbo.UserType");
            DropTable("dbo.User");
            DropTable("dbo.Statistics");
            DropTable("dbo.LetterType");
            DropTable("dbo.Letter");
            DropTable("dbo.Feedback");
            DropTable("dbo.File");
            DropTable("dbo.Email");
            DropTable("dbo.Action");
        }
    }
}
