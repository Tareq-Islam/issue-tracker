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

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Issue).WithMany(p => p.Assignees)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK_Assignee_Issues");

            entity.HasOne(d => d.User).WithMany(p => p.Assignees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Assignee_Users");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<CauseFinding>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentText)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("CommentText");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Assignee).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK_Comments_Assignee");

            entity.HasOne(d => d.Issue).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK_Comments_Issues");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Issues)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_Category");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Issues)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_Vendor");
        });

        modelBuilder.Entity<IssueCauseFindingsMapping>(entity =>
        {
            entity.ToTable("IssueCauseFindingsMapping");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CauseFinding).WithMany(p => p.IssueCauseFindingsMappings)
                .HasForeignKey(d => d.CauseFindingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IssueCauseFindingsMapping_CauseFindings");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueCauseFindingsMappings)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IssueCauseFindingsMapping_Issues");
        });

        modelBuilder.Entity<IssueSolutionTagMapping>(entity =>
        {
            entity.ToTable("IssueSolutionTagMapping");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueSolutionTagMappings)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IssueSolutionTagMapping_Issues");

            entity.HasOne(d => d.SolutionTag).WithMany(p => p.IssueSolutionTagMappings)
                .HasForeignKey(d => d.SolutionTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IssueSolutionTagMapping_SolutionTag");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
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

            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
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
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
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
