using Common;
using Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public ApplicationUser GetDetail(Guid id)
        {
            return db.Users.SingleOrDefault(x => x.Id == id.ToString());
        }

        public int Update(Guid id, ApplicationUser user)
        {
            if (id != null && user != null)
            {
                var userNew = db.Users.FirstOrDefault(x => x.Id == id.ToString());
                userNew.UserName = user.UserName;
                userNew.PhoneNumber = user.PhoneNumber;
                userNew.Email = user.Email;
                return 1;
            }
            return 0;
        }
        public int UpdateRole(Guid id, Guid[]roleId)
        {
            ApplicationUser model = db.Users.SingleOrDefault(x=>x.Id==id.ToString());
            if (id != null && roleId.Length >0)
            {
                foreach (var item in roleId)
                {
                    IdentityRole role = db.Roles.SingleOrDefault(x=>x.Id==item.ToString());

                    model.Roles.Add(new IdentityUserRole() { UserId = id.ToString(), RoleId = item.ToString() });
                    //model.Roles.Add(item);
                }
                db.SaveChanges();
            }
            return 0;
        }
        public int DeleteFromRoles(Guid id, Guid roleId)
        {
            ApplicationUser model = db.Users.SingleOrDefault(x => x.Id == id.ToString());

            if (model != null && roleId != null)
            {
                model.Roles.Remove(model.Roles.Single(x => x.RoleId == roleId.ToString()));
                db.SaveChanges();
                return 1;
            }
            return 0;

        }

        public int DeleteUser(Guid id)
        {
            if (id != null)
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id.ToString());
                db.Users.Remove(user);
                db.SaveChanges();
                return 1;
            }
            return 0;
            
        }
        public int DeleteUsers(Guid[] id)
        {
            if (id.Length > 0)
            {
                foreach(var item in id)
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == item.ToString());
                    db.Users.Remove(user);
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Create(UserCreateViewModel user, Guid roleId)
        {
            if (user != null && roleId!=null)
            {
                user.Roles.Add(new IdentityUserRole() { UserId = user.Id.ToString(), RoleId = roleId.ToString() });
                //db.Users.Add(user);

                //var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                //IdentityResult result = UserManager.CreateAsync(user, model.Pass);

                //if (!result.Succeeded)
                //{
                //    return GetErrorResult(result);
                //}


                db.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<UserWithRoleViewModel> UsersWithRoles()
        {
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role).ToList(),
                                      CreateDate = user.CreateDate,
                                      EmailConfirmed = user.EmailConfirmed

                                  }).ToList().Select(p => new UserWithRoleViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      //Role = string.Join(",", p.RoleNames),
                                      Roles = p.RoleNames,
                                      CreateDate = p.CreateDate,
                                      EmailConfirmed = p.EmailConfirmed
                                  }) ;


            return usersWithRoles;
        }

        public UserWithRoleViewModel SearchByName(string name)
        {

            var usersWithRoles = (from user in db.Users where user.UserName.Contains(name)
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role).ToList(),
                                      CreateDate = user.CreateDate,
                                      EmailConfirmed = user.EmailConfirmed

                                  }).ToList().Select(p => new UserWithRoleViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      //Role = string.Join(",", p.RoleNames),
                                      Roles = p.RoleNames,
                                      CreateDate = p.CreateDate,
                                      EmailConfirmed = p.EmailConfirmed
                                  });
            return usersWithRoles;
        }

        public PagedResults<UserWithRoleViewModel> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            //var list = db.Rooms.OrderBy(t => t.RoomID).Skip(skipAmount).Take(pageSize);

            var list = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role).ToList(),
                                      CreateDate = user.CreateDate,
                                      EmailConfirmed = user.EmailConfirmed

                                  }).ToList().Select(p => new UserWithRoleViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      //Role = string.Join(",", p.RoleNames),
                                      Roles = p.RoleNames,
                                      CreateDate = p.CreateDate,
                                      EmailConfirmed = p.EmailConfirmed
                                  }).Skip(skipAmount).Take(pageSize);

           

            var results = list.ToList();

            var listAll = (from user in db.Users
                           select new
                           {
                               UserId = user.Id,
                               Username = user.UserName,
                               Email = user.Email,
                               RoleNames = (from userRole in user.Roles
                                            join role in db.Roles on userRole.RoleId
                                            equals role.Id
                                            select role).ToList(),
                               CreateDate = user.CreateDate,
                               EmailConfirmed = user.EmailConfirmed

                           }).ToList().Select(p => new UserWithRoleViewModel()

                           {
                               UserId = p.UserId,
                               UserName = p.Username,
                               Email = p.Email,
                               //Role = string.Join(",", p.RoleNames),
                               Roles = p.RoleNames,
                               CreateDate = p.CreateDate,
                               EmailConfirmed = p.EmailConfirmed
                           });
            var totalNumberOfRecords = listAll.Count();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<UserWithRoleViewModel>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }

    }
}
