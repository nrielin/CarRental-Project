using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLPhuongTienGiaoThong.Models;

public partial class HireCarContext : DbContext
{
    public HireCarContext()
    {
    }

    public HireCarContext(DbContextOptions<HireCarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBooking> TblBookings { get; set; }

    public virtual DbSet<TblCar> TblCars { get; set; }

    public virtual DbSet<TblHangXe> TblHangXes { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WIN-A6BR7503G50;Initial Catalog=Hire_Car;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__tblBooki__5DE3A5B10617D5F9");

            entity.ToTable("tblBooking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("booking_date");
            entity.Property(e => e.CarBookingId).HasColumnName("car_booking_id");
            entity.Property(e => e.RentalEndDate)
                .HasColumnType("datetime")
                .HasColumnName("rental_end_date");
            entity.Property(e => e.RentalStartDate)
                .HasColumnType("datetime")
                .HasColumnName("rental_start_date");
            entity.Property(e => e.StatusCar)
                .HasMaxLength(20)
                .HasColumnName("status_car");
            entity.Property(e => e.TotalCost)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total_cost");
            entity.Property(e => e.UserBookingId).HasColumnName("user_booking_id");
        });

        modelBuilder.Entity<TblCar>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__tblCars__4C9A0DB3E8285B71");

            entity.ToTable("tblCars");

            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.CarMake).HasColumnName("car_make");
            entity.Property(e => e.CarModel)
                .HasMaxLength(100)
                .HasColumnName("car_model");
            entity.Property(e => e.Color)
                .HasMaxLength(100)
                .HasColumnName("color");
            entity.Property(e => e.FuelType)
                .HasMaxLength(20)
                .HasColumnName("fuel_type");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(10)
                .HasColumnName("license_plate");
            entity.Property(e => e.PricePerDay).HasColumnName("price_per_day");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.Seats).HasColumnName("seats");
            entity.Property(e => e.Transmission)
                .HasMaxLength(10)
                .HasColumnName("transmission");
            entity.Property(e => e.YearProduction).HasColumnName("year_production");

            entity.HasOne(d => d.CarMakeNavigation).WithMany(p => p.TblCars)
                .HasForeignKey(d => d.CarMake)
                .HasConstraintName("FK_tblCars_tblHangXe");
        });

        modelBuilder.Entity<TblHangXe>(entity =>
        {
            entity.HasKey(e => e.CarMake);

            entity.ToTable("tblHangXe");

            entity.Property(e => e.CarMake).HasColumnName("car_make");
            entity.Property(e => e.NameMake)
                .HasMaxLength(50)
                .HasColumnName("name_make");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.IRoleId);

            entity.ToTable("tblRole");

            entity.Property(e => e.IRoleId).HasColumnName("iRoleID");
            entity.Property(e => e.SRoleName)
                .HasMaxLength(50)
                .HasColumnName("sRoleName");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUsers__B9BE370F3CF189D6");

            entity.ToTable("tblUsers");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IUserRoleId).HasColumnName("iUserRoleID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.IUserRole).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.IUserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblUser_tblRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
