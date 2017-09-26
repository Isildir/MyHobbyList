using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class RecommendedByUsers
    {
        public int Id { get; set; }

        public int FromId { get; set; }

        public int TargetId { get; set; }
    }
}