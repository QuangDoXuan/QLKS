﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoomSearchViewModel
    {
        public Guid RoomID { get; set; }

        [StringLength(10)]
        public string RoomNo { get; set; }

        [StringLength(100)]
        public string RoomName { get; set; }

        [StringLength(10)]
        public string NoP { get; set; }
        public int? NumPerson { get; set; }

        public decimal? Price { get; set; }

        public int? Floor { get; set; }

        [StringLength(50)]
        public string StatusStay { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public Guid? TypeRoomID { get; set; }
        public string TypeRoomNo { get; set; }

        [StringLength(100)]
        public string TypeRoomName { get; set; }

        public decimal? BasicPrice { get; set; }

    }
}
