using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Dob { get; set; }
        public int Gentle { get; set; }
        public float CoefficientSalary { get; set; }
        //public Guid DepartmentId { get; set; }
        //public Department Departments { get; set; }   
    }
}
