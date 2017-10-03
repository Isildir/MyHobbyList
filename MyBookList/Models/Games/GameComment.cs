using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyHobbyList.Models.Games
{
    public class GameComment
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string CommentData { get; set; }

        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public virtual List<GameLikedBy> GameLikedBy { get; set; }
    }
}