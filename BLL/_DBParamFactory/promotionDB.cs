

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

    public class promotionDB : baseDB
    {
        public static PromotionBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public promotionDB()
           : base()
        {
            repo = new PromotionBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<PromotionBLL> GetAll()
        {
            try
            {
                var lst = new List<PromotionBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_Promotion_admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PromotionBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PromotionBLL Get(int id)
        {
            try
            {
                var _obj = new PromotionBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetPromotionbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PromotionBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(PromotionBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Descriiption", data.Descriiption);
                p[2] = new SqlParameter("@Discount", data.Discount);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Discount", data.Discount);              
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[7] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[8] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[9] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                


                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertPromotion_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(PromotionBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Descriiption", data.Descriiption);
                p[2] = new SqlParameter("@Discount", data.Discount);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Discount", data.Discount);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[7] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[8] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[9] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[10] = new SqlParameter("@PromotionID", data.PromotionID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updatePromotion_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(PromotionBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.PromotionID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeletePromotion", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
