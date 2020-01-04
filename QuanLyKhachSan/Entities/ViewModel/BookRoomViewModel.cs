using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookRoomViewModel
    {
        public Guid BookRoomID { get; set; }

        [StringLength(10)]
        public string BookRoomNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }
        public bool? IsCancel { get; set; }
        public decimal? Deposit { get; set; }
        public bool? Status { get; set; }
        public Customer Customer{ get; set; }
        public List<Room> Rooms { get; set; }

    }
}
