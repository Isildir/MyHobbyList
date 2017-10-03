﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models.Books
{
    public class BookLikedBy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual BookComment BookComment { get; set; }
    }
}