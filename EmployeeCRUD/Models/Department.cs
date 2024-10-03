using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUD.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}