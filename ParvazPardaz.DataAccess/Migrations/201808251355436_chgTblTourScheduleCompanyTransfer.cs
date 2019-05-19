namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblTourScheduleCompanyTransfer : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.TourScheduleCompanyTransfers", "VehicleTypeClassId", c => c.Int(nullable: false));
            CreateIndex("Tour.TourScheduleCompanyTransfers", "VehicleTypeClassId");
            AddForeignKey("Tour.TourScheduleCompanyTransfers", "VehicleTypeClassId", "Tour.VehicleTypeClasses", "Id");
            DropColumn("Tour.TourScheduleCompanyTransfers", "FlightClass");
        }
        
        public override void Down()
        {
            AddColumn("Tour.TourScheduleCompanyTransfers", "FlightClass", c => c.String());
            DropForeignKey("Tour.TourScheduleCompanyTransfers", "VehicleTypeClassId", "Tour.VehicleTypeClasses");
            DropIndex("Tour.TourScheduleCompanyTransfers", new[] { "VehicleTypeClassId" });
            DropColumn("Tour.TourScheduleCompanyTransfers", "VehicleTypeClassId");
        }
    }
}
