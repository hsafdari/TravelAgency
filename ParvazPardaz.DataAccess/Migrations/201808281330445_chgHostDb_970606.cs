namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970606 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Book.VoucherReceivers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Book.VoucherReceivers", "FullName", c => c.String(nullable: false));
        }
    }
}
