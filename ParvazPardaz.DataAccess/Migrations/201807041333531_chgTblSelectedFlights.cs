namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgTblSelectedFlights : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.SelectedFlights", "CompanyTransferId", c => c.Int());
            CreateIndex("Book.SelectedFlights", "CompanyTransferId");
            AddForeignKey("Book.SelectedFlights", "CompanyTransferId", "Tour.CompanyTransfers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Book.SelectedFlights", "CompanyTransferId", "Tour.CompanyTransfers");
            DropIndex("Book.SelectedFlights", new[] { "CompanyTransferId" });
            DropColumn("Book.SelectedFlights", "CompanyTransferId");
        }
    }
}
