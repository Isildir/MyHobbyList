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
        
        protected IList<T> GetEntities<T>(string userId) where T : Entity
        {
            return !string.IsNullOrEmpty(userId) ? _context.Set<T>().Include(a => a.Genre).AsNoTracking().Where(x => x.CreateById == userId).ToList<T>() :
                _context.Set<T>().AsNoTracking().ToList<T>();
        }
    }
}