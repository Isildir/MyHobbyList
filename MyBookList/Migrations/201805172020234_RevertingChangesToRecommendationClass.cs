namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertingChangesToRecommendationClass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Recommends", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recommends", "Title", c => c.String());
        }
    }
}
