using BL;
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace BL
{
    public class RoleBL:IRoleRepository<IdentityRole>
    {
        private HotelManageDbContext db = new HotelManageDbContext();
        public IEnumerable<IdentityRole> GetAll()
        {
            return db.Roles.ToList();
        }
        public IdentityRole GetDetail(Guid id)
        {
            return db.Roles.FirstOrDefault(x => x.Id == id.ToString());
        }
        public int CreateRole(IdentityRole role)
        {
            if (role!=null)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int DeleteRole(Guid id)
        {
            if (id != null)
            {
                var roleDeleted = db.Roles.FirstOrDefault(x => x.Id == id.ToString());
                db.Roles.Remove(roleDeleted);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

    }
}
