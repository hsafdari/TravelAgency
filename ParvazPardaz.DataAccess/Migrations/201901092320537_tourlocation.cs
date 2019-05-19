namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Magazine.Locations", "ImageURL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("Magazine.Locations", "ImageURL");
        }
    }
}
