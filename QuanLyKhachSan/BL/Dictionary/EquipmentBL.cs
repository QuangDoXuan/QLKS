using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EquipmentBL
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<EquipmentViewModel> GetAll()
        {
            var lstEquip = from e in db.Equipments
                           join r in db.Rooms on e.RoomID equals r.RoomID
                           join d in db.Devices on e.DeviceID equals d.DeviceID
                           select new EquipmentViewModel()
                           {
                               Room = r,
                               Device = d,
                               Quantity = e.Quantity

                           } ;
            return lstEquip;
        }
        //public Equipment GetDetail(Guid id)
        //{
        //    return db.Equipments.FirstOrDefault(x => x.EquipmentID == id);
        //}
        public int Create(Equipment equipment)
        {

            if (equipment != null)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }


        public PagedResults<Equipment> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Equipments.OrderBy(t => t.DeviceID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.Equipments.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Equipment>
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
