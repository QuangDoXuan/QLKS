using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EquipmentViewModel
    {
        //public Guid EquimentID { get; set; }

        public int? Quantity { get; set; }
        public Room Room { get; set; }
        public Device Device { get; set; }
    }
}
