namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeHost_970515_1538 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Book.Orders", "BaseCommission", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.Orders", "CommissionPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Book.Orders", "ProfileType", c => c.Int(nullable: false));
            AddColumn("Book.Coupons", "DiscountPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("Book.Coupons", "ValidatedUserId");
            AddForeignKey("Book.Coupons", "ValidatedUserId", "User.Users", "Id");
            DropColumn("Book.Coupons", "DiscountPrice");
        }
        
        public override void Down()
        {
            AddColumn("Book.Coupons", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("Book.Coupons", "ValidatedUserId", "User.Users");
            DropIndex("Book.Coupons", new[] { "ValidatedUserId" });
            DropColumn("Book.Coupons", "DiscountPercent");
            DropColumn("Book.Orders", "ProfileType");
            DropColumn("Book.Orders", "CommissionPrice");
            DropColumn("Book.Orders", "BaseCommission");
        }
    }
}
