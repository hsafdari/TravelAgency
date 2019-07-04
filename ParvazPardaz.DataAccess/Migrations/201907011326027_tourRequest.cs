namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Tour.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InfantCount = c.Int(nullable: false),
                        ChildCount = c.Int(nullable: false),
                        AdultCount = c.Int(nullable: false),
                        RoomType = c.String(),
                        TourPackageId = c.Int(nullable: false),
                        TourPackageTitle = c.String(),
                        HotelPackageId = c.Int(nullable: false),
                        HotelPackageTitle = c.String(),
                        DepartureFlightId = c.Int(nullable: false),
                        ArrivalFlightId = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Tour.Requests", "ModifierUserId", "User.Users");
            DropForeignKey("Tour.Requests", "CreatorUserId", "User.Users");
            DropIndex("Tour.Requests", new[] { "ModifierUserId" });
            DropIndex("Tour.Requests", new[] { "CreatorUserId" });
            DropTable("Tour.Requests");
        }
    }
}
