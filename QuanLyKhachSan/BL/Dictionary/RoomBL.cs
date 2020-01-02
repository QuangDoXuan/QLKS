using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public IEnumerable<Room> GetRoomEmpty()
        {
            return db.Rooms.Where(x=>x.Status=="Trống");
        }
        public Room GetDetail(Guid id)
        {
            return db.Rooms.FirstOrDefault(x => x.RoomID == id);
        }
        public int CreateRoom(Room room)
        {
            
            if (room != null)
            {
                room.RoomID = Guid.NewGuid();
                db.Rooms.Add(room);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        
        public IEnumerable<RoomSearchViewModel> Search(string searchTerm,string sortColumn, string sortOrder, int pageNumber, int pageSize, string roomNo, string roomTypeId, string status, string statusStay,int nop)
        {


            SqlParameter param1 = new SqlParameter("@SearchTerm", (object)searchTerm ?? DBNull.Value);
            SqlParameter param2 = new SqlParameter("@SortColumn", (object)sortColumn ?? DBNull.Value);
            SqlParameter param3 = new SqlParameter("@SortOrder", (object)sortOrder ?? DBNull.Value);
            SqlParameter param4 = new SqlParameter("@PageNumber", (object)pageNumber ?? DBNull.Value);

            SqlParameter param5 = new SqlParameter("@PageSize", (object)pageSize ?? DBNull.Value);
            SqlParameter param6 = new SqlParameter("@roomNo", (object)roomNo ?? DBNull.Value);
            SqlParameter param7 = new SqlParameter("@roomTypeId", (object)roomTypeId ?? DBNull.Value);
            SqlParameter param8 = new SqlParameter("@status", (object)status ?? DBNull.Value);
            SqlParameter param9 = new SqlParameter("@statusStay", (object)statusStay ?? DBNull.Value);
            SqlParameter param10 = new SqlParameter("@nop", (object)nop ?? DBNull.Value);
            //var lstPost = db.Database.SqlQuery<Post>("Search @id, @name, @req, @fromDate, @toDate", param1, param2, param3, param4, param5);
            var lstRoom = db.Database.SqlQuery<RoomSearchViewModel>("SearchAndPaging @SearchTerm, @SortColumn, @SortOrder, @PageNumber, @PageSize,  @roomNo, @roomTypeId, @status, @statusStay,@nop",param1,param2,param3,param4,param5, param6, param7, param8, param9,param10);
            return lstRoom;
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

            var totalNumberOfRecords = db.Rooms.ToList().Count();

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
