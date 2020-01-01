using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookRoomDetail
    {


        [Key, Column(Order = 0)]
        public Guid BookRoomID { get; set; }
        [Key, Column(Order = 1)]
        public Guid RoomID { get; set; }

        public BookRoomDetail()
        {

        }

        public BookRoomDetail(Guid BookRoomID, Guid RoomID)
        {
            this.BookRoomID = BookRoomID;
            this.RoomID = RoomID;
        }

    }
}
