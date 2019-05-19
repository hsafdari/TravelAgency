namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelroomprice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Hotel.HotelPackageHotelRooms", "AdultPrice", c => c.Decimal(nullable: false, precision: 13, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "ChildPrice", c => c.Decimal(nullable: false, precision: 13, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "InfantPrice", c => c.Decimal(nullable: false, precision: 13, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "AdultOtherCurrencyPrice", c => c.Decimal(precision: 13, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "ChildOtherCurrencyPrice", c => c.Decimal(precision: 13, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "InfantOtherCurrencyPrice", c => c.Decimal(precision: 13, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("Hotel.HotelPackageHotelRooms", "InfantOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "ChildOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "AdultOtherCurrencyPrice", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "InfantPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "ChildPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("Hotel.HotelPackageHotelRooms", "AdultPrice", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
