

using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
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

    public class eventCategoryDB : baseDB
    {
        public static EventCategoryBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public eventCategoryDB()
           : base()
        {
            repo = new EventCategoryBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<EventCategoryBLL> GetAll()
        {
            try
            {
                var lst = new List<EventCategoryBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllEventCategory_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventCategoryBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<EventCategoryBLL> GetAllDropdown()
        {
            try
            {
                var lst = new List<EventCategoryBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllEventCategoryDropdown_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventCategoryBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EventCategoryBLL Get(int id)
        {
            try
            {
                var _obj = new EventCategoryBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetEventCategorybyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventCategoryBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(EventCategoryBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@CreatedOn", data.Createdon);
                p[5] = new SqlParameter("@DisplayOrder", data.DisplayOrder);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertEventCategory_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(EventCategoryBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@Updatedon", data.Updatedon);
                p[5] = new SqlParameter("@EventCategoryID", data.EventCategoryID);
                p[6] = new SqlParameter("@DisplayOrder", data.DisplayOrder);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateEventCategory_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(EventCategoryBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@EventCategoryID", data.EventCategoryID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteEventCategory_Admin", p);

                return _obj;
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
    }
}
