using BL;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuanLyKhachSan.Controllers
{
    public class RoomTypeController : ApiController
    {
        private RoomTypeBL typeRoomBL = new RoomTypeBL();

        /// <summary>
        /// Danh sách loại phòng
        /// </summary>
        /// <returns></returns>
        [Route("api/RoomType/all")]
        //[Authorize(TypeRooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<TypeRoom>> response = new HttpResponseDTO<IEnumerable<TypeRoom>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.GetAll();
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
        [Route("api/RoomType/page")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<TypeRoom>> response = new HttpResponseDTO<PagedResults<TypeRoom>>();
            await Task.Delay(500);
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.CreatePagedResults(pageNumber, pageSize);
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
        [Route("api/RoomType/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<TypeRoom> response = new HttpResponseDTO<TypeRoom>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.GetDetail(id);
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
        /// Tạo mới loại phòng
        /// </summary>
        /// <param name="typeRoom">object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/RoomType/create")]
        public IHttpActionResult CreateRoom([FromBody]TypeRoom typeRoom)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.Create(typeRoom);
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
        /// Xóa loại phòng
        /// </summary>
        /// <param name="id">TypeRoomID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/RoomType/delete")]
        public IHttpActionResult DeleteRoom([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.Delete(id);
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
        /// Xóa nhiều loại phòng
        /// </summary>
        /// <param name="id">List TypeRoomId</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/RoomType/deletes")]
        public IHttpActionResult DeleteTypeRooms([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.Deletes(id);
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
        /// Sửa thông tin loại phòng
        /// </summary>
        /// <param name="id">TypeRoomId</param>
        /// <param name="room">newTypeRoom</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/RoomType/update")]
        public IHttpActionResult Update(Guid id, [FromBody]TypeRoom room)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = typeRoomBL.Update(id, room);
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
