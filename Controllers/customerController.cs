
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class customerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        customerService _service;
        public customerController(IWebHostEnvironment env)
        {
            _service = new customerService();
            _env = env;
        }
        [HttpGet("all")]
        public List<CustomerBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("alldropdown")]
        public List<CustomerDropdownBLL> GetAlldropdown()
        {
            return _service.GetAlldropdown();
        }
        [HttpGet("alldropdownRNo")]
        public List<CustomerRNoDropdownBLL> GetAlldropdownRNo()
        {
            return _service.GetAlldropdownRNo();
        }
        [HttpGet("customer/{id}")]
        public CustomerBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]CustomerBLL obj)
        {
            return _service.Insert(obj,_env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] CustomerBLL obj)
        {
            return _service.Update(obj, _env);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]CustomerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}