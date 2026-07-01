using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites;

public class Department : BaseAuditableEntity
{
    public string DepartmentName { get; set; }
    public string DepartmentDescription { get; set; }

    public ICollection<Employee> employees { get; set; }
}
