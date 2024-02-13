
using System;
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
  
    public class workshopController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        workshopService _service;
      
        public workshopController(IWebHostEnvironment env)
        {
            _service = new workshopService();
            _env = env;
        }

        [HttpGet("all")]
        public List<WorkshopBLL> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("alldropdown")]
        public List<EventBLL> GetAllDropdown()
        {
            return _service.GetAllDropdown();
        }       
        [HttpGet("allattendees")]
        public List<EventAttendeesBLL> GetAllAttendees()
        {
            return _service.GetAllAttendees();
        }

        [HttpGet("{id}")]
        public WorkshopBLL Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet("images/{id}")]
        public List<string> GetImages(int id)
        {
            return _service.GetItemImages(id);
        }
        [HttpGet("settings/{brandid}")]
        public ItemSettingsBLL GetItemSettings(int brandid)
        {
            return _service.GetItemSettings(brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]WorkshopBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] WorkshopBLL obj)
        {
            return _service.Update(obj, _env);
        }

        [HttpPost]
        [Route("update/settings")]
        public int PostUpdateSettings([FromBody] ItemSettingsBLL obj)
        {
            return _service.UpdateItemSettings(obj);
        }

        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] WorkshopBLL obj)
        {
            return _service.Delete(obj);
        }
        [HttpGet("EventRpt/{EventID}/{fromDate}/{toDate}")]
        public List<EventDetailsBLL> GetEventsDetail(string EventID, string FromDate, string ToDate)
        {
            return _service.GetEventsDetailRpt(EventID, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
        }
        [HttpGet("ConfirmListReport/{EventID}/{fromDate}/{toDate}")]
        public List<EventDetailsBLL> ConfirmListReport(string EventID, string FromDate, string ToDate)
        {
            return _service.ConfirmListReport(EventID, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
        }     
        [HttpGet("AttendeesReport/{AttendeesID}/{fromDate}/{toDate}")]
        public List<EventDetailsBLL> AttendeesReport(string AttendeesID, string FromDate, string ToDate)
        {
            return _service.AttendeesReport(AttendeesID, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
        }
        
    }
}
