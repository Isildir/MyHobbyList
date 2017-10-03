namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookLikedBies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookComments", t => t.BookCommentId, cascadeDelete: true)
                .Index(t => t.BookCommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookLikedBies", "BookCommentId", "dbo.BookComments");
            DropIndex("dbo.BookLikedBies", new[] { "BookCommentId" });
            DropTable("dbo.BookLikedBies");
        }
    }
}
