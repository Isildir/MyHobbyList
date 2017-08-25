using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class UserProfileViewModel
    {
        public IEnumerable<Book> BooksList { get; set; }

        public IEnumerable<Movie> MoviesList { get; set; }

        public IEnumerable<Game> GamesList { get; set; }

        public IEnumerable<Series> SeriesList { get; set; }
    }
}