using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dictionary
{
    public class EquipmentBL
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Equipment> GetAll()
        {
            return db.Equipments.ToList();
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
