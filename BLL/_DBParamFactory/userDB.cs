
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

    public class userDB : baseDB
    {
        public static LoginBLL repo;
        public static UserBLL user;
        public static DataTable _dt;
        public static DataSet _ds;
        public userDB()
            : base()
        {
            user = new UserBLL();
            repo = new LoginBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<UserBLL> GetAllUser()
        {
            try
            {
                var lst = new List<UserBLL>();
                SqlParameter[] p = new SqlParameter[0];

                //_dt = (new DBHelper().GetTableFromSP)("sp_GetAllUser_Admin", p);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetRegisterUser_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<UserBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public UserBLL GetUser(int id)
        {
            try
            {
                var _obj = new UserBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetUserbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<UserBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int InsertUser(UserBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@UserName", data.UserName);
                p[1] = new SqlParameter("@Email", data.Email);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@Address", data.Address);
                p[4] = new SqlParameter("@ContactNo", data.ContactNo);
                p[5] = new SqlParameter("@Password", data.Password);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@CreatedDate", data.CreatedDate);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_InsertUser_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateUser(UserBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[9];

                p[0] = new SqlParameter("@UserName", data.UserName);
                p[1] = new SqlParameter("@Email", data.Email);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@Address", data.Address);
                p[4] = new SqlParameter("@ContactNo", data.ContactNo);
                p[5] = new SqlParameter("@Password", data.Password);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@UpdatedDate", data.UpdatedDate);
                p[8] = new SqlParameter("@UserID", data.UserID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateUser_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteUser(UserBLL data)
        {
            try
            {
                int _obj = 0;
                data.UpdatedDate = DateTime.UtcNow.AddMinutes(180);
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@UserID", data.UserID);
                p[1] = new SqlParameter("@UpdatedDate", data.UpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteUser_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public List<LoginBLL> GetAll()
        {
            try
            {
                var lst = new List<LoginBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getuser", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LoginBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }      
        public List<PermissionBLL> GetRoles()
        {
            try
            {
                var lst = new List<PermissionBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getRoles", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PermissionBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public LoginBLL Get(int id)
        {
            try
            {
                var _obj = new LoginBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetUserbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LoginBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(LoginBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[7];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Email", data.Email);                
                p[2] = new SqlParameter("@Password", data.Password);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@Createdon", data.Createdon);
                p[5] = new SqlParameter("@Updatedon", data.Updatedon);


                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertUser_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(LoginBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];
                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Email", data.Email);                
                p[3] = new SqlParameter("@Password", data.Password);
                p[4] = new SqlParameter("@StatusID", data.StatusID);
                p[5] = new SqlParameter("@LastUpdatedBy", data.UpdatedBy);
                p[6] = new SqlParameter("@Updatedon", data.Updatedon);
                p[7] = new SqlParameter("@ID", data.AdminID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateUser_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LoginBLL data)
        {
            try
            {
                int _obj = 0;
                data.Updatedon = DateTime.UtcNow;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@AdminID", data.AdminID);
                p[1] = new SqlParameter("@Updatedon", data.Updatedon);


                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteUser_Admin", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UpdatePermission(PermissionBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[6];

                p[0] = new SqlParameter("@FormName", data.FormName);
                p[1] = new SqlParameter("@ForAccess", data.FormAccess);
                p[2] = new SqlParameter("@RoleName", data.RoleName);
                p[3] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[4] = new SqlParameter("@UpdatedDate", data.UpdatedDate);
                p[5] = new SqlParameter("@PermissionID", data.PermissionID);


                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updatepermission_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public PermissionFormBLL GetPermission(string rn)
        {
            try
            {
                var _obj = new PermissionBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", rn);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetUserPermissionbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PermissionBLL>>().FirstOrDefault();
                    }
                }

                return new PermissionFormBLL();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<PermissionFormBLL> GetAllPermission()
        {
            try
            {
                var lst = new List<PermissionFormBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getAllPermission", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PermissionFormBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Status(UserBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@AttendeesID", data.UserID);
                p[1] = new SqlParameter("@StatusID", data.StatusID);
              //p[2] = new SqlParameter("@MessageForAttendee", data.MessageForAttendee);
              //p[3] = new SqlParameter("@Subject", data.Subject);
                p[4] = new SqlParameter("@Updatedon", DateTime.UtcNow.AddMinutes(300));
              //p[5] = new SqlParameter("@MeetingLink", data.MeetingLink);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_EventAttendeesStatus", p);
                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
