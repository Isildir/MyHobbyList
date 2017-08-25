namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulationgGenresList : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MovieGenres VALUES('Action')");
            Sql("INSERT INTO MovieGenres VALUES('Documentary')");
            Sql("INSERT INTO MovieGenres VALUES('Drama')");
            Sql("INSERT INTO MovieGenres VALUES('Thriller')");
            Sql("INSERT INTO MovieGenres VALUES('Comedy')");
            Sql("INSERT INTO MovieGenres VALUES('Historical')");
            Sql("INSERT INTO MovieGenres VALUES('Adventure')");
            Sql("INSERT INTO MovieGenres VALUES('Animated')");
            Sql("INSERT INTO MovieGenres VALUES('Western')");
            Sql("INSERT INTO MovieGenres VALUES('Scientific')");

            Sql("INSERT INTO BookGenres VALUES('Fantasy')");
            Sql("INSERT INTO BookGenres VALUES('Drama')");
            Sql("INSERT INTO BookGenres VALUES('Romance')");
            Sql("INSERT INTO BookGenres VALUES('Horror')");
            Sql("INSERT INTO BookGenres VALUES('Science')");
            Sql("INSERT INTO BookGenres VALUES('History')");
            Sql("INSERT INTO BookGenres VALUES('Comics')");
            Sql("INSERT INTO BookGenres VALUES('Science fiction')");

            Sql("INSERT INTO GameGenres VALUES('Real Time Strategy')");
            Sql("INSERT INTO GameGenres VALUES('First Person Shooter')");
            Sql("INSERT INTO GameGenres VALUES('Action')");
            Sql("INSERT INTO GameGenres VALUES('4X Game')");
            Sql("INSERT INTO GameGenres VALUES('Sport')");
            Sql("INSERT INTO GameGenres VALUES('Logic')");
            Sql("INSERT INTO GameGenres VALUES('Survival')");
            Sql("INSERT INTO GameGenres VALUES('Role-Playing')");
            Sql("INSERT INTO GameGenres VALUES('Board-Game')");
        }
        
        public override void Down()
        {
        }
    }
}
