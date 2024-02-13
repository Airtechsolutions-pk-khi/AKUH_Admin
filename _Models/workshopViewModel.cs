using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AKU_Admin._Models
{
    public class workshopViewModel
    {
    }
    public class WorkshopBLL
    {
        public int WorkshopID { get; set; }

        public int? OrganizerID { get; set; }

        public string Name { get; set; }

        public string OrganizerName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime? Date { get; set; }

        public int? DisplayOrder { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string PdfLink { get; set; }

        public string Link { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? Updatedby { get; set; }

    }

   
}
