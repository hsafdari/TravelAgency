namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTblHotelRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("Hotel.HotelRooms", "HasAdult", c => c.Boolean(nullable: false));
            AddColumn("Hotel.HotelRooms", "HasChild", c => c.Boolean(nullable: false));
            AddColumn("Hotel.HotelRooms", "HasInfant", c => c.Boolean(nullable: false));
            DropColumn("Hotel.HotelRooms", "AdultCapacity");
            DropColumn("Hotel.HotelRooms", "ChildCapacity");
            DropColumn("Hotel.HotelRooms", "InfantCapacity");
        }
        
        public override void Down()
        {
            AddColumn("Hotel.HotelRooms", "InfantCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "ChildCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "AdultCapacity", c => c.Int(nullable: false));
            DropColumn("Hotel.HotelRooms", "HasInfant");
            DropColumn("Hotel.HotelRooms", "HasChild");
            DropColumn("Hotel.HotelRooms", "HasAdult");
        }
    }
}
