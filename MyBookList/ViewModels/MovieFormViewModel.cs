using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class MovieFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<MovieGenre> MovieGenre { get; set; }

        public Movie Movie { get; set; }
    }
}