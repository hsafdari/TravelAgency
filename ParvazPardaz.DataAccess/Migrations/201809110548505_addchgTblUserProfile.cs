namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchgTblUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("User.UserProfiles", "RemainingCreditValue", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("User.UserProfiles", "RemainingCreditValue");
        }
    }
}
