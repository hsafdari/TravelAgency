namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblSliderGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("Core.SliderGroups", "ColorCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Core.SliderGroups", "ColorCode");
        }
    }
}
