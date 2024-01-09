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
    public class nursingappointmentDB : baseDB
    {
        public static AppointmentBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public nursingappointmentDB()
           : base()
        {
            repo = new AppointmentBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<AppointmentBLL> GetAll()
        {
            try
            {
                var lst = new List<AppointmentBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetNursingAppointment_admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AppointmentBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public AppointmentBLL Get(int id)
        {
            try
            {
                var _obj = new AppointmentBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetNursingAppointmentbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AppointmentBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(AppointmentBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[16];

                p[0] = new SqlParameter("@AppointmentNo", data.AppointmentNo);
                p[1] = new SqlParameter("@PatientName", data.FullName);
                p[2] = new SqlParameter("@Age", data.Address);
                p[3] = new SqlParameter("@Gender", data.Email);
                p[4] = new SqlParameter("@Fees", data.Fees);
                p[5] = new SqlParameter("@BookingDate", data.BookingDate);
                p[6] = new SqlParameter("@Day", data.Day);
                p[7] = new SqlParameter("@Timeslot", data.Timeslot);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[10] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[11] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[12] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[13] = new SqlParameter("@AppointmentID", data.AppointmentID);
                p[14] = new SqlParameter("@DoctorID", data.DoctorID);
                p[15] = new SqlParameter("@CustomerID", data.CustomerID);


                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertAppointment_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //public int Update(AppointmentBLL data)
        //{
        //    try
        //    {
        //        int rtn = 0;
        //        SqlParameter[] p = new SqlParameter[14];

        //        p[0] = new SqlParameter("@FirstName", data.FirstName);
        //        p[1] = new SqlParameter("@LastName", data.LastName);
        //        p[2] = new SqlParameter("@FullName", data.FullName);
        //        p[3] = new SqlParameter("@ImagePath", data.ImagePath);
        //        p[4] = new SqlParameter("@Email", data.Email);
        //        p[5] = new SqlParameter("@Profile", data.Profile);
        //        p[6] = new SqlParameter("@Skills", data.Skills);
        //        p[7] = new SqlParameter("@Education", data.Education);
        //        p[8] = new SqlParameter("@StatusID", data.StatusID);
        //        p[9] = new SqlParameter("@CreatedBy", data.CreatedBy);
        //        p[10] = new SqlParameter("@CreatedOn", data.CreatedOn);
        //        p[11] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
        //        p[12] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
        //        p[13] = new SqlParameter("@doctorID", data.DoctorID);

        //        rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateAppointment_Admin", p);

        //        return rtn;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        public int Delete(AppointmentBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.AppointmentID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteAppointment", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Status(AppointmentBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@id", data.AppointmentID);
                p[1] = new SqlParameter("@AppointmentStatus", data.AppointmentStatus);
                p[2] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_NursingAppointmentStatus", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}