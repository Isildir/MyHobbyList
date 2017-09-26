namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresInItemModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PhotoURL", c => c.String());
            DropColumn("dbo.Series", "NumberOfVoters");
            DropColumn("dbo.Series", "AverageScore");
            DropColumn("dbo.Movies", "NumberOfVoters");
            DropColumn("dbo.Movies", "AverageScore");
            DropColumn("dbo.Games", "NumberOfVoters");
            DropColumn("dbo.Games", "AverageScore");
            AddColumn("dbo.Games", "AverageScore", c => c.Double(nullable: false));
            AddColumn("dbo.Games", "NumberOfVoters", c => c.Long(nullable: false));
            AddColumn("dbo.Movies", "AverageScore", c => c.Double(nullable: false));
            AddColumn("dbo.Movies", "NumberOfVoters", c => c.Long(nullable: false));
            AddColumn("dbo.Series", "AverageScore", c => c.Double(nullable: false));
            AddColumn("dbo.Series", "NumberOfVoters", c => c.Long(nullable: false));
            DropColumn("dbo.Books", "PhotoURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PhotoURL", c => c.String());
            DropColumn("dbo.Series", "NumberOfVoters");
            DropColumn("dbo.Series", "AverageScore");
            DropColumn("dbo.Movies", "NumberOfVoters");
            DropColumn("dbo.Movies", "AverageScore");
            DropColumn("dbo.Games", "NumberOfVoters");
            DropColumn("dbo.Games", "AverageScore");
        }
    }
}
