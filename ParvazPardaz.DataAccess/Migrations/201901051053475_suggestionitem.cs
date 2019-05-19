namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suggestionitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("Magazine.TourSuggestion", "Priority", c => c.Int(nullable: false));
            AddColumn("Magazine.TourSuggestion", "ImageIsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Magazine.TourSuggestion", "ImageIsActive");
            DropColumn("Magazine.TourSuggestion", "Priority");
        }
    }
}
