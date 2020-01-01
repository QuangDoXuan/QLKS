using BL;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLyKhachSan.Controllers
{
    /// <summary>
    /// RoomController
    /// </summary>
    public class RoomController : ApiController
    {
        private RoomBL roomBL = new RoomBL();

        /// <summary>
        /// Danh sách Room
        /// </summary>
        /// <returns></returns>
        [Route("api/Room/all")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<Room>> response = new HttpResponseDTO<IEnumerable<Room>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.GetAll();
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
        /// Phân trang
        /// </summary>
        /// <returns></returns>
        [Route("api/Room/page")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<Room>> response = new HttpResponseDTO<PagedResults<Room>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.CreatePagedResults(pageNumber,pageSize);
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
        /// Chi tiết Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Room/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<Room> response = new HttpResponseDTO<Room>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.GetDetail(id);
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
        /// Tạo mới Room
        /// </summary>
        /// <param name="Room"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Room/create")]
        public IHttpActionResult CreateRoom([FromBody]Room Room)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.CreateRoom(Room);
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
        /// Xóa Room
        /// </summary>
        /// <param name="id">RoomID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Room/delete")]
        public IHttpActionResult DeleteRoom([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.DeleteRoom(id);
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
        /// Xóa nhiều Room
        /// </summary>
        /// <param name="id">List RoomID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Room/deletes")]
        public IHttpActionResult DeleteRooms([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.DeleteRooms(id);
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
