using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class BookFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<BookGenre> BookGenres = new ApplicationDbContext().BookGenres.ToList();

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public int BookGenreId { get; set; }

        public string Description { get; set; }

        public int ImageId { get; set; }
    }
}