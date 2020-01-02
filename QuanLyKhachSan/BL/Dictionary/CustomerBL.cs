using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public IEnumerable<CustomerViewModel> Search(string customerName, string customerNo, string nation, string identityCard, bool? gender, DateTime? dob, string phone, string email)
        {
            SqlParameter param1 = new SqlParameter("@customerName", (object)customerName ?? DBNull.Value);
            SqlParameter param2 = new SqlParameter("@customerNo", (object)customerNo ?? DBNull.Value);
            SqlParameter param3 = new SqlParameter("@nation", (object)nation ?? DBNull.Value);
            SqlParameter param4 = new SqlParameter("@identityCard", (object)identityCard ?? DBNull.Value);
            SqlParameter param5 = new SqlParameter("@gender", (object)gender ?? DBNull.Value);
            SqlParameter param6 = new SqlParameter("@dob", (object)dob ?? DBNull.Value);
            SqlParameter param7 = new SqlParameter("@phone", (object)phone ?? DBNull.Value);
            SqlParameter param8 = new SqlParameter("@email", (object)email ?? DBNull.Value);

            var lstCustomer = db.Database.SqlQuery<CustomerViewModel>("SearchCustomer @customerName, @customerNo, @nation, @identityCard ,@gender,@dob,@phone,@email ", param1, param2, param3, param4, param5,param6,param7,param8);
            return lstCustomer;
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
