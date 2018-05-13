using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        [Required, StringLength(100), Index(IsUnique = true)]
        public string Title { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ReleaseDate { get; set; }

        [Required, Column(TypeName = "DateTime2")]
        public DateTime DateAdded { get; set; }

        [Required]
        public ElementType ElementType { get; set; }

        public int GenreId { get; set; }

        [Required, ForeignKey("GenreId"), Display(Name = "Gatunek")]
        public virtual Genre Genre { get; set; }

        [StringLength(800), Display(Name = "Opis")]
        public string Description { get; set; }

        public string CreateById { get; set; }

        public virtual List<Comment> Comments { get; set; }

        [Required]
        public int ImageId { get; set; }

        public double AverageScore { get; set; }

        public long NumberOfVoters { get; set; }

        public Entity()
        {
            Comments = new List<Comment>();
            DateAdded = DateTime.Now;
        }
    }

}