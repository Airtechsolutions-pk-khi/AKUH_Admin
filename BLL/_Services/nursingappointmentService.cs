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
    public class nursingappointmentService : baseService
    {
        nursingappointmentDB _service;
        public nursingappointmentService()
        {
            _service = new nursingappointmentDB();
        }
        public List<AppointmentBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<AppointmentBLL>();
            }
        }
        public AppointmentBLL Get(int id)
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
        public int Insert(AppointmentBLL data)
        {
            try
            {
                data.CreatedOn = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public int Update(AppointmentBLL data)
        //{
        //    try
        //    {
        //        data.LastUpdatedDate = _UTCDateTime_SA();
        //        var result = _service.Update(data);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public int Delete(AppointmentBLL data)
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
        public int Status(AppointmentBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Status(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
