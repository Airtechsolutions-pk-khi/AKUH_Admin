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
    public class medicalService : baseService
    {
        medicalServiceDB _service;
        public medicalService()
        {
            _service = new medicalServiceDB();
        }
        public List<medicalServiceBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception)
            {
                return new List<medicalServiceBLL>();
            }
        }
       
        public medicalServiceBLL Get(int id)
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
        public medicalServiceTypeBLL Getbyid(int id)
        {
            try
            {
                return _service.Getbyid(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(medicalServiceBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Service", _env);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public int Update(medicalServiceBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Service", _env);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        


        public int Delete(medicalServiceBLL data)
        {
            try
            {
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

     
    }
}

