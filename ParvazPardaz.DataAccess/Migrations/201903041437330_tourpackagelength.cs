namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourpackagelength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Tour.TourPackage", "Title", c => c.String(maxLength: 300));
            AlterColumn("Tour.TourPackage", "DateTitle", c => c.String(maxLength: 50));
            AlterColumn("Tour.TourPackage", "Code", c => c.String(maxLength: 50));
            AlterColumn("Tour.TourPackage", "FromPrice", c => c.String(maxLength: 50));
            AlterColumn("Tour.TourPackage", "OfferPrice", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("Tour.TourPackage", "OfferPrice", c => c.String());
            AlterColumn("Tour.TourPackage", "FromPrice", c => c.String());
            AlterColumn("Tour.TourPackage", "Code", c => c.String());
            AlterColumn("Tour.TourPackage", "DateTitle", c => c.String());
            AlterColumn("Tour.TourPackage", "Title", c => c.String());
        }
    }
}
