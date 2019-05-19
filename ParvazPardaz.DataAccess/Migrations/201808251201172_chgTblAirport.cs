namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblAirport : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.Airports", "TitleEn", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Tour.Airports", "TitleEn");
        }
    }
}
