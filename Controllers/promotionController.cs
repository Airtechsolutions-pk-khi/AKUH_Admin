
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class promotionController : Controller
    {
        private readonly IWebHostEnvironment _env;
        promotionService _service;
        public promotionController(IWebHostEnvironment env)
        {
            _service = new promotionService();
            _env = env;
        }

        [HttpGet("{all}")]
        public List<PromotionBLL> GetAlldoctor()
        {
            return _service.GetAll();
        }

        [HttpGet("promotion/{id}")]
        public PromotionBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] PromotionBLL obj)
        {
            return _service.Insert(obj, _env);
        }
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] PromotionBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] PromotionBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
