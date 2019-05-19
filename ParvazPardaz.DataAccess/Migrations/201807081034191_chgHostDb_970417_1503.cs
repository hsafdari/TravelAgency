namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970417_1503 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Orders", "ReturnFlightDateTime", c => c.DateTime());
            AddColumn("Book.Passengers", "Birthdate", c => c.DateTime());
            AddColumn("Book.Passengers", "EnFirstName", c => c.String());
            AddColumn("Book.Passengers", "EnLastName", c => c.String());
            AddColumn("Book.Passengers", "PassportNo", c => c.String());
            AddColumn("Book.Passengers", "PassportExpirationDate", c => c.DateTime());
            AddColumn("Book.Passengers", "NationalityTitle", c => c.Int(nullable: false));
            AddColumn("Book.Passengers", "BirthCountryId", c => c.Int());
            AddColumn("Book.Passengers", "PassportExporterCountryId", c => c.Int());
            AddColumn("Book.SelectedFlights", "CompanyTransferId", c => c.Int());
            AddColumn("Hotel.HotelRooms", "HasAdult", c => c.Boolean(nullable: false));
            AddColumn("Hotel.HotelRooms", "HasChild", c => c.Boolean(nullable: false));
            AddColumn("Hotel.HotelRooms", "HasInfant", c => c.Boolean(nullable: false));
            CreateIndex("Book.Passengers", "BirthCountryId");
            CreateIndex("Book.Passengers", "PassportExporterCountryId");
            CreateIndex("Book.SelectedFlights", "CompanyTransferId");
            AddForeignKey("Book.Passengers", "BirthCountryId", "Country.Countries", "Id");
            AddForeignKey("Book.Passengers", "PassportExporterCountryId", "Country.Countries", "Id");
            AddForeignKey("Book.SelectedFlights", "CompanyTransferId", "Tour.CompanyTransfers", "Id");
            DropColumn("Book.Passengers", "Nationality");
            DropColumn("Hotel.HotelRooms", "AdultCapacity");
            DropColumn("Hotel.HotelRooms", "ChildCapacity");
            DropColumn("Hotel.HotelRooms", "InfantCapacity");
        }
        
        public override void Down()
        {
            AddColumn("Hotel.HotelRooms", "InfantCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "ChildCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "AdultCapacity", c => c.Int(nullable: false));
            AddColumn("Book.Passengers", "Nationality", c => c.String(nullable: false));
            DropForeignKey("Book.SelectedFlights", "CompanyTransferId", "Tour.CompanyTransfers");
            DropForeignKey("Book.Passengers", "PassportExporterCountryId", "Country.Countries");
            DropForeignKey("Book.Passengers", "BirthCountryId", "Country.Countries");
            DropIndex("Book.SelectedFlights", new[] { "CompanyTransferId" });
            DropIndex("Book.Passengers", new[] { "PassportExporterCountryId" });
            DropIndex("Book.Passengers", new[] { "BirthCountryId" });
            DropColumn("Hotel.HotelRooms", "HasInfant");
            DropColumn("Hotel.HotelRooms", "HasChild");
            DropColumn("Hotel.HotelRooms", "HasAdult");
            DropColumn("Book.SelectedFlights", "CompanyTransferId");
            DropColumn("Book.Passengers", "PassportExporterCountryId");
            DropColumn("Book.Passengers", "BirthCountryId");
            DropColumn("Book.Passengers", "NationalityTitle");
            DropColumn("Book.Passengers", "PassportExpirationDate");
            DropColumn("Book.Passengers", "PassportNo");
            DropColumn("Book.Passengers", "EnLastName");
            DropColumn("Book.Passengers", "EnFirstName");
            DropColumn("Book.Passengers", "Birthdate");
            DropColumn("Book.Orders", "ReturnFlightDateTime");
        }
    }
}
