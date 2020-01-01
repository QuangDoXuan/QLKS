using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserWithRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
