using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class doctorViewModel
    {
    }

    public class DoctorBLL
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string UrduName { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Skills { get; set; }
        public string Profile { get; set; }
        public double? Fees { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public int StatusID { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public List<DoctorScheduleBLL> doctorSchedule { get; set; }
        public List<DoctorProfilesBLL> DoctorProfiles { get; set; }
        public List<DoctorTimingBLL> DoctorTimings { get; set; }
    }
    public class DoctorProfilesBLL
    {        
        public int DoctorID { get; set; }
        public int SpecialistID { get; set; }
        public string Profile { get; set; }
        public int? StatusID { get; set; }
        public string Name { get; set; }
        public double? Fees { get; set; }
    }

    public class DoctorScheduleBLL
    {
        public int DoctorID { get; set; }
        public int SpecialistID { get; set; }
        public string Name { get; set; }
        public string DayName { get; set; }        
        public string[] TimeSlot { get; set; }

        public List<DoctorBLL> Doctors { get; set; }
    }
    public class DoctorTimingBLL
    {
        public int DaysID { get; set; }
        public int DoctorID { get; set; }
        public int SpecialistID { get; set; }
        public string TimeSlots { get; set; }
        public string Name { get; set; }
        public string DayName { get; set; }
        public string[] TimeSlot { get; set; }
    }

}
