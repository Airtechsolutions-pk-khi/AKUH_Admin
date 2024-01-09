

using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class dashboardDB : baseDB
    {
        public static DataTable _dt;
        public static DataSet _ds;
        public dashboardDB()
           : base()
        {
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<DashboardSummary> GetDashboardSummary()
        {
            try
            {
                var lst = new List<DashboardSummary>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("GetDashboard", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DashboardSummary>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DashboardMAEN GetMAENSummary()
        {
            var obj = new DashboardMAEN();

            try
            {
                SqlParameter[] p = new SqlParameter[0];
                //p[0] = new SqlParameter("@FromDate", FromDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_SalesMAEN", p);

                obj.Morning = Convert.ToDouble(_dt.Rows[0]["Sales"].ToString());
                obj.AfterNoon = Convert.ToDouble(_dt.Rows[1]["Sales"].ToString());
                obj.Evening = Convert.ToDouble(_dt.Rows[2]["Sales"].ToString());
                obj.Night = Convert.ToDouble(_dt.Rows[3]["Sales"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                obj.Morning = 0;
                obj.AfterNoon = 0;
                obj.Evening = 0;
                obj.Night = 0;
                return obj;
            }
        }
        public DashboardToday GetTodaySales()
        {
            var rsp = new DashboardToday();
            var lstS = new List<string>();
            var lstTS = new List<string>();

            try
            {
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_SaleaToday_admin", p);

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstS.Add(_dt.Rows[i]["Sales"].ToString());
                }
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstTS.Add(_dt.Rows[i]["TimeSlot"].ToString());
                }
                rsp.Sales = lstS;
                rsp.TimeSlot = lstTS;
            }
            catch (Exception ex)
            {
                rsp.Sales = new List<string>();
                rsp.TimeSlot = new List<string>();
            }

            return rsp;
        }
    }
}
