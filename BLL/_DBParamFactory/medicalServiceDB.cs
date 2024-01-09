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
    public class medicalServiceDB : baseDB
    {
        public static medicalServiceBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public medicalServiceDB() : base()
        {
            repo = new medicalServiceBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<medicalServiceBLL> GetAll()
        {
            try
            {
                var lst = new List<medicalServiceBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllServices");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<medicalServiceBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
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
        public medicalServiceBLL Get(int id)
        {
            try
            {
                var _obj = new medicalServiceBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetServicebyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<medicalServiceBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
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
        public int Insert(medicalServiceBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Requirment", data.Requirment);
                p[4] = new SqlParameter("@Fees", data.Fees);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@NursingTypeID", data.NursingTypeID);
                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertService_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
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
        public int Update(medicalServiceBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Requirment", data.Requirment);
                p[4] = new SqlParameter("@Fees", data.Fees);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@NursingTypeID", data.NursingTypeID);
                p[7] = new SqlParameter("@MedicalServiceID", data.MedicalServiceID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateService_Admin", p);

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
        public int Delete(medicalServiceBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.MedicalServiceID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteService", p);

                return _obj;
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
