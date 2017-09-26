namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeriesGenrePopulating : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SeriesGenres VALUES('Action')");
            Sql("INSERT INTO SeriesGenres VALUES('Documentary')");
            Sql("INSERT INTO SeriesGenres VALUES('Drama')");
            Sql("INSERT INTO SeriesGenres VALUES('Thriller')");
            Sql("INSERT INTO SeriesGenres VALUES('Comedy')");
            Sql("INSERT INTO SeriesGenres VALUES('Historical')");
            Sql("INSERT INTO SeriesGenres VALUES('Adventure')");
            Sql("INSERT INTO SeriesGenres VALUES('Animated')");
            Sql("INSERT INTO SeriesGenres VALUES('Western')");
            Sql("INSERT INTO SeriesGenres VALUES('Scientific')");
        }
        
        public override void Down()
        {
        }
    }
}
