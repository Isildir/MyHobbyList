namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTicketFeautureToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ticket = c.String(nullable: false),
                        SendingUserName = c.String(nullable: false),
                        TimeSend = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Resolved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminDatas");
        }
    }
}
