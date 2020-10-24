using Concierto.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concierto.Common.Responses
{
    public class BuyTicketResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<Ticket> Tickets { get; set; }
        public int TicketsNumber { get; set; }
    }
}
