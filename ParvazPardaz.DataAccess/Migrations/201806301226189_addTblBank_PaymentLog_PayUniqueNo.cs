namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblBank_PaymentLog_PayUniqueNo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.PaymentUniqueNumbers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateTime = c.DateTime(),
                        CreatorDateTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                        CreatorUserIpAddress = c.String(maxLength: 20),
                        ModifierDateTime = c.DateTime(),
                        ModifierUserId = c.Int(),
                        ModifierUserIpAddress = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .ForeignKey("Book.Orders", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            CreateTable(
                "Book.Banks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogoUrl = c.String(nullable: false),
                        EnTitle = c.String(nullable: false),
                        FaTitle = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsRedirectToBank = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        BankTerminalId = c.String(nullable: false),
                        BankUserName = c.String(nullable: false),
                        BankPassword = c.String(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateTime = c.DateTime(),
                        CreatorDateTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                        CreatorUserIpAddress = c.String(maxLength: 20),
                        ModifierDateTime = c.DateTime(),
                        ModifierUserId = c.Int(),
                        ModifierUserIpAddress = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            CreateTable(
                "Book.PaymentLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrackingCode = c.String(nullable: false),
                        PaymentResponseCode = c.String(nullable: false),
                        PaymentResponseMessage = c.String(nullable: false),
                        IsSuccessful = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankId = c.Int(),
                        OrderId = c.Long(nullable: false),
                        PaymentUniqueNumberID = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateTime = c.DateTime(),
                        CreatorDateTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                        CreatorUserIpAddress = c.String(maxLength: 20),
                        ModifierDateTime = c.DateTime(),
                        ModifierUserId = c.Int(),
                        ModifierUserIpAddress = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Book.Banks", t => t.BankId)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .Index(t => t.BankId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Book.PaymentLogs", "ModifierUserId", "User.Users");
            DropForeignKey("Book.PaymentLogs", "CreatorUserId", "User.Users");
            DropForeignKey("Book.PaymentLogs", "BankId", "Book.Banks");
            DropForeignKey("Book.Banks", "ModifierUserId", "User.Users");
            DropForeignKey("Book.Banks", "CreatorUserId", "User.Users");
            DropForeignKey("Book.PaymentUniqueNumbers", "OrderId", "Book.Orders");
            DropForeignKey("Book.PaymentUniqueNumbers", "ModifierUserId", "User.Users");
            DropForeignKey("Book.PaymentUniqueNumbers", "CreatorUserId", "User.Users");
            DropIndex("Book.PaymentLogs", new[] { "ModifierUserId" });
            DropIndex("Book.PaymentLogs", new[] { "CreatorUserId" });
            DropIndex("Book.PaymentLogs", new[] { "BankId" });
            DropIndex("Book.Banks", new[] { "ModifierUserId" });
            DropIndex("Book.Banks", new[] { "CreatorUserId" });
            DropIndex("Book.PaymentUniqueNumbers", new[] { "ModifierUserId" });
            DropIndex("Book.PaymentUniqueNumbers", new[] { "CreatorUserId" });
            DropIndex("Book.PaymentUniqueNumbers", new[] { "OrderId" });
            DropTable("Book.PaymentLogs");
            DropTable("Book.Banks");
            DropTable("Book.PaymentUniqueNumbers");
        }
    }
}
