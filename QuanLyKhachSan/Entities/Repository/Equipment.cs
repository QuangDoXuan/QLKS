namespace Entities.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoomID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid DeviceID { get; set; }

        public int? Quantity { get; set; }

        public virtual Device Device { get; set; }

        public virtual Room Room { get; set; }
    }
}
