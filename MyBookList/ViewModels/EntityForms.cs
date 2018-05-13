using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels
{
    public class BookFormViewModel : EntityViewModelFormProperties
    {
        public string Author { get; set; }
    }
    public class GameFormViewModel : EntityViewModelFormProperties
    {
        public string Studio { get; set; }
    }

    public class MovieFormViewModel : EntityViewModelFormProperties
    {
        public string Director { get; set; }
    }
}