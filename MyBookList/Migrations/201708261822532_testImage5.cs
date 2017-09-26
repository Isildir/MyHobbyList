namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testImage5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "ImageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "ImageId");
            DropColumn("dbo.Movies", "ImageId");
            DropColumn("dbo.Games", "ImageId");
        }
    }
}
