using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class BannedUser
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}