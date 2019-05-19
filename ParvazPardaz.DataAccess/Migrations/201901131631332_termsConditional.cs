namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class termsConditional : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AddTermsandConditionViewModels");
            DropTable("dbo.EditTermsandConditionViewModels");
            DropTable("dbo.GridTermsandConditionViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GridTermsandConditionViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        CreatorUserName = c.String(),
                        CreatorDateTime = c.DateTime(),
                        LastModifierUserName = c.String(),
                        LastModifierDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EditTermsandConditionViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AddTermsandConditionViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
