using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class RoomTypeCreateViewModel
    {
        [StringLength(10)]
        public string TypeRoomNo { get; set; }

        [StringLength(100)]
        public string TypeRoomName { get; set; }

        public decimal? BasicPrice { get; set; }
    }
}
