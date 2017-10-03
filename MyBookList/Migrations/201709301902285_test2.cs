namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedByNames", "BookComment_Id", "dbo.BookComments");
            DropIndex("dbo.LikedByNames", new[] { "BookComment_Id" });
            DropTable("dbo.LikedByNames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikedByNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookComment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.LikedByNames", "BookComment_Id");
            AddForeignKey("dbo.LikedByNames", "BookComment_Id", "dbo.BookComments", "Id");
        }
    }
}
