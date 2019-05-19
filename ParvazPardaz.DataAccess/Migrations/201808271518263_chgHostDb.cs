namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Book.Coupons", "OrderId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("Book.Coupons", "OrderId", c => c.Int());
        }
    }
}
