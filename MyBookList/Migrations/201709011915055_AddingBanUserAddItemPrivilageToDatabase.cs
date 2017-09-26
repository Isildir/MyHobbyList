namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBanUserAddItemPrivilageToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannedUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BannedUsers");
        }
    }
}
