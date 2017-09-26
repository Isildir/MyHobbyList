namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresForBooks3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AverageScore", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AverageScore");
        }
    }
}
