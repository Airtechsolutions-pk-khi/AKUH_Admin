
using System;
using System.Collections.Generic;
using BAL.Repositories;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]

    public class formpermissionController : ControllerBase
    {
        formpermissionService _service;
        public formpermissionController()
        {
            _service = new formpermissionService();
        }
        [HttpGet("all")]
        public List<FormPermissionBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("permission/{rolename}")]
        public FormPermissionBLL Get(string rolename)
        {
            return _service.Get(rolename);
        }
      
        //[HttpPost]
        //[Route("insert")]
        //public int Post([FromBody] FormPermissionBLL obj)
        //{
        //    return _service.Insert(obj);
        //}
        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] FormPermissionBLL obj)
        {
            return _service.Update(obj);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] FormPermissionBLL obj)
        {
            try
            {
                var result = _service.Delete(obj);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}