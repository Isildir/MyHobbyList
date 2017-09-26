using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class UserMoviesList
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public bool Reccomended { get; set; } = false;

        public string RecommendingUserName { get; set; }
    }
}