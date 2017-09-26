using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string TicketBody { get; set; }

        [Required]
        public string SendingUserName { get; set; }
        
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime TimeSend { get; set; }

        [Required]
        public string TicketTitle { get; set; }
    }
}