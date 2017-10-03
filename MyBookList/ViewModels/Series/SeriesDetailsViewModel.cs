using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Migrations;
using MyBookList.Models;
using MyBookList.ViewModels.Series;
using MyHobbyList.ViewModels.User;

namespace MyBookList.ViewModels
{
    public class SeriesDetailsViewModel : ViewModelSharedProperties
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int NumberOfSeasons { get; set; }

        public int ImageId { get; set; }

        public SeriesGenre SeriesGenre { get; set; }

        public string Description { get; set; }

        public IEnumerable<SimiliarSeriesMini> SimiliarSeries { get; set; }

        public double YourScore { get; set; } = 0;

        public long NumberOfVoters { get; set; } = 0;

        public IEnumerable<CommentViewModel> comments { get; set; }
    }
}