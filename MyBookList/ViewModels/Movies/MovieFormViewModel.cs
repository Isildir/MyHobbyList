using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class MovieFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<MovieGenre> MovieGenres = new ApplicationDbContext().MovieGenres.ToList();

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int MovieGenreId { get; set; }
        
        public string Description { get; set; }

        [Required]
        public string Director { get; set; }

        public int ImageId { get; set; }
    }
}