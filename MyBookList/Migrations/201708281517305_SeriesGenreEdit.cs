namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeriesGenreEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeriesGenres", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeriesGenres", "Name", c => c.String());
        }
    }
}
