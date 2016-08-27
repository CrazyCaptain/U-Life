namespace UHacMnl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SmsMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmsReceives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestId = c.String(),
                        Message = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        SubscriberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscribers", t => t.SubscriberId, cascadeDelete: true)
                .Index(t => t.SubscriberId);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        SubscriberId = c.Int(nullable: false, identity: true),
                        ContactNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CertificatesLeft = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriberId);
            
            CreateTable(
                "dbo.SmsRecipients",
                c => new
                    {
                        SmsRecipientId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        SubscriberId = c.Int(nullable: false),
                        SmsMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SmsRecipientId)
                .ForeignKey("dbo.SmsMessages", t => t.SmsMessageId, cascadeDelete: true)
                .ForeignKey("dbo.Subscribers", t => t.SubscriberId, cascadeDelete: true)
                .Index(t => t.SubscriberId)
                .Index(t => t.SmsMessageId);
            
            CreateTable(
                "dbo.SmsReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestId = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        SubscriberId = c.Int(nullable: false),
                        SmsReceiveId = c.Int(nullable: false),
                        SmsMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SmsMessages", t => t.SmsMessageId, cascadeDelete: true)
                .ForeignKey("dbo.SmsReceives", t => t.SmsReceiveId, cascadeDelete: true)
                .Index(t => t.SmsReceiveId)
                .Index(t => t.SmsMessageId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SmsReplies", "SmsReceiveId", "dbo.SmsReceives");
            DropForeignKey("dbo.SmsReplies", "SmsMessageId", "dbo.SmsMessages");
            DropForeignKey("dbo.SmsRecipients", "SubscriberId", "dbo.Subscribers");
            DropForeignKey("dbo.SmsRecipients", "SmsMessageId", "dbo.SmsMessages");
            DropForeignKey("dbo.SmsReceives", "SubscriberId", "dbo.Subscribers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SmsReplies", new[] { "SmsMessageId" });
            DropIndex("dbo.SmsReplies", new[] { "SmsReceiveId" });
            DropIndex("dbo.SmsRecipients", new[] { "SmsMessageId" });
            DropIndex("dbo.SmsRecipients", new[] { "SubscriberId" });
            DropIndex("dbo.SmsReceives", new[] { "SubscriberId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SmsReplies");
            DropTable("dbo.SmsRecipients");
            DropTable("dbo.Subscribers");
            DropTable("dbo.SmsReceives");
            DropTable("dbo.SmsMessages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
