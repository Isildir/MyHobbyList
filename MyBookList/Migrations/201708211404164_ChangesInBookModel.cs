namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInBookModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "BookGenre_Id", newName: "BookGenreId");
            RenameIndex(table: "dbo.Books", name: "IX_BookGenre_Id", newName: "IX_BookGenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Books", name: "IX_BookGenreId", newName: "IX_BookGenre_Id");
            RenameColumn(table: "dbo.Books", name: "BookGenreId", newName: "BookGenre_Id");
        }
    }
}
