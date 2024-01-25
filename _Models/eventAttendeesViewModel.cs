using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class eventAttendeesViewModel
    {
    }
    public class EventAttendeesBLL
    {
        public int AttendeesID { get; set; }

        public int? EventID { get; set; }

        public int? UserID { get; set; }

        public string FullName { get; set; }

        public string EventName { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Occupation { get; set; }

        public string Gender { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    
}
