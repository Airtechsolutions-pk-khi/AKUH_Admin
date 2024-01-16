

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

    public class bannerDB : baseDB
    {
        public static BannerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public bannerDB()
           : base()
        {
            repo = new BannerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<BannerBLL> GetAll()
        {
            try
            {
                var lst = new List<BannerBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetBanner", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BannerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BannerBLL Get(int id)
        {
            try
            {
                var _obj = new BannerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@BannerID", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBannerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<BannerBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BannerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(BannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[5] = new SqlParameter("@Updatedon", data.Updatedon);
                p[6] = new SqlParameter("@Type", data.Type);
                p[7] = new SqlParameter("@Screen", data.Screen);
                p[8] = new SqlParameter("@DisplayOrder", 1);
                p[9] = new SqlParameter("@Createdon", data.Createdon);
 
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_InsertBanner_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(BannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[5] = new SqlParameter("@Updatedon", data.Updatedon);
                p[6] = new SqlParameter("@Type", data.Type);
                p[7] = new SqlParameter("@Screen", data.Screen);
                p[8] = new SqlParameter("@DisplayOrder", data.DisplayOrder);
                p[9] = new SqlParameter("@Createdon", data.Createdon);
                p[10] = new SqlParameter("@BannerID", data.BannerID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateBanner_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(BannerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@BannerID", data.BannerID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteBanner", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
