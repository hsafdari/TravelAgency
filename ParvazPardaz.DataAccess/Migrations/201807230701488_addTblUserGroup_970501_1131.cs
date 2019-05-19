namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblUserGroup_970501_1131 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("User.UserProfiles", "UserGroupId", c => c.Int());
            CreateIndex("User.UserProfiles", "UserGroupId");
            AddForeignKey("User.UserProfiles", "UserGroupId", "User.UserGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("User.UserProfiles", "UserGroupId", "User.UserGroups");
            DropForeignKey("User.UserGroups", "ModifierUserId", "User.Users");
            DropForeignKey("User.UserGroups", "CreatorUserId", "User.Users");
            DropIndex("User.UserGroups", new[] { "ModifierUserId" });
            DropIndex("User.UserGroups", new[] { "CreatorUserId" });
            DropIndex("User.UserProfiles", new[] { "UserGroupId" });
            DropColumn("User.UserProfiles", "UserGroupId");
            DropTable("User.UserGroups");
        }
    }
}
