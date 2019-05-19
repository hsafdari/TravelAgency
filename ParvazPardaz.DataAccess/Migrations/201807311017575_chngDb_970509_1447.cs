namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngDb_970509_1447 : DbMigration
    {
        public override void Up()
        {
            DropColumn("Book.SelectedRooms", "ReserveCapacity");
        }
        
        public override void Down()
        {
            AddColumn("Book.SelectedRooms", "ReserveCapacity", c => c.Int(nullable: false));
        }
    }
}
