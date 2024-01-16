

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

    public class partnerDB : baseDB
    {
        public static PartnerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public partnerDB()
           : base()
        {
            repo = new PartnerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<PartnerBLL> GetAll()
        {
            try
            {
                var lst = new List<PartnerBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllPartners_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PartnerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PartnerBLL Get(int id)
        {
            try
            {
                var _obj = new PartnerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetPartnerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PartnerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(PartnerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@CreatedOn", data.Createdon);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertPartner_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(PartnerBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@PartnerID", data.PartnerID);
                p[4] = new SqlParameter("@Updatedon", data.Updatedon);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdatePartner_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(PartnerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@PartnerID", data.PartnerID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeletePartner_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
