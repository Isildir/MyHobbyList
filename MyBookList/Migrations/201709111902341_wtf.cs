namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PhotoURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "PhotoURL");
        }
    }
}
