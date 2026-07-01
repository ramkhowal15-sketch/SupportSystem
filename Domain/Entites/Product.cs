using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Product : BaseAuditableEntity
{
    public string ProductName { get; set; }
    public int InvoiceNo { get; set; }
    public int SerialNo { get; set; }
    public int IMEINo { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? User { get; set; }

    
}
