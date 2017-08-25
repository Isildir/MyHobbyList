namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingOthersModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Series", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Series", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
