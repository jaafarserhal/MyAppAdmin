using System;
using System.Collections.Generic;

namespace MyApp.Core.Models;

/// <summary>
/// Stores owned by users with geolocation data
/// </summary>
public partial class Store
{
    public int StoreId { get; set; }

    /// <summary>
    /// Foreign key to users table - store owner
    /// </summary>
    public int UserId { get; set; }

    public string StoreName { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public string Country { get; set; }

    /// <summary>
    /// Latitude coordinate for store location
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude coordinate for store location
    /// </summary>
    public decimal Longitude { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string WebsiteUrl { get; set; }

    public string StoreImageUrl { get; set; }

    public TimeOnly? OpeningTime { get; set; }

    public TimeOnly? ClosingTime { get; set; }

    public string OperatingDays { get; set; }

    public bool? IsVerified { get; set; }

    public decimal? AverageRating { get; set; }

    public int? TotalReviews { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User User { get; set; }
}
