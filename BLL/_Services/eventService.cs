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
    public class eventService : baseService
    {
        eventDB _service;
        public eventService()
        {
            _service = new eventDB();
        }

        public List<EventBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<EventBLL>();
            }
        }
        
        public ItemBLL Get(int id, int brandID)
        {
            try
            {
                return _service.Get(id, brandID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ItemSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }
        public int Insert(EventBLL data, IWebHostEnvironment _env)
        {
            List<ItemimagesBLL> imBLL = new List<ItemimagesBLL>();
            try
            {
                //data.Image = UploadImage(data.Image, "Items", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.Image = UploadImage(img, "Event", _env);
                    }
                     
                    imBLL.Add(new ItemimagesBLL
                    {
                        Image = UploadImage(img, "Event", _env),
                        Createdon = data.Createdon
                    });

                }
                data.ItemImages = imBLL;

                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateItemSettings(ItemSettingsBLL data)
        {
            try
            {
                
                var result = _service.UpdateItemSettings(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(ItemBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Items", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(ItemBLL data)
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
