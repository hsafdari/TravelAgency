namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngHostDb_970420_1747 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Tour.TourPackage", "OwnerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("Tour.TourPackage", "OwnerId", c => c.Int(nullable: false));
        }
    }
}
