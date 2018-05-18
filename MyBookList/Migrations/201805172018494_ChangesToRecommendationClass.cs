namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToRecommendationClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recommends", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recommends", "Title");
        }
    }
}
