using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;

namespace MyBookList.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.MovieGenre).OrderBy(x => x.Title).ToList();

            var view = new List<MovieFormViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                foreach (var movie in movies)
                {
                    view.Add(new MovieFormViewModel
                    {
                        Movie = movie,
                        CanEdit = true ? movie.AddyeByUserId == User.Identity.GetUserId() : false,
                        InUse = true ? _context.UserMoviesLists.Any(x => x.MovieId == movie.Id) : false
                    });
                }
            }
            else
            {
                foreach (var movie in movies)
                {
                    view.Add(new MovieFormViewModel
                    {
                        Movie = movie
                    });
                }
            }

            return View(view);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                MovieGenre = movieGenres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize]
        public ActionResult AddToUserBase(int id)
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
            }

            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        public ActionResult Update(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    MovieGenre = _context.MovieGenres.ToList()
                };

                return View("MovieForm", viewModel);

            }

            if (movie.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                movie.AddyeByUserId = userId;

                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Title = movie.Title;
                movieInDb.Director = movie.Director;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Description = movie.Description;
                movieInDb.MovieGenreId = movie.MovieGenreId;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                MovieGenre = _context.MovieGenres.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}