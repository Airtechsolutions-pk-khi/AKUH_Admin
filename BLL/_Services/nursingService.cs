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
    public class nursingService : baseService
    {
        nursingServiceDB _service;
        public nursingService()
        {
            _service = new nursingServiceDB();
        }
       
        public List<medicalServiceTypeBLL> type()
        {
            try
            {
                return _service.Type();
            }
            catch (Exception)
            {
                return new List<medicalServiceTypeBLL>();
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
       
        public int InsertType(medicalServiceTypeBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "Service", _env);
                var result = _service.InsertType(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public int UpdateType(medicalServiceTypeBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "Service", _env);
                var result = _service.UpdateType(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
 
        public int DeleteType(medicalServiceTypeBLL data)
        {
            try
            {
                var result = _service.DeleteType(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

