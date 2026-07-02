using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Ticket : BaseAuditableEntity
{
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public string Status { get; set; }
    public string TicketPriorty {  get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public Customer? customer { get; set; }

    [ForeignKey("AssignedemplyoeeId")]
    public int AssignedemplyoeeId {  get; set; }
    public Employee? employee { get; set; }

    public ICollection<TicketReply> replies { get; set; }
    public ICollection<TicketAttechment> attechments { get; set; }
    public ICollection<TicketHistory> ticketHistories { get; set; }
}
