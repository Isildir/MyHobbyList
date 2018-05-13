using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MyHobbyList.Models;
using MyHobbyList.ViewModels;
using MyHobbyList.FunctionalClasses;
using AutoMapper;

namespace MyHobbyList.Controllers
{
    [Authorize]
    public class MovieController : EntityController
    {
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var entities = GetEntities<Movie>(null);

            entities = entities.OrderByDescending(x => x.AverageScore).ToList();

            var view = new List<MovieIndexViewModel>();

            foreach (var movie in entities)
            {
                view.Add(Mapper.Map<Movie, MovieIndexViewModel>(movie));
            }

            return View(view);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var element = GetEntity<Movie>(id, null);

            if (element == null)
                return HttpNotFound();

            var view = Mapper.Map<Movie, MovieDetailsViewModel>(element);

            if (User.Identity.IsAuthenticated)
            {
                var user = GetUserData();

                var score = user.Scores.FirstOrDefault(x => x.Id == element.Id && x.Entity.ElementType == ElementType.Movie);

                view.YourScore = score != null ? score.Value : 0.0;
            }

            //making list of similiar books 1/same author 5/same genre
            var SimiliarList = new List<SimiliarEntityMini>();

            var entity = SameAuthorEntity(id, element.Director, ElementType.Movie) as Movie;
            var entities = SameGenreEntities<Movie>(id, element.Genre, entity == null ? 1 : 0);

            if (entities != null)
            {
                if (entity != null)
                    entities.Add(entity);

                foreach (var item in entities)
                {
                    SimiliarList.Add(new SimiliarEntityMini() { Id = item.Id, ImageId = item.ImageId, Title = item.Title });
                }

                view.SimiliarEntities = SimiliarList;
            }

            var comments = _context.Comments.Where(x => x.ElementType == ElementType.Movie && x.Entity.Id == id).ToList();

            var commentsView = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                commentsView.Add(new CommentViewModel()
                {
                    Id = comment.Id,
                    CommentData = comment.CommentData,
                    UserLogin = comment.UserLogin,
                    DateAdded = comment.DateAdded
                });
            }

            view.Comments = commentsView;

            //informations to recommend
            ViewBag.EntityId = id;
            ViewBag.ElementType = ElementType.Movie;

            return View(view);
        }

        public ActionResult Delete(int id)
        {
            bool result;

            if (User.IsInRole("Admin"))
                result = RemoveEntity(GetEntity<Movie>(id, null));
            else
                result = RemoveEntity(GetEntity<Movie>(id, GetUserData().UserId));

            if (result)
                TempData.Add("success", "Movie Successfully Deleted");
            else
                TempData.Add("fail", "Application encounter an error");

            return RedirectToAction("Index", "Movie");
        }

        public ActionResult New()
        {
            var userData = GetUserData();

            if (userData != null && userData.AccountState == AccountState.Active)
            {
                var viewModel = new MovieFormViewModel()
                {
                    Id = 0,
                    IsNew = true
                };

                return PartialView("_MovieFormModal", viewModel);
            }
            else
                return RedirectToAction("Index", "UserProfile");
        }

        public ActionResult Edit(int id)
        {
            Movie entity;

            if (User.IsInRole("Admin"))
                entity = GetEntity<Movie>(id, null);
            else
                entity = GetEntity<Movie>(id, GetUserData().UserId);

            var viewModel = Mapper.Map<Movie, MovieFormViewModel>(entity);

            viewModel.IsNew = false;

            return View("_MovieFormModal", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(MovieFormViewModel form, HttpPostedFileBase UploadImage)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return new HttpNotFoundResult();

            if (!ModelState.IsValid)
                return PartialView("_MovieFormModal", form);

            if (form.IsNew && !_context.Movies.Any(x => x.Title.Equals(form.Title)))
            {
                TempData.Add("fail", "Entity with this title already in base");

                return RedirectToAction("Index", "UserProfile");
            }

            var imageHandler = new ImageHandler();

            int imageId = imageHandler.AddImage(UploadImage);

            if (form.Id == 0)
            {
                var userData = GetUserData();

                var entity = Mapper.Map<MovieFormViewModel, Movie>(form);

                entity.CreateById = userData.UserId;
                entity.ImageId = imageId;
                entity.GenreId = form.GenreId;

                if (String.IsNullOrWhiteSpace(form.Description))
                    entity.Description = GlobalVariables.EmptyDescription;

                var result = AddEntity(entity);

                if (result == 1)
                    TempData.Add("success", "Entity Successfully Added To Base");
            }
            else
            {
                var entity = GetEntity<Movie>(form.Id, null);

                entity.Director = form.Director;
                entity.Description = form.Description;
                entity.ReleaseDate = form.ReleaseDate;
                entity.Title = form.Title;
                entity.GenreId = form.GenreId;

                if (entity.ImageId == GlobalVariables.DefaultImageId)
                    entity.ImageId = imageId;
                else
                {
                    _context.Files.Remove(_context.Files.Single(x => x.Id == entity.ImageId));

                    entity.ImageId = imageId;
                }

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                TempData.Add("success", "Entity Successfully Updated");
            }

            return RedirectToAction("Index", "UserProfile");
        }
    }
}