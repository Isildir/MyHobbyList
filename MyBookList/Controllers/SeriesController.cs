using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookList.Models;
using MyBookList.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace MyBookList.Controllers
{
    public class SeriesController : Controller
    {

        private ApplicationDbContext _context;

        public SeriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Series
        public ActionResult Index()
        {
            var seriesList = _context.Series.OrderBy(x => x.Title).ToList();

            var view = new List<SeriesFormViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                foreach (var series in seriesList)
                {
                    view.Add(new SeriesFormViewModel
                    {
                        Series = series,
                        CanEdit = true ? series.AddedByUserId == User.Identity.GetUserId() : false,
                        InUse = true ? _context.UserSeriesLists.Any(x => x.SeriesId == series.Id) : false
                    });
                }
            }
            else
            {
                foreach (var series in seriesList)
                {
                    view.Add(new SeriesFormViewModel
                    {
                        Series = series
                    });
                }
            }

            return View(view);
        }

        [Authorize]
        public ActionResult AddToUserBase(int id)
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
            }

            return RedirectToAction("Index", "Series");
        }

        public ActionResult New()
        {
            var viewModel = new SeriesFormViewModel()
            {
                Series = new Series()
            };

            return View("SeriesForm", viewModel);
        }
        
        public ActionResult Details(int id)
        {
            var series = _context.Series.SingleOrDefault(m => m.Id == id);

            return View(series);
        }

        [HttpPost]
        public ActionResult Update(Series series)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SeriesFormViewModel()
                {
                    Series = series
                };

                return View("SeriesForm", viewModel);

            }

            if (series.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                series.AddedByUserId = userId;

                _context.Series.Add(series);
            }
            else
            {
                var seriesInDb = _context.Series.Single(m => m.Id == series.Id);

                seriesInDb.Title = series.Title;
                seriesInDb.NumberOfSeasons = series.NumberOfSeasons;
                seriesInDb.ReleaseDate = series.ReleaseDate;
                seriesInDb.Description = series.Description;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Series");
        }

        public ActionResult Edit(int id)
        {
            var series = _context.Series.SingleOrDefault(m => m.Id == id);

            if (series == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SeriesFormViewModel()
            {
                Series = series
            };

            return View("SeriesForm", viewModel);
        }
    }
}