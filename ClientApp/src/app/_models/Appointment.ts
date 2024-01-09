export class Appointment {
  appointmentID: number;
  appointmentNo: string;
  patientName: string;
  fullName:string;
  doctorName:string;
  address:string;
  email:string;
  mobile:string;
  age: string;
  gender: string;
  fees: string;
  appointmentType:string;
  day:string;
  bookingDate: string;  
  bookingDateNursing: string;  
  timeslot: string;
  testName: string;
  appointmentStatus: number;
  statusID: number;
  doctor: string;
  createdOn: string;
  userName: string;
  lastUpdatedDate: string;
  lastUpdatedBy: string;
  specialities: DoctorSpeciality[];
}

export class DoctorSpeciality
{
    specialistID: number
    specialityName: string
}

export class AppointmentDetail {
  appointmentDetailID: number;
  appointmentID: number;
  medicalServiceID: number;
  fees: number;
  bookingDate: string;
  timeslot: string;
  description: string;
  statusID: number;
}

export class Doctors {
  doctorID: number;
  fullName: string;
  urduName: string;
  imagePath: string;
  gender: string;
  email: string;
  skills: string;
  education: string;
  profile: string;
  statusID: number;
}
