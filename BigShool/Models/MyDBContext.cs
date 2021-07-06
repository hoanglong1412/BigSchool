using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BigShool.Models
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
