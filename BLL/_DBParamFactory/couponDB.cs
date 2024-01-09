

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

    public class couponDB : baseDB
    {
        public static CouponBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public couponDB()
           : base()
        {
            repo = new CouponBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<CouponBLL> GetAll()
        {
            try
            {
                var lst = new List<CouponBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetCoupon", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CouponBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CouponBLL Get(int id)
        {
            try
            {
                var _obj = new CouponBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetCouponbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CouponBLL>>().FirstOrDefault();
                        //_obj = _dt.DataTableToList<CouponBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(CouponBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Type", data.Type);
                p[2] = new SqlParameter("@Amount", data.Amount);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@CouponCode", data.CouponCode);
                p[5] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[6] = new SqlParameter("@CouponID", data.CouponID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertCoupon_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CouponBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Title", data.Title);
                p[1] = new SqlParameter("@Type", data.Type);
                p[2] = new SqlParameter("@Amount", data.Amount);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@CouponCode", data.CouponCode);
                p[5] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[6] = new SqlParameter("@CouponID", data.CouponID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateCoupon_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(CouponBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.CouponID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteCoupon", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
