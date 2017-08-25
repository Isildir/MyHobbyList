using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class BookFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<BookGenre> BookGenres { get; set; }

        public Book Book { get; set; }
    }
}