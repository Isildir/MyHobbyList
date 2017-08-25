namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingToUseUserIdAsaString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserBooksLists", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserBooksLists", "UserId", c => c.Int(nullable: false));
        }
    }
}
