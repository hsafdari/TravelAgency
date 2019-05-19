namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_960409_1821 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.OrderInformations",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        NationalCode = c.String(maxLength: 10),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Title = c.String(nullable: false),
                        Age = c.String(),
                        Tel = c.String(),
                        Cellphone = c.String(),
                        Address = c.String(),
                        UserId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        NationalityId = c.Int(nullable: false),
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
                .ForeignKey("Country.Cities", t => t.CityId)
                .ForeignKey("Country.Countries", t => t.NationalityId)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .ForeignKey("Book.Orders", t => t.Id)
                .ForeignKey("User.Users", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.CityId)
                .Index(t => t.NationalityId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Book.OrderInformations", "UserId", "User.Users");
            DropForeignKey("Book.OrderInformations", "Id", "Book.Orders");
            DropForeignKey("Book.OrderInformations", "ModifierUserId", "User.Users");
            DropForeignKey("Book.OrderInformations", "CreatorUserId", "User.Users");
            DropForeignKey("Book.OrderInformations", "NationalityId", "Country.Countries");
            DropForeignKey("Book.OrderInformations", "CityId", "Country.Cities");
            DropIndex("Book.OrderInformations", new[] { "ModifierUserId" });
            DropIndex("Book.OrderInformations", new[] { "CreatorUserId" });
            DropIndex("Book.OrderInformations", new[] { "NationalityId" });
            DropIndex("Book.OrderInformations", new[] { "CityId" });
            DropIndex("Book.OrderInformations", new[] { "UserId" });
            DropIndex("Book.OrderInformations", new[] { "Id" });
            DropTable("Book.OrderInformations");
        }
    }
}
