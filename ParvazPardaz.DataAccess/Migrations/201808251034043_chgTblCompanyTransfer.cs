namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblCompanyTransfer : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tour.CompanyTransfers", "IataCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Tour.CompanyTransfers", "IataCode");
        }
    }
}
