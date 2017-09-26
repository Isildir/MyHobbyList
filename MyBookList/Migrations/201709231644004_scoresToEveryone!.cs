namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresToEveryone : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SeriesScoreLists");
            DropTable("dbo.MovieScoreLists");
            DropTable("dbo.GameScoreLists");
            CreateTable(
                "dbo.GameScoreLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        GameId = c.Int(nullable: false),
                        Score = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieScoreLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        MovieId = c.Int(nullable: false),
                        Score = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeriesScoreLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        SeriesId = c.Int(nullable: false),
                        Score = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SeriesScoreLists");
            DropTable("dbo.MovieScoreLists");
            DropTable("dbo.GameScoreLists");
        }
    }
}
