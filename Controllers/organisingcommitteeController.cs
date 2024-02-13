
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class organisingcommitteeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        organisingcommitteeService _service;
        public organisingcommitteeController(IWebHostEnvironment env)
        {
            _service = new organisingcommitteeService();
            _env = env;
        }
        [HttpGet("all")]
        public List<OrganisingCommitteeBLL> Get()
        {
            return _service.Get();
        }
        [HttpGet("DropdownAll")]
        public List<SpeakerBLL> GetDropdown()
        {
            return _service.GetDropdown();
        }
        [HttpGet("organisingcommittee/{id}")]
        public OrganisingCommitteeBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] OrganisingCommitteeBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] OrganisingCommitteeBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] OrganisingCommitteeBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
