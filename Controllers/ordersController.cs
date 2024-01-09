using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class ordersController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        ordersService _service;
      
        public ordersController(IWebHostEnvironment env)
        {
            _service = new ordersService();
            _env = env;
        }


        [HttpGet("all/{customerid}/{fromDate}/{toDate}")]
        public List<OrdersBLL> GetAll(int customerid, string fromDate, string toDate)
        {
            return _service.GetAll(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
        }


        [HttpGet("{id}")]
        public RspOrderDetail Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet("print/{id}")]
        public RspPrintReceipt GetPrint(int id)
        {
            return _service.GetPrint(id,  _env);
        }

        [HttpPost]
        [Route("insert")]   
        public async Task<int> Post([FromBody]OrdersBLL obj)
        {
            var result = _service.Insert(obj, _env);
            await PushAndriod.PushNotify("Success!", "Your order has been posted!");
            return result;
        }

        [HttpPost]
        [Route("update")]
        public async Task<int> PostUpdate([FromBody]OrdersBLL obj)
        {
            var result = _service.Update(obj, _env);
            await PushAndriod.PushNotify("Success!", "Your order has been updated!");
            return result;
        }


        [HttpPost]
        [Route("delete")]
        public async Task<int> PostDelete([FromBody]OrdersBLL obj)
        {
            var result = _service.Delete(obj);
            await PushAndriod.PushNotify("Success!", "Order Deleted!");
            return result;
        }
    }
}
