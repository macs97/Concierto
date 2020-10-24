namespace Concierto.Common.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public bool WasUsed { get; set; }
        public int TicketsNumber { get; set; }
    }
}
