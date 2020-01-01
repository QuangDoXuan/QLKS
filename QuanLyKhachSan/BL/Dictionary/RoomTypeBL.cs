using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class RoomTypeBL:IRoomTypeRepository<TypeRoom>
    {
        private HotelManageDbContext db = new HotelManageDbContext();
        public IEnumerable<TypeRoom> GetAll()
        {
            return db.TypeRooms.ToList();
        }
        public TypeRoom GetDetail(Guid id)
        {
            return db.TypeRooms.FirstOrDefault(x => x.TypeRoomID == id);
        }
        public int Create(TypeRoom roomType)
        {
            if (roomType != null)
            {
                db.TypeRooms.Add(roomType);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int Update(Guid id, TypeRoom room)
        {
            var newRoom = db.TypeRooms.FirstOrDefault(x => x.TypeRoomID == id);
            if (room != null && newRoom != null)
            {
                newRoom.TypeRoomName = room.TypeRoomName;
                newRoom.BasicPrice = room.BasicPrice;
                newRoom.TypeRoomNo = room.TypeRoomNo;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int Delete(Guid id)
        {
            if (id != null)
            {
                var roomDeleted = db.TypeRooms.FirstOrDefault(x => x.TypeRoomID == id);
                db.TypeRooms.Remove(roomDeleted);
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
                    var roomDeleted = db.TypeRooms.FirstOrDefault(x => x.TypeRoomID == item);
                    db.TypeRooms.Remove(roomDeleted);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<TypeRoom> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.TypeRooms.OrderBy(t => t.TypeRoomID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = list.Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<TypeRoom>
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
