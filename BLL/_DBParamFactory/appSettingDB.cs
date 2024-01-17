

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

    public class appSettingDB : baseDB
    {
        public static AppSetingBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public appSettingDB()
           : base()
        {
            repo = new AppSetingBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<AppSetingBLL> GetAll()
        {
            try
            {
                var lst = new List<AppSetingBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetSetting", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AppSetingBLL>>();
                        //lst = _dt.DataTableToList<AppSetingBLL>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AppSetingBLL Get(int id)
        {
            try
            {
                var _obj = new AppSetingBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetSettingsByID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AppSetingBLL>>().FirstOrDefault();
                        //_obj = _dt.DataTableToList<AppSetingBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(AppSetingBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@About", data.About);
                p[1] = new SqlParameter("@PrivacyPolicy", data.PrivacyPolicy);
                p[2] = new SqlParameter("@SplashScreen", data.SplashScreen);
                p[3] = new SqlParameter("@AppName", data.AppName);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@AppVersion", data.AppVersion);
                p[6] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                p[7] = new SqlParameter("@SettingID", data.SettingID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertSetting_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(AppSetingBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[12];

                p[0] = new SqlParameter("@About", data.About);
                p[1] = new SqlParameter("@PrivacyPolicy", data.PrivacyPolicy);
                p[2] = new SqlParameter("@SplashScreen", data.SplashScreen);
                p[3] = new SqlParameter("@AppName", data.AppName);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@AppVersion", data.AppVersion);
                p[6] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                p[7] = new SqlParameter("@SettingID", data.SettingID);
                p[8] = new SqlParameter("@FacebookUrl", data.FacebookUrl);
                p[9] = new SqlParameter("@InstagramUrl", data.InstagramUrl);
                p[10] = new SqlParameter("@TwitterUrl", data.TwitterUrl);
                p[11] = new SqlParameter("@YoutubeUrl", data.YoutubeUrl);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateSetting_Admin", p);
                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(AppSetingBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.SettingID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.Createdon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteSetting", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
