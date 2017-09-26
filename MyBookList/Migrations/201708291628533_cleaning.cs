namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleaning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "AddedByUserId", c => c.String());
            DropColumn("dbo.Movies", "AddyeByUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "AddyeByUserId", c => c.String());
            DropColumn("dbo.Movies", "AddedByUserId");
        }
    }
}
