using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Product : BaseAuditableEntity<int>
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public long Price { get; set; }
    public ICollection<Purchase> purchases { get; set; }

    //[ForeignKey("UserId")]
    //public int UserId { get; set; }
    //public User? User { get; set; }

    
}
