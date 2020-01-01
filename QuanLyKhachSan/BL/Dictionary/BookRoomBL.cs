using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BookRoomBL
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<BookRoom> GetAll()
        {
            return db.BookRooms.ToList();
        }
        public BookRoom GetDetail(Guid id)
        {
            return db.BookRooms.FirstOrDefault(x => x.BookRoomID == id);
        }
        public int Create(BookRoom bookRoom)
        {
            if (bookRoom != null)
            {
                bookRoom.BookRoomID = Guid.NewGuid();
                db.BookRooms.Add(bookRoom);
                db.SaveChanges();
               
                return 1;
            }
            return 0;
        }

        public int CreateDetail(Guid id, Guid[] roomID)
        {
            if (id != null && roomID.Length > 0)
            {
                foreach (var item in roomID)
                {
                    BookRoomDetail newBookDetail = new BookRoomDetail(id, item);
                    db.BookRoomDetails.Add(newBookDetail);
                    db.SaveChanges();

                }
                return 1;
            }
            
            return 0;
        }

        public int Update(Guid id, BookRoom bookingRoom, Guid[]roomID)
        {
            var newBookRoom = db.BookRooms.FirstOrDefault(x => x.BookRoomID == id);
            var bookRoomDT = db.BookRoomDetails.Where(x => x.BookRoomID == id); 
            
            if (newBookRoom != null && bookingRoom != null)
            {
                newBookRoom.BookRoomNo = bookingRoom.BookRoomNo;
                newBookRoom.CreateDate = bookingRoom.CreateDate;
                newBookRoom.CustomerID = bookingRoom.CustomerID;
                newBookRoom.Deposit = bookingRoom.Deposit;
                newBookRoom.CreateDate = bookingRoom.CreateDate;
                newBookRoom.Status = bookingRoom.Status;

                foreach (var item in bookRoomDT)
                {
                    db.BookRoomDetails.Remove(item);

                }
                //this.Create(bookingRoom, roomID);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var roomDeleted = db.BookRooms.FirstOrDefault(x => x.BookRoomID == id);

                var detailDeleted = db.BookRoomDetails.Where(x => x.BookRoomID == id);
                foreach(var item in detailDeleted)
                {
                    db.BookRoomDetails.Remove(item);
                    db.SaveChanges();
                }
                db.BookRooms.Remove(roomDeleted);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Deletes(Guid[] id)
        {
            if (id != null && id.Length > 0)
            {
                foreach (var item in id)
                {
                    var roomDeleted = db.Rooms.FirstOrDefault(x => x.RoomID == item);
                    db.Rooms.Remove(roomDeleted);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<BookRoom> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.BookRooms.OrderBy(t => t.BookRoomID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.BookRooms.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<BookRoom>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }
    }
}
