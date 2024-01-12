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

    public class organizerDB : baseDB
    {
        public static OrganizerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public organizerDB()
           : base()
        {
            repo = new OrganizerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<OrganizerBLL> GetAll()
        {
            try
            {
                var lst = new List<OrganizerBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllOrganisers_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganizerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public OrganizerBLL Get(int id)
        {
            try
            {
                var _obj = new OrganizerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetOrganizerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganizerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(OrganizerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image); 
                p[3] = new SqlParameter("@StatusID", data.StatusID);                
                p[4] = new SqlParameter("@CreatedOn", data.Createdon);                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertOrganizer_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(OrganizerBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@Updatedon", data.Updatedon);
                p[5] = new SqlParameter("@OrganizerID", data.OrganizerID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateOrganizer_Admin", p);
               
                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(OrganizerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@OrganizerID", data.OrganizerID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteOrganizer_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
