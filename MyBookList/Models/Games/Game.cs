using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models.Games;
using MyHobbyList.Models.Games;

namespace MyBookList.Models
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int GameGenreId { get; set; }

        public GameGenre GameGenre { get; set; }

        [StringLength(800)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Studio { get; set; }
        
        public string AddedByUserId { get; set; }

        [Required]
        public int ImageId { get; set; }

        public double AverageScore { get; set; }

        public long NumberOfVoters { get; set; }

        public virtual List<GameComment> GameComments { get; set; }

        public virtual List<GameScoreList> GameScoreLists { get; set; }
    }
}