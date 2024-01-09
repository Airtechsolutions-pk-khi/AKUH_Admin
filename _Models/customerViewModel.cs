using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class customerViewModel
    {
    }
    public class CustomerBLL1
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
         
    }
    public class CustomerBLL
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public string RegistrationNo { get; set; }
        public int StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string Password { get; set; }
    }
    public class CustomerDropdownBLL
    {
        public int? CustomerID { get; set; }
        public string FullName { get; set; }
        
    }
    public class CustomerRNoDropdownBLL
    {
        public int? CustomerID { get; set; }
        public string RegistrationNo { get; set; }

    }
}