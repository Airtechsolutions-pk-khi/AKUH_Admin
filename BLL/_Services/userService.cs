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
    public class userService : baseService
    {
        userDB _service;
        public userService()
        {
            _service = new userDB();
        }
        public List<UserBLL> GetAllUser()
        {
            try
            {
                return _service.GetAllUser();
            }
            catch (Exception ex)
            {
                return new List<UserBLL>();
            }
        }
        public UserBLL GetUser(int id)
        {
            try
            {
                return _service.GetUser(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int InsertUser(UserBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "User", _env);
                data.CreatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.InsertUser(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateUser(UserBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "User", _env);
                data.UpdatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.UpdateUser(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteUser(UserBLL data)
        {
            try
            {

                var result = _service.DeleteUser(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<LoginBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<LoginBLL>();
            }
        }
		public List<PermissionBLL> GetRoles()
		{
			try
			{
				return _service.GetRoles();
			}
			catch (Exception ex)
			{
				return new List<PermissionBLL>();
			}
		}
		public LoginBLL Get(int id)
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
        public int Insert(LoginBLL data)
        {
            try
            {
                data.Updatedon = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LoginBLL data)
        {
            try
            {
                data.Updatedon = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LoginBLL data)
        {
            try
            {
                
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdatePermission(PermissionBLL data)
        {
            try
            {
                //for (int i = 0; i < length; i++)
                //{

                //}
                data.CreatedDate = _UTCDateTime_SA();
                var result = _service.UpdatePermission(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public PermissionFormBLL GetPermission(string rn)
        {
            try
            {
                return _service.GetPermission(rn);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PermissionFormBLL> GetAllPermissions()
        {
            try
            {
                return _service.GetAllPermission();
            }
            catch (Exception ex)
            {
                return new List<PermissionFormBLL>();
            }
        }
    }
}
