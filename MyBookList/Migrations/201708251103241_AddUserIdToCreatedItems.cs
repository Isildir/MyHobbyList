namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToCreatedItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AddedByUserId", c => c.String());
            AddColumn("dbo.Games", "AddedByUserId", c => c.String());
            AddColumn("dbo.Movies", "AddyeByUserId", c => c.String());
            AddColumn("dbo.Series", "AddedByUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "AddedByUserId");
            DropColumn("dbo.Movies", "AddyeByUserId");
            DropColumn("dbo.Games", "AddedByUserId");
            DropColumn("dbo.Books", "AddedByUserId");
        }
    }
}
