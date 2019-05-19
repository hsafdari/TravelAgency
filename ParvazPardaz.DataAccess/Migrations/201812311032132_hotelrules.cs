namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelrules : DbMigration
    {
        public override void Up()
        {
            AddColumn("Hotel.Hotels", "HotelRule", c => c.String());
            AddColumn("Hotel.Hotels", "CancelationPolicy", c => c.String());
            AlterColumn("AccessLevel.ControllersLists", "PageName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("AccessLevel.ControllersLists", "PageUrl", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("AccessLevel.ControllersLists", "ControllerName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("AccessLevel.ControllersLists", "ActionName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("AccessLevel.Permissions", "PermissionName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("AccessLevel.Permissions", "PermissionName", c => c.String(nullable: false));
            AlterColumn("AccessLevel.ControllersLists", "ActionName", c => c.String(nullable: false));
            AlterColumn("AccessLevel.ControllersLists", "ControllerName", c => c.String(nullable: false));
            AlterColumn("AccessLevel.ControllersLists", "PageUrl", c => c.String(nullable: false));
            AlterColumn("AccessLevel.ControllersLists", "PageName", c => c.String(nullable: false));
            DropColumn("Hotel.Hotels", "CancelationPolicy");
            DropColumn("Hotel.Hotels", "HotelRule");
        }
    }
}
