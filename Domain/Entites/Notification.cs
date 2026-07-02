using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Notification : BaseAuditableEntity
{
    public string Title { get; set; }
    public string massage { get; set; }
    public bool isRead { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? user { get; set; }


}
