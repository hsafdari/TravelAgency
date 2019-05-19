namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tourslidercapacity1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Core.Sliders", "NavDescription", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("Core.Sliders", "NavDescription", c => c.String(maxLength: 50));
        }
    }
}
