using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.ViewModels
{
    public class AdminPanelIndexView
    {
        public int Id { get; set; }
        
        public string TicketBody { get; set; }
        
        public string SendingUserName { get; set; }
        
        public DateTime TimeSend { get; set; }
        
        public string TicketTitle { get; set; }
    }
}