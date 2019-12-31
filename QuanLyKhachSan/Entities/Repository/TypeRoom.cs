namespace Entities.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeRoom")]
    public partial class TypeRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeRoom()
        {
            Rooms = new HashSet<Room>();
        }

        public Guid TypeRoomID { get; set; }

        [StringLength(10)]
        public string TypeRoomNo { get; set; }

        [StringLength(100)]
        public string TypeRoomName { get; set; }

        public decimal? BasicPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
