using EmployeeCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); 

           
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .HasMaxLength(100);

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

        }
            
        
    }
}

