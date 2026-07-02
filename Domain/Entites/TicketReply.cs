using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class TicketReply : BaseAuditableEntity
{
    public string TicketMassage { get; set; }

    [ForeignKey("TicketId")]
    public int TicketId { get; set; }
    public Ticket? ticket { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? user { get; set; }
}
