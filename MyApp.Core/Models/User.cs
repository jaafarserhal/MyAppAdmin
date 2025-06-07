using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

/// <summary>
/// Application users with location tracking
/// </summary>
public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    /// <summary>
    /// User current latitude for nearby searches
    /// </summary>
    public decimal? CurrentLatitude { get; set; }

    /// <summary>
    /// User current longitude for nearby searches
    /// </summary>
    public decimal? CurrentLongitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
