namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedByNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookComment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookComments", t => t.BookComment_Id)
                .Index(t => t.BookComment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedByNames", "BookComment_Id", "dbo.BookComments");
            DropIndex("dbo.LikedByNames", new[] { "BookComment_Id" });
            DropTable("dbo.LikedByNames");
        }
    }
}
