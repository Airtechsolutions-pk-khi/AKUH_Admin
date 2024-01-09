using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class dashboardViewModel
    {
    }
    public class RspDashboard
    {
        public DashboardSummary summarysales { get; set; }
        public DashboardToday todaysales{ get; set; }
        public DashboardMAEN maensales { get; set; }
    }
    public class DashboardSummary
    {
        public string TotalDoctors { get; set; }
        public string TotalPatients { get; set; }
        public string TotalDoctorAppointments { get; set; }
        public string TotalNursingAppointments { get; set; }
        public string TotalSales { get; set; }

        public string TodayAppointments { get; set; }
        public string TodayNursingAppointments { get; set; }
        public string TodayDoctorAppointments { get; set; }
        public string TodaySales { get; set; }
        //public string TodaySales { get; set; }
    }
    public class DashboardToday
    {
        public List<string> Sales { get; set; }
        public List<string> TimeSlot { get; set; }
    }
    public class DashboardMAEN
    {
        public double Morning { get; set; }
        public double Evening { get; set; }
        public double AfterNoon { get; set; }
        public double Night { get; set; }
    }
}
