using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmployeeBL:IEmployeeRepository<Employee>
    {
        private HotelManageDbContext db = new HotelManageDbContext();

        //private readonly DbSet<T> _dbSet;
        public IEnumerable<Employee> GetAll()
        {
            return db.Employees.ToList();
        }
        public Employee GetDetail(Guid id)
        {
            return db.Employees.FirstOrDefault(x => x.EmployeeID == id);
        }
        public int Create(Employee employee)
        {

            if (employee != null)
            {
                employee.EmployeeID = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Update(Guid id, Employee employee)
        {
            var newEmployee = db.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (employee != null && newEmployee != null)
            {
                newEmployee.Name = employee.Name;
                newEmployee.Phone = employee.Phone;
                newEmployee.EmployeeNo = employee.EmployeeNo;
                newEmployee.Email = employee.Email;
                newEmployee.IdentityCard = employee.IdentityCard;
                newEmployee.Gender = employee.Gender;
                newEmployee.Address = employee.Address;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Delete(Guid id)
        {
            if (id != null)
            {
                var employee = db.Employees.FirstOrDefault(x => x.EmployeeID == id);
                db.Employees.Remove(employee);
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
                    var employee = db.Employees.FirstOrDefault(x => x.EmployeeID == item);
                    db.Employees.Remove(employee);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public PagedResults<Employee> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = db.Employees.OrderBy(t => t.EmployeeID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = db.Employees.ToList().Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Employee>
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
