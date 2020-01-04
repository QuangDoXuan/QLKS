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
        public string Create(BookRoom bookRoom)
        {
            if (bookRoom != null)
            {
                bookRoom.BookRoomID = Guid.NewGuid();
                db.BookRooms.Add(bookRoom);
                db.SaveChanges();
               
                return bookRoom.BookRoomID.ToString();
            }
            return null;
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


        public PagedResults<BookRoomViewModel> Search(int pageNumber, int pageSize, Guid? customerId, string bookNo)
        {
            var skipAmount = pageSize * pageNumber;
            var list = (from bookRoom in db.BookRooms 
                        join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                        where (bookNo != null ? bookRoom.BookRoomNo.Contains(bookNo) : true)
                        && (customerId != null ? c.CustomerID == customerId : true)
                        select new
                        {
                            BookRoomId = bookRoom.BookRoomID,
                            BookRoomNo = bookRoom.BookRoomNo,
                            StartDate = bookRoom.StartDate,
                            EndDate = bookRoom.EndDate,
                            CreateDate = bookRoom.CreateDate,
                            IsCancel = bookRoom.IsCancel,
                            Deposit = bookRoom.Deposit,
                            Status = bookRoom.Status,
                            Customer = c,

                            //Rooms = (from d in db.BookRoomDetails
                            //         join r in db.Rooms on d.RoomID equals r.RoomID
                            //         select r).ToList(),
                            Rooms = (from d in db.BookRoomDetails
                                     where d.BookRoomID == bookRoom.BookRoomID
                                     join r in db.Rooms on d.RoomID equals r.RoomID
                                     select r).ToList()
                        }).ToList().Select(p => new BookRoomViewModel()

                        {
                            BookRoomID = p.BookRoomId,
                            BookRoomNo = p.BookRoomNo,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            CreateDate = p.CreateDate,
                            IsCancel = p.IsCancel,
                            Deposit = p.Deposit,
                            Status = p.Status,
                            //Role = string.Join(",", p.RoleNames),
                            Customer = p.Customer,
                            Rooms = p.Rooms,

                        }).Skip(skipAmount).Take(pageSize);

            var results = list.ToList();

            var listAll = (from bookRoom in db.BookRooms
                           join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                           select new
                           {
                               BookRoomId = bookRoom.BookRoomID,
                               BookRoomNo = bookRoom.BookRoomNo,
                               StartDate = bookRoom.StartDate,
                               EndDate = bookRoom.EndDate,
                               CreateDate = bookRoom.CreateDate,
                               IsCancel = bookRoom.IsCancel,
                               Deposit = bookRoom.Deposit,
                               Status = bookRoom.Status,
                               Customer = c,

                               Rooms = (from d in db.BookRoomDetails
                                        join r in db.Rooms on d.RoomID equals r.RoomID
                                        select r).ToList(),
                               //Rooms = (from d in db.BookRoomDetails where d.BookRoomID == bookRoom.BookRoomID
                               //         join r in db.Rooms on d.RoomID equals r.RoomID
                               //         select r).ToList()
                           }).ToList().Select(p => new BookRoomViewModel()

                           {
                               BookRoomID = p.BookRoomId,
                               BookRoomNo = p.BookRoomNo,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               CreateDate = p.CreateDate,
                               IsCancel = p.IsCancel,
                               Deposit = p.Deposit,
                               Status = p.Status,
                               //Role = string.Join(",", p.RoleNames),
                               Customer = p.Customer,
                               Rooms = p.Rooms,

                           });
            var totalNumberOfRecords = listAll.Count();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);


            return new PagedResults<BookRoomViewModel>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }


        public PagedResults<BookRoomViewModel> GetAllBookRooms(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;
            var list = (from bookRoom in db.BookRooms join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                                  select new
                                  {
                                      BookRoomId = bookRoom.BookRoomID,
                                      BookRoomNo = bookRoom.BookRoomNo,
                                      StartDate = bookRoom.StartDate,
                                      EndDate = bookRoom.EndDate,
                                      CreateDate = bookRoom.CreateDate,
                                      IsCancel = bookRoom.IsCancel,
                                      Deposit = bookRoom.Deposit,
                                      Status = bookRoom.Status,
                                      Customer =c ,

                                      Rooms = (from d in db.BookRoomDetails
                                               join r in db.Rooms on d.RoomID equals r.RoomID
                                               select r).ToList(),
                                      //Rooms = (from d in db.BookRoomDetails where d.BookRoomID == bookRoom.BookRoomID
                                      //         join r in db.Rooms on d.RoomID equals r.RoomID
                                      //         select r).ToList()
                                  }).ToList().Select(p => new BookRoomViewModel()

                                  {
                                      BookRoomID = p.BookRoomId,
                                      BookRoomNo = p.BookRoomNo,
                                      StartDate = p.StartDate,
                                      EndDate = p.EndDate,
                                      CreateDate = p.CreateDate,
                                      IsCancel = p.IsCancel,
                                      Deposit = p.Deposit,
                                      Status = p.Status,
                                      //Role = string.Join(",", p.RoleNames),
                                      Customer = p.Customer,
                                      Rooms = p.Rooms,
                                    
                                  }).Skip(skipAmount).Take(pageSize);

            var results = list.ToList();

            var listAll = (from bookRoom in db.BookRooms
                           join c in db.Customers on bookRoom.CustomerID equals c.CustomerID
                           select new
                           {
                               BookRoomId = bookRoom.BookRoomID,
                               BookRoomNo = bookRoom.BookRoomNo,
                               StartDate = bookRoom.StartDate,
                               EndDate = bookRoom.EndDate,
                               CreateDate = bookRoom.CreateDate,
                               IsCancel = bookRoom.IsCancel,
                               Deposit = bookRoom.Deposit,
                               Status = bookRoom.Status,
                               Customer = c,

                               Rooms = (from d in db.BookRoomDetails
                                        join r in db.Rooms on d.RoomID equals r.RoomID
                                        select r).ToList(),
                               //Rooms = (from d in db.BookRoomDetails where d.BookRoomID == bookRoom.BookRoomID
                               //         join r in db.Rooms on d.RoomID equals r.RoomID
                               //         select r).ToList()
                           }).ToList().Select(p => new BookRoomViewModel()

                           {
                               BookRoomID = p.BookRoomId,
                               BookRoomNo = p.BookRoomNo,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               CreateDate = p.CreateDate,
                               IsCancel = p.IsCancel,
                               Deposit = p.Deposit,
                               Status = p.Status,
                               //Role = string.Join(",", p.RoleNames),
                               Customer = p.Customer,
                               Rooms = p.Rooms,

                           });
            var totalNumberOfRecords = listAll.Count();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);


            return new PagedResults<BookRoomViewModel>
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
