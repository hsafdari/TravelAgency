namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblVehicleTypeClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Tour.VehicleTypeClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        TitleEn = c.String(nullable: false),
                        Code = c.String(nullable: false, maxLength: 3),
                        VehicleTypeId = c.Int(nullable: false),
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
                .ForeignKey("Tour.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Tour.VehicleTypeClasses", "VehicleTypeId", "Tour.VehicleTypes");
            DropForeignKey("Tour.VehicleTypeClasses", "ModifierUserId", "User.Users");
            DropForeignKey("Tour.VehicleTypeClasses", "CreatorUserId", "User.Users");
            DropIndex("Tour.VehicleTypeClasses", new[] { "ModifierUserId" });
            DropIndex("Tour.VehicleTypeClasses", new[] { "CreatorUserId" });
            DropIndex("Tour.VehicleTypeClasses", new[] { "VehicleTypeId" });
            DropTable("Tour.VehicleTypeClasses");
        }
    }
}
