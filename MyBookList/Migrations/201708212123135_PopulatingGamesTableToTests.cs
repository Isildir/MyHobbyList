namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingGamesTableToTests : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Games VALUES('Europa Universalis 4','20121212','Best grand strategy','Paradox Interactive',1)");
            Sql("INSERT INTO Games VALUES('Call of Duty','20041212','World War shooter','No idea',4)");
            Sql("INSERT INTO Games VALUES('Civilization 4','20151212','4X strategy','No idea 2',7)");
        }
        
        public override void Down()
        {
        }
    }
}
