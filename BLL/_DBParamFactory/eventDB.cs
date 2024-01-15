

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

        public ItemBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new ItemBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", id);
                p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetItembyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<ItemBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
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
                SqlParameter[] p = new SqlParameter[19];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Type", data.Type);                
                p[2] = new SqlParameter("@Description", data.Description);                
                p[3] = new SqlParameter("@FromDate", data.FromDate);
                p[4] = new SqlParameter("@ToDate", data.ToDate);
                p[5] = new SqlParameter("@EventDate", data.EventDate);
                p[6] = new SqlParameter("@EventCity", data.EventCity);
                p[7] = new SqlParameter("@LocationLink", data.LocationLink);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@PhoneNo", data.PhoneNo);
                p[10] = new SqlParameter("@Email", data.Email);
                p[11] = new SqlParameter("@Facebook", data.Facebook);
                p[12] = new SqlParameter("@Instagram", data.Instagram);
                p[13] = new SqlParameter("@Twitter", data.Twitter);
                p[14] = new SqlParameter("@Image", data.Image);
                p[15] = new SqlParameter("@Createdon", data.Createdon);
                p[16] = new SqlParameter("@IsFeatured", data.IsFeatured);
                p[17] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[18] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                
                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertEvent_Admin", p).Rows[0]["EventID"].ToString());

                if (data.EventCategories != "" && data.EventCategories != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@EventID", rtn);
                    p1[1] = new SqlParameter("@EventCategory", data.EventCategories);
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

        public int Update(ItemBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[19];

                p[0] = new SqlParameter("@CategoryID", data.CategoryID);
                p[1] = new SqlParameter("@UnitID", data.UnitID);
                p[2] = new SqlParameter("@Name", data.Name);
                p[3] = new SqlParameter("@ArabicName", data.ArabicName);
                p[4] = new SqlParameter("@Description", data.Description);
                p[5] = new SqlParameter("@Image", data.Image);
                p[6] = new SqlParameter("@Barcode", data.Barcode);
                p[7] = new SqlParameter("@SKU", data.SKU);
                p[8] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[9] = new SqlParameter("@Price", data.Price);
                p[10] = new SqlParameter("@Cost", data.Cost);
                p[11] = new SqlParameter("@ItemType", data.ItemType);
                p[12] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[13] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[14] = new SqlParameter("@StatusID", data.StatusID);
                p[15] = new SqlParameter("@IsFeatured", data.IsFeatured);
                p[16] = new SqlParameter("@Calories", data.Calories);
                p[17] = new SqlParameter("@ItemID", data.ItemID);
                p[18] = new SqlParameter("@IsApplyDiscount", data.IsApplyDiscount ?? true);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateItem_Admin", p);


                if (data.Modifiers != "" && data.Modifiers != null)
                {
                   
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@ItemID", data.ItemID);
                    p1[1] = new SqlParameter("@Modifiers", data.Modifiers);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertItemModifiers_Admin", p1);
                }
                else
                {
                    SqlParameter[] p1 = new SqlParameter[1];
                    p1[0] = new SqlParameter("@ItemID", data.ItemID);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteItemModifiers_Admin", p1);
                }
                if (data.Addons != "" && data.Addons != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@ItemID", data.ItemID);
                    p1[1] = new SqlParameter("@Addons", data.Addons);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertItemAddons_Admin", p1);

                }
                else
                {
                    SqlParameter[] p1 = new SqlParameter[1];
                    p1[0] = new SqlParameter("@ItemID", data.ItemID);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteItemAddons_Admin", p1);
                }

                return rtn;
            }
            catch (Exception)
            {
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
        //public int UpdateItemSettings(ItemSettingsBLL data)
        //{
        //    try
        //    {

        //        if (data.Items != "" && data.Items != null)
        //        {
        //            SqlParameter[] p1 = new SqlParameter[2];
        //            p1[0] = new SqlParameter("@BrandID", data.BrandID);
        //            p1[1] = new SqlParameter("@Items", data.Items);
        //            (new DBHelper().ExecuteNonQueryReturn)("sp_insertItemSettings_Admin", p1);
        //        }
        //        else
        //        {
        //            SqlParameter[] pp = new SqlParameter[2];
        //            pp[0] = new SqlParameter("@ItemSettingTitle", data.ItemSettingTitle);
        //            pp[1] = new SqlParameter("@IsItemSetting", data.IsItemSetting);
        //            (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateTodaySpecial_Admin", pp);
        //        }
        //        return 1;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        public int Delete(ItemBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.ItemID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteItem", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
