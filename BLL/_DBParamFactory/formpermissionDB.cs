
using DAL.Models;
using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class formpermission : baseDB
    {
        public static FormPermissionBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public formpermission()
            : base()
        {
            repo = new FormPermissionBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<FormPermissionBLL> GetAll()
        {
            try
            {
                var lst = new List<FormPermissionBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getuser", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FormPermissionBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public FormPermissionBLL Get(string rolename)
        {
            try
            {
                var _obj = new FormPermissionBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@RoleName", rolename);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetFormPermissionID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<FormPermissionBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        //public int Insert(FormPermissionBLL data)
        //{
        //    try
        //    {
        //        int rtn = 0;
        //        SqlParameter[] p = new SqlParameter[7];

        //        p[0] = new SqlParameter("@UserName", data.UserName);
        //        p[1] = new SqlParameter("@Email", data.Email);
        //        p[2] = new SqlParameter("@Type", data.Type);
        //        p[3] = new SqlParameter("@Password", data.Password);
        //        p[4] = new SqlParameter("@StatusID", data.StatusID);
        //        p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdateBy);
        //        p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);


        //        rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertUser_Admin", p);
        //        return rtn;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
        public int Update(FormPermissionBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];
                p[0] = new SqlParameter("@Notification", data.Notification);
                p[1] = new SqlParameter("@Doctor", data.Doctor);
                p[2] = new SqlParameter("@MamjiUser", data.MamjiUser);
                p[3] = new SqlParameter("@Pharmacy", data.Pharmacy);
                p[4] = new SqlParameter("@Reception", data.Reception);
                p[5] = new SqlParameter("@Diagnostic", data.Diagnostic);
                p[6] = new SqlParameter("@Reports", data.Reports);
                p[7] = new SqlParameter("@Setting", data.Setting);
                p[8] = new SqlParameter("@RoleName", data.RoleName);
                p[9] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[10] = new SqlParameter("@CreatedDate", data.CreatedDate);
                //p[10] = new SqlParameter("@PermissionID", data.FormPermissionID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateformpermission_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(FormPermissionBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.FormPermissionID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteUser", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
