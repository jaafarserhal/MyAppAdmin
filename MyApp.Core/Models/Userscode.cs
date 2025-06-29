using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

public partial class Userscode
{
    public int UserCodeId { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; }

    public int? StatusLookupId { get; set; }

    public string Note { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public virtual GenLookup StatusLookup { get; set; }

    public virtual User User { get; set; }
}
