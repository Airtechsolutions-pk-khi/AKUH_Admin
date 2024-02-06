using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        public List<EventBLL> GetAllDropdown()
        {
            try
            {
                return _service.GetAllDropdown();
            }
            catch (Exception ex)
            {
                return new List<EventBLL>();
            }
        }    
        public List<EventAttendeesBLL> GetAllAttendees()
        {
            try
            {
                return _service.GetAllAttendees();
            }
            catch (Exception ex)
            {
                return new List<EventAttendeesBLL>();
            }
        }

        public EventBLL Get(int id)
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
        public List<string> GetItemImages(int id)
        {
            try
            {
                return _service.GetItemImages(id);
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
            List<EventImagesBLL> imBLL = new List<EventImagesBLL>();
            try
            {
                //data.Image = UploadImage(data.Image, "Event", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.Image = UploadImage(img, "Event", _env);
                    }

                    imBLL.Add(new EventImagesBLL
                    {
                        Image = UploadImage(img, "Event", _env),
                        Createdon = data.Createdon
                    });

                }
                data.EventImages = imBLL;

                var result = _service.Insert(data);

                //try
                //{
                //    var ds = _service.GetToken();
                //    var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                //    foreach (var item in getTokens)
                //    {
                //        var token = new PushNotificationBLL();
                //        token.Title = "AKU Surgery Conference" + " | Event Info";
                //        token.Message = data.Name;
                //        token.DeviceID = item.Token;
                //        _service.PushNotificationAndroid(token);
                //    }
                //}
                //catch (Exception)
                //{
                //}

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
        public int Update(EventBLL data, IWebHostEnvironment _env)
        {
            List<EventImagesBLL> imBLL = new List<EventImagesBLL>();
            try
            {
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
                for (int i = 0; i < data.ImagesSource.Count; i++)
                {
                    var img = data.ImagesSource[i].ToString();
                    if (i == 0)
                    {
                        data.Image = UploadImage(img, "Event", _env);
                    }

                    imBLL.Add(new EventImagesBLL
                    {
                        Image = UploadImage(img, "Event", _env),
                        Createdon = data.Createdon
                    });

                }
                data.EventImages = imBLL;

                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(EventBLL data)
        {
            try
            {
                data.Updatedon = DateTime.Now.AddMinutes(300);
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<EventDetailsBLL> GetEventsDetailRpt(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetEventsDetailRpt(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        public List<EventDetailsBLL> ConfirmListReport(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.ConfirmListReport(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }   
        public List<EventDetailsBLL> AttendeesReport(string AttendeesID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.AttendeesReport(AttendeesID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        
    }
}
