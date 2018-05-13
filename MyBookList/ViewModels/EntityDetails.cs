using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels
{
    public class BookDetailsViewModel : EntityViewModelDetailsProperties
    {
        public string Author { get; set; }
    }

    public class GameDetailsViewModel : EntityViewModelDetailsProperties
    {
        public string Studio { get; set; }
    }

    public class MovieDetailsViewModel : EntityViewModelDetailsProperties
    {
        public string Director { get; set; }
    }
}