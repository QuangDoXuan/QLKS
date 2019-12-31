using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IUserRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetDetail(Guid id);
        int Create(UserCreateViewModel user, Guid roleId);
        int Update(Guid id, ApplicationUser user);
        int UpdateRole(Guid id, Guid[]roleId);
        int DeleteFromRoles(Guid id, Guid roleId);
        int DeleteUser(Guid id);
        int DeleteUsers(Guid[] id);
    }
}
