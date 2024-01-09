

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

    public class TimeSlotDB : baseDB
    {
        public static TimeSlotBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public TimeSlotDB()
           : base()
        {
            repo = new TimeSlotBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<TimeSlotBLL> GetAll()
        {
            try
            {
                var lst = new List<TimeSlotBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllTimeSlot", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<TimeSlotBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TimeSlotBLL Get(int id)
        {
            try
            {
                var _obj = new TimeSlotBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetTimeslotbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<TimeSlotBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(TimeSlotBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@TimeSlot", data.TimeSlot);
                p[1] = new SqlParameter("@StatusID", data.StatusID);
                p[2] = new SqlParameter("@CreatedDate", data.CreatedDate);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertTimeSlot_Admin", p);

                return rtn;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(TimeSlotBLL data)
        {
            try
            {
                int rtn = 0;

                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@TimeSlot", data.TimeSlot);
                p[1] = new SqlParameter("@StatusID", data.StatusID);
                p[2] = new SqlParameter("@CreatedDate", data.CreatedDate);
                p[3] = new SqlParameter("@TimeSlotID", data.TimeSlotID);


                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateTimeSlot_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(TimeSlotBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@id", data.TimeSlotID);
                p[1] = new SqlParameter("@CreatedDate", data.CreatedDate);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteTimeslot", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
