using Microsoft.AspNet.Identity;
using MyHobbyList.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MyHobbyList.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext _context;

        protected UserData GetUserData()
        {
            var userID = User.Identity.GetUserId();

            return _context.UserDatas.FirstOrDefault(m => m.UserId.Equals(userID));
        }
        
        protected IList<T> GetEntities<T>(bool userOnly) where T : Entity
        {
            if(!userOnly)
                return _context.Set<T>().Include(a => a.Genre).AsNoTracking().ToList<T>();
            
            var userData = GetUserData().Entities.Where(x => x.ElementType.ToString().Equals(typeof(T).Name)).Select(x => x.Id).ToList();

            return _context.Set<T>().Include(a => a.Genre).AsNoTracking().Where(x => userData.Contains(x.Id)).ToList<T>();
        }
    }
}