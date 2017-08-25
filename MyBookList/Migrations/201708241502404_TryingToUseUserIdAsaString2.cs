namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingToUseUserIdAsaString2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMoviesLists", "MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserGamesLists", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.UserMoviesLists", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.UserSeriesLists", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.UserMoviesLists", "UserGamesList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserMoviesLists", "UserGamesList", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSeriesLists", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserMoviesLists", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserGamesLists", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.UserMoviesLists", "MovieId");
        }
    }
}
