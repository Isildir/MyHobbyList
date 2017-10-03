using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models.Books
{
    public class BookScoreList
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public short Score { get; set; }
        
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}