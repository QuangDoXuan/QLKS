using Entities.Repository;
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
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<BookRoom> BookRooms { get; set; }
        public virtual DbSet<TypeRoom> TypeRooms { get; set; }
    }
}
