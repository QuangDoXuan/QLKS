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
    public class EmployeeController : ApiController
    {
        private EmployeeBL employeeBL = new EmployeeBL();
        private readonly IMapper _mapper;

        public EmployeeController()
        {

        }

        public EmployeeController(EmployeeBL employeeBL, IMapper mapper)
        {
            this.employeeBL = employeeBL;
            this._mapper = mapper;
        }
        /// <summary>
        /// Danh sách Employee
        /// </summary>
        /// <returns></returns>
        [Route("api/Employee/all")]
        //[Authorize(Employees = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<Employee>> response = new HttpResponseDTO<IEnumerable<Employee>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.GetAll();
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
        [Route("api/Employee/page")]
        //[Authorize(Employees = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<Employee>> response = new HttpResponseDTO<PagedResults<Employee>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.CreatePagedResults(pageNumber, pageSize);
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
        /// Chi tiết Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Employee/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<Employee> response = new HttpResponseDTO<Employee>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.GetDetail(id);
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
        /// <param name="Employee">      
        /// <returns></returns>
        [HttpPost]
        [Route("api/Employee/create")]
        public IHttpActionResult CreateEmployee([FromBody]Employee Employee)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.Create(Employee);
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
        /// Xóa Employee
        /// </summary>
        /// <param name="id">EmployeeID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Employee/delete")]
        public IHttpActionResult DeleteEmployee([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.Delete(id);
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
        /// Xóa nhiều Employee
        /// </summary>
        /// <param name="id">List EmployeeID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Employee/deletes")]
        public IHttpActionResult DeleteEmployees([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = employeeBL.Deletes(id);
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
