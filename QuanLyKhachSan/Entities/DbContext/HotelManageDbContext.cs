using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class HotelManageDbContext : IdentityDbContext<ApplicationUser>
    {
        public HotelManageDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public HotelManageDbContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static HotelManageDbContext Create()
        {
            return new HotelManageDbContext();
        }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
