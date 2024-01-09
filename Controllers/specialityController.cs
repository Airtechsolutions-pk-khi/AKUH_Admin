
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class specialityController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        specialitiesService _service;
        public specialityController(IWebHostEnvironment env)
        {
            _service = new specialitiesService();
            _env = env;
        }
        [HttpGet("{all}")]
        public List<SpecialistBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("speciality/{id}")]
        public SpecialistBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]SpecialistBLL obj)
        {
            return _service.Insert(obj, _env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] SpecialistBLL obj)
        {
            return _service.Update(obj, _env);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] SpecialistBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
