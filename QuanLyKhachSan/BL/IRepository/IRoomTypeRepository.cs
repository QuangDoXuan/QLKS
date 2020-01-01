using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IRoomTypeRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetDetail(Guid id);
        int Create(T entity);
        int Update(Guid id, T entity);
        int Delete(Guid id);
        int Deletes(Guid[] id);
    }
}
