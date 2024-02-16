
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class popupbannerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        popupbannerService _service;
        public popupbannerController(IWebHostEnvironment env)
        {
            _service = new popupbannerService();
            _env = env;
        }

        [HttpGet("all")]
        public List<PopupBannerBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}")]
        public PopupBannerBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] PopupBannerBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] PopupBannerBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] PopupBannerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
