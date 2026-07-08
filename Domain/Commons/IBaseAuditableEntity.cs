using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commons;

public interface IBaseAuditableEntity
{
  
    int? CreatedBy { get; set; }
    int? UpdatedBy { get; set; }
    DateTime? CreateDate { get; set; }
    DateTime? UpdateDate { get; set; }
    bool IsActive { get; set; }
    bool IsDelete { get; set; }
}
