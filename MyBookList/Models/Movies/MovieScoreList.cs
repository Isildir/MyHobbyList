using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models.Movies
{
    public class MovieScoreList
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MovieId { get; set; }

        public short Score { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}