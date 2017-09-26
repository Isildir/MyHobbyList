using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookList.Models.User
{
    public class BookScoreList
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public short Score { get; set; }
    }
}