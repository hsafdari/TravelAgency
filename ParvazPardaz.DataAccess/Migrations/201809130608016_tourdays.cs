namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourdays : DbMigration
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
            
            AddColumn("Tour.TourPackage", "TourPackgeDayId", c => c.Int());
            CreateIndex("Tour.TourPackage", "TourPackgeDayId");
            AddForeignKey("Tour.TourPackage", "TourPackgeDayId", "Tour.TourPackageDays", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Tour.TourPackageDays", "ModifierUserId", "User.Users");
            DropForeignKey("Tour.TourPackageDays", "CreatorUserId", "User.Users");
            DropForeignKey("Tour.TourPackage", "TourPackgeDayId", "Tour.TourPackageDays");
            DropIndex("Tour.TourPackage", new[] { "TourPackgeDayId" });
            DropIndex("Tour.TourPackageDays", new[] { "ModifierUserId" });
            DropIndex("Tour.TourPackageDays", new[] { "CreatorUserId" });
            DropColumn("Tour.TourPackage", "TourPackgeDayId");
            DropTable("Tour.TourPackageDays");
        }
    }
}
