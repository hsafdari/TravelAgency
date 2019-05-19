namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970409_1557 : DbMigration
    {
        public override void Up()
        {
            DropIndex("Book.OrderInformations", new[] { "Order_Id" });
            DropColumn("Book.OrderInformations", "Id");
            RenameColumn(table: "Book.OrderInformations", name: "Order_Id", newName: "Id");
            DropPrimaryKey("Book.OrderInformations");
            AddColumn("Book.OrderInformations", "NationalCode", c => c.String(maxLength: 10));
            AddColumn("Book.OrderInformations", "NationalityId", c => c.Int(nullable: false));
            AlterColumn("Book.OrderInformations", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("Book.OrderInformations", "Id");
            CreateIndex("Book.OrderInformations", "Id");
            CreateIndex("Book.OrderInformations", "NationalityId");
            AddForeignKey("Book.OrderInformations", "NationalityId", "Country.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Book.OrderInformations", "NationalityId", "Country.Countries");
            DropIndex("Book.OrderInformations", new[] { "NationalityId" });
            DropIndex("Book.OrderInformations", new[] { "Id" });
            DropPrimaryKey("Book.OrderInformations");
            AlterColumn("Book.OrderInformations", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("Book.OrderInformations", "NationalityId");
            DropColumn("Book.OrderInformations", "NationalCode");
            AddPrimaryKey("Book.OrderInformations", "Id");
            RenameColumn(table: "Book.OrderInformations", name: "Id", newName: "Order_Id");
            AddColumn("Book.OrderInformations", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("Book.OrderInformations", "Order_Id");
        }
    }
}
