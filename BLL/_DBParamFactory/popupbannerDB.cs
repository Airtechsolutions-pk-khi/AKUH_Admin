

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
using static System.Net.Mime.MediaTypeNames;

namespace BAL.Repositories
{

    public class popupbannerDB : baseDB
    {
        public static PopupBannerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public popupbannerDB()
           : base()
        {
            repo = new PopupBannerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<PopupBannerBLL> GetAll()
        {
            try
            {
                var lst = new List<PopupBannerBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetBannerPopup", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PopupBannerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PopupBannerBLL Get(int id)
        {
            try
            {
                var _obj = new PopupBannerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@BannerID", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBannerPopupbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<BannerBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PopupBannerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(PopupBannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);                
                p[3] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
 
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_InsertPopupBanner_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(PopupBannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@Updatedon",DateTime.UtcNow.AddMinutes(300));              
                p[4] = new SqlParameter("@ID", data.ID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdatePopupBanner_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(PopupBannerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", data.ID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeletePopupBanner", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
