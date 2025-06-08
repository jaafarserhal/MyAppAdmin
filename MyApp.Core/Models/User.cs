using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string HashPassword { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Role Role { get; set; }
}
