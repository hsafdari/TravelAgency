namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblOrderInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.OrderInformations", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Book.OrderInformations", "Email");
        }
    }
}
