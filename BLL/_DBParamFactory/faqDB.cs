

using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
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

    public class faqDB : baseDB
    {
        public static EventCategoryBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public faqDB()
           : base()
        {
            repo = new EventCategoryBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<FaqBLL> GetAll()
        {
            try
            {
                var lst = new List<FaqBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllFaq_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FaqBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public FaqBLL Get(int id)
        {
            try
            {
                var _obj = new FaqBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetFaqByID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FaqBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(FaqBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@FaqQ", data.FaqQ);
                p[1] = new SqlParameter("@FaqA", data.FaqA);                
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@CreatedOn", data.Createdon);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertFaq_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(FaqBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@FaqQ", data.FaqQ);
                p[1] = new SqlParameter("@FaqA", data.FaqA);                
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@Createdon", data.Createdon);
                p[4] = new SqlParameter("@FaqID", data.FaqID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateFaq_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(FaqBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@FaqID", data.FaqID);
                p[1] = new SqlParameter("@Createdon", data.Createdon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteFaq_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
