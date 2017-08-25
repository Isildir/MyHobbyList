using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;

namespace MyBookList.Controllers
{
    public class UserProfileController : Controller
    {
        private ApplicationDbContext _context;

        public UserProfileController()
        {
            _context = new ApplicationDbContext();
        }

        public List<Book> GetUserBooks(string userId)
        {
            var books = _context.UserBooksLists.Where(m => m.UserId == userId).ToList();

            var booksIds = books.Select(book => book.BookId).ToList();

            return booksIds.Select(bookId => _context.Books.Single(m => m.Id == bookId)).ToList();
        }

        public List<Movie> GetUserMovies(string userId)
        {
            var movies = _context.UserMoviesLists.Where(m => m.UserId == userId).ToList();

            var moviesIds = movies.Select(movie => movie.MovieId).ToList();

            return moviesIds.Select(movieId => _context.Movies.Single(m => m.Id == movieId)).ToList();
        }

        public List<Game> GetUserGames(string userId)
        {
            var games = _context.UserGamesLists.Where(m => m.UserId == userId).ToList();

            var gamesIds = games.Select(game => game.GameId).ToList();

            return gamesIds.Select(gameId => _context.Games.Single(m => m.Id == gameId)).ToList();
        }

        public List<Series> GetUserSeries(string userId)
        {
            var series = _context.UserSeriesLists.Where(m => m.UserId == userId).ToList();

            var seriesIds = series.Select(m => m.SeriesId).ToList();

            return seriesIds.Select(seriesId => _context.Series.Single(m => m.Id == seriesId)).ToList();
        }

        // GET: UserProfile
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            
            var view = new UserProfileViewModel()
            {
                BooksList = GetUserBooks(userId),
                MoviesList = GetUserMovies(userId),
                GamesList = GetUserGames(userId),
                SeriesList = GetUserSeries(userId)

            };

            return View(view);
        }
        
        public ActionResult Delete(int id,string modelType)
        {
            var userId = User.Identity.GetUserId();

            if (modelType == "Book")
            {
                var book = _context.UserBooksLists.Single(x => x.UserId == userId && x.BookId == id);

                _context.UserBooksLists.Remove(book);
            }
            else if (modelType == "Movie")
            {
                var movie = _context.UserMoviesLists.Single(x => x.UserId == userId && x.MovieId == id);

                _context.UserMoviesLists.Remove(movie);
            }
            else if (modelType == "Game")
            {
                var game = _context.UserGamesLists.Single(x => x.UserId == userId && x.GameId == id);

                _context.UserGamesLists.Remove(game);
            }
            else if (modelType == "Series")
            {
                var series = _context.UserSeriesLists.Single(x => x.UserId == userId && x.SeriesId == id);

                _context.UserSeriesLists.Remove(series);
            }

            _context.SaveChanges();
            
            return RedirectToAction("Index", "UserProfile");
        }
    }
}