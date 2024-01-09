using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class couponViewModel
    {
    }
    public class CouponBLL1
    {
        public int CouponID { get; set; }
        
    }
    public class CouponBLL
    {
        public int CouponID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }      
        public int? Amount { get; set; }
        public string CouponCode { get; set; }
        public int? StatusID { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }

}
