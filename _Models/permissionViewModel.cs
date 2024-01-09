using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class permissionViewModel
    {
    }

    public class PermissionBLL
    {
        public int PermissionID { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public bool FormAccess { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<PermissionFormBLL> permissionForms { get; set; }
    }
    public class PermissionFormBLL
    {
        public int PermissionID { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public bool FormAccess { get; set; }
        public bool? Notification { get; set; }
        public bool? Doctor { get; set; }
        public bool? User { get; set; }
        public bool? Pharmacy { get; set; }
    }
}
