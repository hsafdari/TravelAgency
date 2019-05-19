namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selectedFlight : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.SelectedFlights", "BaggageAmount", c => c.String());
            AddColumn("Book.SelectedFlights", "VehicleTypeClassId", c => c.Int());
            CreateIndex("Book.SelectedFlights", "VehicleTypeClassId");
            AddForeignKey("Book.SelectedFlights", "VehicleTypeClassId", "Tour.VehicleTypeClasses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Book.SelectedFlights", "VehicleTypeClassId", "Tour.VehicleTypeClasses");
            DropIndex("Book.SelectedFlights", new[] { "VehicleTypeClassId" });
            DropColumn("Book.SelectedFlights", "VehicleTypeClassId");
            DropColumn("Book.SelectedFlights", "BaggageAmount");
        }
    }
}
