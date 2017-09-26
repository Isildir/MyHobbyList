namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Image", c => c.Binary());
            AddColumn("dbo.Books", "ThumbnailImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ThumbnailImage");
            DropColumn("dbo.Books", "Image");
        }
    }
}
