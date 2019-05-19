namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tourslidercapacity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Core.Sliders", "HeaderDays", c => c.String(maxLength: 20));
            AlterColumn("Core.Sliders", "NavDescription", c => c.String(maxLength: 50));
            AlterColumn("Core.Sliders", "footerLine1", c => c.String(maxLength: 50));
            AlterColumn("Core.Sliders", "footerLine2", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("Core.Sliders", "footerLine2", c => c.String(maxLength: 10));
            AlterColumn("Core.Sliders", "footerLine1", c => c.String(maxLength: 10));
            AlterColumn("Core.Sliders", "NavDescription", c => c.String(maxLength: 10));
            AlterColumn("Core.Sliders", "HeaderDays", c => c.String(maxLength: 10));
        }
    }
}
