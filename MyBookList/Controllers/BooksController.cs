using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyBookList.Models;
using MyBookList.ViewModels;
using MyBookList.FunctionalClasses;
using System.IO;
using AutoMapper;
using MyBookList.ViewModels.Books;
using MyHobbyList.ViewModels.User;
using MyHobbyList.Models.Books;
using MyBookList.Models.Books;

namespace MyBookList.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;
        
        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Books
        [AllowAnonymous]
        public ActionResult Index()
        {
            var books = new List<Book>();

            books = _context.Books.Include(m => m.BookGenre).OrderBy(x => x.AverageScore).ToList();

            books.Reverse();
            
            var view = new List<BookIndexViewModel>();

            foreach (var book in books)
            {
                view.Add(Mapper.Map<Book, BookIndexViewModel>(book));
            }
            
            return View(view);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var currentUserId = User.Identity.GetUserId();

            var book = _context.Books.Include(m => m.BookGenre).SingleOrDefault(m => m.Id == id);

            if(book == null)
            {
                return HttpNotFound();
            }
            
            var view = Mapper.Map<Book, BookDetailsViewModel>(book);
            
            if (User.Identity.IsAuthenticated)
            {
                var score = book.BookScoreLists.SingleOrDefault(m => m.UserId == currentUserId);
                
                if(score != null)
                {
                    view.YourScore = score.Score;
                }
                else
                {
                    view.YourScore = 0;
                }

                view.CanEdit = true ? book.AddedByUserId == currentUserId : false;
                view.InUse = true ? _context.UserBooksLists.Any(x => x.BookId == book.Id) : false;
                view.IsAdded = true ? _context.UserBooksLists.Any(x => x.BookId == book.Id && x.UserId == currentUserId) : false;
                
            }

            //making list of similiar books 1/same author 3/same genre
            var totalBooksNum = _context.Books.Count(m => m.BookGenreId == book.BookGenreId);

            var totalThisAuthorBooksNum = _context.Books.Count(m => m.Author == book.Author);

            var SimiliarList = new List<SimiliarBookMini>();

            if (totalThisAuthorBooksNum > 1 && totalBooksNum > GlobalVariables.SimiliarListSize)
            {
                Random r = new Random();

                var nextNum = r.Next(0, totalThisAuthorBooksNum);

                var items = _context.Books.Where(m => m.Author == book.Author).ToList();

                var item = items.ElementAt(nextNum);

                if (!SimiliarList.Exists(m => m.Title == item.Title))
                {
                    SimiliarList.Add(new SimiliarBookMini()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ImageId = item.ImageId
                    });
                }
            }

            if(totalBooksNum > GlobalVariables.SimiliarListSize)
            {
                while (SimiliarList.Count < GlobalVariables.SimiliarListSize)
                {
                    Random r = new Random();

                    var nextNum = r.Next(0, totalBooksNum);
                    
                    var items = _context.Books.Where(m => m.BookGenreId == book.BookGenreId).ToList();

                    var item = items.ElementAt(nextNum);

                    if(!SimiliarList.Exists(m => m.Title == item.Title))
                    {
                        SimiliarList.Add(new SimiliarBookMini()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            ImageId = item.ImageId
                        });
                    }
                }
            }

            //comments
            var comments = new List<CommentViewModel>();

            var DbComments = book.BookComments;

            foreach (var item in DbComments)
            {
                var namesModel = item.BookLikedBy.ToList();

                var names = new List<string>();

                foreach(var element in namesModel)
                {
                    names.Add(element.Name);
                }

                comments.Add(new CommentViewModel()
                {
                    Id = item.Id,
                    CommentData = item.CommentData,
                    UserId = item.UserId,
                    DateAdded = item.DateAdded,
                    LikeUserNames = names
                });
            }

            view.comments = comments;

            view.SimiliarBooks = SimiliarList;

            return View(view);
        }

        public ActionResult New()
        {
            var currentUser = User.Identity.GetUserName();

            if (_context.BannedUsers.SingleOrDefault(m => m.UserId == currentUser) == null)
            {
                var bookGenres = _context.BookGenres.ToList();
                var viewModel = new BookFormViewModel()
                {
                    Id = 0,
                    BookGenres = bookGenres
                };

                return PartialView("_BookFormModal", viewModel);
            }
            else
            {
                return RedirectToAction("Index","UserProfile");
            }
        }

        public ActionResult Delete(int id)
        {
            var book = _context.Books.Single(x => x.Id == id);

            _context.Books.Remove(book);
            _context.SaveChanges();

            TempData.Add("success", "Book Successfully Deleted");
            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public ActionResult Update(BookFormViewModel bookForm, HttpPostedFileBase UploadImage)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new HttpNotFoundResult();
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_BookFormModal", bookForm);
            }

            var imageHandler = new ImageHandler(); 

            int imageId = imageHandler.AddImage(UploadImage);
            
            if (bookForm.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                var book = Mapper.Map<BookFormViewModel, Book>(bookForm);

                book.AddedByUserId = userId;
                book.ImageId = imageId;
                book.BookComments = new List<BookComment>();
                book.BookScoreLists = new List<BookScoreList>();

                if (String.IsNullOrWhiteSpace(bookForm.Description))
                {
                    book.Description = GlobalVariables.EmptyDescription;
                }

                TempData.Add("success", "Book Successfully Added To Base");

                _context.Books.Add(book);
                _context.SaveChanges();
            }
            else
            {
                var bookInDb = _context.Books.Single(m => m.Id == bookForm.Id);
                
                bookInDb.Title = bookForm.Title;
                bookInDb.Author = bookForm.Author;
                bookInDb.ReleaseDate = bookForm.ReleaseDate;
                if(!String.IsNullOrWhiteSpace(bookForm.Description))
                {
                    bookInDb.Description = bookForm.Description;
                }
                bookInDb.BookGenreId = bookForm.BookGenreId;
                if(bookInDb.ImageId == GlobalVariables.DefaultImageId)
                {
                    bookInDb.ImageId = imageId;
                }
                TempData.Add("success", "Book Successfully Updated");
                _context.SaveChanges();
            }


            return RedirectToAction("Index", "UserProfile");
        }

        public EmptyResult AddScore(int id,short score)
        {   
            var currentUserId = User.Identity.GetUserId();

            var book = _context.Books.Single(m => m.Id == id);

            var currentScore = book.BookScoreLists.SingleOrDefault(m => m.UserId == currentUserId);
            
            if (currentScore == null)
            {
                book.BookScoreLists.Add(new BookScoreList()
                {
                    BookId = id,
                    UserId = currentUserId,
                    Score = score
                });
                
                book.NumberOfVoters++;
                book.AverageScore = ((book.AverageScore * (book.NumberOfVoters - 1)) + score) / book.NumberOfVoters;
            }
            else
            {
                book.AverageScore = ((book.AverageScore * book.NumberOfVoters) + (score - currentScore.Score)) / book.NumberOfVoters;
                
                currentScore.Score = score;
            }

            _context.SaveChanges();

            return new EmptyResult();
        }
        
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(m => m.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Book, BookFormViewModel>(book);

            viewModel.BookGenres = _context.BookGenres.ToList();

            return PartialView("_BookFormModal", viewModel);
        }

        public ActionResult AddComment(int id,string CommentData)
        {
            if (String.IsNullOrWhiteSpace(CommentData))
            {
                TempData.Add("fail", "You need to write something...");

                return RedirectToAction("Details/" + id, "Books");
            }
            else if(_context.Books.SingleOrDefault(x => x.Id == id) == null)
            {
                TempData.Add("fail", "No book with this id");

                return RedirectToAction("Details/" + id, "Books");
            }
            else
            {
                var comment = new BookComment()
                {
                    BookId = id,
                    CommentData = CommentData,
                    UserId = User.Identity.GetUserName(),
                    DateAdded = DateTime.Now,
                    BookLikedBy = new List<BookLikedBy>()
                };

                _context.Books.Single(x => x.Id == id).BookComments.Add(comment);
                
                _context.SaveChanges();

                TempData.Add("success", "Your comment was added");

                return RedirectToAction("Details/" + id, "Books");
            }
        }

        public ActionResult LikeComment(int id,int commentId)
        {
            var book = _context.Books.SingleOrDefault(m => m.Id == id);

            if(book == null) { return new HttpNotFoundResult(); }

            var comment = book.BookComments.SingleOrDefault(m => m.Id == commentId);

            if (comment == null) { return new HttpNotFoundResult(); }

            var user = User.Identity.GetUserName();

            if(comment.BookLikedBy.SingleOrDefault(m => m.Name == user) == null)
            {
                comment.BookLikedBy.Add(new BookLikedBy() { Name = user });
            }

            _context.SaveChanges();

            return RedirectToAction("Details/" + id, "Books");
        }
    }
}