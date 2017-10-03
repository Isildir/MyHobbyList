using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models.Games
{
    public class GameScoreList
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int GameId { get; set; }

        public short Score { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}