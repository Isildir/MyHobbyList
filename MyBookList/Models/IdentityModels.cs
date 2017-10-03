using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyHobbyList.Models.Books;
using MyHobbyList.Models.Games;
using MyHobbyList.Models.Movies;

namespace MyBookList.Models
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


        //public List<int> BooksIdsList { get; set; }
       
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<SeriesGenre> SeriesGenres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ticket> AdminTickets { get; set; }
        public DbSet<BannedUser> BannedUsers { get; set; }

        public DbSet<UserBooksList> UserBooksLists { get; set; }
        public DbSet<UserMoviesList> UserMoviesLists { get; set; }
        public DbSet<UserGamesList> UserGamesLists { get; set; }
        public DbSet<UserSeriesList> UserSeriesLists { get; set; }
        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}