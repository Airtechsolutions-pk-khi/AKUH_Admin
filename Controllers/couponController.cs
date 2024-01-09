
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]

    public class couponController : ControllerBase
    {
        couponService _service;
        public couponController()
        {
            _service = new couponService();

        }
        [HttpGet("all")]
        public List<CouponBLL> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public CouponBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] CouponBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int Put([FromBody] CouponBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("deleteCoupon")]
        public int Delete([FromBody] CouponBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
