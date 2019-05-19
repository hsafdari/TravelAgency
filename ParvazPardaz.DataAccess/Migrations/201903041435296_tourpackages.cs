namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourpackages : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.TourPackage", "OfferPrice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Tour.TourPackage", "OfferPrice");
        }
    }
}
