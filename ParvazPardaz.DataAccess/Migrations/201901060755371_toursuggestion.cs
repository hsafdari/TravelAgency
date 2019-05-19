namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toursuggestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Magazine.TourSuggestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourTitle = c.String(nullable: false, maxLength: 200),
                        TourDate = c.String(maxLength: 100),
                        AirlineTitle = c.String(nullable: false, maxLength: 50),
                        TourDuration = c.String(nullable: false, maxLength: 50),
                        TourPrice = c.String(nullable: false, maxLength: 25),
                        ImageURL = c.String(nullable: false, maxLength: 250),
                        NavigationUrl = c.String(maxLength: 400),
                        Priority = c.Int(nullable: false),
                        ImageIsActive = c.Boolean(nullable: false),
                        LocationId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedDateTime = c.DateTime(),
                        CreatorDateTime = c.DateTime(),
                        CreatorUserId = c.Int(),
                        CreatorUserIpAddress = c.String(maxLength: 20),
                        ModifierDateTime = c.DateTime(),
                        ModifierUserId = c.Int(),
                        ModifierUserIpAddress = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("Magazine.Locations", t => t.LocationId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .ForeignKey("Magazine.Locations", t => t.Location_Id)
                .Index(t => t.LocationId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId)
                .Index(t => t.Location_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Magazine.TourSuggestion", "Location_Id", "Magazine.Locations");
            DropForeignKey("Magazine.TourSuggestion", "ModifierUserId", "User.Users");
            DropForeignKey("Magazine.TourSuggestion", "LocationId", "Magazine.Locations");
            DropForeignKey("Magazine.TourSuggestion", "CreatorUserId", "User.Users");
            DropIndex("Magazine.TourSuggestion", new[] { "Location_Id" });
            DropIndex("Magazine.TourSuggestion", new[] { "ModifierUserId" });
            DropIndex("Magazine.TourSuggestion", new[] { "CreatorUserId" });
            DropIndex("Magazine.TourSuggestion", new[] { "LocationId" });
            DropTable("Magazine.TourSuggestion");
        }
    }
}
