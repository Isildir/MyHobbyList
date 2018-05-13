using MyHobbyList.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyHobbyList.FunctionalClasses;

namespace MyHobbyList.Controllers
{
    [Authorize]
    public abstract class EntityController : BaseController
    {
        protected int AddEntity<T>(T entity)
        {
            _context.Set(entity.GetType()).Add(entity);

            return _context.SaveChanges();
        }

        protected T GetEntity<T>(int id, string userId) where T : Entity
        {
            return !string.IsNullOrEmpty(userId) ? _context.Set<T>().Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId) :
                _context.Set<T>().Find(id);
        }

        protected bool RemoveEntity<T>(T entity) where T : Entity
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        protected  SelectList GetGenres(ElementType elementType)
        {
            var genres = _context.Genres.Where(x => x.ElementType == elementType).Select(x => new { x.Id, x.Name });

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var genre in genres)
                dict.Add(genre.Name, genre.Id);

            return new SelectList(dict, "Value", "Key");
        }

        public static SelectList GetGenresStatic(ElementType elementType)
        {
            var context = new ApplicationDbContext();

            var genres = context.Genres.Where(x => x.ElementType == elementType).Select(x => new { x.Id, x.Name });

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var genre in genres)
                dict.Add(genre.Name, genre.Id);

            return new SelectList(dict, "Value", "Key");
        }

        /*
        protected Entity GetEntity(int id, ElementType elementType, int? userId)
        {
            switch (elementType)
            {
                case ElementType.Book:
                    return userId != null ? _context.Books.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId.Value) : _context.Books.Find(id);
                case ElementType.Game:
                    return userId != null ? _context.Games.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId.Value) : _context.Games.Find(id);
                case ElementType.Movie:
                    return userId != null ? _context.Movies.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId.Value) : _context.Movies.Find(id);
                default:
                    return null;
            }
        }
        */

        /*
        protected List<Entity> GetEntities(ElementType elementType, int? userId)
        {
            switch (elementType)
            {
                case ElementType.Book:
                    return userId != null ? _context.Books.Include(a => a.Genre).Where(x => x.CreateById == userId.Value).ToList<Entity>() : _context.Books.ToList<Entity>();
                case ElementType.Game:
                    return userId != null ? _context.Games.Include(a => a.Genre).Where(x => x.CreateById == userId.Value).ToList<Entity>() : _context.Games.ToList<Entity>();
                case ElementType.Movie:
                    return userId != null ? _context.Movies.Include(a => a.Genre).Where(x => x.CreateById == userId.Value).ToList<Entity>() : _context.Movies.ToList<Entity>();
                default:
                    return null;
            }
        }
        */

        protected Entity SameAuthorEntity(int id, string maker, ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Book:
                    return _context.Books.Include(a => a.Genre).FirstOrDefault(x => x.Id != id && x.Author.Equals(maker));
                case ElementType.Game:
                    return _context.Games.Include(a => a.Genre).FirstOrDefault(x => x.Id != id && x.Studio.Equals(maker));
                case ElementType.Movie:
                    return _context.Movies.Include(a => a.Genre).FirstOrDefault(x => x.Id != id && x.Director.Equals(maker));
                default:
                    return null;
            }
        }

        protected IList<T> SameGenreEntities<T>(int id, Genre genre, int incrementor) where T : Entity
        {
            var ids = _context.Set<T>().Where(x => x.Genre.Id == genre.Id && x.Id != id).Select(x => x.Id).ToList();
            if (ids.Count() < GlobalVariables.SimiliarListSize + incrementor)
                return null;

            List<int> randNumbers = new List<int>();

            Random r = new Random();

            for (int i = 0; i < GlobalVariables.SimiliarListSize + incrementor; i++)
                randNumbers.Add(ids[r.Next(0, ids.Count())]);

            return _context.Set<T>().Include(a => a.Genre).Where(x => randNumbers.Contains(x.Id)).ToList<T>();
        }

        /*
        protected IList<Entity> SameGenreEntities(int id, Genre genre, int incrementor, ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Book:
                    {
                        var ids = _context.Books.Where(x => x.Genre.Id == genre.Id && x.Id != id).Select(x => x.Id).ToList();
                        if (ids.Count() < GlobalVariables.SimiliarListSize + incrementor)
                            return null;

                        List<int> randNumbers = new List<int>();

                        Random r = new Random();

                        for (int i = 0; i < GlobalVariables.SimiliarListSize + incrementor; i++)
                            randNumbers.Add(ids[r.Next(0, ids.Count())]);

                        return _context.Books.Include(a => a.Genre).Where(x => randNumbers.Contains(x.Id)).ToList<Entity>();
                    }
                case ElementType.Game:
                    {
                        var ids = _context.Games.Where(x => x.Genre.Id == genre.Id && x.Id != id).Select(x => x.Id).ToList();
                        if (ids.Count() < GlobalVariables.SimiliarListSize + incrementor)
                            return null;

                        List<int> randNumbers = new List<int>();

                        Random r = new Random();

                        for (int i = 0; i < GlobalVariables.SimiliarListSize + incrementor; i++)
                            randNumbers.Add(ids[r.Next(0, ids.Count())]);

                        return _context.Games.Include(a => a.Genre).Where(x => randNumbers.Contains(x.Id)).ToList<Entity>();
                    }
                case ElementType.Movie:
                    {
                        var ids = _context.Movies.Where(x => x.Genre.Id == genre.Id && x.Id != id).Select(x => x.Id).ToList();
                        if (ids.Count() < GlobalVariables.SimiliarListSize + incrementor)
                            return null;

                        List<int> randNumbers = new List<int>();

                        Random r = new Random();

                        for (int i = 0; i < GlobalVariables.SimiliarListSize + incrementor; i++)
                            randNumbers.Add(ids[r.Next(0, ids.Count())]);

                        return _context.Movies.Include(a => a.Genre).Where(x => randNumbers.Contains(x.Id)).ToList<Entity>();
                    }
                default:
                    return null;
            }
        }
        */
        /*
        [AllowAnonymous]
        public ActionResult Index(ElementType elementType)
        {
            var items = GetEntities(elementType, null);

            items.Reverse();

            var result = MapEntitiesToViewModels(items, elementType);
            
            return View(string.Format("{0}Index", System.Enum.GetName(typeof(ElementType), elementType).ToString()), result);
        }

        [AllowAnonymous]
        public ActionResult Details(int id, ElementType elementType)
        {
            var element = GetEntity(id, elementType, null);

            if (element == null)
                return HttpNotFound();

            var view = MapEntityToViewModel(element, elementType);

            if (User.Identity.IsAuthenticated)
            {
                var user = GetUserData();

                view.YourScore = user.Scores.First(x => x.Id == element.Id && x.Entity.ElementType == elementType).Value;
            }

            //making list of similiar books 1/same author 3/same genre
            var SimiliarList = new List<SimiliarEntityMini>();

            string maker = null;
            maker = (element as Book).Author;
            maker = maker == null ? (element as Game).Studio : null;
            maker = maker == null ? (element as Movie).Director : null;

            var entity = SameAuthorEntity(id, maker, elementType);
            var entities = SameGenreEntities(id, element.Genre, entity == null ? 1 : 0, elementType);

            if(entities != null)
            {
                if (entity != null)
                    entities.Add(entity);

                foreach(var item in entities)
                {
                    SimiliarList.Add(new SimiliarEntityMini() { Id = item.Id, ImageId = item.ImageId, Title = item.Title });
                }

                view.SimiliarEntities = SimiliarList;
            }

            var comments = _context.Comments.Where(x => x.ElementType == elementType && x.Entity.Id == id).ToList();
            
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

            return View(string.Format("{0}Details", System.Enum.GetName(typeof(ElementType), elementType).ToString()), view);
        }

        public ActionResult New(ElementType elementType)
        {
            var userData = GetUserData();

            if (userData != null && userData.AccountState == AccountState.Active)
            {
                var viewModel = new EntityViewModelFormProperties()
                {
                    Id = 0,
                    Genres = GetGenres(elementType)
                };

                return View(string.Format("_{0}FormModal",System.Enum.GetName(typeof(ElementType), elementType).ToString()), viewModel);
            }
            else
                return RedirectToAction("Index", "UserProfile");
        }

        public ActionResult Delete(int id, ElementType elementType)
        {
            RemoveEntity(id, elementType);

            _context.SaveChanges();

            TempData.Add("success", "Entity Successfully Deleted");

            return RedirectToAction("Index", "Books");
        }

        public ActionResult Edit(int id, ElementType elementType)
        {
            var entity = GetEntity(id, elementType, null);

            var viewModel = MapEntityToViewModel(entity, elementType);

            return View(string.Format("_{0}FormModal", System.Enum.GetName(typeof(ElementType), elementType).ToString()), viewModel);
        }

        [HttpPost]
        public ActionResult Update(EntityViewModelFormProperties form, ElementType elementType, HttpPostedFileBase UploadImage)
        {
            if (!ModelState.IsValid)
            {
                return View(string.Format("_{0}FormModal", System.Enum.GetName(typeof(ElementType), elementType).ToString()), form);
            }

            if (CheckIfEntityExistByTitle(form.Title, elementType))
            {
                TempData.Add("fail", "Entity with this title already in base");

                return RedirectToAction("Index", "UserProfile");
            }

            var imageHandler = new ImageHandler();

            int imageId = imageHandler.AddImage(UploadImage);

            if (form.Id == 0)
            {
                var userID = User.Identity.GetUserId();

                var userId = _context.UserDatas.First(x => x.UserId.Equals(userID)).Id;

                if (form is BookFormViewModel)
                    userId = 9;
                Book entity = null;// MapViewModelToEntity(form, elementType);
                
                entity.CreateById = userId;
                entity.ImageId = imageId;

                if (String.IsNullOrWhiteSpace(form.Description))
                    entity.Description = GlobalVariables.EmptyDescription;

                TempData.Add("success", "Entity Successfully Added To Base");

                AddEntity(entity, elementType);
                _context.SaveChanges();
            }
            else
            {
                var entity = GetEntity(form.Id, elementType, null);

                Book formEntity = null; //MapViewModelToEntity(form, elementType);

                foreach(var property in formEntity.GetType().GetProperties())
                {
                    entity.GetType().GetProperty(property.Name).SetValue(entity, property.GetValue(formEntity));
                }

                if (entity.ImageId == GlobalVariables.DefaultImageId)
                    entity.ImageId = imageId;
                else
                {
                    _context.Files.Remove(_context.Files.Single(x => x.Id == entity.ImageId));

                    entity.ImageId = imageId;
                }

                TempData.Add("success", "Entity Successfully Updated");
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index", "UserProfile");
        }*/
    }
}