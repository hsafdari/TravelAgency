namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblVoucherReceiver : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Book.VoucherReceivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Book.VoucherReceivers", "OrderId", "Book.Orders");
            DropForeignKey("Book.VoucherReceivers", "ModifierUserId", "User.Users");
            DropForeignKey("Book.VoucherReceivers", "CreatorUserId", "User.Users");
            DropIndex("Book.VoucherReceivers", new[] { "ModifierUserId" });
            DropIndex("Book.VoucherReceivers", new[] { "CreatorUserId" });
            DropIndex("Book.VoucherReceivers", new[] { "OrderId" });
            DropTable("Book.VoucherReceivers");
        }
    }
}
