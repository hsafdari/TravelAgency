namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970716_1802 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AccessLevel.RolePermissionControllers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        ControllerId = c.Int(nullable: false),
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
                .ForeignKey("AccessLevel.ControllersLists", t => t.ControllerId)
                .ForeignKey("User.Users", t => t.CreatorUserId)
                .ForeignKey("User.Users", t => t.ModifierUserId)
                .ForeignKey("AccessLevel.Permissions", t => t.PermissionId)
                .ForeignKey("User.Roles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId)
                .Index(t => t.ControllerId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.ModifierUserId);
            
            CreateTable(
                "AccessLevel.ControllersLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageName = c.String(nullable: false),
                        PageUrl = c.String(nullable: false),
                        ControllerName = c.String(nullable: false),
                        ActionName = c.String(nullable: false),
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
                "AccessLevel.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(nullable: false),
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
            DropForeignKey("AccessLevel.RolePermissionControllers", "RoleId", "User.Roles");
            DropForeignKey("AccessLevel.RolePermissionControllers", "PermissionId", "AccessLevel.Permissions");
            DropForeignKey("AccessLevel.Permissions", "ModifierUserId", "User.Users");
            DropForeignKey("AccessLevel.Permissions", "CreatorUserId", "User.Users");
            DropForeignKey("AccessLevel.RolePermissionControllers", "ModifierUserId", "User.Users");
            DropForeignKey("AccessLevel.RolePermissionControllers", "CreatorUserId", "User.Users");
            DropForeignKey("AccessLevel.RolePermissionControllers", "ControllerId", "AccessLevel.ControllersLists");
            DropForeignKey("AccessLevel.ControllersLists", "ModifierUserId", "User.Users");
            DropForeignKey("AccessLevel.ControllersLists", "CreatorUserId", "User.Users");
            DropIndex("AccessLevel.Permissions", new[] { "ModifierUserId" });
            DropIndex("AccessLevel.Permissions", new[] { "CreatorUserId" });
            DropIndex("AccessLevel.ControllersLists", new[] { "ModifierUserId" });
            DropIndex("AccessLevel.ControllersLists", new[] { "CreatorUserId" });
            DropIndex("AccessLevel.RolePermissionControllers", new[] { "ModifierUserId" });
            DropIndex("AccessLevel.RolePermissionControllers", new[] { "CreatorUserId" });
            DropIndex("AccessLevel.RolePermissionControllers", new[] { "ControllerId" });
            DropIndex("AccessLevel.RolePermissionControllers", new[] { "PermissionId" });
            DropIndex("AccessLevel.RolePermissionControllers", new[] { "RoleId" });
            DropTable("AccessLevel.Permissions");
            DropTable("AccessLevel.ControllersLists");
            DropTable("AccessLevel.RolePermissionControllers");
        }
    }
}
