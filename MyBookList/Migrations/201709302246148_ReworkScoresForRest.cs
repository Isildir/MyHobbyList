namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkScoresForRest : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GameScoreLists", "GameId");
            CreateIndex("dbo.MovieScoreLists", "MovieId");
            CreateIndex("dbo.SeriesScoreLists", "SeriesId");
            AddForeignKey("dbo.GameScoreLists", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieScoreLists", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeriesScoreLists", "SeriesId", "dbo.Series", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeriesScoreLists", "SeriesId", "dbo.Series");
            DropForeignKey("dbo.MovieScoreLists", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.GameScoreLists", "GameId", "dbo.Games");
            DropIndex("dbo.SeriesScoreLists", new[] { "SeriesId" });
            DropIndex("dbo.MovieScoreLists", new[] { "MovieId" });
            DropIndex("dbo.GameScoreLists", new[] { "GameId" });
        }
    }
}
