
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class SpeakerController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        speakerService _service;
        public SpeakerController(IWebHostEnvironment env)
        {
            _service = new speakerService();
            _env = env;
        }
        [HttpGet("all")]
        public List<SpeakerBLL> Get()
        {
            return _service.Get();
        }
        [HttpGet("DropdownAll")]
        public List<SpeakerBLL> GetDropdown()
        {
            return _service.GetDropdown();
        }
        [HttpGet("speaker/{id}")]
        public SpeakerBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]SpeakerBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] SpeakerBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] SpeakerBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
