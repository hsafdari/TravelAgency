namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeHostDb_970510_1910 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Book.Passengers", "OrderId", "Book.Orders");
            DropIndex("Book.Passengers", new[] { "OrderId" });
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .ForeignKey("User.UserProfiles", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            CreateTable(
                "User.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinCreditValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesCountPerDay = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            AddColumn("User.Users", "RemainCreditValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.Passengers", "SelectedRoomId", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "AdultCount", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "ChildCount", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "InfantCount", c => c.Int(nullable: false));
            AddColumn("User.UserProfiles", "ProfileType", c => c.Int(nullable: false));
            AddColumn("User.UserProfiles", "UserGroupId", c => c.Int());
            CreateIndex("Book.Passengers", "SelectedRoomId");
            CreateIndex("User.UserProfiles", "UserGroupId");
            AddForeignKey("Book.Passengers", "SelectedRoomId", "Book.SelectedRooms", "Id");
            AddForeignKey("User.UserProfiles", "UserGroupId", "User.UserGroups", "Id");
            DropColumn("Book.Passengers", "OrderId");
            DropColumn("Book.SelectedRooms", "ReserveCapacity");
            DropColumn("User.UserProfiles", "Shasi");
        }
        
        public override void Down()
        {
            AddColumn("User.UserProfiles", "Shasi", c => c.String());
            AddColumn("Book.SelectedRooms", "ReserveCapacity", c => c.Int(nullable: false));
            AddColumn("Book.Passengers", "OrderId", c => c.Long(nullable: false));
            DropForeignKey("Book.Coupons", "ModifierUserId", "User.Users");
            DropForeignKey("Book.Coupons", "CreatorUserId", "User.Users");
            DropForeignKey("Core.UserCommissions", "Id", "User.UserProfiles");
            DropForeignKey("Core.UserCommissions", "ModifierUserId", "User.Users");
            DropForeignKey("Core.UserCommissions", "CreatorUserId", "User.Users");
            DropForeignKey("User.UserProfiles", "UserGroupId", "User.UserGroups");
            DropForeignKey("User.UserGroups", "ModifierUserId", "User.Users");
            DropForeignKey("User.UserGroups", "CreatorUserId", "User.Users");
            DropForeignKey("Book.Passengers", "SelectedRoomId", "Book.SelectedRooms");
            DropIndex("Book.Coupons", new[] { "ModifierUserId" });
            DropIndex("Book.Coupons", new[] { "CreatorUserId" });
            DropIndex("User.UserGroups", new[] { "ModifierUserId" });
            DropIndex("User.UserGroups", new[] { "CreatorUserId" });
            DropIndex("User.UserProfiles", new[] { "UserGroupId" });
            DropIndex("Book.Passengers", new[] { "SelectedRoomId" });
            DropIndex("Core.UserCommissions", new[] { "ModifierUserId" });
            DropIndex("Core.UserCommissions", new[] { "CreatorUserId" });
            DropIndex("Core.UserCommissions", new[] { "Id" });
            DropColumn("User.UserProfiles", "UserGroupId");
            DropColumn("User.UserProfiles", "ProfileType");
            DropColumn("Book.SelectedRooms", "InfantCount");
            DropColumn("Book.SelectedRooms", "ChildCount");
            DropColumn("Book.SelectedRooms", "AdultCount");
            DropColumn("Book.Passengers", "SelectedRoomId");
            DropColumn("User.Users", "RemainCreditValue");
            DropTable("Book.Coupons");
            DropTable("User.UserGroups");
            DropTable("Core.UserCommissions");
            CreateIndex("Book.Passengers", "OrderId");
            AddForeignKey("Book.Passengers", "OrderId", "Book.Orders", "Id");
        }
    }
}
