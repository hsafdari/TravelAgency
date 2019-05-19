namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngDb_970509_1242 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Book.Passengers", "OrderId", "Book.Orders");
            DropForeignKey("Core.UserCommissions", "CreatorUserId", "User.Users");
            DropForeignKey("Core.UserCommissions", "ModifierUserId", "User.Users");
            DropForeignKey("Core.UserCommissions", "Id", "User.UserProfiles");
            DropForeignKey("Book.Coupons", "CreatorUserId", "User.Users");
            DropForeignKey("Book.Coupons", "ModifierUserId", "User.Users");
            DropIndex("Book.Passengers", new[] { "OrderId" });
            DropIndex("Core.UserCommissions", new[] { "Id" });
            DropIndex("Core.UserCommissions", new[] { "CreatorUserId" });
            DropIndex("Core.UserCommissions", new[] { "ModifierUserId" });
            DropIndex("Book.Coupons", new[] { "CreatorUserId" });
            DropIndex("Book.Coupons", new[] { "ModifierUserId" });
            AddColumn("Book.Passengers", "SelectedRoomId", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "AdultCount", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "ChildCount", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "InfantCount", c => c.Int(nullable: false));
            AddColumn("User.UserProfiles", "Shasi", c => c.String());
            CreateIndex("Book.Passengers", "SelectedRoomId");
            AddForeignKey("Book.Passengers", "SelectedRoomId", "Book.SelectedRooms", "Id");
            DropColumn("Book.Passengers", "OrderId");
            DropColumn("User.UserProfiles", "ProfileType");
            DropTable("Core.UserCommissions");
            DropTable("Book.Coupons");
        }
        
        public override void Down()
        {
            CreateTable(
                "Book.Coupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 10),
                        Code = c.String(nullable: false, maxLength: 10),
                        DiscountPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpireDate = c.DateTime(nullable: false),
                        ValidatedUserId = c.Int(),
                        ValidatedDate = c.DateTime(),
                        OrderId = c.Int(),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Core.UserCommissions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CommissionPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        ConditionPercent = c.Decimal(precision: 18, scale: 2),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("User.UserProfiles", "ProfileType", c => c.Int(nullable: false));
            AddColumn("Book.Passengers", "OrderId", c => c.Long(nullable: false));
            DropForeignKey("Book.Passengers", "SelectedRoomId", "Book.SelectedRooms");
            DropIndex("Book.Passengers", new[] { "SelectedRoomId" });
            DropColumn("User.UserProfiles", "Shasi");
            DropColumn("Book.SelectedRooms", "InfantCount");
            DropColumn("Book.SelectedRooms", "ChildCount");
            DropColumn("Book.SelectedRooms", "AdultCount");
            DropColumn("Book.Passengers", "SelectedRoomId");
            CreateIndex("Book.Coupons", "ModifierUserId");
            CreateIndex("Book.Coupons", "CreatorUserId");
            CreateIndex("Core.UserCommissions", "ModifierUserId");
            CreateIndex("Core.UserCommissions", "CreatorUserId");
            CreateIndex("Core.UserCommissions", "Id");
            CreateIndex("Book.Passengers", "OrderId");
            AddForeignKey("Book.Coupons", "ModifierUserId", "User.Users", "Id");
            AddForeignKey("Book.Coupons", "CreatorUserId", "User.Users", "Id");
            AddForeignKey("Core.UserCommissions", "Id", "User.UserProfiles", "Id");
            AddForeignKey("Core.UserCommissions", "ModifierUserId", "User.Users", "Id");
            AddForeignKey("Core.UserCommissions", "CreatorUserId", "User.Users", "Id");
            AddForeignKey("Book.Passengers", "OrderId", "Book.Orders", "Id");
        }
    }
}
