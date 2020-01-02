using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomerViewModel
    {
        public Guid CustomerID { get; set; }

        [StringLength(10)]
        public string CustomerNo { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool? Gender { get; set; }

        public DateTime? Dob { get; set; }

        public string Address { get; set; }

        [StringLength(10)]
        public string Phone_Fax { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(12)]
        public string IdentityCard { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }
    }
}
