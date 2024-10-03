﻿using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace EmployeeCRUD.Models
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Employee> Employees{ get; set; }
		public DbSet<Department> Departments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Employee>()
				.HasOne(e => e.Department)
				.WithMany(d => d.Employees)
				.HasForeignKey(e => e.DepartmentId);
		}
	}
}

