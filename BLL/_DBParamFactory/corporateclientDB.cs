

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

    public class corporateclientDB : baseDB
    {
        public static CorporateClientBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public corporateclientDB()
           : base()
        {
            repo = new CorporateClientBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<CorporateClientBLL> GetAll()
        {
            try
            {
                var lst = new List<CorporateClientBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_getCorporateClient", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<CorporateClientBLL>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CorporateClientBLL Get(int id)
        {
            try
            {
                var _obj = new CorporateClientBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetCorporateClientbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {                        
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CorporateClientBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(CorporateClientBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@ClientName", data.ClientName);                 
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[4] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertCorporateClient_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CorporateClientBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@ClientName", data.ClientName);                 
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[4] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[5] = new SqlParameter("@CorporateClientID", data.CorporateClientID);
                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateCorporateClient_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(CorporateClientBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.CorporateClientID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteCorporateClient", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
