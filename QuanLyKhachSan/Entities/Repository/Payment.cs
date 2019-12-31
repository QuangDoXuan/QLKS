namespace Entities.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public Guid PaymentID { get; set; }

        [StringLength(10)]
        public string PaymentNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public decimal? TotalCost { get; set; }

        public Guid? BookRoomID { get; set; }

        public virtual BookRoom BookRoom { get; set; }
    }
}
