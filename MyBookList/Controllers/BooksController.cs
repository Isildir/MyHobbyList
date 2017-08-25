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

namespace MyBookList.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;
        
        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = _context.Books.Include(m => m.BookGenre).OrderBy(x => x.Title).ToList();
            
            var view = new List<BookFormViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                foreach (var book in books)
                {
                    view.Add(new BookFormViewModel
                    {
                        Book = book,
                        CanEdit = true ? book.AddedByUserId == User.Identity.GetUserId() : false,
                        InUse = true ? _context.UserBooksLists.Any(x => x.BookId == book.Id) : false
                    });
                }
            }
            else
            {
                foreach (var book in books)
                {
                    view.Add(new BookFormViewModel
                    {
                        Book = book
                    });
                }
            }

            return View(view);
        }

        public ActionResult Details(int id)
        {
            var books = _context.Books.Include(m => m.BookGenre).SingleOrDefault(m => m.Id == id);

            return View(books);
        }

        public ActionResult New()
        {
            var bookGenres = _context.BookGenres.ToList();
            var viewModel = new BookFormViewModel()
            {
                Book = new Book(),
                BookGenres = bookGenres
            };

            return View("BookForm",viewModel);
        }

        [Authorize]
        public ActionResult AddToUserBase(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var book = new UserBooksList()
            {
                UserId = currentUserId,
                BookId = id
            };

            if (!_context.UserBooksLists.Any(m => m.UserId == currentUserId && m.BookId == id))
            {
                _context.UserBooksLists.Add(book);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public ActionResult Update(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel()
                {
                    Book = book,
                    BookGenres = _context.BookGenres.ToList()
                };

                return View("BookForm", viewModel);

            }

            if (book.Id == 0)
            {
                var userId = User.Identity.GetUserId();

                book.AddedByUserId = userId;

                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.Single(m => m.Id == book.Id);

                bookInDb.Title = book.Title;
                bookInDb.Author = book.Author;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.Description = book.Description;
                bookInDb.BookGenreId = book.BookGenreId;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(m => m.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BookFormViewModel()
            {
                Book = book,
                BookGenres = _context.BookGenres.ToList()
            };

            return View("BookForm", viewModel);
        }
    }
}