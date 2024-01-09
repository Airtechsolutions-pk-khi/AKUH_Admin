
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class bannerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        bannerService _service;
        public bannerController(IWebHostEnvironment env)
        {
            _service = new bannerService();
            _env = env;
        }

        [HttpGet("all")]
        public List<BannerBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public BannerBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]BannerBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]BannerBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]BannerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
