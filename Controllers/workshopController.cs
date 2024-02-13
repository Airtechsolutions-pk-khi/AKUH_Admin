
using System;
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class workshopController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        workshopService _service;
      
        public workshopController(IWebHostEnvironment env)
        {
            _service = new workshopService();
            _env = env;
        }
        [HttpGet("all")]
        public List<WorkshopBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("alldropdown")]
        public List<EventBLL> GetAllDropdown()
        {
            return _service.GetAllDropdown();
        }       
        [HttpGet("allattendees")]
        public List<EventAttendeesBLL> GetAllAttendees()
        {
            return _service.GetAllAttendees();
        }
        [HttpGet("{id}")]
        public WorkshopBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]WorkshopBLL obj)
        {
            return _service.Insert(obj, _env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] WorkshopBLL obj)
        {
            return _service.Update(obj, _env);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] WorkshopBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
