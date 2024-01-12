
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class EventCategoryController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        eventCategoryService _service;
        public EventCategoryController(IWebHostEnvironment env)
        {
            _service = new eventCategoryService();
            _env = env;
        }
        [HttpGet("all")]
        public List<EventCategoryBLL> Get()
        {
            return _service.Get();
        }
        [HttpGet("eventcategory/{id}")]
        public EventCategoryBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] EventCategoryBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] EventCategoryBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] EventCategoryBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
