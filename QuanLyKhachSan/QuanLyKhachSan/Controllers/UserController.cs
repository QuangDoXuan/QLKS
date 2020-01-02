using AutoMapper;
using BL;
using Common;
using Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;


namespace QuanLyKhachSan.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private UserBL userBL = new UserBL();
        private readonly IMapper _mapper;
        private ApplicationUserManager _userManager;
        //private SignInManager<IdentityUser> _signInManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        public UserController()
        {

        }

        public UserController(UserBL userBL, IMapper mapper, ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            this.userBL = userBL;
            this._mapper = mapper;
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        #region methods

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/all")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<ApplicationUser>> response = new HttpResponseDTO<IEnumerable<ApplicationUser>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.GetAll();
            }
            catch(Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <returns></returns>
        [Route("api/User/page")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<UserWithRoleViewModel>> response = new HttpResponseDTO<PagedResults<UserWithRoleViewModel>>();
            await Task.Delay(500);
            try
            {
             
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.CreatePagedResults(pageNumber, pageSize);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }


        /// <summary>
        /// Chi tiết người dùng
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        [Route("api/User/detail")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<ApplicationUser> response = new HttpResponseDTO<ApplicationUser>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.GetDetail(id);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="name">UserName</param>
        /// <returns></returns>
        [Route("api/User/search")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult Search(string name)
        {
            HttpResponseDTO<IEnumerable<UserWithRoleViewModel>> response = new HttpResponseDTO<IEnumerable<UserWithRoleViewModel>>();

            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.SearchByName(name);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }


        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="model">Truyền Role Name vào user</param>
        /// <returns></returns>
        [Route("api/User/create")]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody]CreateBindingModel model)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                var user = new ApplicationUser() {
                    UserName = model.UserName,
                    Email = model.Email,
                    CreateDate = DateTime.Now,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, model.RoleName);
                    response.code = 0;
                    response.message = Constanst.SUCCESS;
                    response.data = 1;
                }
                else
                {
                    response.code = 1;
                    response.message = Constanst.FAIL;
                    response.data = 0;
                }

                
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);



        }

        /// <summary>
        /// sửa  quyền user
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="roleId">List RoleId</param>
        /// <returns></returns>
        [Route("api/User/updateRole")]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult UpdateUserRoles(Guid id, Guid[] roleId)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.UpdateRole(id, roleId);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("api/User/edit")]
        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IHttpActionResult> Edit([FromUri]Guid id, [FromBody]ApplicationUser user)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.Update(id, user);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }


        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <returns></returns>
        [Route("api/User/update")]
        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUser([FromUri] string Id, [FromBody] UpdateBindingModel model)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();

            try
            {
                var userNew = UserManager.FindById(Id);
                if (userNew != null) {
                    userNew.UserName = model.UserName;
                    userNew.Email = model.Email;
                    userNew.PhoneNumber = model.PhoneNumber;
                }
                var result = await UserManager.UpdateAsync(userNew);
                //if (!User.IsInRole(model.RoleName))
                //{
                //    var result2 = await UserManager.RemoveFromRoleAsync(Id,User.Identity.Name);
                //    var result3 = await UserManager.AddToRolesAsync(Id,)
                //}
                if (result.Succeeded)
                {
                    response.code = 0;
                    response.message = Constanst.SUCCESS;
                    response.data = 1;
                }
                else
                {
                    response.code = 1;
                    response.message = Constanst.FAIL;
                    response.data = 0;
                }
               
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }

        /// <summary>
        /// Xóa một quyền của user
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="roleId">RoleId</param>
        /// <returns></returns>
        [Route("api/User/delete-role")]
        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult DeleteUserRole([FromBody]Guid id, [FromBody]Guid roleId)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.DeleteFromRoles(id, roleId);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns></returns>
        [Route("api/User/delete")]
        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUser ([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.DeleteUser(id);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }


        /// <summary>
        /// Xóa nhiều người dùng
        /// </summary>
        /// <param name="id">List UserId</param>
        /// <returns></returns>
        [Route("api/User/delete-users")]
        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUsers([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.DeleteUsers(id);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng và role của user
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/get-all")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUserWithRole()
        {
            HttpResponseDTO<IEnumerable<UserWithRoleViewModel>> response = new HttpResponseDTO<IEnumerable<UserWithRoleViewModel>>();

            if (User.IsInRole("Admin"))
            {
                response.code = 4;
                response.message = Constanst.USER_NOT_ALLOWED;
                response.data = null;
            }
            else
            {
                await Task.Delay(500);
                try
                {
                    response.code = 0;
                    response.message = Constanst.SUCCESS;
                    response.data = userBL.UsersWithRoles();
                }
                catch (Exception e)
                {
                    response.code = 500;
                    response.message = Constanst.FAIL;
                    response.data = null;
                }
            }

           
            return Ok(response);
        }
        
        #endregion
    }
}
