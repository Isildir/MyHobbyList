using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int MovieGenreId { get; set; }

        public MovieGenre MovieGenre { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }

        public string Director { get; set; }

        public List<string> Actors { get; set; }
        
        public string AddyeByUserId { get; set; }
    }
}