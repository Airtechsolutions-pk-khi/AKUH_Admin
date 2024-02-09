 
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

    public class messageDB : baseDB
    {
        public static MessageBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public messageDB()
           : base()
        {
            repo = new MessageBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<MessageBLL> GetAll()
        {
            try
            {
                var lst = new List<MessageBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllMsgs_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MessageBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<OrganizerBLL> GetAllDropdown()
        {
            try
            {
                var lst = new List<OrganizerBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllOrganisersDropdown_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganizerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public MessageBLL Get(int id)
        {
            try
            {
                var _obj = new MessageBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetMsgByID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MessageBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(MessageBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@CreatedOn", DateTime.UtcNow.AddMinutes(300));

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertMessage_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(MessageBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@UpdatedOn", DateTime.UtcNow.AddMinutes(300));
                p[6] = new SqlParameter("@MessageID", data.MessageID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateMessage_Admin", p);

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
        public int Delete(MessageBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@MessageID", data.MessageID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteMsg_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
