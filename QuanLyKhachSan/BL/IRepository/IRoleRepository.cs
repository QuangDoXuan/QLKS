using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IRoleRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetDetail(Guid id);
        int CreateRole(IdentityRole role);
        int DeleteRole(Guid id);
    }
}
