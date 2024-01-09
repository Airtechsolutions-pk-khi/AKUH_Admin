
using System.Collections.Generic;
using BAL.Repositories;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]

    public class appsettingController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        appSettingService _service;
        public appsettingController(IWebHostEnvironment env)
        {
            _service = new appSettingService();
            _env = env;
        }

        [HttpGet("all")]
        public List<AppSetingBLL> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public AppSetingBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] AppSetingBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int Put([FromBody] AppSetingBLL obj)
        {
            return _service.Update(obj);
        }

        [HttpPost]
        [Route("delete")]
        public int Delete(AppSetingBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
