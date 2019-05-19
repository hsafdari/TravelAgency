namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblCharteranDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("Hotel.HotelPackageHotelRooms", "AdultPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "ChildPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "InfantPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "AdultOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "ChildOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "InfantOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "AdultCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "ChildCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "InfantCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "AdultCapacitySold", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "ChildCapacitySold", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "InfantCapacitySold", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "CurrentAdultCapacity", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "CurrentChildCapacity", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "CurrentInfantCapacity", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "AdultCurrencyPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "ChildCurrencyPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "InfantCurrencyPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "AdultUnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "ChildUnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "InfantUnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedHotelPackages", "TotalAdultPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedHotelPackages", "TotalChildPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedHotelPackages", "TotalInfantPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("Hotel.HotelPackageHotelRooms", "Price");
            DropColumn("Hotel.HotelPackageHotelRooms", "OtherCurrencyPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "Capacity");
            DropColumn("Hotel.HotelPackageHotelRooms", "CapacitySold");
            DropColumn("Book.SelectedRooms", "CurrentCapacity");
            DropColumn("Book.SelectedRooms", "CurrencyPrice");
            DropColumn("Book.SelectedRooms", "UnitPrice");
            DropColumn("Book.SelectedHotelPackages", "TotalServicePrice");
        }
        
        public override void Down()
        {
            AddColumn("Book.SelectedHotelPackages", "TotalServicePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "CurrencyPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.SelectedRooms", "CurrentCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "CapacitySold", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "Capacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelPackageHotelRooms", "OtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("Hotel.HotelPackageHotelRooms", "Price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            DropColumn("Book.SelectedHotelPackages", "TotalInfantPrice");
            DropColumn("Book.SelectedHotelPackages", "TotalChildPrice");
            DropColumn("Book.SelectedHotelPackages", "TotalAdultPrice");
            DropColumn("Book.SelectedRooms", "InfantUnitPrice");
            DropColumn("Book.SelectedRooms", "ChildUnitPrice");
            DropColumn("Book.SelectedRooms", "AdultUnitPrice");
            DropColumn("Book.SelectedRooms", "InfantCurrencyPrice");
            DropColumn("Book.SelectedRooms", "ChildCurrencyPrice");
            DropColumn("Book.SelectedRooms", "AdultCurrencyPrice");
            DropColumn("Book.SelectedRooms", "CurrentInfantCapacity");
            DropColumn("Book.SelectedRooms", "CurrentChildCapacity");
            DropColumn("Book.SelectedRooms", "CurrentAdultCapacity");
            DropColumn("Hotel.HotelPackageHotelRooms", "InfantCapacitySold");
            DropColumn("Hotel.HotelPackageHotelRooms", "ChildCapacitySold");
            DropColumn("Hotel.HotelPackageHotelRooms", "AdultCapacitySold");
            DropColumn("Hotel.HotelPackageHotelRooms", "InfantCapacity");
            DropColumn("Hotel.HotelPackageHotelRooms", "ChildCapacity");
            DropColumn("Hotel.HotelPackageHotelRooms", "AdultCapacity");
            DropColumn("Hotel.HotelPackageHotelRooms", "InfantOtherCurrencyPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "ChildOtherCurrencyPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "AdultOtherCurrencyPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "InfantPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "ChildPrice");
            DropColumn("Hotel.HotelPackageHotelRooms", "AdultPrice");
        }
    }
}
