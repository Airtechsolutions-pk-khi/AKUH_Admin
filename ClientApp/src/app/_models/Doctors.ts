import * as internal from "stream";

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
  fees: number;

  doctorSchedule: DoctorSchedule[];
  doctorProfiles: DoctorProfiles[];
}
export class DoctorProfiles {
  specialistID: number;
  fees: number;
  profile: string;
  dayName: string;
  name: string;
}
export class DoctorSchedule {
  specialistID: number;
  name: string;
  dayName: string;
  timeSlot: string;
}
