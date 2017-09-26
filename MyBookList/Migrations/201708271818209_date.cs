namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeriesGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Series", "SeriesGenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Series", "SeriesGenreId");
            AddForeignKey("dbo.Series", "SeriesGenreId", "dbo.SeriesGenres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "SeriesGenreId", "dbo.SeriesGenres");
            DropIndex("dbo.Series", new[] { "SeriesGenreId" });
            DropColumn("dbo.Series", "SeriesGenreId");
            DropTable("dbo.SeriesGenres");
        }
    }
}
