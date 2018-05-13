namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reccomends", newName: "Recommends");
            AddColumn("dbo.Recommends", "FromUserEmail", c => c.String());
            AddColumn("dbo.Recommends", "ElementType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recommends", "ElementType");
            DropColumn("dbo.Recommends", "FromUserEmail");
            RenameTable(name: "dbo.Recommends", newName: "Reccomends");
        }
    }
}
