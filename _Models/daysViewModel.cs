using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class daysViewModel
    {
    }

    public class DaysBLL
    {
        public int DaysID { get; set; }
        public int SpecialistID { get; set; }
        public int DoctorID { get; set; }
        public string Name { get; set; }
    }

}
