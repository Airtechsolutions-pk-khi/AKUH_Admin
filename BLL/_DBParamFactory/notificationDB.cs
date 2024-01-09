

using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class notificationDB : baseDB
    {
        public static NotificationBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public notificationDB()
           : base()
        {
            repo = new NotificationBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<NotificationBLL> GetAll(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<NotificationBLL>();
				SqlParameter[] p = new SqlParameter[2];

				p[0] = new SqlParameter("@fromdate", FromDate.Date);
				p[1] = new SqlParameter("@todate", ToDate.Date);

				_dt = (new DBHelper().GetTableFromSP)("sp_GetNotifications_V2", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<NotificationBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public NotificationBLL Get(int id)
        {
            try
            {
                var _obj = new NotificationBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetNotificationbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<NotificationBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Status(NotificationBLL data)
        {
            try
            {
                int _obj = 0;
                int isread = data.IsRead == true ? 1 : 0;  
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.NotificationID);
                p[1] = new SqlParameter("@IsRead", isread);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_NotificationStatus", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
