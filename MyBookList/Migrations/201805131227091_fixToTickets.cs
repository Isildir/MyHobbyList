namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixToTickets : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "UserId", c => c.String());
            DropColumn("dbo.Tickets", "TargetUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TargetUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "UserId", c => c.Int(nullable: false));
        }
    }
}
