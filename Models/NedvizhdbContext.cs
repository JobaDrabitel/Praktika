using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Praktika.Models;

public partial class NedvizhdbContext : DbContext
{
    public NedvizhdbContext()
    {
    }

    public NedvizhdbContext(DbContextOptions<NedvizhdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<Demand> Demands { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Realty> Realties { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UP53UGO;Database=nedvizhdb;TrustServerCertificate=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasOne(d => d.Demand).WithMany(p => p.Deals)
                .HasForeignKey(d => d.DemandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deals_Demands");

            entity.HasOne(d => d.Supply).WithMany(p => p.Deals)
                .HasForeignKey(d => d.SupplyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deals_Supplies");
        });

        modelBuilder.Entity<Demand>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_City");
            entity.Property(e => e.AddressHouse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_House");
            entity.Property(e => e.AddressNumber).HasColumnName("Address_Number");
            entity.Property(e => e.AddressStreet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_Street");

            entity.HasOne(d => d.Agent).WithMany(p => p.Demands)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Demands_Agents");

            entity.HasOne(d => d.Client).WithMany(p => p.Demands)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Demands_Clients");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Area).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Realty>(entity =>
        {
            entity.ToTable("Realty");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddressCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_City");
            entity.Property(e => e.AddressHouse).HasColumnName("Address_House");
            entity.Property(e => e.AddressNumber).HasColumnName("Address_Number");
            entity.Property(e => e.AddressStreet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_Street");
            entity.Property(e => e.CoordinateLatitude).HasColumnName("Coordinate_latitude");
            entity.Property(e => e.CoordinateLongitude).HasColumnName("Coordinate_longitude");

            entity.HasOne(d => d.District).WithMany(p => p.Realties)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Realty_Districts");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Agent).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Agents");

            entity.HasOne(d => d.Client).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Clients");

            entity.HasOne(d => d.Realty).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.RealtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Realty");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
