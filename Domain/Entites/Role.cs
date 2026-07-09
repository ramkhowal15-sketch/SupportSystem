using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites;

public class Role : BaseAuditableEntity<Guid>
{
    public string RoleName {  get; set; }
    public string RoleDescription { get; set; }
    public ICollection<User> Users { get; set; }
}
