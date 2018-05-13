using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels
{
    public class BookIndexViewModel : EntityViewModelIndexDetailsProperties
    {
        public string Author { get; set; }
    }

    public class GameIndexViewModel : EntityViewModelIndexDetailsProperties
    {
        public string Studio { get; set; }
    }

    public class MovieIndexViewModel : EntityViewModelIndexDetailsProperties
    {
        public string Director { get; set; }
    }

}