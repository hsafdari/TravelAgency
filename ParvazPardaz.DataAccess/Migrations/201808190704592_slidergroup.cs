namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slidergroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("Core.SliderGroups", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Core.SliderGroups", "GroupTitle", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("Core.SliderGroups", "GroupTitle", c => c.String(nullable: false));
            DropColumn("Core.SliderGroups", "Name");
        }
    }
}
