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
    public class PaymentController : ApiController
    {
        private PayBL payBL = new PayBL();
        private readonly IMapper _mapper;

        public PaymentController()
        {

        }

        public PaymentController(PayBL payBL, IMapper mapper)
        {
            this.payBL = payBL;
            this._mapper = mapper;
        }
        /// <summary>
        /// Danh sách Payment
        /// </summary>
        /// <returns></returns>
        [Route("api/Payment/all")]
        //[Authorize(Rooms = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<Payment>> response = new HttpResponseDTO<IEnumerable<Payment>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = payBL.GetAll();
            }
            catch (Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }



        //[Route("api/Payment/search")]
        //[HttpGet]
        //public IHttpActionResult Search(string searchTerm, string sortColumn, string sortOrder, int pageNumber, int pageSize, string roomNo, string roomTypeId, string status, string statusStay, int? nop)
        //{
        //    HttpResponseDTO<IEnumerable<RoomSearchViewModel>> response = new HttpResponseDTO<IEnumerable<RoomSearchViewModel>>();
        //    try
        //    {
        //        response.code = 0;
        //        response.message = Constanst.SUCCESS;
        //        response.data = payBL.Search(searchTerm, sortColumn, sortOrder, pageNumber, pageSize, roomNo, roomTypeId, status, statusStay, nop);
        //    }
        //    catch (Exception e)
        //    {
        //        response.code = 500;
        //        response.message = Constanst.FAIL;
        //        response.data = null;
        //    }
        //    return Ok(response);
        //}

     
        /// <summary>
        /// Chi tiết Hóa đơn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Room/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<Payment> response = new HttpResponseDTO<Payment>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = payBL.GetDetail(id);
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
        [Route("api/Payment/create")]
        public IHttpActionResult CreateRoom([FromBody]Payment payment)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = payBL.Create(payment);
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
        /// Xóa nhiều hóa đơn
        /// </summary>
        /// <param name="id">List PaymentID</param>
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
                response.data = payBL.Delete(id);
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
        /// Sửa thông tin hóa đơn
        /// </summary>
        /// <param name="id">PaymentID</param>
        /// <param name="room">payment</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Payment/update")]
        public IHttpActionResult Update(Guid id, [FromBody]Payment payment)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = payBL.Update(id, payment);
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
