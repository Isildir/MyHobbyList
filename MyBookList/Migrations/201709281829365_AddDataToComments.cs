namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookComments", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookComments", "DateAdded");
        }
    }
}
