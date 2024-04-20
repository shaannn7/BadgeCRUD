using System.ComponentModel.DataAnnotations;

namespace BadgeCRUD.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public int EmployeeAge { get; set; }
        [Required]
        public int EmployeeSalary { get; set; }
    }
}
