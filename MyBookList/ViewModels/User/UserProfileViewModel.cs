using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyHobbyList.Models;

namespace MyHobbyList.ViewModels
{
    public class UserProfileViewModel
    {
        public IEnumerable<BookIndexViewModel> BooksList { get; set; }

        public IEnumerable<MovieIndexViewModel> MoviesList { get; set; }

        public IEnumerable<GameIndexViewModel> GamesList { get; set; }
    }
}