using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

public partial class GenLookup
{
    public int LookupId { get; set; }

    public int LookupTypeId { get; set; }

    public string Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual GenLookuptype LookupType { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    public virtual ICollection<Userscode> Userscodes { get; set; } = new List<Userscode>();
}
