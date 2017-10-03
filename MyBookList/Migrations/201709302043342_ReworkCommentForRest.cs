namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkCommentForRest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookLikedBies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.GameLikedBies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameComments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.MovieLikedBies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieComments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateIndex("dbo.GameComments", "GameId");
            CreateIndex("dbo.MovieComments", "MovieId");
            CreateIndex("dbo.SeriesComments", "SeriesId");
            AddForeignKey("dbo.GameComments", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieComments", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeriesComments", "SeriesId", "dbo.Series", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LikedBies", "CommentId", "dbo.SeriesComments", "Id", cascadeDelete: true);
        } 
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedBies", "CommentId", "dbo.SeriesComments");
            DropForeignKey("dbo.SeriesComments", "SeriesId", "dbo.Series");
            DropForeignKey("dbo.MovieLikedBies", "CommentId", "dbo.MovieComments");
            DropForeignKey("dbo.MovieComments", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.GameLikedBies", "CommentId", "dbo.GameComments");
            DropForeignKey("dbo.GameComments", "GameId", "dbo.Games");
            DropIndex("dbo.SeriesComments", new[] { "SeriesId" });
            DropIndex("dbo.MovieLikedBies", new[] { "CommentId" });
            DropIndex("dbo.MovieComments", new[] { "MovieId" });
            DropIndex("dbo.GameLikedBies", new[] { "CommentId" });
            DropIndex("dbo.GameComments", new[] { "GameId" });
            DropIndex("dbo.BookLikedBies", new[] { "CommentId" });
            DropTable("dbo.MovieLikedBies");
            DropTable("dbo.GameLikedBies");
            DropTable("dbo.BookLikedBies");
        }
    }
}
