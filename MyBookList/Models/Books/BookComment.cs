using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyHobbyList.Models.Books
{
    public class BookComment
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string CommentData { get; set; }

        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public virtual List<BookLikedBy> BookLikedBy { get; set; }
    }
}