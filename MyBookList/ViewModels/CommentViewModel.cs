using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string CommentData { get; set; }

        public string UserLogin { get; set; }

        public DateTime DateAdded { get; set; }
    }
}