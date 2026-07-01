using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entites;

public class Employee : BaseAuditableEntity
{
    public int EmployeeNumber { get; set; }
    public string Position { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User user { get; set; }

    [ForeignKey("DepartmentId")]
    public int DepartmentId { get; set; }
    public Department? department { get; set; }
}
