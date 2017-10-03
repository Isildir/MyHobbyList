namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkBookComments : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookLikedBies", newName: "LikedBies");
            RenameColumn(table: "dbo.LikedBies", name: "BookCommentId", newName: "CommentId");
            RenameIndex(table: "dbo.LikedBies", name: "IX_BookCommentId", newName: "IX_CommentId");
            CreateIndex("dbo.BookComments", "BookId");
            AddForeignKey("dbo.BookComments", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookComments", "BookId", "dbo.Books");
            DropIndex("dbo.BookComments", new[] { "BookId" });
            RenameIndex(table: "dbo.LikedBies", name: "IX_CommentId", newName: "IX_BookCommentId");
            RenameColumn(table: "dbo.LikedBies", name: "CommentId", newName: "BookCommentId");
            RenameTable(name: "dbo.LikedBies", newName: "BookLikedBies");
        }
    }
}
