namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourpackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.TourPackage", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Tour.TourPackage", "ImageURL");
        }
    }
}
