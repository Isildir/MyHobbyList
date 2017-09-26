using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class UserProfileViewModel
    {
        public IEnumerable<BookIndexViewModel> BooksList { get; set; }

        public IEnumerable<MovieIndexViewModel> MoviesList { get; set; }

        public IEnumerable<GameIndexViewModel> GamesList { get; set; }

        public IEnumerable<SeriesIndexViewModel> SeriesList { get; set; }
    }
}