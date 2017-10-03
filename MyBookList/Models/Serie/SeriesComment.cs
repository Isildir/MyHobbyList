using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyHobbyList.Models
{
    public class SeriesComment
    {
        public int Id { get; set; }

        public int SeriesId { get; set; }

        public string CommentData { get; set; }

        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public virtual List<SeriesLikedBy> SeriesLikedBy { get; set; }
    }
}