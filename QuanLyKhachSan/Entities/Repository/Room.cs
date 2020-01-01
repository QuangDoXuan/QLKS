namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            BookRooms = new HashSet<BookRoom>();
            Equipments = new HashSet<Equipment>();
        }

        public Guid RoomID { get; set; }

        [StringLength(10)]
        public string RoomNo { get; set; }

        [StringLength(100)]
        public string RoomName { get; set; }

        [StringLength(10)]
        public string NoP { get; set; }

        public decimal? Price { get; set; }

        public int? Floor { get; set; }

        [StringLength(50)]
        public string StatusStay { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public Guid? TypeRoomID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookRoom> BookRooms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual TypeRoom TypeRoom { get; set; }
    }
}
