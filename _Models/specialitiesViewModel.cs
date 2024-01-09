using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class specialitiesViewModel
    {
    }

    public class SpecialistBLL
    {
        public int SpecialistID { get; set; }
        public string Name { get; set; }
        public string UrduName { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
    }

}
