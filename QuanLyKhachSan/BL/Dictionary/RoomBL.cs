using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class RoomBL:IRoomRepository<Room>
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Room> GetAll()
        {
            return db.Rooms.ToList();
        }
        public Room GetDetail(Guid id)
        {
            return db.Rooms.FirstOrDefault(x => x.RoomID == id);
        }
        public int CreateRoom(Room room)
        {
            if (room != null)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Update(Guid id,Room room)
        {
            var newRoom = db.Rooms.FirstOrDefault(x => x.RoomID == id);
            if (room != null&&newRoom!=null)
            {
                newRoom.RoomName = room.RoomName;
                newRoom.Price = room.Price;
                newRoom.RoomNo = room.RoomNo;
                newRoom.Status = room.Status;
                newRoom.StatusStay = room.StatusStay;
                newRoom.TypeRoomID = room.TypeRoomID;
                newRoom.Floor = room.Floor;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int DeleteRoom(Guid id)
        {
            if (id != null)
            {
                var roomDeleted = db.Rooms.FirstOrDefault(x => x.RoomID == id);
                db.Rooms.Remove(roomDeleted);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int DeleteRooms(Guid[] id)
        {
            if (id != null&&id.Length>0)
            {
                foreach(var item in id)
                {
                    var roomDeleted = db.Rooms.FirstOrDefault(x => x.RoomID == item);
                    db.Rooms.Remove(roomDeleted);
                }       
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<Room> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Rooms.OrderBy(t => t.RoomID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = list.Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Room>
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
