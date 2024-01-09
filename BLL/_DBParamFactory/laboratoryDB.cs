using AKU_Admin._Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MohsinFoodAdmin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{
	public class laboratoryDB : baseDB
	{
		public static LaboratoryBLL repo;
		public static DataTable _dt;
		public static DataSet _ds;
		public laboratoryDB() : base()
		{
			repo = new LaboratoryBLL();
			_dt = new DataTable();
			_ds = new DataSet();
		}
		public List<LaboratoryBLL> GetAll(DateTime FromDate, DateTime ToDate)
		{
			try
			{
				var lst = new List<LaboratoryBLL>();
				SqlParameter[] p = new SqlParameter[2];

				p[0] = new SqlParameter("@fromdate", FromDate);
				p[1] = new SqlParameter("@todate", ToDate);
				_dt = (new DBHelper().GetTableFromSP)("sp_getAllReports_V2", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LaboratoryBLL>>();
					}
				}
				return lst;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public LaboratoryBLL Get(int id)
		{
			try
			{
				var _obj = new LaboratoryBLL();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@id", id);
				_dt = (new DBHelper().GetTableFromSP)("sp_GetReportbyID_Admin", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						_obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LaboratoryBLL>>().FirstOrDefault();
					}
				}
				return _obj;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public CustomerBLL GetDetail(string registrationNo)
		{
			try
			{
				var _obj = new CustomerBLL();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@registrationNo", registrationNo);
				_dt = (new DBHelper().GetTableFromSP)("sp_GetCustomerDetail_Admin", p);
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
		public CustomerBLL Getcustomer(int id)
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
		public int Insert(LaboratoryBLL data)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[9];

				p[0] = new SqlParameter("@Name", data.Name);
				p[1] = new SqlParameter("@ImagePath", data.FilePath);
				p[2] = new SqlParameter("@LabReferenceNo", data.LabReferenceNo);
				p[3] = new SqlParameter("@RegistrationNo", data.RegistrationNo);
				p[4] = new SqlParameter("@StatusID", data.StatusID);
				p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
				p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
				p[7] = new SqlParameter("@DiagnoseCatID", data.DiagnoseCatID);
                p[8] = new SqlParameter("@CustomerID", data.CustomerID);


                //rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertReport_Admin_V2", p);
                rtn = int.Parse(new DBHelper().GetTableFromSP("sp_insertReport_Admin_V2", p).Rows[0][0].ToString());

                return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}

		public int Update(LaboratoryBLL data)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[10];

				p[0] = new SqlParameter("@Name", data.Name);
				p[1] = new SqlParameter("@ImagePath", data.FilePath);
				p[2] = new SqlParameter("@LabReferenceNo", data.LabReferenceNo);
				p[3] = new SqlParameter("@RegistrationNo", data.RegistrationNo);
				p[4] = new SqlParameter("@StatusID", data.StatusID);
				p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
				p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
				p[7] = new SqlParameter("@DiagnoseCatID", data.DiagnoseCatID);
				p[8] = new SqlParameter("@LaboratoryID", data.LaboratoryID);
                p[9] = new SqlParameter("@CustomerID", data.CustomerID);

                //rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateReport_Admin_V2", p);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateReport_Admin_V2", p);
				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
		public int Delete(LaboratoryBLL data)
		{
			try
			{
				int _obj = 0;
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@id", data.LaboratoryID);

				_obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteLabReport", p);

				return _obj;
			}
			catch (Exception)
			{
				return 0;
			}
		}
	}
}
