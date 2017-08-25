namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingOthersModel2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Games", name: "GameGenre_Id", newName: "GameGenreId");
            RenameColumn(table: "dbo.Movies", name: "MovieGenre_Id", newName: "MovieGenreId");
            RenameIndex(table: "dbo.Games", name: "IX_GameGenre_Id", newName: "IX_GameGenreId");
            RenameIndex(table: "dbo.Movies", name: "IX_MovieGenre_Id", newName: "IX_MovieGenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Movies", name: "IX_MovieGenreId", newName: "IX_MovieGenre_Id");
            RenameIndex(table: "dbo.Games", name: "IX_GameGenreId", newName: "IX_GameGenre_Id");
            RenameColumn(table: "dbo.Movies", name: "MovieGenreId", newName: "MovieGenre_Id");
            RenameColumn(table: "dbo.Games", name: "GameGenreId", newName: "GameGenre_Id");
        }
    }
}
