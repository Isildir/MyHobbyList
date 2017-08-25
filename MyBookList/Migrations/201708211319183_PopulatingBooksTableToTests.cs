namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingBooksTableToTests : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Books VALUES('Malazan Books Of Fallen 1','Steven Erickson','19951122','this is description',1)");
            Sql("INSERT INTO Books VALUES('Malazan Books Of Fallen 2','Steven Erickson','19961223','this is description 2',1)");
            Sql("INSERT INTO Books VALUES('Malazan Books Of Fallen 3','Steven Erickson','19970625','this is description 3',1)");
        }
        
        public override void Down()
        {
        }
    }
}
