﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyHobbyList.Models.Movies
{
    public class MovieComment
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string CommentData { get; set; }

        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public virtual List<MovieLikedBy> MovieLikedBy { get; set; }
    }
}