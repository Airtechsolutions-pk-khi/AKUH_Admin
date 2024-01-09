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
    public class timeslotService : baseService
    {
        TimeSlotDB _service;
        public timeslotService()
        {
            _service = new TimeSlotDB();
        }

        public List<TimeSlotBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<TimeSlotBLL>();
            }
        }
        
        public TimeSlotBLL Get(int id)
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
        public int Insert(TimeSlotBLL data)
        {
            try
            {
                data.CreatedDate = DateTime.UtcNow.AddMinutes(300);
				var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(TimeSlotBLL data)
        {
            try
            {
                data.CreatedDate = DateTime.UtcNow.AddMinutes(300);
				var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(TimeSlotBLL data)
        {
            try
            {
                data.CreatedDate = DateTime.UtcNow.AddMinutes(300);
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
