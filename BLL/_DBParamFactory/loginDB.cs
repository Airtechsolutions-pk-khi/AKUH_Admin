
using DAL.Models;
using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class loginDB : baseDB
    {
        
        public static DataTable _dt;
        public static DataSet _ds;

        public loginDB()
           : base()
        {
           
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public LoginBLL GetAuthenticateUser(string username, string password)
        {
             
            var repo = new LoginBLL();
            try
            {

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@email", username);
                p[1] = new SqlParameter("@password", password);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_Authenticate_admin", p);
                if (_ds != null)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<LoginBLL>>().FirstOrDefault();
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return repo;
        }
    }
}
