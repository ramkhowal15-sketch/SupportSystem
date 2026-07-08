using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Notification : BaseAuditableEntity<int>
{
    public string Title { get; set; }
    public string Massage { get; set; }
    public bool IsRead { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }


}
