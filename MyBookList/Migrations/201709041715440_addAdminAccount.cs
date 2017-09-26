namespace MyBookList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminAccount : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0ee4575d-e84c-4e38-9454-4510e68da5da', N'grzanka357@gmail.com', 0, N'AKA+SpAc34baFiNer9ffm5xMzMHCAnJwvr2O4KZK4k6h0EYtX7JU0hs2Zhbcz2vnRQ==', N'3eb8d964-7ca8-431c-bbed-64230281e03d', NULL, 0, 0, NULL, 1, 0, N'grzanka357@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'271326e5-56d9-45b0-b9be-3c3b7396abe0', N'Kami@op.com', 0, N'ADXV8ThVPssW4cWRb42ZvPQrKM/5cknTrvytdS2zUZP2qh9RMLsp4xfM+IPg6Hau7w==', N'69ef45ff-ff3e-401a-af16-a619d4241a3b', NULL, 0, 0, NULL, 1, 0, N'Kami@op.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'63a5d52b-d787-4c84-8a29-3a9c43f3ca67', N'Admin')


INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0ee4575d-e84c-4e38-9454-4510e68da5da', N'63a5d52b-d787-4c84-8a29-3a9c43f3ca67')


");
        }
        
        public override void Down()
        {
        }
    }
}
