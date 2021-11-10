using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GroupMana.Models
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=model_lillabconnect")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Issue>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
