namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sliderGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("Core.Sliders", "Expirationdate", c => c.DateTime());
            AddColumn("Core.SliderGroups", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Core.SliderGroups", "Description");
            DropColumn("Core.Sliders", "Expirationdate");
        }
    }
}
