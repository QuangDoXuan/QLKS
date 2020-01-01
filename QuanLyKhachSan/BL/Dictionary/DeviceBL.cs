using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DeviceBL
    {

        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Device> GetAll()
        {
            return db.Devices.ToList();
        }
        public Device GetDetail(Guid id)
        {
            return db.Devices.FirstOrDefault(x => x.DeviceID == id);
        }
        public int Create(Device device)
        {

            if (device != null)
            {
                device.DeviceID = Guid.NewGuid();
                db.Devices.Add(device);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Update(Guid id, Device device)
        {
            var newDevice = db.Devices.FirstOrDefault(x => x.DeviceID == id);
            if (device != null && newDevice != null)
            {
                newDevice.DeviceName = device.DeviceName;
                newDevice.DeviceNo = device.DeviceNo;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var device = db.Devices.FirstOrDefault(x => x.DeviceID == id);
                db.Devices.Remove(device);
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
                    var device = db.Devices.FirstOrDefault(x => x.DeviceID == item);
                    db.Devices.Remove(device);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<Device> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Devices.OrderBy(t => t.DeviceID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.Devices.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Device>
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
