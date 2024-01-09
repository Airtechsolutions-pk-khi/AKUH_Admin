using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class bannerViewModel
    {
    }

    public class BannerBLL
    {
        public int BannerID { get; set; }

            public string Type { get; set; }

            public string Screen { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Image { get; set; }

            public int? DisplayOrder { get; set; }

            public int? StatusID { get; set; }

            public DateTime? Createdon { get; set; }

            public DateTime? Updatedon { get; set; }

            public int? UpdatedBy { get; set; }
    }

}
