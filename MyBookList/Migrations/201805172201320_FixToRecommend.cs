namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixToRecommend : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recommends", "EntityId", "dbo.Entities");
            DropIndex("dbo.Recommends", new[] { "EntityId" });
            AlterColumn("dbo.Recommends", "EntityId", c => c.Int());
            CreateIndex("dbo.Recommends", "EntityId");
            AddForeignKey("dbo.Recommends", "EntityId", "dbo.Entities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommends", "EntityId", "dbo.Entities");
            DropIndex("dbo.Recommends", new[] { "EntityId" });
            AlterColumn("dbo.Recommends", "EntityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recommends", "EntityId");
            AddForeignKey("dbo.Recommends", "EntityId", "dbo.Entities", "Id", cascadeDelete: true);
        }
    }
}
