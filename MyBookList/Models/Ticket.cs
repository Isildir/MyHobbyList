using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobbyList.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string TicketTitle { get; set; }

        [Required]
        public string TicketBody { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime TimeSend { get; set; }
        
        public string UserId { get; set; }
        
        public string UserName { get; set; }

        public Ticket()
        {
            TimeSend = DateTime.Now;
        }
    }
}