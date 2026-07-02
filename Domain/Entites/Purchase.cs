using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Purchase : BaseAuditableEntity
{
    public int Quantity { get; set; }
    public DateOnly PurchaseDate { get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public Customer? customer { get; set; }
}
