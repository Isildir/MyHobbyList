﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyHobbyList.Models;
using MyHobbyList.ViewModels;
using System.Data.Entity;
using AutoMapper;
using System;

namespace MyHobbyList.Controllers
{
    [Authorize]
    public class UserProfileController : BaseController
    {
        public UserProfileController()
        {
            _context = new ApplicationDbContext();
        }

        private Entity GetEntity(int id, ElementType elementType, string userId)
        {
            switch (elementType)
            {
                case ElementType.Book:
                    return userId != null ? _context.Books.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId) : _context.Books.Find(id);
                case ElementType.Game:
                    return userId != null ? _context.Games.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId) : _context.Games.Find(id);
                case ElementType.Movie:
                    return userId != null ? _context.Movies.Include(a => a.Genre).FirstOrDefault(x => x.Id == id && x.CreateById == userId) : _context.Movies.Find(id);
                default:
                    return null;
            }
        }

        private IList<V> MapEntitiesToIndexViewModels<T,V>(IList<T> entities)
        {
            var result = new List<V>();

            foreach(var entity in entities)
                result.Add(Mapper.Map<T, V>(entity));

            return result;
        }

        private UserData GetUserDataByEmail(string email)
        {
            return _context.UserDatas.FirstOrDefault(m => m.Email.Equals(email));
        }

        public ActionResult Index()
        {
            var userData = base.GetUserData();

            var booksList = base.GetEntities<Book>(userData.UserId);
            var moviesList = base.GetEntities<Movie>(userData.UserId);
            var gamesList = base.GetEntities<Game>(userData.UserId);

            var view = new UserProfileViewModel()
            {
                BooksList = MapEntitiesToIndexViewModels<Book, BookIndexViewModel>(booksList),
                MoviesList = MapEntitiesToIndexViewModels<Movie, MovieIndexViewModel>(moviesList),
                GamesList = MapEntitiesToIndexViewModels<Game, GameIndexViewModel>(gamesList)
            };

            return View(view);
        }

        public EmptyResult AddScore(int id, int elementTypeNumber, short score)
        {
            var user = GetUserData();
            var elementType = (ElementType)elementTypeNumber;

            var entityScore = user.Scores.FirstOrDefault(e => e.EntityId == id && e.ElementType == elementType);
            var entity = GetEntity(id, elementType, user.UserId);

            if (entity == null)
                return null;

            if (entityScore != null)
            {
                entity.AverageScore = ((entity.AverageScore * entity.NumberOfVoters) - entityScore.Value + score) / entity.NumberOfVoters;
                entityScore.Value = score;
            }
            else
            {
                user.Scores.Add(new Score()
                {
                    EntityId = id,
                    UserId = user.Id,
                    Value = score,
                    ElementType = elementType
                });

                _context.Entry(user).State = EntityState.Modified;
                entity.NumberOfVoters++;
                entity.AverageScore = ((entity.AverageScore * (entity.NumberOfVoters - 1)) + score) / entity.NumberOfVoters;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return new EmptyResult();
        }

        public ActionResult AddComment(int id, ElementType elementType, string CommentData)
        {
            if (String.IsNullOrWhiteSpace(CommentData))
                TempData.Add("fail", "You need to write something...");
            else if (_context.Books.SingleOrDefault(x => x.Id == id) == null)
                TempData.Add("fail", "No book with this id");
            else
            {
                var entity = GetEntity(id, elementType, null);
                var userName = GetUserData();

                entity.Comments.Add(new Comment(userName.Email, CommentData)
                {
                    ElementType = elementType
                });

                _context.SaveChanges();

                TempData.Add("success", "Your comment was added");
            }

            return RedirectToAction(string.Format("Details/{0}", id), System.Enum.GetName(typeof(ElementType), elementType).ToString());
        }

        /*
        public ActionResult SendTicket(UserTicketViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View("SendTicket", form);
            }

            var ticket = new Ticket()
            {
                UserId = base.GetUserData().UserId,
                TicketBody = form.TicketBody,
                TicketTitle = form.TicketTitle,
                TimeSend = DateTime.Now.Date
            };

            _context.AdminTickets.Add(ticket);

            _context.SaveChanges();

            TempData.Add("success", "Ticket has been send");

            return RedirectToAction("Index");
        }
        */

        public ActionResult AddEntityToUserBase(int id, ElementType elementType)
        {
            var user = GetUserData();
            var entity = GetEntity(id, elementType, null);

            if (user.Entities.Any(x => x.Id == id && x.ElementType == elementType))
            {
                user.Entities.Add(entity);

                _context.SaveChanges();

                TempData.Add("success", "Entity added to your base");
            }
            else
                TempData.Add("fail", "Entity already in you base");

            return RedirectToAction(string.Format("Details/{0}", id), System.Enum.GetName(typeof(ElementType), elementType).ToString());
        }

        public ActionResult DeleteEntityFromUserBase(int id, ElementType elementType)
        {
            var user = GetUserData();
            var entity = GetEntity(id, elementType, null);

            if (user.Entities.Any(x => x.Id == id && x.ElementType == elementType))
            {
                user.Entities.Remove(entity);

                _context.SaveChanges();

                TempData.Add("success", "Entity deleted from your base");
            }
            else
                TempData.Add("fail", "Something went wrong");

            return RedirectToAction(string.Format("Details/{0}", id), System.Enum.GetName(typeof(ElementType), elementType).ToString());
        }
        
        public ActionResult Recommend(string userName, Recommend model)
        {
            var targetUser = GetUserDataByEmail(userName);

            if (string.IsNullOrEmpty(userName) || targetUser == null)
            {
                TempData.Add("fail", "Wrong user email");

                return null;
            }

            var user = GetUserData();

            model.FromUserEmail = user.Email;

            targetUser.Reccomendations.Add(model);

            _context.SaveChanges();

            TempData.Add("success", System.Enum.GetName(typeof(ElementType), model.ElementType) + " sucessfully recommended");

            return RedirectToAction("Index", "UserProfile");
        }
    }
}