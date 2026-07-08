using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Ticket : BaseAuditableEntity<int>
{
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public string Status { get; set; }
    public string TicketPriorty {  get; set; }

    public ICollection<TicketReply> Replies { get; set; }
    public ICollection<TicketAttechment> Attechments { get; set; }
    public ICollection<TicketHistory> TicketHistories { get; set; }
}
