using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyBookList.Models.Books;
using MyHobbyList.Models.Books;
using WebGrease.Activities;

namespace MyBookList.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [StringLength(100)]
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

        [StringLength(800)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        [Required]
        public int ImageId { get; set; }

        public double AverageScore { get; set; }

        public long NumberOfVoters { get; set; }

        public virtual List<BookComment> BookComments { get; set; }

        public virtual List<BookScoreList> BookScoreLists { get; set; }
    }
}