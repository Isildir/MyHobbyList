using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int EntityId { get; set; }
        public virtual Entity Entity { get; set; }

        public string CommentData { get; set; } 

        public string UserLogin { get; set; }

        public DateTime DateAdded { get; set; }

        public ElementType ElementType { get; set; }

        public Comment() { }

        public Comment(string userLogin, string content) : base()
        {
            UserLogin = userLogin;
            CommentData = content;
            DateAdded = DateTime.Now;
        }
    }
}