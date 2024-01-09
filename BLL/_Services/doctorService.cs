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
    public class doctorService : baseService
    {
        doctorDB _service;
        public doctorService()
        {
            _service = new doctorDB();
        }
        public List<DoctorBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<DoctorBLL>();
            }
        }
        public List<SpecialistBLL> GetSpeciality()
        {
            try
            {
                return _service.GetSpeciality();
            }
            catch (Exception ex)
            {
                return new List<SpecialistBLL>();
            }
        }
        public List<DaysBLL> GetDocDays()
        {
            try
            {
                return _service.GetDocDays();
            }
            catch (Exception ex)
            {
                return new List<DaysBLL>();
            }
        }
        public List<DoctorBLL> Get(int id)
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
        public int Insert(DoctorBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.ImagePath = UploadImage(data.ImagePath, "Doctor", _env);
                data.CreatedOn = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DoctorBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.ImagePath = UploadImage(data.ImagePath, "Doctor", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DoctorBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
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
