using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class corporateclientViewModel
    {
    }

    public class CorporateClientBLL
    {
        public int CorporateClientID { get; set; }
        public string ClientName { get; set; }
         
        public string Image { get; set; }
        public int StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }

}
