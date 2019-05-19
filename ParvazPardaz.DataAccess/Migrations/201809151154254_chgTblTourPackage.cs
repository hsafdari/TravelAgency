namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblTourPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.TourPackage", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Tour.TourPackage", "Description");
        }
    }
}
