﻿using AutoMapper;
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
    public class CustomerController : ApiController
    {
        private CustomerBL customerBL = new CustomerBL();
        private readonly IMapper _mapper;

        public CustomerController()
        {

        }

        public CustomerController(CustomerBL customerBL, IMapper mapper)
        {
            this.customerBL = customerBL;
            this._mapper = mapper;
        }
        /// <summary>
        /// Danh sách Customer
        /// </summary>
        /// <returns></returns>
        [Route("api/Customer/all")]
        //[Authorize(Customers = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            HttpResponseDTO<IEnumerable<Customer>> response = new HttpResponseDTO<IEnumerable<Customer>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.GetAll();
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
        [Route("api/Customer/page")]
        //[Authorize(Customers = "Moderator")]
        [HttpGet]
        public IHttpActionResult GetByPage(int pageNumber, int pageSize)
        {
            HttpResponseDTO<PagedResults<Customer>> response = new HttpResponseDTO<PagedResults<Customer>>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.CreatePagedResults(pageNumber, pageSize);
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
        /// Chi tiết Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Customer/detail")]
        public IHttpActionResult GetDetail([FromUri]Guid id)
        {
            HttpResponseDTO<Customer> response = new HttpResponseDTO<Customer>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.GetDetail(id);
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
        /// <param name="Customer">      
        /// <returns></returns>
        [HttpPost]
        [Route("api/Customer/create")]
        public IHttpActionResult CreateCustomer([FromBody]Customer customer)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.Create(customer);
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
        /// Xóa Customer
        /// </summary>
        /// <param name="id">CustomerID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Customer/delete")]
        public IHttpActionResult DeleteCustomer([FromUri]Guid id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.Delete(id);
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
        /// Xóa nhiều Customer
        /// </summary>
        /// <param name="id">List CustomerID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Customer/deletes")]
        public IHttpActionResult DeleteCustomers([FromBody]Guid[] id)
        {
            HttpResponseDTO<int> response = new HttpResponseDTO<int>();
            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = customerBL.Deletes(id);
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