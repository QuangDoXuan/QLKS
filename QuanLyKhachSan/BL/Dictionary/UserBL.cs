using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserBL : IUserRepository<ApplicationUser>
    {
        private HotelManageDbContext db = new HotelManageDbContext();
        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users.ToList();
        }
    }
}
