using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class TicketHistory : BaseAuditableEntity<int>
{
    public string Action {  get; set; }
    public DateTime ActionDate { get; set; }

    [ForeignKey("ticket")]
    public int TicketId { get; set; }
    public Ticket? ticket { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

}
