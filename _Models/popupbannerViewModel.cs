using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class popupbannerViewModel
    {
    }

    public class PopupBannerBLL
    {
        public int ID { get; set; }

        public string Name { get; set; }


        public string Image { get; set; }


        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

    }

}
