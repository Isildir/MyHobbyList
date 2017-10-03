using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;
using MyBookList.ViewModels.Movies;
using MyHobbyList.ViewModels.User;

namespace MyBookList.ViewModels
{
    public class MovieDetailsViewModel : ViewModelSharedProperties
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public MovieGenre MovieGenre { get; set; }

        public string Director { get; set; }

        public int ImageId { get; set; }

        public string Description { get; set; }

        public IEnumerable<SimiliarMovieMini> SimiliarMovies { get; set; }

        public double YourScore { get; set; } = 0;

        public long NumberOfVoters { get; set; } = 0;

        public IEnumerable<CommentViewModel> comments { get; set; }
    }
}