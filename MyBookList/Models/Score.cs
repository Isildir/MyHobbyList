using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public class Score
    {
        public int Id { get; set; }

        public ElementType ElementType { get; set; }

        public short Value { get; set; }

        public int EntityId { get; set; }

        [ForeignKey("EntityId")]
        public virtual Entity Entity { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserData User { get; set; }
    }
}