namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblHotelRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("Hotel.HotelRooms", "AdultMaxCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "AdultMinCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "ChildMaxCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "ChildMinCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "InfantMaxCapacity", c => c.Int(nullable: false));
            AddColumn("Hotel.HotelRooms", "InfantMinCapacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Hotel.HotelRooms", "InfantMinCapacity");
            DropColumn("Hotel.HotelRooms", "InfantMaxCapacity");
            DropColumn("Hotel.HotelRooms", "ChildMinCapacity");
            DropColumn("Hotel.HotelRooms", "ChildMaxCapacity");
            DropColumn("Hotel.HotelRooms", "AdultMinCapacity");
            DropColumn("Hotel.HotelRooms", "AdultMaxCapacity");
        }
    }
}
