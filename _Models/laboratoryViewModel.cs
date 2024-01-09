using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class laboratoryViewModel
    {
    }
    public class LaboratoryBLL
    {
        public int LaboratoryID { get; set; }
        public int? CustomerID { get; set; }
        public int DiagnoseCatID { get; set; }
        public IFormFile SelectedFile { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<DateTime> LastUpdatedDate { get; set; }
        public string FilePath { get; set; } = "";
        public string Image { get; set; }
        public string Name { get; set; }
        public string LabReferenceNo { get; set; }
        public string RegistrationNo { get; set; }
        public string CategoryName { get; set; }
    }
}
