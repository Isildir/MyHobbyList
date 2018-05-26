using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyHobbyList.Models;
using MyHobbyList.ViewModels;
using MyHobbyList.FunctionalClasses;
using AutoMapper;

namespace MyHobbyList.Controllers
{
    [Authorize]
    public class BookController : EntityController
    {
        public BookController()
        {
            _context = new ApplicationDbContext();
        }
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            var entities = GetEntities<Book>(false);

            entities = entities.OrderByDescending(x => x.AverageScore).ToList();
            
            var view = new List<BookIndexViewModel>();

            foreach (var book in entities)
            {
                view.Add(Mapper.Map<Book, BookIndexViewModel>(book));
            }
            
            return View(view);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var element = GetEntity<Book>(id, null);

            if (element == null)
                return HttpNotFound();

            var view = Mapper.Map<Book, BookDetailsViewModel>(element);

            if (User.Identity.IsAuthenticated)
            {
                var user = GetUserData();
                ViewBag.IsBanned = user.AccountState;
                ViewBag.IsOwner = _context.Books.Any(x => x.CreateById.Equals(user.UserId) && x.Id == id);
                ViewBag.AlreadyAdded = user.Entities.Any(x => x.ElementType == ElementType.Book && x.Id == id);

                var score = user.Scores.FirstOrDefault(x => x.EntityId == element.Id && x.ElementType == ElementType.Book);

                view.YourScore = score != null ? score.Value : (short)0;
            }

            //making list of similiar books 1/same author 5/same genre
            var SimiliarList = new List<SimiliarEntityMini>();
            
            var entity = SameAuthorEntity(id, element.Author, ElementType.Book) as Book;
            var entities = SameGenreEntities<Book>(id, element.Genre, entity != null ? entity.Id : 0);

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

            var comments = _context.Comments.Where(x => x.ElementType == ElementType.Book && x.Entity.Id == id).ToList();

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
            
            return View(view);
        }
        
        public ActionResult Delete(int id)
        {
            var userData = GetUserData();

            if (userData.AccountState == AccountState.Blocked)
                return View("Error");

            bool result;

            if(User.IsInRole("Admin"))
                result = RemoveEntity(GetEntity<Book>(id, null));
            else
                result = RemoveEntity(GetEntity<Book>(id, GetUserData().UserId));

            if (result)
                TempData.Add("success", "Book Successfully Deleted");
            else
                TempData.Add("fail", "Application encounter an error");

            return RedirectToAction("Index", "Book");
        }

        public ActionResult New()
        {
            var userData = GetUserData();

            if (userData != null && userData.AccountState == AccountState.Active)
            {
                var viewModel = new BookFormViewModel()
                {
                    Id = 0,
                    IsNew = true
                };

                return PartialView("_BookFormModal", viewModel);
            }
            else
                return View("Error");
        }
        
        public ActionResult Edit(int id)
        {
            var userData = GetUserData();

            if(userData.AccountState == AccountState.Blocked)
                return View("Error");

            Book entity;

            if(User.IsInRole("Admin"))
                entity = GetEntity<Book>(id, null);
            else
                entity = GetEntity<Book>(id, GetUserData().UserId);

            if (entity == null)
                return null;

            var viewModel = Mapper.Map<Book, BookFormViewModel>(entity);

            viewModel.IsNew = false;

            return PartialView("_BookFormModal", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(BookFormViewModel form, HttpPostedFileBase UploadImage)
        {
            var userData = GetUserData();

            if (userData.AccountState == AccountState.Blocked)
                return View("Error");

            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return new HttpNotFoundResult();

            if (!ModelState.IsValid)
                return PartialView("_BookFormModal", form);

            if (form.IsNew && !_context.Books.Any(x => x.Title.Equals(form.Title)))
            {
                TempData.Add("fail", "Entity with this title already in base");

                return RedirectToAction("Index", "UserProfile");
            }
            
            int imageId = ImageHandler.AddImage(UploadImage, ref _context);
            
            if (form.Id == 0)
            {
                var entity = Mapper.Map<BookFormViewModel, Book>(form);

                entity.CreateById = userData.UserId;
                entity.ImageId = imageId;
                entity.Genre = _context.Genres.First(x => x.Id == form.GenreId);

                if (String.IsNullOrWhiteSpace(form.Description))
                    entity.Description = GlobalVariables.EmptyDescription;

                var result = AddEntity(entity);

                if(result == 1)
                    TempData.Add("success", "Entity Successfully Added To Base");
            }
            else
            {
                var entity = GetEntity<Book>(form.Id, null);

                entity.Author = form.Author;
                entity.Description = form.Description;
                entity.ReleaseDate = form.ReleaseDate;
                entity.Title = form.Title;
                entity.Genre = _context.Genres.First(x => x.Id == form.GenreId);

                if (entity.ImageId == GlobalVariables.DefaultImageId)
                    entity.ImageId = imageId;
                else if(imageId != GlobalVariables.DefaultImageId)
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