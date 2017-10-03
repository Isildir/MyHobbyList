namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCommentsForNow : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookComments", "NumOfComments");
            DropTable("dbo.BookLikeUserNames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookLikeUserNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BookComments", "NumOfComments", c => c.Int(nullable: false));
        }
    }
}
