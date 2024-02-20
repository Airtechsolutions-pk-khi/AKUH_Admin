


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

    public class organisingcommitteeDB : baseDB
    {
        public static OrganisingCommitteeBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public organisingcommitteeDB()
           : base()
        {
            repo = new OrganisingCommitteeBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<OrganisingCommitteeBLL> GetAll()
        {
            try
            {
                var lst = new List<OrganisingCommitteeBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllOrganisingCommittee_Admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganisingCommitteeBLL>>();
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
        public OrganisingCommitteeBLL Get(int id)
        {
            try
            {
                var _obj = new OrganisingCommitteeBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetOrganisingCommitteeByID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganisingCommitteeBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(OrganisingCommitteeBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[5];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);                
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@CreatedOn", DateTime.UtcNow.AddMinutes(300));

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_InsertOrganisingCommittee_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(OrganisingCommitteeBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Designation", data.Designation);               
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@Updatedon",DateTime.UtcNow.AddMinutes(300));
                p[5] = new SqlParameter("@ID", data.ID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_UpdateOrganisingCommittee_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(OrganisingCommitteeBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", data.ID);
                p[1] = new SqlParameter("@Updatedon", DateTime.UtcNow.AddMinutes(300));

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteOrganisingCommittee_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
