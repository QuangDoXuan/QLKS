using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CustomerBL:ICustomerRepository<Customer>
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Customer> GetAll()
        {
            return db.Customers.ToList();
        }
        public Customer GetDetail(Guid id)
        {
            return db.Customers.FirstOrDefault(x => x.CustomerID == id);
        }
        public int Create(Customer customer)
        {

            if (customer != null)
            {
                customer.CustomerID = Guid.NewGuid();
                db.Customers.Add(customer);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Update(Guid id, Customer customer)
        {
            var newCustomer = db.Customers.FirstOrDefault(x => x.CustomerID == id);
            if (customer != null && newCustomer != null)
            {
                newCustomer.Name = customer.Name;
                newCustomer.IdentityCard = customer.IdentityCard;
                newCustomer.CustomerNo = customer.CustomerNo;
                newCustomer.Nationality = customer.Nationality;
                newCustomer.Phone_Fax = customer.Phone_Fax;
                newCustomer.Gender = customer.Gender;
                newCustomer.Address = customer.Address;
                newCustomer.Email = customer.Email;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var customer = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                db.Customers.Remove(customer);
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
                    var customer = db.Customers.FirstOrDefault(x => x.CustomerID == item);
                    db.Customers.Remove(customer);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<Customer> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Customers.OrderBy(t => t.CustomerID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.Customers.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Customer>
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
