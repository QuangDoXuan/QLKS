using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IRoomRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetDetail(Guid id);
        int CreateRoom(T entity);
        int Update(Guid id, T entity);
        int DeleteRoom(Guid id);
        int DeleteRooms(Guid[] id);
    }
}
