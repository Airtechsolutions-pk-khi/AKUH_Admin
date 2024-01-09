using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class prescriptionViewModel
    {
    }

    public class PrescriptionBLL
    {
        public int PrescriptionID { get; set; }
        public string CustomerName { get; set; }
        public string Image { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public int StatusID { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

}
