using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels.User
{
    public class UserTicketViewModel
    {
        [Required]
        public string TicketBody { get; set; }
        
        [Required]
        public string TicketTitle { get; set; }
    }
}