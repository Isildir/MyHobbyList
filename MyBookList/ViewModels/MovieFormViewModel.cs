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
        public IEnumerable<MovieGenre> MovieGenre { get; set; }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int MovieGenreId { get; set; }
        
        public string Description { get; set; }

        public string Director { get; set; }
    }
}