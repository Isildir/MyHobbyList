namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingBookModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
