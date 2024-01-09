

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

    public class specialitiesDB : baseDB
    {
        public static SpecialistBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public specialitiesDB()
           : base()
        {
            repo = new SpecialistBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<SpecialistBLL> GetAll()
        {
            try
            {
                var lst = new List<SpecialistBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllSpecialities");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpecialistBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SpecialistBLL Get(int id)
        {
            try
            {
                var _obj = new SpecialistBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetSpecialitybyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpecialistBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(SpecialistBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@UrduName", data.UrduName);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertSpeciality_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(SpecialistBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@UrduName", data.UrduName);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@SpecialistID", data.SpecialistID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateSpeciality_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(SpecialistBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.SpecialistID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteSpeciality", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
