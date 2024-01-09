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
    public class couponService : baseService
    {
        couponDB _service;
        public couponService()
        {
            _service = new couponDB();
        }

        public List<CouponBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<CouponBLL>();
            }
        }
        
        public CouponBLL Get(int id)
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
        public int Insert(CouponBLL data)
        {
            try
            {
                data.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CouponBLL data)
        {
            try
            {
                data.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
				var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(CouponBLL data)
        {
            try
            {
                data.LastUpdatedDate = DateTime.UtcNow.AddMinutes(300);
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
