using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int GameGenreId { get; set; }

        public GameGenre GameGenre { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Studio { get; set; }
        
        public string AddedByUserId { get; set; }

        [Required]
        public int ImageId { get; set; }
    }
}