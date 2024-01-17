
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class faqController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        faqService _service;
        public faqController(IWebHostEnvironment env)
        {
            _service = new faqService();
            _env = env;
        }
        [HttpGet("all")]
        public List<FaqBLL> Get()
        {
            return _service.Get();
        }
        
        [HttpGet("faq/{id}")]
        public FaqBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] FaqBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] FaqBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] FaqBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
