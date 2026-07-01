using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Customer : BaseAuditableEntity
{
    public string CompanyName { get; set; }
    public string Address { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User user { get; set; }

    public ICollection<Purchase> purchases { get; set; }
    public ICollection<Ticket> tickets { get; set; }
}
