namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingTicketVariables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketBody = c.String(nullable: false),
                        SendingUserName = c.String(nullable: false),
                        TimeSend = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TicketTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.BannedUsers", "UserId", c => c.String(nullable: false));
            DropTable("dbo.AdminDatas");
        }
        
        public override void Down()
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
            
            AlterColumn("dbo.BannedUsers", "UserId", c => c.String());
            DropTable("dbo.Tickets");
        }
    }
}
