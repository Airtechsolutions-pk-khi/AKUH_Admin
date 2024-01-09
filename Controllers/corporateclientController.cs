
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class corporateclientController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        corporateclientService _service;
        public corporateclientController(IWebHostEnvironment env)
        {
            _service = new corporateclientService();
            _env = env;
        }

        [HttpGet("all")]
        public List<CorporateClientBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public CorporateClientBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] CorporateClientBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] CorporateClientBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] CorporateClientBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
