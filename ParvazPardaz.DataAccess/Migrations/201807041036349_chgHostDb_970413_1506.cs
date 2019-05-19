namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970413_1506 : DbMigration
    {
        public override void Up()
        {
            DropIndex("Book.OrderInformations", new[] { "CityId" });
            DropIndex("Book.OrderInformations", new[] { "NationalityId" });
            AddColumn("Book.OrderInformations", "Email", c => c.String());
            AlterColumn("Book.OrderInformations", "CityId", c => c.Int());
            AlterColumn("Book.OrderInformations", "NationalityId", c => c.Int());
            CreateIndex("Book.OrderInformations", "CityId");
            CreateIndex("Book.OrderInformations", "NationalityId");
        }
        
        public override void Down()
        {
            DropIndex("Book.OrderInformations", new[] { "NationalityId" });
            DropIndex("Book.OrderInformations", new[] { "CityId" });
            AlterColumn("Book.OrderInformations", "NationalityId", c => c.Int(nullable: false));
            AlterColumn("Book.OrderInformations", "CityId", c => c.Int(nullable: false));
            DropColumn("Book.OrderInformations", "Email");
            CreateIndex("Book.OrderInformations", "NationalityId");
            CreateIndex("Book.OrderInformations", "CityId");
        }
    }
}