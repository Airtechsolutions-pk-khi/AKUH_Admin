

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

    public class workshopDB : baseDB
    {
        public static WorkshopBLL repo;
        public static DataTable _dt;
        public static DataTable _dt1;
        public static DataSet _ds;
        public workshopDB()
           : base()
        {
            repo = new WorkshopBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<WorkshopBLL> GetAll()
        {
            try
            {
                var lst = new List<WorkshopBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllWorkshops", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<WorkshopBLL>>();
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

        public WorkshopBLL Get(int id)
        {
            try
            {
                var _obj = new WorkshopBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetWorkshopbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<WorkshopBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(WorkshopBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[12];

                p[0] = new SqlParameter("@OrganizerID", data.OrganizerID);
                p[1] = new SqlParameter("@Name", data.Name);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Date", data.Date);
                p[5] = new SqlParameter("@StartTime", data.StartTime);
                p[6] = new SqlParameter("@EndTime", data.EndTime);
                p[7] = new SqlParameter("@PdfLink", data.PdfLink);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@Link", data.Link);
                p[10] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                p[11] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertWorkshop_Admin", p);
                //rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertWorkshop_Admin", p).Rows[0]["EventID"].ToString());
                 
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

        public int Update(WorkshopBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[13];

                p[0] = new SqlParameter("@OrganizerID", data.OrganizerID);
                p[1] = new SqlParameter("@Name", data.Name);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Date", data.Date);
                p[5] = new SqlParameter("@StartTime", data.StartTime);
                p[6] = new SqlParameter("@EndTime", data.EndTime);
                p[7] = new SqlParameter("@PdfLink", data.PdfLink);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@Link", data.Link);
                p[10] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                p[11] = new SqlParameter("@WorkshopID", data.WorkshopID);
                p[12] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateWorkshop_Admin", p);
                 
                
                return rtn;
            }
            catch (Exception ex)
            {
                LogExceptionToFile(ex, @"E:\exceptionLog.txt");
                return 0;
            }
        }
        public int Delete(WorkshopBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@WorkshopID", data.WorkshopID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteWorkshop_Admin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                LogExceptionToFile(ex, @"E:\exceptionLog.txt");
                return 0;
            }
        }
    }
}
