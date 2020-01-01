using AutoMapper;
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
    public class DeviceController : ApiController
    {
        private DeviceBL deviceBL = new DeviceBL();
        private readonly IMapper _mapper;

        public DeviceController()
        {

        }

        public DeviceController(DeviceBL deviceBL, IMapper mapper)
        {
            this.deviceBL = deviceBL;
            this._mapper = mapper;
        }
        /// <summary>
        /// Danh sách Device
        /// </summary>
        /// <returns></returns>
        [Route("api/Device/all")]
        //[Authorize(Devices = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<Device>> response = new HttpResponseDTO<IEnumerable<Device>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.GetAll();
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
        [Route("api/Device/page")]
        //[Authorize(Devices = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<Device>> response = new HttpResponseDTO<PagedResults<Device>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.CreatePagedResults(pageNumber, pageSize);
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
        /// Chi tiết Device
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Device/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<Device> response = new HttpResponseDTO<Device>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.GetDetail(id);
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
        /// <param name="Device">      
        /// <returns></returns>
        [HttpPost]
        [Route("api/Device/create")]
        public IHttpActionResult CreateDevice([FromBody]Device Device)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.Create(Device);
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
        /// Xóa Device
        /// </summary>
        /// <param name="id">DeviceID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Device/delete")]
        public IHttpActionResult DeleteDevice([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.Delete(id);
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
        /// Xóa nhiều Device
        /// </summary>
        /// <param name="id">List DeviceID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Device/deletes")]
        public IHttpActionResult DeleteDevices([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = deviceBL.Deletes(id);
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
