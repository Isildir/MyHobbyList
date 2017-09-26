using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class SeriesFormViewModel :  ViewModelSharedProperties
    {
        public IEnumerable<SeriesGenre> SeriesGenre { get; set; }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
        
        public string Description { get; set; }

        [Required]
        public int SeriesGenreId { get; set; }

        public int NumberOfSeasons { get; set; }
    }
}