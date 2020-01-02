using AutoMapper;
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
    /// <summary>
    /// RoomController
    /// </summary>
    public class RoomController : ApiController
    {
        private RoomBL roomBL = new RoomBL();
        private readonly IMapper _mapper;

        public RoomController()
        {
            
        }

        public RoomController(RoomBL roomBL, IMapper mapper)
        {
            this.roomBL = roomBL;
            this._mapper = mapper;
        }
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
        /// Tìm kiếm Room
        /// </summary>
        /// <returns></returns>
        [Route("api/Room/search")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult Search(string searchTerm, string sortColumn, string sortOrder, int pageNumber, int pageSize, string roomNo, string roomTypeId, string status, string statusStay,int nop)
        {
            HttpResponseDTO<IEnumerable<RoomSearchViewModel>> response = new HttpResponseDTO<IEnumerable<RoomSearchViewModel>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.Search(searchTerm, sortColumn,sortOrder, pageNumber,pageSize, roomNo,roomTypeId,status,statusStay,nop);
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
        public async Task<IHttpActionResult> GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<Room>> response = new HttpResponseDTO<PagedResults<Room>>();
            await Task.Delay(500);
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
        /// 
        /// </summary>
        /// <param name="room">      
        /// /// "RoomNo": "P109",
        /// "RoomName": "string",
        ///"NoP": "9",
        ///"Price": 9000000,
        ///"Floor": 10,
        ///"StatusStay": "string",
        ///"Status": "Trống",
        ///"TypeRoomID": "00000000-0000-0000-0000-000000000000"</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Room/create")]
        public IHttpActionResult CreateRoom([FromBody]Room room)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.CreateRoom(room);
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
   
        /// <summary>
        /// Sửa thông tin room
        /// </summary>
        /// <param name="id">RoomId</param>
        /// <param name="room">newRoom</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Room/update")]
        public IHttpActionResult Update(Guid id, [FromBody]Room room)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = roomBL.Update(id,room);
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
