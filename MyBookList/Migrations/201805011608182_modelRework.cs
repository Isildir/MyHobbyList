namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelRework : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "UserDatas");
            RenameColumn(table: "dbo.Entities", name: "User_Id", newName: "UserData_Id");
            RenameIndex(table: "dbo.Entities", name: "IX_User_Id", newName: "IX_UserData_Id");
            AddColumn("dbo.Entities", "DateAdded", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Entities", "CreateById", c => c.String());
            CreateIndex("dbo.Entities", "Title", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Entities", new[] { "Title" });
            AlterColumn("dbo.Entities", "CreateById", c => c.Int(nullable: false));
            DropColumn("dbo.Entities", "DateAdded");
            RenameIndex(table: "dbo.Entities", name: "IX_UserData_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Entities", name: "UserData_Id", newName: "User_Id");
            RenameTable(name: "dbo.UserDatas", newName: "Users");
        }
    }
}
