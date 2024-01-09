

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

    public class customerDB : baseDB
    {
        public static CustomerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public customerDB()
           : base()
        {
            repo = new CustomerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<CustomerBLL> GetAll()
        {
            try
            {
                var lst = new List<CustomerBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getcustomer", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<CustomerBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<CustomerDropdownBLL> GetAlldropdown()
        {
            try
            {
                var lst = new List<CustomerDropdownBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getcustomer_DropDown", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<CustomerBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerDropdownBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<CustomerRNoDropdownBLL> GetAlldropdownRNo()
        {
            try
            {
                var lst = new List<CustomerRNoDropdownBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getcustomerRNo_DropDown", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<CustomerBLL>();
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerRNoDropdownBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public CustomerBLL Get(int id)
        {
            try
            {
                var _obj = new CustomerBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetCustomerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(CustomerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@FullName", data.FullName);
                p[1] = new SqlParameter("@Email", data.Email);
                p[2] = new SqlParameter("@Password", data.Password);
                p[3] = new SqlParameter("@Mobile", data.Mobile);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@StatusID", data.StatusID);
                p[6] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[7] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[8] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[9] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[10] = new SqlParameter("@CustomerID", data.CustomerID);
                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertCustomer_Admin", p);              
                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Update(CustomerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@FullName", data.FullName);
                p[1] = new SqlParameter("@Email", data.Email);
                p[2] = new SqlParameter("@Password", data.Password);
                p[3] = new SqlParameter("@Mobile", data.Mobile);
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@RegistrationNo", data.RegistrationNo);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);               
                p[8] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[9] = new SqlParameter("@CustomerID", data.CustomerID);
                

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateCustomer_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(CustomerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.CustomerID);
                p[1] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(300));

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteCustomer", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
