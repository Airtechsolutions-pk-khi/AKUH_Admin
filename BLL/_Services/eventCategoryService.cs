

using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
    public class eventCategoryService : baseService
    {
        eventCategoryDB _service;
        public eventCategoryService()
        {
            _service = new eventCategoryDB();
        }


        public List<EventCategoryBLL> Get()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EventCategoryBLL> GetDropdown()
        {
            try
            {
                return _service.GetAllDropdown();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public EventCategoryBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(EventCategoryBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "EventCategory", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(180);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(EventCategoryBLL data, IWebHostEnvironment _env)
        {
            try
            {
                // data.Image = UploadImage(data.Image, "EventCategory", _env);
                data.Updatedon = DateTime.UtcNow.AddMinutes(180);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(EventCategoryBLL data)
        {
            try
            {
                data.Updatedon = DateTime.UtcNow.AddMinutes(180);
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
