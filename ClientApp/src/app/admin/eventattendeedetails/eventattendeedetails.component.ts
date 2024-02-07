import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
 
import { ToastService } from 'src/app/_services/toastservice';
 
import { EventAttendees } from 'src/app/_models/EventAttendees';
import { EventAttendeesService } from 'src/app/_services/eventattendees.service';
@Component({
  selector: 'app-eventattendeedetails',
  templateUrl: './eventattendeedetails.component.html',
  providers: []
})
export class EventattendeedetailsComponent implements OnInit {
  public eventAttendees = new EventAttendees();
   
  subject = "";
  messageForAttendee = "";
  meetingLink = "";
  constructor(public service: EventAttendeesService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {
   // this.userName = this.ls.getSelectedBrand().userName;
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
  updateAttendeeDetail(eventAttendees, status) {
    debugger
    eventAttendees.statusID  = status;
    eventAttendees.subject = this.subject;
    eventAttendees.messageForAttendee = this.messageForAttendee;
    eventAttendees.meetingLink = this.meetingLink;
    //Update 
    this.service.statusUpdate(eventAttendees).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['reception/appointment']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  private editForm(obj) {
    this.eventAttendees = obj;
    
  }
}
