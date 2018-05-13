using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public ElementType ElementType { get; set; }

        [Required]
        public string Name { get; set; }
    }
}