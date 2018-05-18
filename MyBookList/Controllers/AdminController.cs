using MyHobbyList.Models;
using MyHobbyList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHobbyList.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var users = _context.UserDatas.ToList();

            return View("UserIndex", users);
        }

        public ActionResult GetBannedUsers()
        {
            var users = _context.UserDatas.Where(x => x.AccountState == AccountState.Blocked).ToList();

            return View("UserIndex", users);
        }

        public ActionResult PendingTickets()
        {
            var tickets = _context.Tickets.ToList();

            foreach (var ticket in tickets)
                ticket.UserId = string.Empty;

            return View("Tickets", tickets);
        }
        
        public ActionResult BanUser(int userId)
        {
            _context.UserDatas.Find(userId).AccountState = AccountState.Blocked;

            _context.SaveChanges();
            TempData.Add("success", "User has been blocked");

            return RedirectToAction("GetUsers");
        }

        public ActionResult UnbanUser(int userId)
        {
            _context.UserDatas.Find(userId).AccountState = AccountState.Active;

            _context.SaveChanges();
            TempData.Add("success", "User has been unblocked");

            return RedirectToAction("GetBannedUsers");
        }

        public ActionResult RemoveTicket(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            _context.Entry(ticket).State = System.Data.Entity.EntityState.Deleted;

            _context.SaveChanges();
            TempData.Add("success", "Ticket has been deleted");

            return RedirectToAction("PendingTickets");
        }
    }
}
