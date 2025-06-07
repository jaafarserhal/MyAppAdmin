using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Models;

namespace MyApp.Core.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=my_app_db;Username=jaafarserhal;Password=ja3farser7al;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("stores_pkey");

            entity.ToTable("stores", tb => tb.HasComment("Stores owned by users with geolocation data"));

            entity.HasIndex(e => e.IsActive, "idx_stores_active");

            entity.HasIndex(e => new { e.City, e.State }, "idx_stores_city_state");

            entity.HasIndex(e => new { e.Latitude, e.Longitude }, "idx_stores_location");

            entity.HasIndex(e => e.AverageRating, "idx_stores_rating").IsDescending();

            entity.HasIndex(e => e.UserId, "idx_stores_user_id");

            entity.HasIndex(e => e.IsVerified, "idx_stores_verified");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.AverageRating)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("average_rating");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.ClosingTime).HasColumnName("closing_time");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasDefaultValueSql("'USA'::character varying")
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(false)
                .HasColumnName("is_verified");
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8)
                .HasComment("Latitude coordinate for store location")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8)
                .HasComment("Longitude coordinate for store location")
                .HasColumnName("longitude");
            entity.Property(e => e.OpeningTime).HasColumnName("opening_time");
            entity.Property(e => e.OperatingDays)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Mon-Sun'::character varying")
                .HasColumnName("operating_days");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.StoreImageUrl)
                .HasMaxLength(500)
                .HasColumnName("store_image_url");
            entity.Property(e => e.StoreName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("store_name");
            entity.Property(e => e.TotalReviews)
                .HasDefaultValue(0)
                .HasColumnName("total_reviews");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasComment("Foreign key to users table - store owner")
                .HasColumnName("user_id");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(500)
                .HasColumnName("website_url");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("zip_code");

            entity.HasOne(d => d.User).WithMany(p => p.Stores)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("stores_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Application users with location tracking"));

            entity.HasIndex(e => e.IsActive, "idx_users_active");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => new { e.CurrentLatitude, e.CurrentLongitude }, "idx_users_location");

            entity.HasIndex(e => e.Username, "idx_users_username");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentLatitude)
                .HasPrecision(10, 8)
                .HasComment("User current latitude for nearby searches")
                .HasColumnName("current_latitude");
            entity.Property(e => e.CurrentLongitude)
                .HasPrecision(11, 8)
                .HasComment("User current longitude for nearby searches")
                .HasColumnName("current_longitude");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
