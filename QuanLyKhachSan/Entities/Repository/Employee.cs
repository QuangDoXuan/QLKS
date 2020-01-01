namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public Guid EmployeeID { get; set; }

        [StringLength(10)]
        public string EmployeeNo { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        [StringLength(12)]
        public string IdentityCard { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Position { get; set; }
    }
}
