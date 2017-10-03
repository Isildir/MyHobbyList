using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class GameFormViewModel : ViewModelSharedProperties
    {
        public IEnumerable<GameGenre> GameGenres = new ApplicationDbContext().GameGenres.ToList();

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int GameGenreId { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        
        public string Studio { get; set; }

        public int ImageId { get; set; }
    }
}