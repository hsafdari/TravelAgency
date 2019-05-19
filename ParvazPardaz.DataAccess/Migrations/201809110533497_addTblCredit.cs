namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblCredit : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Book.Credits", "UserId", "User.UserProfiles");
            DropForeignKey("Book.Credits", "OrderId", "Book.Orders");
            DropForeignKey("Book.Credits", "ModifierUserId", "User.Users");
            DropForeignKey("Book.Credits", "CreatorUserId", "User.Users");
            DropIndex("Book.Credits", new[] { "ModifierUserId" });
            DropIndex("Book.Credits", new[] { "CreatorUserId" });
            DropIndex("Book.Credits", new[] { "UserId" });
            DropIndex("Book.Credits", new[] { "OrderId" });
            DropTable("Book.Credits");
        }
    }
}
