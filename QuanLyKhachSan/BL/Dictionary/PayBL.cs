using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class PayBL
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Payment> GetAll()
        {
            return db.Payments.ToList();
        }

        public Payment GetDetail(Guid id)
        {
            return db.Payments.FirstOrDefault(x => x.PaymentID == id);
        }
        public int Create(Payment payment)
        {

            if (payment != null)
            {
                var book = db.BookRooms.FirstOrDefault(x => x.BookRoomID == payment.BookRoomID);
                var lstRoomRemake = (from b in db.BookRooms
                                     join d in db.BookRoomDetails on b.BookRoomID equals d.BookRoomID
                                     join r in db.Rooms on d.RoomID equals r.RoomID
                                     select r).ToList();
                foreach(var item in lstRoomRemake)
                {
                    var room = db.Rooms.SingleOrDefault(x => x.RoomID == item.RoomID);
                    room.StatusStay = "Trống";
                }
                db.SaveChanges();
                payment.PaymentID = Guid.NewGuid();
                payment.Date = DateTime.Now;
                db.Payments.Add(payment);
                db.SaveChanges();
                book.Status = true;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Update(Guid id, Payment payment)
        {
            var newPay = db.Payments.FirstOrDefault(x => x.PaymentID == id);
            if (payment != null && newPay != null)
            {
                payment.PaymentNo = newPay.PaymentNo;
                payment.TotalCost = newPay.TotalCost;
                payment.BookRoomID = newPay.BookRoomID;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        //public int DeleteRoom(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var roomDeleted = db.Rooms.FirstOrDefault(x => x.RoomID == id);
        //        db.Rooms.Remove(roomDeleted);
        //        db.SaveChanges();
        //        return 1;
        //    }
        //    return 0;
        //}

        public int Delete(Guid[] id)
        {
            if (id != null && id.Length > 0)
            {
                foreach (var item in id)
                {
                    var payDeleted = db.Payments.FirstOrDefault(x => x.PaymentID == item);
                    db.Payments.Remove(payDeleted);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<PaymentSearchViewModel> Search(int pageNumber, int pageSize, Guid? bookRoomId, decimal? totalCost, string paymentNo)
        {
            var skipAmount = pageSize * pageNumber;
            var list = (from pa in db.Payments join bookRoom in db.BookRooms on pa.BookRoomID equals bookRoom.BookRoomID
                        join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                        where 
                        (bookRoomId!=null? pa.BookRoomID == bookRoomId:true )&&
                        (totalCost != null ? pa.TotalCost ==totalCost : true)
                        && (paymentNo != null ? pa.PaymentNo.Contains(paymentNo) : true)
                        select new
                        {
                            PaymentID = pa.PaymentID,
                            PaymentNo = pa.PaymentNo,
                            TotalCost = pa.TotalCost,

                            BookRoom = bookRoom,
                            Customer = c,

                            Rooms = (from d in db.BookRoomDetails
                                     join r in db.Rooms on d.RoomID equals r.RoomID
                                     select r).ToList(),
                            //Rooms = (from d in db.BookRoomDetails where d.BookRoomID == bookRoom.BookRoomID
                            //         join r in db.Rooms on d.RoomID equals r.RoomID
                            //         select r).ToList()
                        }).ToList().Select(p => new PaymentSearchViewModel()

                        {
                            PaymentID = p.PaymentID,
                            PaymentNo = p.PaymentNo,
                            TotalCost = p.TotalCost,
                            BookRoom = p.BookRoom,
                            Rooms = p.Rooms,
                            //Role = string.Join(",", p.RoleNames),

                        }).Skip(skipAmount).Take(pageSize);

            var results = list.ToList();

            var listAll = (from pa in db.Payments
                           join bookRoom in db.BookRooms on pa.BookRoomID equals bookRoom.BookRoomID
                           join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                           select new
                           {
                               PaymentID = pa.PaymentID,
                               PaymentNo = pa.PaymentNo,
                               TotalCost = pa.TotalCost,

                               BookRoom = bookRoom,
                               Customer = c,

                               Rooms = (from d in db.BookRoomDetails
                                        join r in db.Rooms on d.RoomID equals r.RoomID
                                        select r).ToList(),
                               //Rooms = (from d in db.BookRoomDetails where d.BookRoomID == bookRoom.BookRoomID
                               //         join r in db.Rooms on d.RoomID equals r.RoomID
                               //         select r).ToList()
                           }).ToList().Select(p => new PaymentSearchViewModel()

                           {
                               PaymentID = p.PaymentID,
                               PaymentNo = p.PaymentNo,
                               TotalCost = p.TotalCost,
                               BookRoom = p.BookRoom,
                               Rooms = p.Rooms,
                               //Role = string.Join(",", p.RoleNames),

                           });
            var totalNumberOfRecords = listAll.Count();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);


            return new PagedResults<PaymentSearchViewModel>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }

        public PagedResults<Payment> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Payments.OrderBy(t => t.PaymentID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.Payments.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Payment>
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
