namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingUserItemsLists : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBooksLists", "Reccomended", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserBooksLists", "RecommendingUserName", c => c.String());
            AddColumn("dbo.UserGamesLists", "Reccomended", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGamesLists", "RecommendingUserName", c => c.String());
            AddColumn("dbo.UserMoviesLists", "Reccomended", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserMoviesLists", "RecommendingUserName", c => c.String());
            AddColumn("dbo.UserSeriesLists", "Reccomended", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserSeriesLists", "RecommendingUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSeriesLists", "RecommendingUserName");
            DropColumn("dbo.UserSeriesLists", "Reccomended");
            DropColumn("dbo.UserMoviesLists", "RecommendingUserName");
            DropColumn("dbo.UserMoviesLists", "Reccomended");
            DropColumn("dbo.UserGamesLists", "RecommendingUserName");
            DropColumn("dbo.UserGamesLists", "Reccomended");
            DropColumn("dbo.UserBooksLists", "RecommendingUserName");
            DropColumn("dbo.UserBooksLists", "Reccomended");
        }
    }
}
