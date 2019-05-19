namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblFooter : DbMigration
    {
        public override void Up()
        {
            AddColumn("Core.Footers", "FooterType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Core.Footers", "FooterType");
        }
    }
}
