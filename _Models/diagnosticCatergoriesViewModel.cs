using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class diagnosticCatergoriesViewModel
    {
    }

    public class DiagnosticCategoriesBLL
    {
        public int DiagnosticCatID { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
    }

}
