
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class PartnerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        partnerService _service;
        public PartnerController(IWebHostEnvironment env)
        {
            _service = new partnerService();
            _env = env;
        }
        [HttpGet("all")]
        public List<PartnerBLL> Get()
        {
            return _service.Get();
        }
        [HttpGet("partner/{id}")]
        public PartnerBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] PartnerBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] PartnerBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] PartnerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
