using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels.User
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        
        public string CommentData { get; set; }
        
        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public List<string> LikeUserNames { get; set; }
    }
}