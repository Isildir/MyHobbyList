namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testImage3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullImage = c.Binary(),
                        ThumbnailImage = c.Binary(),
                        ImageMimeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "ImageId", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "Image");
            DropColumn("dbo.Books", "ThumbnailImage");
            DropColumn("dbo.Books", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "ImageMimeType", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "ThumbnailImage", c => c.Binary());
            AddColumn("dbo.Books", "Image", c => c.Binary());
            DropColumn("dbo.Books", "ImageId");
            DropTable("dbo.Images");
        }
    }
}
