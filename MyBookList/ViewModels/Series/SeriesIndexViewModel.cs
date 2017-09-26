using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class SeriesIndexViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public int NumberOfSeasons { get; set; }
        
        public int ImageId { get; set; }

        public SeriesGenre SeriesGenre { get; set; }

        public bool Recommended { get; set; }

        public string RecommenderName { get; set; }

        public double AverageScore { get; set; } = 0;
    }
}