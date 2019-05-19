namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selectedFlight : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.SelectedFlights", "BaggageAmount", c => c.String(maxLength: 20));
            AddColumn("Book.SelectedFlights", "VehicleTypeClassId", c => c.Int());
            AlterColumn("Book.SelectedFlights", "AirlineIATACode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("Book.SelectedFlights", "FlightNo", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("Book.SelectedFlights", "FromIATACode", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("Book.SelectedFlights", "ToIATACode", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("Tour.TourScheduleCompanyTransfers", "Description", c => c.String(maxLength: 400));
            AlterColumn("Tour.TourScheduleCompanyTransfers", "FlightNumber", c => c.String(maxLength: 5));
            AlterColumn("Tour.TourScheduleCompanyTransfers", "BaggageAmount", c => c.String(maxLength: 20));
            CreateIndex("Book.SelectedFlights", "VehicleTypeClassId");
            AddForeignKey("Book.SelectedFlights", "VehicleTypeClassId", "Tour.VehicleTypeClasses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Book.SelectedFlights", "VehicleTypeClassId", "Tour.VehicleTypeClasses");
            DropIndex("Book.SelectedFlights", new[] { "VehicleTypeClassId" });
            AlterColumn("Tour.TourScheduleCompanyTransfers", "BaggageAmount", c => c.String());
            AlterColumn("Tour.TourScheduleCompanyTransfers", "FlightNumber", c => c.String());
            AlterColumn("Tour.TourScheduleCompanyTransfers", "Description", c => c.String());
            AlterColumn("Book.SelectedFlights", "ToIATACode", c => c.String(nullable: false));
            AlterColumn("Book.SelectedFlights", "FromIATACode", c => c.String(nullable: false));
            AlterColumn("Book.SelectedFlights", "FlightNo", c => c.String(nullable: false));
            AlterColumn("Book.SelectedFlights", "AirlineIATACode", c => c.String(nullable: false));
            DropColumn("Book.SelectedFlights", "VehicleTypeClassId");
            DropColumn("Book.SelectedFlights", "BaggageAmount");
        }
    }
}
