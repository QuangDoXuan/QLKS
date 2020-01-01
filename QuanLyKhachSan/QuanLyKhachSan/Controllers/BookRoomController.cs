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
    /// BookRoomController
    /// </summary>
    public class BookRoomController : ApiController
    {
        private BookRoomBL bookRoomBL = new BookRoomBL();

        /// <summary>
        /// Danh sách Room
        /// </summary>
        /// <returns></returns>
        [Route("api/BookRoom/all")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<BookRoom>> response = new HttpResponseDTO<IEnumerable<BookRoom>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.GetAll();
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
        [Route("api/BookRoom/page")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<BookRoom>> response = new HttpResponseDTO<PagedResults<BookRoom>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.CreatePagedResults(pageNumber, pageSize);
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
        /// Chi tiết book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/BookRoom/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<BookRoom> response = new HttpResponseDTO<BookRoom>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.GetDetail(id);
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
       /// Tạo mới đặt phòng
       /// </summary>
       /// <param name="bookRoom"></param>
       /// <param name="roomID"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("api/BookRoom/create")]
        public IHttpActionResult Create([FromBody]BookRoom bookRoom)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.Create(bookRoom);
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = 0;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("api/BookRoom/create-detail")]
        public IHttpActionResult CreateDetail(Guid id,[FromBody]Guid[]roomID)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.CreateDetail(id,roomID);
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
        /// Xóa đặt phòng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/BookRoom/delete")]
        public IHttpActionResult Delete([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.Delete(id);
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
        /// Xóa nhiều đặt phòng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/BookRoom/deletes")]
        public IHttpActionResult Deletes([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = bookRoomBL.Deletes(id);
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
