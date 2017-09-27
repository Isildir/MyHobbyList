using AutoMapper;
using Microsoft.AspNet.Identity;
using MyBookList.FunctionalClasses;
using MyBookList.Models;
using MyBookList.ViewModels;
using MyBookList.ViewModels.Series;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBookList.Controllers
{
    [Authorize]
    public class SeriesController : Controller
    {

        private ApplicationDbContext _context;

        public SeriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Series
        [AllowAnonymous]
        public ActionResult Index()
        {
            var seriesList = new List<Series>();
            
            seriesList = _context.Series.Include(m => m.SeriesGenre).OrderBy(x => x.AverageScore).ToList();

            seriesList.Reverse();
            
            var currentUserId = User.Identity.GetUserId();

            var view = new List<SeriesIndexViewModel>();

            foreach (var series in seriesList)
            {
                view.Add(Mapper.Map<Series, SeriesIndexViewModel>(series));
            }

            return View(view);
        }

        public ActionResult Delete(int id)
        {
            var series = _context.Series.Single(x => x.Id == id);

            _context.Series.Remove(series);

            _context.SaveChanges();

            TempData.Add("success", "Series Successfully Deleted");
            return RedirectToAction("Index", "Series");
        }

        public ActionResult New()
        {
            var currentUser = User.Identity.GetUserName();

            if (_context.BannedUsers.SingleOrDefault(m => m.UserId == currentUser) == null)
            {
                var SeriesGenre = _context.SeriesGenres.ToList();
                var viewModel = new SeriesFormViewModel()
                {
                    Id = 0
                };

                return PartialView("_SeriesFormModal", viewModel);
            }
            else
            {
                return RedirectToAction("Index", "UserProfile");
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var serie = _context.Series.Include(m => m.SeriesGenre).SingleOrDefault(m => m.Id == id);

            var view = Mapper.Map<Series, SeriesDetailsViewModel>(serie);

            if (User.Identity.IsAuthenticated)
            {
                var score = _context.SeriesScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.SeriesId == serie.Id);

                if (score != null)
                {
                    view.YourScore = score.Score;
                }
                else
                {
                    view.YourScore = 0;
                }
                view.CanEdit = true ? serie.AddedByUserId == currentUserId : false;
                view.InUse = true ? _context.UserSeriesLists.Any(x => x.SeriesId == serie.Id) : false;
                view.IsAdded = true ? _context.UserSeriesLists.Any(x => x.SeriesId == serie.Id && x.UserId == currentUserId) : false;
            }

            var totalSeriessNum = _context.Series.Count(m => m.SeriesGenreId == serie.SeriesGenreId);

            var totalThisAuthorSeriessNum = _context.Series.Count(m => m.Title == serie.Title);

            var SimiliarList = new List<SimiliarSeriesMini>();

            if (totalThisAuthorSeriessNum > 1 && totalSeriessNum > GlobalVariables.SimiliarListSize)
            {
                Random r = new Random();

                var nextNum = r.Next(0, totalThisAuthorSeriessNum);

                var items = _context.Series.Where(m => m.Title == serie.Title).ToList();

                var item = items.ElementAt(nextNum);

                if (!SimiliarList.Exists(m => m.Title == item.Title))
                {
                    SimiliarList.Add(new SimiliarSeriesMini()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ImageId = item.ImageId
                    });
                }
            }

            if (totalSeriessNum > GlobalVariables.SimiliarListSize)
            {
                while (SimiliarList.Count < GlobalVariables.SimiliarListSize)
                {
                    Random r = new Random();

                    var nextNum = r.Next(0, totalSeriessNum);

                    var items = _context.Series.Where(m => m.SeriesGenreId == serie.SeriesGenreId).ToList();

                    var item = items.ElementAt(nextNum);

                    if (!SimiliarList.Exists(m => m.Title == item.Title))
                    {
                        SimiliarList.Add(new SimiliarSeriesMini()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ImageId = item.ImageId
                        });
                    }
                }
            }

            view.SimiliarSeries = SimiliarList;
            
            return View(view);
        }

        [HttpPost]
        public ActionResult Update(SeriesFormViewModel seriesForm, HttpPostedFileBase UploadImage)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new HttpNotFoundResult();
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_SeriesFormModal", seriesForm);
            }

            var imageHandler = new ImageHandler();

            int imageId = imageHandler.AddImage(UploadImage);

            if (seriesForm.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                var series = Mapper.Map<SeriesFormViewModel, Series>(seriesForm);

                series.AddedByUserId = userId;
                series.ImageId = imageId;

                TempData.Add("success", "Series Successfully Added to base");
                _context.Series.Add(series);
            }
            else
            {
                var seriesInDb = _context.Series.Single(m => m.Id == seriesForm.Id);

                seriesInDb.Title = seriesForm.Title;
                seriesInDb.NumberOfSeasons = seriesForm.NumberOfSeasons;
                seriesInDb.ReleaseDate = seriesForm.ReleaseDate;
                seriesInDb.Description = seriesForm.Description;
                seriesInDb.SeriesGenreId = seriesForm.SeriesGenreId;
                if (seriesInDb.ImageId == GlobalVariables.DefaultImageId)
                {
                    seriesInDb.ImageId = imageId;
                }
                TempData.Add("success", "Series Successfully Edited");
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Series");
        }
        
        public EmptyResult AddScore(int id, short score)
        {
            var currentUserId = User.Identity.GetUserId();

            var currentScore = _context.SeriesScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.SeriesId == id);

            var serie = _context.Series.Single(m => m.Id == id);

            if (currentScore == null)
            {
                _context.SeriesScoreLists.Add(new Models.User.SeriesScoreList()
                {
                    SeriesId = id,
                    UserId = currentUserId,
                    Score = score
                });


                serie.NumberOfVoters++;
                serie.AverageScore = ((serie.AverageScore * (serie.NumberOfVoters - 1)) + score) / serie.NumberOfVoters;
            }
            else
            {
                serie.AverageScore = ((serie.AverageScore * serie.NumberOfVoters) + (score - currentScore.Score)) / serie.NumberOfVoters;

                currentScore.Score = score;
            }

            _context.SaveChanges();

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var series = _context.Series.SingleOrDefault(m => m.Id == id);

            if (series == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Series, SeriesFormViewModel>(series);
            
            return PartialView("_SeriesFormModal", viewModel);
        }
    }
}