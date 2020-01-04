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
    public class EquipmentController : ApiController
    {
        private EquipmentBL equipBL = new EquipmentBL();

        [HttpGet]
        [Route("api/equip/all")]
        public async Task<IHttpActionResult> GetAll()
        {
            HttpResponseDTO<IEnumerable<EquipmentViewModel>> response = new HttpResponseDTO<IEnumerable<EquipmentViewModel>>();

            try
            {
                response.code = 0;
                response.message = Constanst.SUCCESS;
                response.data = equipBL.GetAll();
            }
            catch(Exception e)
            {
                response.code = 500;
                response.message = Constanst.FAIL;
                response.data = null;
            }
            return Ok(response);
        }
    }
}
