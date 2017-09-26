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
        public IEnumerable<BookGenre> BookGenres { get; set; }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Data Wydania")]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public int BookGenreId { get; set; }

        public string Description { get; set; }

    }
}