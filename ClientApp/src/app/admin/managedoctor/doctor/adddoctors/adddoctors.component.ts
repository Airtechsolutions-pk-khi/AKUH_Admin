import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { DoctorsService } from 'src/app/_services/doctors.service';
import { ToastService } from 'src/app/_services/toastservice';
import { AppointmentService } from 'src/app/_services/appointment.service';
import { DoctorProfiles, DoctorSchedule } from 'src/app/_models/Doctors';

@Component({
  selector: 'app-adddoctors',
  templateUrl: './adddoctors.component.html',

})
export class AdddoctorsComponent implements OnInit {

  submitted = false;
  public spec = new DoctorSchedule();
  public drProfile = new DoctorProfiles();
  dayName = [];
  timeSlot = [];
  doctorForm: FormGroup;
  loading = false;
  loadingDoctor = false;
  ButtonText = "Save"; selectedCityIds
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];
  DoctorDaysDetailList = [];
  selectedSpecialityList = [];
  DoctorSchedule = [];

  DoctorProfiles = [];

  selectedSpecialistIds = [];
  DoctorDaysList = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
  selectedTimeList = [];


  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private doctorService: DoctorsService,
    private appointmentService: AppointmentService

  ) {
    this.createForm();
    // this.loadDay();
    this.loadSpecialitiesAll();
    this.loadTimeSlotsAll();
  }
  ngOnInit() {
    this.setSelectedDoctor();
  }

  get f() { return this.doctorForm.controls; }

  private createForm() {
    this.doctorForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      urduName: [''],
      email: [''],
      skills: ['', Validators.required],
      education: ['', Validators.required],
      imagePath: [''],
      profile: [''],
      statusID: [true],
      gender: [''],
      timeSlot: [''],
      doctorID: [0],

      doctorProfiles: [],

      doctorSchedule: [],
    });
  }
  private editForm(obj1) {
    var obj = obj1[0];
    this.f.fullName.setValue(obj.fullName);
    this.f.urduName.setValue(obj.urduName);
    this.f.email.setValue(obj.email);
    this.f.profile.setValue(obj.profile);
    this.f.skills.setValue(obj.skills);
    this.f.gender.setValue(obj.gender);
    this.f.doctorID.setValue(obj.doctorID);
    this.f.imagePath.setValue(obj.imagePath);
    this.f.education.setValue(obj.education);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);

    if (obj.doctorProfiles != "") {
      this.DoctorProfiles = obj.doctorProfiles;
    }
    if (obj.doctorTimings != "") {
      this.DoctorSchedule = obj.doctorTimings;
    }
  }

  setSelectedDoctor() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingDoctor = true;
        this.f.doctorID.setValue(sid);
        this.doctorService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingDoctor = false;
        });
      }
    })
  }

  loadSpecialitiesAll() {
    this.appointmentService.loadSpecialities().subscribe((res: any) => {
      this.selectedSpecialityList = res;
    });
  }
  loadTimeSlotsAll() {
    this.appointmentService.loadTimeLists().subscribe((res: any) => {
      this.selectedTimeList = res;
    });
  }

  onSubmit() {
    this.doctorForm.markAllAsTouched();
    this.submitted = true;
    if (this.doctorForm.invalid) { return; }
    this.loading = true;

    this.f.doctorSchedule.setValue(this.DoctorSchedule);
    this.f.doctorProfiles.setValue(this.DoctorProfiles);

    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.imagePath.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.doctorID.value) === 0) {
      //Insert doctor
      console.log(JSON.stringify(this.doctorForm.value));
      this.doctorService.insert(this.doctorForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/managedoctor/doctor']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update doctor
      this.doctorService.update(this.doctorForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/managedoctor/doctor']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }

  RemoveChild(obj) {
    const index = this.DoctorSchedule.indexOf(obj);
    this.DoctorSchedule.splice(index, 1);
  }

  RemoveProfileChild(obj) {
    const index = this.DoctorProfiles.indexOf(obj);
    this.DoctorProfiles.splice(index, 1);
  }
  AddChild(val) {
    var obj = this.selectedSpecialityList.find(element => element.specialistID == val.specialistID);
    if (val.specialistID != null) {
      //if (!this.DoctorSchedule.find(element => element.specialistID == val.specialistID)) {
      this.DoctorSchedule.push({
        specialistID: val.specialistID,
        name: obj.name,
        dayName: val.dayName,
        timeSlot: val.timeSlot,
      });
      /*}*/
      //else {
      //  alert("Item already added in list")
      //}
      this.clearSpec();
    }
  }
  clearSpec() {
    this.spec.name = "";
    this.spec.dayName = "";
    this.spec.timeSlot = "";

  }
  AddProfileChild(val) {
    var obj = this.selectedSpecialityList.find(element => element.specialistID == val.specialistID);
    if (val != null) {

      this.DoctorProfiles.push({
        name: obj.name,
        specialistID: val.specialistID,
        fees: val.fees,
        profile: val.profile,
      });
    }
    else {
      alert("Item already added in list")
    }
    this.clear();
  }
  clear() {
    this.drProfile.fees = 0;
    this.drProfile.profile = "";
  }
}
