namespace ParvazPardaz.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgHostDb_970517_1312 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Post.Posts", "WriterId", c => c.Int());
            CreateIndex("Post.Posts", "WriterId");
            AddForeignKey("Post.Posts", "WriterId", "User.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Post.Posts", "WriterId", "User.Users");
            DropIndex("Post.Posts", new[] { "WriterId" });
            DropColumn("Post.Posts", "WriterId");
        }
    }
}
