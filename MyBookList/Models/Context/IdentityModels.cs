using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MyHobbyList.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("UserBooks", this.BooksIdsList.ToString()));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<UserData> UserDatas { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration() : base()
        {
            string path;
            try
            {
                LocalResource myConfigsStorage = RoleEnvironment.GetLocalResource("EFCache");
                path = Path.GetDirectoryName(myConfigsStorage.RootPath + GetType());
            }
            catch(Exception e)
            {
                path = Path.GetDirectoryName(GetType().Assembly.Location);
            }
            SetModelStore(new DefaultDbModelStore(path));
        }
    }
}