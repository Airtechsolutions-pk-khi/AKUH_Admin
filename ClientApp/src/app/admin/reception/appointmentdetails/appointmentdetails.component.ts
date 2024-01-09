import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Appointment, AppointmentDetail, Doctors } from 'src/app/_models/Appointment';
import { ToastService } from 'src/app/_services/toastservice';
import { Location } from 'src/app/_models/Location';
import { AppointmentService } from 'src/app/_services/appointment.service';
@Component({
  selector: 'app-appointmentdetails',
  templateUrl: './appointmentdetails.component.html',
  providers: []
})
export class AppointmentdetailsComponent implements OnInit {
  public appointment = new Appointment();
  public doctor = new Doctors();
  private selectedBrand;
  StatusMsg = "";
  userName = "";
  constructor(public service: AppointmentService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {
    this.userName = this.ls.getSelectedBrand().userName;
  }

  ngOnInit() {
    this.setSelectedAppointment();
  }
  setSelectedAppointment() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.service.getById(sid).subscribe(res => {
          
          this.editForm(res);
        });
      }
    })
  }
  updateAppointment(appointment, status) {
    debugger
    appointment.appointmentStatus  = status;
    appointment.statusMsg = this.StatusMsg;
    appointment.userName = this.userName;
    //Update 
    this.service.statusUpdate(appointment).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['reception/appointment']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  private editForm(obj) {
    this.appointment = obj;
    this.doctor = obj.doctor
  }
}
