
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class eventController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        eventService _service;
      
        public eventController(IWebHostEnvironment env)
        {
            _service = new eventService();
            _env = env;
        }

        [HttpGet("all")]
        public List<EventBLL> GetAll()
        {
            return _service.GetAll();
        }


        [HttpGet("{id}/brand/{brandid}")]
        public ItemBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpGet("settings/{brandid}")]
        public ItemSettingsBLL GetItemSettings(int brandid)
        {
            return _service.GetItemSettings(brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]EventBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]ItemBLL obj)
        {
            return _service.Update(obj, _env);
        }

        [HttpPost]
        [Route("update/settings")]
        public int PostUpdateSettings([FromBody] ItemSettingsBLL obj)
        {
            return _service.UpdateItemSettings(obj);
        }

        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]ItemBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
