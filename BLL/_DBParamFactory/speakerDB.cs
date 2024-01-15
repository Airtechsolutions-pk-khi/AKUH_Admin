


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

    public class speakerDB : baseDB
    {
        public static SpeakerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public speakerDB()
           : base()
        {
            repo = new SpeakerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<SpeakerBLL> GetAll()
        {
            try
            {
                var lst = new List<SpeakerBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllSpeaker_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpeakerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<SpeakerBLL> GetDropdown()
        {
            try
            {
                var lst = new List<SpeakerBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllSpeakerDropdown_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpeakerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SpeakerBLL Get(int id)
        {
            try
            {
                var _obj = new SpeakerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetSpeakerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpeakerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(SpeakerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);
                p[2] = new SqlParameter("@Company", data.Company);
                p[3] = new SqlParameter("@About", data.About);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@CreatedOn", data.Createdon);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertSpeaker_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(SpeakerBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);
                p[2] = new SqlParameter("@Company", data.Company);
                p[3] = new SqlParameter("@About", data.About);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@Updatedon", data.Updatedon);
                p[7] = new SqlParameter("@SpeakerID", data.SpeakerID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateSpeaker_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(SpeakerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@SpeakerID", data.SpeakerID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteSpeaker_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
