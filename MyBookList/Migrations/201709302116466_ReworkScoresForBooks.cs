namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkScoresForBooks : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BookScoreLists", "BookId");
            AddForeignKey("dbo.BookScoreLists", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookScoreLists", "BookId", "dbo.Books");
            DropIndex("dbo.BookScoreLists", new[] { "BookId" });
        }
    }
}
