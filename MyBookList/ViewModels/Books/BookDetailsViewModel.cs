using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;
using MyBookList.ViewModels.Books;
using MyHobbyList.ViewModels.User;

namespace MyBookList.ViewModels
{
    public class BookDetailsViewModel : ViewModelSharedProperties
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime ReleaseDate { get; set; }

        public BookGenre BookGenre { get; set; }

        public int ImageId { get; set; }

        public string Description { get; set; }

        public IEnumerable<SimiliarBookMini> SimiliarBooks { get; set; }

        public double YourScore { get; set; } = 0;

        public long NumberOfVoters { get; set; } = 0;

        public IEnumerable<CommentViewModel> comments { get; set; }
    }
}