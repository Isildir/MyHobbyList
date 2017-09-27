using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyBookList.Models;
using MyBookList.ViewModels;
using MyBookList.FunctionalClasses;
using AutoMapper;
using MyBookList.ViewModels.Games;

namespace MyBookList.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {

        private ApplicationDbContext _context;

        public GamesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Games
        [AllowAnonymous]
        public ActionResult Index()
        {
            var games = new List<Game>();

            games = _context.Games.Include(m => m.GameGenre).OrderBy(x => x.AverageScore).ToList();

            games.Reverse();
            
            var currentUserId = User.Identity.GetUserId();

            var view = new List<GameIndexViewModel>();

            foreach (var game in games)
            {
                view.Add(Mapper.Map<Game, GameIndexViewModel>(game));
            }

            return View(view);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var game = _context.Games.Include(m => m.GameGenre).SingleOrDefault(m => m.Id == id);

            var view = Mapper.Map<Game, GamesDetailsViewModel>(game);

            if (User.Identity.IsAuthenticated)
            {
                var score = _context.GameScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.GameId == game.Id);

                if (score != null)
                {
                    view.YourScore = score.Score;
                }
                else
                {
                    view.YourScore = 0;
                }
                view.CanEdit = true ? game.AddedByUserId == currentUserId : false;
                view.InUse = true ? _context.UserGamesLists.Any(x => x.GameId == game.Id) : false;
                view.IsAdded = true ? _context.UserGamesLists.Any(x => x.GameId == game.Id && x.UserId == currentUserId) : false;
            }

            var totalGamesNum = _context.Games.Count(m => m.GameGenreId == game.GameGenreId);

            var totalThisAuthorGamesNum = _context.Games.Count(m => m.Studio == game.Studio);

            var SimiliarList = new List<SimiliarGameMini>();

            if (totalThisAuthorGamesNum > 1 && totalGamesNum > GlobalVariables.SimiliarListSize)
            {
                Random r = new Random();

                var nextNum = r.Next(0, totalThisAuthorGamesNum);

                var items = _context.Games.Where(m => m.Studio == game.Studio).ToList();

                var item = items.ElementAt(nextNum);

                if (!SimiliarList.Exists(m => m.Title == item.Studio))
                {
                    SimiliarList.Add(new SimiliarGameMini()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ImageId = item.ImageId
                    });
                }
            }

            if (totalGamesNum > GlobalVariables.SimiliarListSize)
            {
                while (SimiliarList.Count < GlobalVariables.SimiliarListSize)
                {
                    Random r = new Random();

                    var nextNum = r.Next(0, totalGamesNum);

                    var items = _context.Games.Where(m => m.GameGenreId == game.GameGenreId).ToList();

                    var item = items.ElementAt(nextNum);

                    if (!SimiliarList.Exists(m => m.Title == item.Studio))
                    {
                        SimiliarList.Add(new SimiliarGameMini()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ImageId = item.ImageId
                        });
                    }
                }
            }

            view.SimiliarGames = SimiliarList;

            return View(view);
        }

        public ActionResult Delete(int id)
        {
            var game = _context.Games.Single(x => x.Id == id);

            _context.Games.Remove(game);

            _context.SaveChanges();

            TempData.Add("success", "Game Successfully Deleted");
            return RedirectToAction("Index", "Games");
        }
        
        public ActionResult New()
        {
            var currentUser = User.Identity.GetUserName();

            if (_context.BannedUsers.SingleOrDefault(m => m.UserId == currentUser) == null)
            {
                var gameGenres = _context.GameGenres.ToList();
                var viewModel = new GameFormViewModel()
                {
                    Id = 0
                };

                return PartialView("_GameFormModal", viewModel);
            }
            else
            {
                return RedirectToAction("Index", "UserProfile");
            }
        }

        [HttpPost]
        public ActionResult Update(GameFormViewModel gameForm, HttpPostedFileBase UploadImage)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new HttpNotFoundResult();
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_GameFormModal", gameForm);
            }

            var imageHandler = new ImageHandler();

            int imageId = imageHandler.AddImage(UploadImage);

            if (gameForm.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                var game = Mapper.Map<GameFormViewModel, Game>(gameForm);

                game.AddedByUserId = userId;
                game.ImageId = imageId;

                TempData.Add("success", "Game Successfully Added To Base");

                _context.Games.Add(game);
            }
            else
            {
                var gameInDb = _context.Games.Single(m => m.Id == gameForm.Id);

                gameInDb.Title = gameForm.Title;
                gameInDb.Studio = gameForm.Studio;
                gameInDb.ReleaseDate = gameForm.ReleaseDate;
                gameInDb.Description = gameForm.Description;
                gameInDb.GameGenreId = gameForm.GameGenreId;
                if (gameInDb.ImageId == GlobalVariables.DefaultImageId)
                {
                    gameInDb.ImageId = imageId;
                }
                TempData.Add("success", "Game Successfully Updated");
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "UserProfile");
        }


        public EmptyResult AddScore(int id, short score)
        {
            var currentUserId = User.Identity.GetUserId();

            var currentScore = _context.GameScoreLists.SingleOrDefault(m => m.UserId == currentUserId && m.GameId == id);

            var game = _context.Games.Single(m => m.Id == id);

            if (currentScore == null)
            {
                _context.GameScoreLists.Add(new Models.User.GameScoreList()
                {
                    GameId = id,
                    UserId = currentUserId,
                    Score = score
                });


                game.NumberOfVoters++;
                game.AverageScore = ((game.AverageScore * (game.NumberOfVoters - 1)) + score) / game.NumberOfVoters;
            }
            else
            {
                game.AverageScore = ((game.AverageScore * game.NumberOfVoters) + (score - currentScore.Score)) / game.NumberOfVoters;

                currentScore.Score = score;
            }

            _context.SaveChanges();

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var game = _context.Games.SingleOrDefault(m => m.Id == id);

            if (game == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Game, GameFormViewModel>(game);
            
            return PartialView("_GameFormModal", viewModel);
        }
    }
}


                