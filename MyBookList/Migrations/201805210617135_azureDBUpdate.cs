namespace MyHobbyList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azureDBUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ReleaseDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ElementType = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        Description = c.String(maxLength: 800),
                        CreateById = c.String(),
                        ImageId = c.Int(nullable: false),
                        AverageScore = c.Double(nullable: false),
                        NumberOfVoters = c.Long(nullable: false),
                        Author = c.String(maxLength: 100),
                        Studio = c.String(maxLength: 100),
                        Director = c.String(maxLength: 100),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        UserData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.UserDatas", t => t.UserData_Id)
                .Index(t => t.Title, unique: true)
                .Index(t => t.GenreId)
                .Index(t => t.UserData_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        CommentData = c.String(),
                        UserLogin = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        ElementType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entities", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElementType = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileType = c.Int(nullable: false),
                        Content = c.Binary(),
                        AdditionalData = c.Binary(),
                        ImageMimeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTitle = c.String(nullable: false),
                        TicketBody = c.String(nullable: false),
                        TimeSend = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(),
                        AccountType = c.Int(nullable: false),
                        AccountState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recommends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        FromUserEmail = c.String(),
                        UserId = c.Int(nullable: false),
                        EntityId = c.Int(),
                        ElementType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entities", t => t.EntityId)
                .ForeignKey("dbo.UserDatas", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElementType = c.Int(nullable: false),
                        Value = c.Short(nullable: false),
                        EntityId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entities", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("dbo.UserDatas", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Scores", "UserId", "dbo.UserDatas");
            DropForeignKey("dbo.Scores", "EntityId", "dbo.Entities");
            DropForeignKey("dbo.Recommends", "UserId", "dbo.UserDatas");
            DropForeignKey("dbo.Recommends", "EntityId", "dbo.Entities");
            DropForeignKey("dbo.Entities", "UserData_Id", "dbo.UserDatas");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Entities", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Comments", "EntityId", "dbo.Entities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Scores", new[] { "UserId" });
            DropIndex("dbo.Scores", new[] { "EntityId" });
            DropIndex("dbo.Recommends", new[] { "EntityId" });
            DropIndex("dbo.Recommends", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Comments", new[] { "EntityId" });
            DropIndex("dbo.Entities", new[] { "UserData_Id" });
            DropIndex("dbo.Entities", new[] { "GenreId" });
            DropIndex("dbo.Entities", new[] { "Title" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Scores");
            DropTable("dbo.Recommends");
            DropTable("dbo.UserDatas");
            DropTable("dbo.Tickets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Files");
            DropTable("dbo.Genres");
            DropTable("dbo.Comments");
            DropTable("dbo.Entities");
        }
    }
}
