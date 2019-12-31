using BL;
using Common;
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QuanLyKhachSan.Controllers
{
    /// <summary>
    /// Role Controller
    /// </summary>
    [EnableCors("*", "*", "*")]
    public class RoleController : ApiController
    {
        private RoleBL roleBL = new RoleBL();

        /// <summary>
        /// Danh sách role
        /// </summary>
        /// <returns></returns>
        [Route("api/Role/all")]
        //[Authorize(Roles = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<IdentityRole>> response = new HttpResponseDTO<IEnumerable<IdentityRole>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roleBL.GetAll();
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
        /// Chi tiết Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Role/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<IdentityRole> response = new HttpResponseDTO<IdentityRole>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roleBL.GetDetail(id);
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
        /// Tạo mới Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Role/create")]
        public IHttpActionResult CreateRole([FromBody]IdentityRole role)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roleBL.CreateRole(role);
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
        /// Xóa role
        /// </summary>
        /// <param name="id">RoleID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Role/delete")]
        public IHttpActionResult DeleteRole([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roleBL.DeleteRole(id);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }


    }
}
