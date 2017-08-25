using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class GameFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<GameGenre> GameGenre { get; set; }

        public Game Game { get; set; }
    }
}