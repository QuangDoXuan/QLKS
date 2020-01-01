using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IEmployeeRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetDetail(Guid id);
        int Delete(Guid id);
        int Deletes(Guid[] id);
    }
}
