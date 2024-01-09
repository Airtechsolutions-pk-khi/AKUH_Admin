
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class diagnosticController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        diagnosticCategoriesService _service;
        public diagnosticController(IWebHostEnvironment env)
        {
            _service = new diagnosticCategoriesService();
            _env = env;
        }
        [HttpGet("{all}")]
        public List<DiagnosticCategoriesBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("diagnostic/{id}")]
        public DiagnosticCategoriesBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] DiagnosticCategoriesBLL obj)
        {
            return _service.Insert(obj, _env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] DiagnosticCategoriesBLL obj)
        {
            return _service.Update(obj, _env);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] DiagnosticCategoriesBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
