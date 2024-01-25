using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class dashboardViewModel
    {
    }
    public class RspDashboardMonth
    {        
        public DashboardMonth monthSale { get; set; }
    }
    public class RspDashboard
    {
        public DashboardSummary summarysales { get; set; }
        public DashboardToday todaysales{ get; set; }
        public DashboardMAEN maensales { get; set; }
    }
    public class DashboardSummary
    {
        public string totalRegisteredAttendees { get; set; }
        public string totalConfirmedAttendees { get; set; }
        public string upcomingEvents { get; set; }
        public string pastEvents { get; set; }
         
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
    public class DashboardMonth
    {
        public double Jan { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Apr { get; set; }
        public double May { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Aug { get; set; }
        public double Sept { get; set; }
        public double Oct { get; set; }
        public double Nov { get; set; }
        public double Dec { get; set; }
    }
}
