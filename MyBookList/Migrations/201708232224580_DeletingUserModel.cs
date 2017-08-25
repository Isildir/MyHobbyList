namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "UserData_Id", "dbo.UserDatas");
            DropForeignKey("dbo.Games", "UserData_Id", "dbo.UserDatas");
            DropForeignKey("dbo.Movies", "UserData_Id", "dbo.UserDatas");
            DropForeignKey("dbo.Series", "UserData_Id", "dbo.UserDatas");
            DropIndex("dbo.Books", new[] { "UserData_Id" });
            DropIndex("dbo.Games", new[] { "UserData_Id" });
            DropIndex("dbo.Movies", new[] { "UserData_Id" });
            DropIndex("dbo.Series", new[] { "UserData_Id" });
            CreateTable(
                "dbo.UserBooksLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Books", "UserData_Id");
            DropColumn("dbo.Games", "UserData_Id");
            DropColumn("dbo.Movies", "UserData_Id");
            DropColumn("dbo.Series", "UserData_Id");
            DropTable("dbo.UserDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Series", "UserData_Id", c => c.Int());
            AddColumn("dbo.Movies", "UserData_Id", c => c.Int());
            AddColumn("dbo.Games", "UserData_Id", c => c.Int());
            AddColumn("dbo.Books", "UserData_Id", c => c.Int());
            DropTable("dbo.UserBooksLists");
            CreateIndex("dbo.Series", "UserData_Id");
            CreateIndex("dbo.Movies", "UserData_Id");
            CreateIndex("dbo.Games", "UserData_Id");
            CreateIndex("dbo.Books", "UserData_Id");
            AddForeignKey("dbo.Series", "UserData_Id", "dbo.UserDatas", "Id");
            AddForeignKey("dbo.Movies", "UserData_Id", "dbo.UserDatas", "Id");
            AddForeignKey("dbo.Games", "UserData_Id", "dbo.UserDatas", "Id");
            AddForeignKey("dbo.Books", "UserData_Id", "dbo.UserDatas", "Id");
        }
    }
}
