using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;
using MyBookList.FunctionalClasses;
using AutoMapper;
using MyBookList.ViewModels.Movies;

namespace MyBookList.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            var movies = new List<Movie>();

            movies = _context.Movies.Include(m => m.MovieGenre).OrderBy(x => x.AverageScore).ToList();

            movies.Reverse();
            
            var currentUserId = User.Identity.GetUserId();

            var view = new List<MovieIndexViewModel>();

            foreach (var movie in movies)
            {
                view.Add(Mapper.Map<Movie, MovieIndexViewModel>(movie));
            }

            return View(view);
        }

        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.Single(x => x.Id == id);

            _context.Movies.Remove(movie);

            _context.SaveChanges();

            TempData.Add("success", "Movie Successfully Deleted");
            return RedirectToAction("Index", "Movies");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id);

            var view = Mapper.Map<Movie, MovieDetailsViewModel>(movie);

            if (User.Identity.IsAuthenticated)
            {
                var score = _context.MovieScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.MovieId == movie.Id);

                if (score != null)
                {
                    view.YourScore = score.Score;
                }
                else
                {
                    view.YourScore = 0;
                }
                view.CanEdit = true ? movie.AddedByUserId == currentUserId : false;
                view.InUse = true ? _context.UserMoviesLists.Any(x => x.MovieId == movie.Id) : false;
                view.IsAdded = true ? _context.UserMoviesLists.Any(x => x.MovieId == movie.Id && x.UserId == currentUserId) : false;
            }

            var totalMoviesNum = _context.Movies.Count(m => m.MovieGenreId == movie.MovieGenreId);

            var totalThisAuthorMoviesNum = _context.Movies.Count(m => m.Director == movie.Director);

            var SimiliarList = new List<SimiliarMovieMini>();

            if (totalThisAuthorMoviesNum > 1 && totalMoviesNum > GlobalVariables.SimiliarListSize)
            {
                Random r = new Random();

                var nextNum = r.Next(0, totalThisAuthorMoviesNum);

                var items = _context.Movies.Where(m => m.Director == movie.Director).ToList();

                var item = items.ElementAt(nextNum);

                if (!SimiliarList.Exists(m => m.Title == item.Director))
                {
                    SimiliarList.Add(new SimiliarMovieMini()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ImageId = item.ImageId
                    });
                }
            }

            if (totalMoviesNum > GlobalVariables.SimiliarListSize)
            {
                while (SimiliarList.Count < GlobalVariables.SimiliarListSize)
                {
                    Random r = new Random();

                    var nextNum = r.Next(0, totalMoviesNum);

                    var items = _context.Movies.Where(m => m.MovieGenreId == movie.MovieGenreId).ToList();

                    var item = items.ElementAt(nextNum);

                    if (!SimiliarList.Exists(m => m.Title == item.Director))
                    {
                        SimiliarList.Add(new SimiliarMovieMini()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ImageId = item.ImageId
                        });
                    }
                }
            }

            view.SimiliarMovies = SimiliarList;

            return View(view);
        }

        public ActionResult New()
        {
            var currentUser = User.Identity.GetUserName();

            if (_context.BannedUsers.SingleOrDefault(m => m.UserId == currentUser) == null)
            {
                var movieGenres = _context.MovieGenres.ToList();
                var viewModel = new MovieFormViewModel()
                {
                    Id = 0
                };

                return PartialView("_MovieFormModal", viewModel);
            }
            else
            {
                return RedirectToAction("Index", "UserProfile");
            }
        }

        [HttpPost]
        public ActionResult Update(MovieFormViewModel movieForm, HttpPostedFileBase UploadImage)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_MovieFormModal", movieForm);
            }

            var imageHandler = new ImageHandler();

            int imageId = imageHandler.AddImage(UploadImage);
            
            if (movieForm.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                var movie = Mapper.Map<MovieFormViewModel, Movie>(movieForm);

                movie.AddedByUserId = userId;
                movie.ImageId = imageId;

                TempData.Add("success", "Movie Successfully Added to base");
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movieForm.Id);

                movieInDb.Title = movieForm.Title;
                movieInDb.Director = movieForm.Director;
                movieInDb.ReleaseDate = movieForm.ReleaseDate;
                movieInDb.Description = movieForm.Description;
                movieInDb.MovieGenreId = movieForm.MovieGenreId;
                if (movieInDb.ImageId == GlobalVariables.DefaultImageId)
                {
                    movieInDb.ImageId = imageId;
                }
                TempData.Add("success", "Movie Successfully Edited");
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        
        public EmptyResult AddScore(int id, short score)
        {
            var currentUserId = User.Identity.GetUserId();

            var currentScore = _context.MovieScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.MovieId == id);

            var movie = _context.Movies.Single(m => m.Id == id);

            if (currentScore == null)
            {
                _context.MovieScoreLists.Add(new Models.User.MovieScoreList()
                {
                    MovieId = id,
                    UserId = currentUserId,
                    Score = score
                });


                movie.NumberOfVoters++;
                movie.AverageScore = ((movie.AverageScore * (movie.NumberOfVoters - 1)) + score) / movie.NumberOfVoters;
            }
            else
            {
                movie.AverageScore = ((movie.AverageScore * movie.NumberOfVoters) + (score - currentScore.Score)) / movie.NumberOfVoters;

                currentScore.Score = score;
            }

            _context.SaveChanges();

            return new EmptyResult();
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Movie, MovieFormViewModel>(movie);
            
            return PartialView("_MovieFormModal", viewModel);
        }
    }
}