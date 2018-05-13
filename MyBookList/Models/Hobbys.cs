using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public class Book : Entity
    {
        [StringLength(100)]
        [Required]
        public string Author { get; set; }
    }

    public class Game : Entity
    {
        [StringLength(100)]
        public string Studio { get; set; }
    }

    public class Movie : Entity
    {
        [StringLength(100)]
        public string Director { get; set; }
    }
}