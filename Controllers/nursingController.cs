
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class nursingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        nursingService _service;
        public nursingController(IWebHostEnvironment env)
        {
            _service = new nursingService();
            _env = env;
        }
       
        [HttpGet("{Alltype}")]
        public List<medicalServiceTypeBLL> type()
        {
            return _service.type();
        }
      
        [HttpGet("servicetype/{id}")]
        public medicalServiceTypeBLL Getbyid(int id)
        {
            return _service.Getbyid(id);
        }
       

        [HttpPost]
        [Route("inserttype")]
        public int PostType([FromBody] medicalServiceTypeBLL obj)
        {
            return _service.InsertType(obj, _env);
        }


     


        [HttpPost]
        [Route("updatetype")]
        public int UpdateType([FromBody] medicalServiceTypeBLL obj)
        {
            return _service.UpdateType(obj, _env);
        }



      
        [HttpPost]
        [Route("deletetype")]
        public int DeleteType([FromBody] medicalServiceTypeBLL obj)
        {
            return _service.DeleteType(obj);
        }
    }
}
