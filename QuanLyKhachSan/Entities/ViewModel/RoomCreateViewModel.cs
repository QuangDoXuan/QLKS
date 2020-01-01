using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoomCreateViewModel
    {
        public RoomCreateViewModel()
        {

        }
        [StringLength(10)]
        public string RoomNo { get; set; }

        [StringLength(100)]
        public string RoomName { get; set; }

        [StringLength(10)]
        public string NoP { get; set; }

        public decimal? Price { get; set; }

        public int? Floor { get; set; }

        public Guid? TypeRoomID { get; set; }
    }
}
