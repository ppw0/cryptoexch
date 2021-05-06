using System.Data.Entity;

namespace Cryptoexch.Models
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext()
            : base("name=EmployeeContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Incidents)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.AssignedTo);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Topics)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Topics1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.EditedBy);

            modelBuilder.Entity<Incident>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Incident>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Incident>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.Content)
                .IsUnicode(false);
        }
    }
}