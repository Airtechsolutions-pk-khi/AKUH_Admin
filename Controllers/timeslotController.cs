
using System.Collections.Generic;
using BAL.Repositories;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]

    public class timeslotController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        timeslotService _service;
        public timeslotController(IWebHostEnvironment env)
        {
            _service = new timeslotService();
            _env = env;
        }

        [HttpGet("all")]
        public List<TimeSlotBLL> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public TimeSlotBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] TimeSlotBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int Put([FromBody] TimeSlotBLL obj)
        {
            return _service.Update(obj);
        }

        [HttpPost]
        [Route("delete")]
        public int Delete([FromBody]  TimeSlotBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
