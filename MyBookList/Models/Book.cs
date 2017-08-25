using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Data Wydania")]
        public DateTime ReleaseDate { get; set; }

        public BookGenre BookGenre { get; set; }

        [Required]
        [Display(Name = "Gatunek")]
        public int BookGenreId { get; set; } 
        
        [StringLength(255)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string AddedByUserId { get; set; }
    }
}