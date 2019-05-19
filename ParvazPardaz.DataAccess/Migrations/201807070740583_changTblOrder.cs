namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changTblOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Orders", "ReturnFlightDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("Book.Orders", "ReturnFlightDateTime");
        }
    }
}
