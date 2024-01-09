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
    public class formpermissionService : baseService
    {
        formpermission _service;
        public formpermissionService()
        {
            _service = new formpermission();
        }

        public List<FormPermissionBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<FormPermissionBLL>();
            }
        }
        public FormPermissionBLL Get(string rolename)
        {
            try
            {
                return _service.Get(rolename);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        //public int Insert(FormPermissionBLL data)
        //{
        //    try
        //    {
        //        data.CreatedDate = _UTCDateTime_SA();
        //        var result = _service.Insert(data);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public int Update(FormPermissionBLL data)
        {
            try
            {
                data.CreatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(FormPermissionBLL data)
        {
            try
            {
                //data.CreatedDate = _UTCDateTime_SA();
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
