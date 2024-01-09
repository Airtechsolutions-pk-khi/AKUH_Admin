using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class promotionViewModel
    {
    }

    public class PromotionBLL
    {
        public int PromotionID { get; set; }
        public string Name { get; set; }
        public string Descriiption { get; set; }
        public string Image { get; set; }      
        public int? Discount { get; set; }       
        public int StatusID { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

}
