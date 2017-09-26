using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class GameIndexViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public GameGenre GameGenre { get; set; }
        
        public string Studio { get; set; }
        
        public int ImageId { get; set; }

        public bool Recommended { get; set; }

        public string RecommenderName { get; set; }

        public double AverageScore { get; set; } = 0;
    }
}