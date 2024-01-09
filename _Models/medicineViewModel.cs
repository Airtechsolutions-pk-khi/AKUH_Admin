using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class medicineViewModel
    {
    }
    public class MedicineBLL
    {
        public int MedicineID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string BrandDetails { get; set; }
        public float Price { get; set; }
        public string QuantityDescription { get; set; }
        public int StatusID { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
