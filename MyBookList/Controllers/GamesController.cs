using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;

namespace MyBookList.Controllers
{
    public class GamesController : Controller
    {

        private ApplicationDbContext _context;

        public GamesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Games
        public ActionResult Index()
        {
            var games = _context.Games.Include(m => m.GameGenre).OrderBy(x => x.Title).ToList();

            var view = new List<GameFormViewModel>();
            
            if (User.Identity.IsAuthenticated)
            {
                foreach (var game in games)
                {
                    view.Add(new GameFormViewModel
                    {
                        Game = game,
                        CanEdit = true ? game.AddedByUserId == User.Identity.GetUserId() : false,
                        InUse = true ? _context.UserGamesLists.Any(x => x.GameId == game.Id) : false
                    });
                }
            }
            else
            {
                foreach (var game in games)
                {
                    view.Add(new GameFormViewModel
                    {
                        Game = game
                    });
                }
            }

            return View(view);
        }

        public ActionResult Details(int id)
        {
            var game = _context.Games.Include(m => m.GameGenre).SingleOrDefault(m => m.Id == id);

            return View(game);
        }

        [Authorize]
        public ActionResult AddToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var game = new UserGamesList()
            {
                UserId = currentUserId,
                GameId = id
            };

            if (!_context.UserGamesLists.Any(m => m.UserId == currentUserId && m.GameId == id))
            {
                _context.UserGamesLists.Add(game);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Games");
        }


        public ActionResult New()
        {
            var gameGenres = _context.GameGenres.ToList();
            var viewModel = new GameFormViewModel()
            {
                Game = new Game(),
                GameGenre = gameGenres
            };

            return View("GameForm", viewModel);
        }

        [HttpPost]
        public ActionResult Update(Game game)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GameFormViewModel()
                {
                    Game = game,
                    GameGenre = _context.GameGenres.ToList()
                };

                return View("GameForm", viewModel);

            }

            if (game.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                game.AddedByUserId = userId;

                _context.Games.Add(game);
            }
            else
            {
                var gameInDb = _context.Games.Single(m => m.Id == game.Id);

                gameInDb.Title = game.Title;
                gameInDb.Studio = game.Studio;
                gameInDb.ReleaseDate = game.ReleaseDate;
                gameInDb.Description = game.Description;
                gameInDb.GameGenreId = game.GameGenreId;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Games");
        }

        public ActionResult Edit(int id)
        {
            var game = _context.Games.SingleOrDefault(m => m.Id == id);

            if (game == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GameFormViewModel()
            {
                Game = game,
                GameGenre = _context.GameGenres.ToList()
            };

            return View("GameForm", viewModel);
        }
    }
}