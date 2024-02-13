using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AKU_Admin.BLL._Services
{
    public class workshopService : baseService
    {
        workshopDB _service;
        public workshopService()
        {
            _service = new workshopDB();
        }

        public List<WorkshopBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<WorkshopBLL>();
            }
        }
        public List<EventBLL> GetAllDropdown()
        {
            try
            {
                return _service.GetAllDropdown();
            }
            catch (Exception ex)
            {
                return new List<EventBLL>();
            }
        }    
        public List<EventAttendeesBLL> GetAllAttendees()
        {
            try
            {
                return _service.GetAllAttendees();
            }
            catch (Exception ex)
            {
                return new List<EventAttendeesBLL>();
            }
        }

        public WorkshopBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<string> GetItemImages(int id)
        {
            try
            {
                return _service.GetItemImages(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ItemSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }
        public int Insert(WorkshopBLL data, IWebHostEnvironment _env)
        {
            
            try
            {
                
                data.Image = UploadImage(data.Image, "Workshop", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
               

                var result = _service.Insert(data);

                

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateItemSettings(ItemSettingsBLL data)
        {
            try
            {

                var result = _service.UpdateItemSettings(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(WorkshopBLL data, IWebHostEnvironment _env)
        {
            
            try
            {
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
                data.Image = UploadImage(data.Image, "Workshop", _env);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(WorkshopBLL data)
        {
            try
            {
                data.Updatedon = DateTime.Now.AddMinutes(300);
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<EventDetailsBLL> GetEventsDetailRpt(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetEventsDetailRpt(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        public List<EventDetailsBLL> ConfirmListReport(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.ConfirmListReport(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }   
        public List<EventDetailsBLL> AttendeesReport(string AttendeesID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.AttendeesReport(AttendeesID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        
    }
}
