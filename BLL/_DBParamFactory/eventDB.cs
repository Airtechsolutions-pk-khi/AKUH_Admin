

using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class eventDB : baseDB
    {
        public static EventBLL repo;
        public static DataTable _dt;
        public static DataTable _dt1;
        public static DataSet _ds;
        public eventDB()
           : base()
        {
            repo = new EventBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<EventBLL> GetAll()
        {
            try
            {
                var lst = new List<EventBLL>();
                SqlParameter[] p = new SqlParameter[0];
                 
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllEvents", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<EventBLL> GetAllDropdown()
        {
            try
            {
                var lst = new List<EventBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllDropdownEvents", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }    
        public List<EventAttendeesBLL> GetAllAttendees()
        {
            try
            {
                var lst = new List<EventAttendeesBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllEventAttendees", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventAttendeesBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public EventBLL Get(int id)
        {
            try
            {
                var _obj = new EventBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetEventbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<string> GetItemImages(int id)
        {

            try
            {
                var _obj = new EventBLL();
                List<string> ImagesSource = new List<string>();
                _dt = new DataTable();
                SqlParameter[] p1 = new SqlParameter[1];
                p1[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetEventImages_Admin", p1);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj.EventImages = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventImagesBLL>>();

                        for (int i = 0; i < _obj.EventImages.Count; i++)
                        {
                            ImagesSource.Add(_obj.EventImages[i].Image);
                        }
                    }
                }

                return ImagesSource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ItemSettingsBLL GetItemSettings(int brandID)
        {
            try
            {
                var _obj = new ItemSettingsBLL();
                var _obj1 = new ItemSettingsBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                SqlParameter[] p1 = new SqlParameter[1];
                p1[0] = new SqlParameter("@AppInfoID",1);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetItemSettings_Admin", p);
                _dt1 = (new DBHelper().GetTableFromSP)("sp_GetItemSettingsTitle_Admin", p1);
                
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj= JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ItemSettingsBLL>>().FirstOrDefault();
                        _obj.BrandID = brandID;
                        _obj1 = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt1)).ToObject<List<ItemSettingsBLL>>().FirstOrDefault();
                        _obj1.Items = _obj.Items;
                        _obj1.BrandID = _obj.BrandID;
                    }
                }
                return _obj1;
            }
            catch (Exception)
            {
                return new ItemSettingsBLL();
            }
        }
        public int Insert(EventBLL data)
        {
            try
            {                 
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[16];

                p[0] = new SqlParameter("@Name", data.Name);
               //p[1] = new SqlParameter("@Type", data.Type);                
                p[1] = new SqlParameter("@Description", data.Description);                
                p[2] = new SqlParameter("@FromDate", data.FromDate);
                p[3] = new SqlParameter("@ToDate", data.ToDate);
                p[4] = new SqlParameter("@EventDate", data.FromDate);
                p[5] = new SqlParameter("@EventCity", data.EventCity);
                p[6] = new SqlParameter("@LocationLink", data.LocationLink);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@PhoneNo", data.PhoneNo);
                p[9] = new SqlParameter("@Email", data.Email);
                //p[11] = new SqlParameter("@Facebook", data.Facebook);
                //p[12] = new SqlParameter("@Instagram", data.Instagram);
                //p[13] = new SqlParameter("@Twitter", data.Twitter);
                //p[14] = new SqlParameter("@Image", data.EventImages[0].Image);
                p[10] = new SqlParameter("@Image", data.Image);
                p[11] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                p[12] = new SqlParameter("@IsFeatured", data.IsFeatured);
                p[13] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[14] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[15] = new SqlParameter("@EventTime", data.EventTime);

                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertEvent_Admin", p).Rows[0]["EventID"].ToString());

                if (data.EventCategoryID != null )
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", rtn);
                    p1[1] = new SqlParameter("@EventCategory", data.EventCategoryID.ToString());
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertEventCatMapping_Admin", p1);
                }
                if (data.Speakers != "" && data.Speakers != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", rtn);
                    p1[1] = new SqlParameter("@Speaker", data.Speakers);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertSpeakerMapping_Admin", p1);
                }
                if (data.Organizers != "" && data.Organizers != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", rtn);
                    p1[1] = new SqlParameter("@Organizer", data.Organizers);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertOrganizerMapping_Admin", p1);
                }
                try
                {
                    var imgStr = String.Join(",", data.EventImages.Select(p => p.Image));
                    SqlParameter[] p3 = new SqlParameter[3];
                    p3[0] = new SqlParameter("@Images", imgStr);
                    p3[1] = new SqlParameter("@EventID", rtn);
                    p3[2] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertEventImages_Admin", p3);
                }
                catch { }
                return rtn;
            }
            catch (Exception ex)
            {
                LogExceptionToFile(ex, @"E:\exceptionLog.txt");
                return 0;
            }

        }
        private void LogExceptionToFile(Exception ex, string logFilePath)
        {
            
            // Log exception details to a text file
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine($"Timestamp: {DateTime.Now}");
                writer.WriteLine($"Exception Type: {ex.GetType().Name}");
                writer.WriteLine($"Message: {ex.Message}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                writer.WriteLine(new string('-', 50)); // Separator for better readability
            }
        }

        public int Update(EventBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[17];

                p[0] = new SqlParameter("@Name", data.Name);
                //p[1] = new SqlParameter("@Type", data.Type);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@FromDate", data.FromDate);
                p[3] = new SqlParameter("@ToDate", data.ToDate);
                p[4] = new SqlParameter("@EventDate", data.FromDate);
                p[5] = new SqlParameter("@EventCity", data.EventCity);
                p[6] = new SqlParameter("@LocationLink", data.LocationLink);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@PhoneNo", data.PhoneNo);
                p[9] = new SqlParameter("@Email", data.Email);
                //p[11] = new SqlParameter("@Facebook", data.Facebook);
                //p[12] = new SqlParameter("@Instagram", data.Instagram);
                //p[13] = new SqlParameter("@Twitter", data.Twitter);
                p[10] = new SqlParameter("@Image", data.Image);
                p[11] = new SqlParameter("@Updatedon", DateTime.UtcNow.AddMinutes(300));
                p[12] = new SqlParameter("@IsFeatured", data.IsFeatured);
                p[13] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[14] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[15] = new SqlParameter("@EventID", data.EventID);
                p[16] = new SqlParameter("@EventTime", data.EventTime);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateEvent_Admin", p);
                

                if (data.EventCategoryID != null )
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", data.EventID);
                    p1[1] = new SqlParameter("@EventCategory", data.EventCategoryID.ToString());
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertEventCatMapping_Admin", p1);
                }
                if (data.Speakers != "" && data.Speakers != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", data.EventID);
                    p1[1] = new SqlParameter("@Speaker", data.Speakers);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertSpeakerMapping_Admin", p1);
                }
                if (data.Organizers != "" && data.Organizers != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", data.EventID);
                    p1[1] = new SqlParameter("@Organizer", data.Organizers);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertOrganizerMapping_Admin", p1);
                }
                try
                {
                    var imgStr = String.Join(",", data.EventImages.Select(p => p.Image));
                    SqlParameter[] p3 = new SqlParameter[3];
                    p3[0] = new SqlParameter("@Images", imgStr);
                    p3[1] = new SqlParameter("@EventID", data.EventID);
                    p3[2] = new SqlParameter("@Createdon", data.Updatedon);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertEventImages_Admin", p3);
                }
                catch { }
                return rtn;
            }
            catch (Exception ex)
            {
                LogExceptionToFile(ex, @"E:\exceptionLog.txt");
                return 0;
            }
        }
        public int UpdateItemSettings(ItemSettingsBLL data)
        {
            try
            {
                if (data.Items == "")
                {
                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("@BrandID", data.BrandID);
                    p[1] = new SqlParameter("@Items", data.Items);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteTodaySpecial_Admin", p);

                    SqlParameter[] pp = new SqlParameter[2];
                    pp[0] = new SqlParameter("@ItemSettingTitle", data.ItemSettingTitle);
                    pp[1] = new SqlParameter("@IsItemSetting", data.IsItemSetting);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateTodaySpecial_Admin", pp);
                }
                else if (data.Items != "" && data.Items != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@BrandID", data.BrandID);
                    p1[1] = new SqlParameter("@Items", data.Items);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertItemSettings_Admin", p1);

                    SqlParameter[] pp = new SqlParameter[2];
                    pp[0] = new SqlParameter("@ItemSettingTitle", data.ItemSettingTitle);
                    pp[1] = new SqlParameter("@IsItemSetting", data.IsItemSetting);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateTodaySpecial_Admin", pp);
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Delete(EventBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@EventID", data.EventID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteEvent_Admin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                LogExceptionToFile(ex, @"E:\exceptionLog.txt");
                return 0;
            }
        }
        public List<EventDetailsBLL> GetEventsDetailRpt(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                string a = EventID.Replace("$", "");
                var lst = new List<EventDetailsBLL>();

                SqlParameter[] p = new SqlParameter[3];
                
                p[0] = new SqlParameter("@EventID", a);
                p[1] = new SqlParameter("@fromdate", FromDate);
                p[2] = new SqlParameter("@todate", ToDate);
                


                _dt = (new DBHelper().GetTableFromSP)("sp_rptEventDetailsReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventDetailsBLL>>();
                    }
                }
                return lst;
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
                string a = EventID.Replace("$", "");
                var lst = new List<EventDetailsBLL>();

                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@EventID", a);
                p[1] = new SqlParameter("@fromdate", FromDate);
                p[2] = new SqlParameter("@todate", ToDate);



                _dt = (new DBHelper().GetTableFromSP)("sp_rptConfirmListReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventDetailsBLL>>();
                    }
                }
                return lst;
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
                string a = AttendeesID.Replace("$", "");
                var lst = new List<EventDetailsBLL>();

                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@AttendeesID", a);
                p[1] = new SqlParameter("@fromdate", FromDate);
                p[2] = new SqlParameter("@todate", ToDate);



                _dt = (new DBHelper().GetTableFromSP)("sp_rptAttendeesReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventDetailsBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        
    }
}
