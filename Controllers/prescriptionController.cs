
using System;
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class prescriptionController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        prescripitonService _service;
        public prescriptionController(IWebHostEnvironment env)
        {
            _service = new prescripitonService();
            _env = env;
        }
        [HttpGet("all/{fromDate}/{toDate}")]
        public List<PrescriptionBLL> GetAll(string fromDate, string toDate)
        {
            return _service.GetAll(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
        }
        [HttpGet("prescription/{id}")]
        public PrescriptionBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] PrescriptionBLL obj)
        {
            return _service.Insert(obj, _env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] PrescriptionBLL obj)
        {
            return _service.Update(obj);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] PrescriptionBLL obj)
        {
            return _service.Delete(obj.PrescriptionID);
        }
    }
}
