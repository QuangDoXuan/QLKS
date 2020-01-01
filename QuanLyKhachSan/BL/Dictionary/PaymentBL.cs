using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PaymentBL
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
                payment.PaymentID = Guid.NewGuid();
                db.Payments.Add(payment);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        //public int Update(Guid id, Payment Payment)
        //{
        //    var newPayment = db.Payments.FirstOrDefault(x => x.PaymentID == id);
        //    if (Payment != null && newPayment != null)
        //    {
        //        newPayment. = Payment.Name;
        //        newPayment.IdentityCard = Payment.IdentityCard;
        //        newPayment.PaymentNo = Payment.PaymentNo;
        //        newPayment.Nationality = Payment.Nationality;
        //        newPayment.Phone_Fax = Payment.Phone_Fax;
        //        newPayment.Gender = Payment.Gender;
        //        newPayment.Address = Payment.Address;
        //        newPayment.Email = Payment.Email;
        //        db.SaveChanges();
        //        return 1;
        //    }
        //    return 0;
        //}

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var Payment = db.Payments.FirstOrDefault(x => x.PaymentID == id);
                db.Payments.Remove(Payment);
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
                    var Payment = db.Payments.FirstOrDefault(x => x.PaymentID == item);
                    db.Payments.Remove(Payment);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
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
