using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites;

public class SLASetting : BaseAuditableEntity<int>
{
    public string Priorty {  get; set; }
    public int ResponseTimeHours { get; set; }
    public int ResolutionTimeHours { get; set; }
}
