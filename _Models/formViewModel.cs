using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class formViewModel
    {
    }
    public class FormPermissionBLL
    {
        public int FormPermissionID { get; set; }
        public string RoleName { get; set; }
        public int? Notification { get; set; }
        public int? Doctor { get; set; }
        public int? MamjiUser { get; set; }
        public int? Pharmacy { get; set; }
        public int? Reception { get; set; }
        public int? Diagnostic { get; set; }
        public int? Reports { get; set; }
        public int? Setting { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
