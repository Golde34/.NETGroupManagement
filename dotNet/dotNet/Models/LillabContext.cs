using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dotNet.Models
{
    public partial class LillabContext : DbContext
    {
        public LillabContext()
        {
        }

        public LillabContext(DbContextOptions<LillabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<MemberIssue> MemberIssues { get; set; }
        public virtual DbSet<Menber> Menbers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =LAPTOP-TIGB0QAG\\SQLEXPRESS; database=Lillab;uid=sa;pwd=sa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .HasColumnName("groupName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.IssueId).HasColumnName("issueId");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.Creator).HasColumnName("creator");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("dueDate");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Issues__projectI__1BFD2C07");
            });

            modelBuilder.Entity<MemberIssue>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IssueId).HasColumnName("issueId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Issue)
                    .WithMany()
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("FK__MemberIss__issue__1ED998B2");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MemberIss__userI__1DE57479");
            });

            modelBuilder.Entity<Menber>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menbers__groupId__15502E78");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Menbers__roleId__164452B1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(255)
                    .HasColumnName("projectName");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Projects__groupI__1920BF5C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("roleName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Profileimage)
                    .HasMaxLength(255)
                    .HasColumnName("profileimage");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
