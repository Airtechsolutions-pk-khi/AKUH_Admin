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
    public class prescripitonService : baseService
    {
        prescriptionDB _service;
        public prescripitonService()
        {
            _service = new prescriptionDB();
        }
        public List<PrescriptionBLL> GetAll(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetAll(FromDate, ToDate);
            }
            catch (Exception)
            {
                return new List<PrescriptionBLL>();
            }
        }
        public PrescriptionBLL Get(int id)
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
        public int Insert(PrescriptionBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Presc", _env);
                data.CreatedOn = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Update(PrescriptionBLL data)
        {
            try
            {
                data.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Delete(int PrescriptionID)
        {
            try
            {
                //data.CreatedOn = DateTime.Now;
                var result = _service.Delete(PrescriptionID);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
