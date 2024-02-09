
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
    public class messageService : baseService
    {
        messageDB _service;
        public messageService()
        {
            _service = new messageDB();
        }


        public List<MessageBLL> Get()
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
        public List<OrganizerBLL> GetDropdown()
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

        public MessageBLL Get(int id)
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
        public int Insert(MessageBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Message", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(MessageBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Message", _env);
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(MessageBLL data)
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
