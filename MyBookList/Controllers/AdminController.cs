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
        /*
        public ActionResult Index()
        {
            var ticketsList = _context.AdminTickets.ToList();

            var view = new List<AdminPanelIndexView>();

            foreach(var ticket in ticketsList)
            {
                view.Add(new AdminPanelIndexView()
                {
                    Id = ticket.Id,
                    TicketTitle = ticket.TicketTitle,
                    SendingUserName = ticket.SendingUserName,
                    TimeSend = ticket.TimeSend,
                });
            }

            return View(view);
        }
        
        public ActionResult TicketDetails(int id)
        {
            var ticket = _context.AdminTickets.Single(x => x.Id == id);

            var view = new AdminPanelIndexView()
            {
                Id = ticket.Id,
                SendingUserName = ticket.SendingUserName,
                TimeSend = ticket.TimeSend,
                TicketBody = ticket.TicketBody,
                TicketTitle = ticket.TicketTitle
            };

            return View(view);
        }
        
        public ActionResult UnbanUserAddPrivilage(int id)
        {
            var user = _context.BannedUsers.Single(x => x.Id == id);

            _context.BannedUsers.Remove(user);

            _context.SaveChanges();

            TempData.Add("success", "User successfully unbanned");
            return RedirectToAction("BannedUsersList");
        }
        
        public ActionResult BanUserAddPrivilage(string userName)
        {
            if (userName.Length == 0)
            {
                return RedirectToAction("Index");
            }

            if(_context.Users.Single(m => m.UserName == userName) != null)
            {
                _context.BannedUsers.Add(new BannedUser() { UserId = userName });
                
                _context.SaveChanges();
            }

            TempData.Add("success", "User " + userName + " successfully Banned");
            return RedirectToAction("BannedUsersList");
        }
        
        public ActionResult BannedUsersList()
        {
            var bannedUsers = _context.BannedUsers.ToList();

            var view = new List<AdminPanelBannedView>();

            foreach(var user in bannedUsers)
            {
                view.Add(new AdminPanelBannedView()
                {
                    Id = user.Id,
                    UserId = user.UserId
                });
            }

            return View(view);
        }
        
        public ActionResult DeleteTicket(int id)
        {
            var ticketToDelete = _context.AdminTickets.Single(x => x.Id == id);

            _context.AdminTickets.Remove(ticketToDelete);

            _context.SaveChanges();

            TempData.Add("success", "Ticket deleted");
            return RedirectToAction("Index");
        }*/
    }
}
