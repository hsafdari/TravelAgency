namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nwvrson : DbMigration
    {
        public override void Up()
        {
            AddColumn("Country.Cities", "IsDddlFrom", c => c.Boolean(nullable: false));
            AddColumn("Country.Cities", "IsDddlDestination", c => c.Boolean(nullable: false));
            AddColumn("Core.Sliders", "HeaderDays", c => c.String(maxLength: 20));
            AddColumn("Core.Sliders", "NavDescription", c => c.String(maxLength: 200));
            AddColumn("Core.Sliders", "Price", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Core.Sliders", "footerLine1", c => c.String(maxLength: 50));
            AddColumn("Core.Sliders", "footerLine2", c => c.String(maxLength: 50));
            AddColumn("Core.SliderGroups", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Core.SliderGroups", "GroupTitle", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("Core.SliderGroups", "GroupTitle", c => c.String(nullable: false));
            DropColumn("Core.SliderGroups", "Name");
            DropColumn("Core.Sliders", "footerLine2");
            DropColumn("Core.Sliders", "footerLine1");
            DropColumn("Core.Sliders", "Price");
            DropColumn("Core.Sliders", "NavDescription");
            DropColumn("Core.Sliders", "HeaderDays");
            DropColumn("Country.Cities", "IsDddlDestination");
            DropColumn("Country.Cities", "IsDddlFrom");
        }
    }
}
