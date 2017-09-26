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

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int MovieGenreId { get; set; }

        public MovieGenre MovieGenre { get; set; }

        [StringLength(800)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Director { get; set; }
        
        public string AddedByUserId { get; set; }

        [Required]
        public int ImageId { get; set; }

        public double AverageScore { get; set; }

        public long NumberOfVoters { get; set; }
    }
}