namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngTblPassenger : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Passengers", "EnFirstName", c => c.String());
            AddColumn("Book.Passengers", "EnLastName", c => c.String());
            AddColumn("Book.Passengers", "PassportNo", c => c.String());
            AddColumn("Book.Passengers", "PassportExpirationDate", c => c.DateTime());
            AddColumn("Book.Passengers", "BirthCountryId", c => c.Int());
            AddColumn("Book.Passengers", "PassportExporterCountryId", c => c.Int());
            AlterColumn("Book.Passengers", "Nationality", c => c.Int(nullable: false));
            CreateIndex("Book.Passengers", "BirthCountryId");
            CreateIndex("Book.Passengers", "PassportExporterCountryId");
            AddForeignKey("Book.Passengers", "BirthCountryId", "Country.Countries", "Id");
            AddForeignKey("Book.Passengers", "PassportExporterCountryId", "Country.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Book.Passengers", "PassportExporterCountryId", "Country.Countries");
            DropForeignKey("Book.Passengers", "BirthCountryId", "Country.Countries");
            DropIndex("Book.Passengers", new[] { "PassportExporterCountryId" });
            DropIndex("Book.Passengers", new[] { "BirthCountryId" });
            AlterColumn("Book.Passengers", "Nationality", c => c.String(nullable: false));
            DropColumn("Book.Passengers", "PassportExporterCountryId");
            DropColumn("Book.Passengers", "BirthCountryId");
            DropColumn("Book.Passengers", "PassportExpirationDate");
            DropColumn("Book.Passengers", "PassportNo");
            DropColumn("Book.Passengers", "EnLastName");
            DropColumn("Book.Passengers", "EnFirstName");
        }
    }
}
