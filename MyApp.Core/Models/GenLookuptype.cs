using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

public partial class GenLookuptype
{
    public int LookupTypeId { get; set; }

    public string Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<GenLookup> GenLookups { get; set; } = new List<GenLookup>();
}
