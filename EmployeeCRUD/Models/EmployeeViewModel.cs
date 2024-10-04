using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCRUD.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty; 

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty; 

        [DisplayName("Position")]
        public string? Position { get; set; }

        [DisplayName("Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        [DisplayName("Full Name")]
        public string FullName => $"{FirstName} {LastName}".Trim();

        [DisplayName("Department ID")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [DisplayName("Department Name")]
        public string? DepartmentName => Department?.DepartmentName; 
    }
}
