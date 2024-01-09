

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

                _dt = (new DBHelper().GetTableFromSP)("sp_GetSettingbyID_Admin", p);
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

                p[0] = new SqlParameter("@Discount", data.Discount);
                p[1] = new SqlParameter("@DeliveryCharges", data.DeliveryCharges);
                p[2] = new SqlParameter("@Tax", data.Tax);
                p[3] = new SqlParameter("@MinimumOrderAmount", data.MinimumOrderAmount);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[7] = new SqlParameter("@AppsettingID", data.AppsettingID);

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
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Discount", data.Discount);
                p[1] = new SqlParameter("@DeliveryCharges", data.DeliveryCharges);
                p[2] = new SqlParameter("@Tax", data.Tax);
                p[3] = new SqlParameter("@MinimumOrderAmount", data.MinimumOrderAmount);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                 

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateSetting_Admin", p);
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
                p[0] = new SqlParameter("@id", data.AppsettingID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

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
