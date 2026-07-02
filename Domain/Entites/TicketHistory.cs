using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class TicketHistory : BaseAuditableEntity
{
    public string Action {  get; set; }
    public DateTime ActionDate { get; set; }

    [ForeignKey("TicketId")]
    public int TicketId { get; set; }
    public Ticket? ticket { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? user { get; set; }

}
