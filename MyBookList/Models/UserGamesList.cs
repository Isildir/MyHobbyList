using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class UserGamesList
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}