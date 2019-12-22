using BL;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QuanLyKhachSan.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private UserBL userBL = new UserBL();
        [Route("api/User/all")]
        [Authorize(Roles = "Moderator")]
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

        [Route("api/User/all")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            HttpResponseDTO<IEnumerable<ApplicationUser>> response = new HttpResponseDTO<IEnumerable<ApplicationUser>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = userBL.GetAll();
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }
    }
}
