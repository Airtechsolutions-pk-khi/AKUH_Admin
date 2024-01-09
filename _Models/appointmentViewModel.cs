using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
	public class appointmentViewModel
	{
	}

	public class AppointmentBLL
	{
		public int AppointmentID { get; set; }
		public int DoctorID { get; set; }
		public int CustomerID { get; set; }
		public string AppointmentNo { get; set; }
		public string FullName { get; set; }
		public string DoctorName { get; set; }
		public List<DoctorSpeciality> Specialities { get; set; } = new();
		public string PatientName { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Fees { get; set; }
		public string BookingDate { get; set; }
		public string BookingDateNursing { get; set; }
		public string Day { get; set; }
		public string Date { get; set; }
		public string Timeslot { get; set; }
		public string TestName { get; set; }
		public int AppointmentStatus { get; set; }
		public int StatusID { get; set; }
		public string CreatedBy { get; set; }
		public string StatusMsg { get; set; }
		public string LastUpdatedBy { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<System.DateTime> CreatedOn { get; set; }
		public string UserName { get; set; }
		public class Doctors
		{
			public int DoctorID { get; set; }
			public string FullName { get; set; }
		}
		public class DoctorSpeciality
		{
			public int SpecialistID { get; set; }
			public string SpecialityName { get; set; }
		}

	}
}