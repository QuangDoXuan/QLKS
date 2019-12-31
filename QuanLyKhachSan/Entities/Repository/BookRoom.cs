namespace Entities.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookRoom")]
    public partial class BookRoom
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

        public Guid? CustomerID { get; set; }

        public Guid? RoomID { get; set; }

        public virtual Room Room { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
