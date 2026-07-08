using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commons;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}
