namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblSliderGroup2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Core.SliderGroups", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Core.SliderGroups", "Priority");
        }
    }
}
