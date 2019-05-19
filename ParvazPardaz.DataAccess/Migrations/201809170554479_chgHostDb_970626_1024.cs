namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970626_1024 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Tour.TourPackageDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Name = c.String(),
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
                "Book.Credits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditType = c.Int(nullable: false),
                        Description = c.String(),
                        OrderId = c.Long(),
                        UserId = c.Int(),
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
                .ForeignKey("User.UserProfiles", t => t.UserId)
                .Index(t => t.OrderId)
                .Index(t => t.UserId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            AddColumn("Tour.TourPackage", "Description", c => c.String());
            AddColumn("Tour.TourPackage", "TourPackgeDayId", c => c.Int());
            AddColumn("User.UserProfiles", "RemainingCreditValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Core.Sliders", "Expirationdate", c => c.DateTime());
            AddColumn("Core.SliderGroups", "Description", c => c.String());
            AddColumn("Core.SliderGroups", "ColorCode", c => c.String());
            AddColumn("Core.SliderGroups", "Priority", c => c.Int(nullable: false));
            CreateIndex("Tour.TourPackage", "TourPackgeDayId");
            AddForeignKey("Tour.TourPackage", "TourPackgeDayId", "Tour.TourPackageDays", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Tour.TourPackageDays", "ModifierUserId", "User.Users");
            DropForeignKey("Tour.TourPackageDays", "CreatorUserId", "User.Users");
            DropForeignKey("Tour.TourPackage", "TourPackgeDayId", "Tour.TourPackageDays");
            DropForeignKey("Book.Credits", "UserId", "User.UserProfiles");
            DropForeignKey("Book.Credits", "OrderId", "Book.Orders");
            DropForeignKey("Book.Credits", "ModifierUserId", "User.Users");
            DropForeignKey("Book.Credits", "CreatorUserId", "User.Users");
            DropIndex("Tour.TourPackage", new[] { "TourPackgeDayId" });
            DropIndex("Book.Credits", new[] { "ModifierUserId" });
            DropIndex("Book.Credits", new[] { "CreatorUserId" });
            DropIndex("Book.Credits", new[] { "UserId" });
            DropIndex("Book.Credits", new[] { "OrderId" });
            DropIndex("Tour.TourPackageDays", new[] { "ModifierUserId" });
            DropIndex("Tour.TourPackageDays", new[] { "CreatorUserId" });
            DropColumn("Core.SliderGroups", "Priority");
            DropColumn("Core.SliderGroups", "ColorCode");
            DropColumn("Core.SliderGroups", "Description");
            DropColumn("Core.Sliders", "Expirationdate");
            DropColumn("User.UserProfiles", "RemainingCreditValue");
            DropColumn("Tour.TourPackage", "TourPackgeDayId");
            DropColumn("Tour.TourPackage", "Description");
            DropTable("Book.Credits");
            DropTable("Tour.TourPackageDays");
        }
    }
}
