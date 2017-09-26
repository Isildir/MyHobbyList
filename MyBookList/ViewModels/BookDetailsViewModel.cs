using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

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
    }
}