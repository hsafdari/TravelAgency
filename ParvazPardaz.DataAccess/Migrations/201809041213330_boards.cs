namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boards : DbMigration
    {
        public override void Up()
        {
            AddColumn("Hotel.HotelBoards", "ImageUrl", c => c.String(maxLength: 300));
            AddColumn("Hotel.HotelBoards", "ImageExtension", c => c.String(maxLength: 5));
            AddColumn("Hotel.HotelBoards", "ImageFileName", c => c.String(maxLength: 250));
            AddColumn("Hotel.HotelBoards", "ImageSize", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("Hotel.HotelBoards", "ImageSize");
            DropColumn("Hotel.HotelBoards", "ImageFileName");
            DropColumn("Hotel.HotelBoards", "ImageExtension");
            DropColumn("Hotel.HotelBoards", "ImageUrl");
        }
    }
}
