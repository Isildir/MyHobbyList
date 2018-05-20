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
        
        protected Entity SameAuthorEntity(int id, string maker, ElementType elementType)
        {
            List<int> ids;
            int number;

            switch (elementType)
            {
                case ElementType.Book:
                    ids = _context.Books.Include(a => a.Genre).Where(x => x.Id != id && x.Author.Equals(maker)).Select(x => x.Id).ToList();
                    if (ids.Count() == 0)
                        return null;
                    number = ids[new Random().Next(0, ids.Count() - 1)];
                    return _context.Books.Include(a => a.Genre).FirstOrDefault(x => x.Id == number);
                case ElementType.Game:
                    ids = _context.Games.Include(a => a.Genre).Where(x => x.Id != id && x.Studio.Equals(maker)).Select(x => x.Id).ToList();
                    if (ids.Count() == 0)
                        return null;
                    number = ids[new Random().Next(0, ids.Count() - 1)];
                    return _context.Games.Include(a => a.Genre).FirstOrDefault(x => x.Id == number);
                case ElementType.Movie:
                    ids = _context.Movies.Include(a => a.Genre).Where(x => x.Id != id && x.Director.Equals(maker)).Select(x => x.Id).ToList();
                    if (ids.Count() == 0)
                        return null;
                    number = ids[new Random().Next(0, ids.Count() - 1)];
                    return _context.Movies.Include(a => a.Genre).FirstOrDefault(x => x.Id == number);
                default:
                    return null;
            }
        }

        protected IList<T> SameGenreEntities<T>(int id, Genre genre, int entityId) where T : Entity
        {
            var ids = _context.Set<T>().Where(x => x.Genre.Id == genre.Id && x.Id != id).Select(x => x.Id).ToList();
            

            if (entityId != 0)
                ids.Remove(entityId);

            if (ids.Count() < GlobalVariables.SimiliarListSize)
                return null;

            if (ids.Count() == GlobalVariables.SimiliarListSize)
                return _context.Set<T>().Include(a => a.Genre).Where(x => ids.Contains(x.Id)).ToList<T>();

            List<int> randNumbers = new List<int>();

            Random r = new Random();

            while(randNumbers.Count < GlobalVariables.SimiliarListSize - (entityId != 0 ? 1 : 0))
            {
                var random = r.Next(0, ids.Count() - 1);

                if (!randNumbers.Contains(ids[random]))
                    randNumbers.Add(ids[random]);
            }

            return _context.Set<T>().Include(a => a.Genre).Where(x => randNumbers.Contains(x.Id)).ToList<T>();
        }
    }
}