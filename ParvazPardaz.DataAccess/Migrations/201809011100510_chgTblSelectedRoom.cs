namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblSelectedRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.SelectedRooms", "CurrentAdultCapacity", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "CurrentChildCapacity", c => c.Int(nullable: false));
            AddColumn("Book.SelectedRooms", "CurrentInfantCapacity", c => c.Int(nullable: false));
            DropColumn("Book.SelectedRooms", "CurrentCapacity");
        }
        
        public override void Down()
        {
            AddColumn("Book.SelectedRooms", "CurrentCapacity", c => c.Int(nullable: false));
            DropColumn("Book.SelectedRooms", "CurrentInfantCapacity");
            DropColumn("Book.SelectedRooms", "CurrentChildCapacity");
            DropColumn("Book.SelectedRooms", "CurrentAdultCapacity");
        }
    }
}
