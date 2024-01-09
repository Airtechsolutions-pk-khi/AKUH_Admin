using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class appSettingViewModel
    {
    }

    public class AppSetingBLL
    {
        public int AppsettingID { get; set; }
        public string Discount { get; set; }
        public string DeliveryCharges { get; set; }      
        public string Tax { get; set; }
        public string MinimumOrderAmount { get; set; }
        public int StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }

}
