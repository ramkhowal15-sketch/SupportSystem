using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class User : BaseAuditableEntity
{
   public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password {  get; set; }
    public long PhoneNumber {  get; set; }

    //[ForeignKey("Role")]
    //public int RoleId { get; set; }
    //public Role? Role { get; set; }
    
}
