using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;
using MyBookList.ViewModels.User;
using AutoMapper;

namespace MyBookList.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private ApplicationDbContext _context;

        public UserProfileController()
        {
            _context = new ApplicationDbContext();
        }

        private List<BookIndexViewModel> GetUserBooks(string userId)
        {
            var books = _context.UserBooksLists.Where(m => m.UserId == userId && !m.Reccomended).ToList();

            var recommendedBooks = _context.UserBooksLists.Where(m => m.UserId == userId && m.Reccomended).ToList();

            var booksIds = books.Select(book => book.BookId).ToList();

            var recommendedData = new Dictionary<int, string>();
            var recommendedDataIds = new List<int>();

            foreach(var x in recommendedBooks)
            {
                recommendedData.Add(x.BookId,x.RecommendingUserName);
                recommendedDataIds.Add(x.BookId);
            }

            var list = booksIds.Select(bookId => _context.Books.Single(m => m.Id == bookId)).ToList();

            var recommendedList = recommendedDataIds.Select(bookId => _context.Books.Single(m => m.Id == bookId)).ToList();

            var result = new List<BookIndexViewModel>();

            foreach (var item in recommendedList)
            {
                var newItem = Mapper.Map<Book, BookIndexViewModel>(item);

                newItem.Recommended = true;
                newItem.RecommenderName = recommendedData.Single(x => x.Key == item.Id).Value;

                result.Add(newItem);
            }
            
            foreach (var item in list)
            {
                var newItem = Mapper.Map<Book, BookIndexViewModel>(item);

                newItem.Recommended = false;

                result.Add(newItem);
            }

            return result;
        }

        private List<MovieIndexViewModel> GetUserMovies(string userId)
        {
            var movies = _context.UserMoviesLists.Where(m => m.UserId == userId && !m.Reccomended).ToList();

            var recommendedMovies = _context.UserMoviesLists.Where(m => m.UserId == userId && m.Reccomended).ToList();

            var movieIds = movies.Select(movie => movie.MovieId).ToList();

            var recommendedData = new Dictionary<int, string>();
            var recommendedDataIds = new List<int>();

            foreach (var x in recommendedMovies)
            {
                recommendedData.Add(x.MovieId, x.RecommendingUserName);
                recommendedDataIds.Add(x.MovieId);
            }

            var list = movieIds.Select(movieId => _context.Movies.Single(m => m.Id == movieId)).ToList();

            var recommendedList = recommendedDataIds.Select(movieId => _context.Movies.Single(m => m.Id == movieId)).ToList();

            var result = new List<MovieIndexViewModel>();

            foreach (var item in recommendedList)
            {
                var newItem = Mapper.Map<Movie, MovieIndexViewModel>(item);

                newItem.Recommended = true;
                newItem.RecommenderName = recommendedData.Single(x => x.Key == item.Id).Value;

                result.Add(newItem);
            }

            foreach (var item in list)
            {
                var newItem = Mapper.Map<Movie, MovieIndexViewModel>(item);

                newItem.Recommended = false;

                result.Add(newItem);
            }

            return result;
        }

        private List<GameIndexViewModel> GetUserGames(string userId)
        {
            var games = _context.UserGamesLists.Where(m => m.UserId == userId && !m.Reccomended).ToList();

            var recommendedGames = _context.UserGamesLists.Where(m => m.UserId == userId && m.Reccomended).ToList();

            var gamesIds = games.Select(game => game.GameId).ToList();

            var recommendedData = new Dictionary<int, string>();
            var recommendedDataIds = new List<int>();

            foreach (var x in recommendedGames)
            {
                recommendedData.Add(x.GameId, x.RecommendingUserName);
                recommendedDataIds.Add(x.GameId);
            }

            var list = gamesIds.Select(bookId => _context.Games.Single(m => m.Id == bookId)).ToList();

            var recommendedList = recommendedDataIds.Select(bookId => _context.Games.Single(m => m.Id == bookId)).ToList();

            var result = new List<GameIndexViewModel>();

            foreach (var item in recommendedList)
            {
                var newItem = Mapper.Map<Game, GameIndexViewModel>(item);

                newItem.Recommended = true;
                newItem.RecommenderName = recommendedData.Single(x => x.Key == item.Id).Value;

                result.Add(newItem);
            }

            foreach (var item in list)
            {
                var newItem = Mapper.Map<Game, GameIndexViewModel>(item);

                newItem.Recommended = false;

                result.Add(newItem);
            }

            return result;
        }

        private List<SeriesIndexViewModel> GetUserSeries(string userId)
        {
            var series = _context.UserSeriesLists.Where(m => m.UserId == userId && !m.Reccomended).ToList();

            var recommendedSeries = _context.UserSeriesLists.Where(m => m.UserId == userId && m.Reccomended).ToList();

            var seriesIds = series.Select(serie => serie.SeriesId).ToList();

            var recommendedData = new Dictionary<int, string>();
            var recommendedDataIds = new List<int>();

            foreach(var x in recommendedSeries)
            {
                recommendedData.Add(x.SeriesId,x.RecommendingUserName);
                recommendedDataIds.Add(x.SeriesId);
            }

            var list = seriesIds.Select(serieId => _context.Series.Single(m => m.Id == serieId)).ToList();

            var recommendedList = recommendedDataIds.Select(bookId => _context.Series.Single(m => m.Id == bookId)).ToList();

            var result = new List<SeriesIndexViewModel>();

            foreach (var item in recommendedList)
            {
                var newItem = Mapper.Map<Series, SeriesIndexViewModel>(item);

                newItem.Recommended = true;
                newItem.RecommenderName = recommendedData.Single(x => x.Key == item.Id).Value;

                result.Add(newItem);
            }
            
            foreach (var item in list)
            {
                var newItem = Mapper.Map<Series, SeriesIndexViewModel>(item);

                newItem.Recommended = false;

                result.Add(newItem);
            }

            return result;
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            
            var view = new UserProfileViewModel()
            {
                BooksList = GetUserBooks(userId),
                BookGenres = _context.BookGenres.ToList(),
                MoviesList = GetUserMovies(userId),
                GamesList = GetUserGames(userId),
                SeriesList = GetUserSeries(userId)
            };

            return View(view);
        }
        
        public ActionResult SendTicket(UserTicketViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View("SendTicket", form);
            }

            var ticket = new Ticket()
            {
                SendingUserName = User.Identity.GetUserName(),
                TicketBody = form.TicketBody,
                TicketTitle = form.TicketTitle,
                TimeSend = DateTime.Now.Date
            };

            _context.AdminTickets.Add(ticket);

            _context.SaveChanges();

            TempData.Add("success", "Ticket has been send");

            return RedirectToAction("Index");
        }
        
        public ActionResult AddBookToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var book = new UserBooksList()
            {
                UserId = currentUserId,
                BookId = id
            };


            if (!_context.UserBooksLists.Any(m => m.UserId == currentUserId && m.BookId == id))
            {
                _context.UserBooksLists.Add(book);
                _context.SaveChanges();
                TempData.Add("success", "Book Added to your books list");

                return RedirectToAction("Details/" + id, "Books");
            }
            else
            {
                var item = _context.UserBooksLists.Single(m => m.UserId == currentUserId && m.BookId == id);

                item.Reccomended = false;
                item.RecommendingUserName = null;

                _context.SaveChanges();

                TempData.Add("success", "Book Added to your series list");

                return RedirectToAction("index", "UserProfile");
            }
        }
        
        public ActionResult AddMovieToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var movie = new UserMoviesList()
            {
                UserId = currentUserId,
                MovieId = id
            };

            if (!_context.UserMoviesLists.Any(m => m.UserId == currentUserId && m.MovieId == id))
            {
                _context.UserMoviesLists.Add(movie);
                _context.SaveChanges();
                TempData.Add("success", "Movie Added to your movies list");

                return RedirectToAction("Details/" + id, "Movies");
            }
            else
            {
                var item = _context.UserMoviesLists.Single(m => m.UserId == currentUserId && m.MovieId == id);

                item.Reccomended = false;
                item.RecommendingUserName = null;

                _context.SaveChanges();

                TempData.Add("success", "Movie Added to your movies list");

                return RedirectToAction("index", "UserProfile");
            }
        }
        
        public ActionResult AddGameToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var game = new UserGamesList()
            {
                UserId = currentUserId,
                GameId = id
            };

            if (!_context.UserGamesLists.Any(m => m.UserId == currentUserId && m.GameId == id))
            {
                _context.UserGamesLists.Add(game);
                _context.SaveChanges();
                TempData.Add("success", "Game Added to your games list");

                return RedirectToAction("Details/" + id, "Games");
            }
            else
            {
                var item = _context.UserGamesLists.Single(m => m.UserId == currentUserId && m.GameId == id);

                item.Reccomended = false;
                item.RecommendingUserName = null;

                _context.SaveChanges();

                TempData.Add("success", "Game Added to your series list");

                return RedirectToAction("index", "UserProfile");
            }
        }
        
        public ActionResult AddSeriesToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var series = new UserSeriesList()
            {
                UserId = currentUserId,
                SeriesId = id
            };
            
            if (!_context.UserSeriesLists.Any(m => m.UserId == currentUserId && m.SeriesId == id))
            {
                _context.UserSeriesLists.Add(series);
                _context.SaveChanges();
                TempData.Add("success", "Series Added to your series list");

                return RedirectToAction("Details/" + id, "Series");
            }
            else
            {
                var item = _context.UserSeriesLists.Single(m => m.UserId == currentUserId && m.SeriesId == id);

                item.Reccomended = false;
                item.RecommendingUserName = null;

                _context.SaveChanges();

                TempData.Add("success", "Series Added to your series list");

                return RedirectToAction("index", "UserProfile");
            }
        }

        public ActionResult Delete(int id,string modelType)
        {
            var userId = User.Identity.GetUserId();

            if (modelType == "Book")
            {
                var book = _context.UserBooksLists.Single(x => x.UserId == userId && x.BookId == id);
                
                _context.UserBooksLists.Remove(book);
                TempData.Add("success", "Book deleted from your list");
            }
            else if (modelType == "Movie")
            {
                var movie = _context.UserMoviesLists.Single(x => x.UserId == userId && x.MovieId == id);

                _context.UserMoviesLists.Remove(movie);
                TempData.Add("success", "Movie deleted from your list");
            }
            else if (modelType == "Game")
            {
                var game = _context.UserGamesLists.Single(x => x.UserId == userId && x.GameId == id);

                _context.UserGamesLists.Remove(game);
                TempData.Add("success", "Game deleted from your list");
            }
            else if (modelType == "Series")
            {
                var series = _context.UserSeriesLists.Single(x => x.UserId == userId && x.SeriesId == id);

                _context.UserSeriesLists.Remove(series);
                TempData.Add("success", "Series deleted from your list");
            }

            _context.SaveChanges();


            return RedirectToAction("Index", "UserProfile");
        }

        public ActionResult Recommend(int id,string type,string userName)
        {
            if(userName.Length == 0 || !_context.Users.Any(m => m.UserName == userName))
            {
                TempData.Add("fail", "There is no user with " + userName + " nick");
                return RedirectToAction("Index", "UserProfile");
            }

            var target = _context.Users.Single(m => m.UserName == userName);
            
            switch (type)
            {
                case "Book":
                    if (_context.UserBooksLists.Any(x => x.BookId == id && x.UserId == target.Id))
                    {
                        TempData.Add("error", userName + " already have this book in his base");
                        break;
                    }
                    else
                    {
                        _context.UserBooksLists.Add(new UserBooksList() { BookId = id, UserId = target.Id, Reccomended = true, RecommendingUserName = User.Identity.GetUserName() });
                        TempData.Add("success", "Book successfully recomended to " + userName);
                        break;
                    }
                case "Game":
                    if (_context.UserGamesLists.Any(x => x.GameId == id && x.UserId == target.Id))
                    {
                        TempData.Add("error", userName + " already have this game in his base");
                        break;
                    }
                    else
                    {
                        _context.UserGamesLists.Add(new UserGamesList() { GameId = id, UserId = target.Id, Reccomended = true, RecommendingUserName = User.Identity.GetUserName() });
                        TempData.Add("success", "Game successfully recomended to " + userName);
                        break;
                    }
                case "Movie":
                    if (_context.UserMoviesLists.Any(x => x.MovieId == id && x.UserId == target.Id))
                    {
                        TempData.Add("error", userName + " already have this movie in his base");
                        break;
                    }
                    else
                    {
                        _context.UserMoviesLists.Add(new UserMoviesList() { MovieId = id, UserId = target.Id, Reccomended = true, RecommendingUserName = User.Identity.GetUserName() });
                        TempData.Add("success", "Movie successfully recomended to " + userName);
                        break;
                    }
                case "Series":
                    if (_context.UserSeriesLists.Any(x => x.SeriesId == id && x.UserId == target.Id))
                    {
                        TempData.Add("error", userName + " already have this series in his base");
                        break;
                    }
                    else
                    {
                        _context.UserSeriesLists.Add(new UserSeriesList() { SeriesId = id, UserId = target.Id, Reccomended = true, RecommendingUserName = User.Identity.GetUserName() });
                        TempData.Add("success", "Series successfully recomended to " + userName);
                        break;
                    }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "UserProfile");
        }
    }
}