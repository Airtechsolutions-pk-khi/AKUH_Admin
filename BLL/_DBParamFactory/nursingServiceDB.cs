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
    public class nursingServiceDB : baseDB
    {
        public static medicalServiceTypeBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public nursingServiceDB() : base()
        {
            repo = new medicalServiceTypeBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
       
        public List<medicalServiceTypeBLL> Type()
        {
            try
            {
                var lst = new List<medicalServiceTypeBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllServicesType");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<medicalServiceTypeBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public medicalServiceTypeBLL Getbyid(int id)
        {
            try
            {
                var _obj = new medicalServiceTypeBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetServiceTypebyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<medicalServiceTypeBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public int InsertType(medicalServiceTypeBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[2];

                p[0] = new SqlParameter("@Type", data.Type);
              
                p[1] = new SqlParameter("@StatusID", data.StatusID);
                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertServiceType_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
       
        public int UpdateType(medicalServiceTypeBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@Type", data.Type);
                p[1] = new SqlParameter("@NursingTypeID", data.NursingTypeID);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
               

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateServiceType_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
       
        public int DeleteType(medicalServiceTypeBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.NursingTypeID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteServiceType", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
