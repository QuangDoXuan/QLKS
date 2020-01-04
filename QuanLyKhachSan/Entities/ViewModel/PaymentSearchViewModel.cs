using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaymentSearchViewModel
    {
        public Guid PaymentID { get; set; }

        [StringLength(10)]
        public string PaymentNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public decimal? TotalCost { get; set; }

        public BookRoom BookRoom { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
