namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresForBooks2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BookScoreLists");

            CreateTable(
                "dbo.BookScoreLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BookId = c.Int(nullable: false),
                        Score = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Books", "AverageScore");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecommendedByUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromId = c.Int(nullable: false),
                        TargetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "AverageScore", c => c.Int(nullable: false));
            DropTable("dbo.BookScoreLists");
        }
    }
}
