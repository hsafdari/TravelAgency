namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblCompanyTransfer : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.CompanyTransfers", "TitleEn", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("Tour.CompanyTransfers", "TitleEn");
        }
    }
}
