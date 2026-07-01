using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Manager : BaseAuditableEntity
{
    [ForeignKey("EmployeeId")]
    public int EmployeeId { get; set; }
    public Employee employee { get; set; }

    [ForeignKey("DepartmentId")]
    public int DepartmentId { get; set; }
    public Department department { get; set; }

}
