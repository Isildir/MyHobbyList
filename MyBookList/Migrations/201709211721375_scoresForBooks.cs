namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresForBooks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Books", "NumberOfVoters");
            DropColumn("dbo.Books", "AverageScore");

            AddColumn("dbo.Books", "AverageScore", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "NumberOfVoters", c => c.Long(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Books", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(maxLength: 255));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Books", "NumberOfVoters");
            DropColumn("dbo.Books", "AverageScore");
        }
    }
}
