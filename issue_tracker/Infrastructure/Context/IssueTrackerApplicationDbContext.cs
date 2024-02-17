using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class IssueTrackerApplicationDbContext : DbContext
{
    public IssueTrackerApplicationDbContext(DbContextOptions<IssueTrackerApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignee> Assignees { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CauseFinding> CauseFindings { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueCauseFindingsMapping> IssueCauseFindingsMappings { get; set; }

    public virtual DbSet<IssueSolutionTagMapping> IssueSolutionTagMappings { get; set; }

    public virtual DbSet<IssueTrack> IssueTracks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<SolutionTag> SolutionTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignee>(entity =>
        {
            entity.ToTable("Assignee");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<CauseFinding>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Comment1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("Comment");
            entity.Property(e => e.LogTime).HasColumnType("datetime");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IssueCauseFindingsMapping>(entity =>
        {
            entity.ToTable("IssueCauseFindingsMapping");
        });

        modelBuilder.Entity<IssueSolutionTagMapping>(entity =>
        {
            entity.ToTable("IssueSolutionTagMapping");
        });

        modelBuilder.Entity<IssueTrack>(entity =>
        {
            entity.ToTable("IssueTrack");

            entity.Property(e => e.Remark)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SiteName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SolutionTag>(entity =>
        {
            entity.ToTable("SolutionTag");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.LoginName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserMobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Users)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_Users_Vendor");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendor__3214EC07E2D285E4");

            entity.ToTable("Vendor");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
