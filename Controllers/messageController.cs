
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class messageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        messageService _service;
        public messageController(IWebHostEnvironment env)
        {
            _service = new messageService();
            _env = env;
        }
        [HttpGet("all")]
        public List<MessageBLL> Get()
        {
            return _service.Get();
        }
        //[HttpGet("DropdownAll")]
        //public List<OrganizerBLL> GetDropdown()
        //{
        //    return _service.GetDropdown();
        //}
        [HttpGet("message/{id}")]
        public MessageBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] MessageBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] MessageBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] MessageBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
