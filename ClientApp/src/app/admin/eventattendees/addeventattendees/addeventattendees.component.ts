import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
//import { ItemsService } from 'src/app/_services/items.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { EventService } from 'src/app/_services/event.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { EventAttendeesService } from 'src/app/_services/eventattendees.service';
//import { CategoryService } from 'src/app/_services/category.service';
const now = new Date();
@Component({
  selector: 'app-addeventattendees',
  templateUrl: './addeventattendees.component.html',
  styleUrls: ['./addeventattendees.component.css']
})

export class AddEventAttendeesComponent implements OnInit {
  submitted = false;
  eventAttendeesForm: FormGroup;
  loading = false;
  loadingItems = false;
  Images = [];
  EventList = [];
  selectedEventCategoryIds: string[];
  OrganizerList = [];
  selectedOrganizerIds: string[];
  SpeakerList = [];
  selectedSpeakerIds: string[];
  ModifiersList = [];
  AddonsList = [];
  selectedModifierIds: string[];
  selectedAddonIds: string[];
  

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
 
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private eventAttendeesService: EventAttendeesService

  ) {
    this.createForm();

    this.loadEvent();
     

  }

  ngOnInit() {
    
    this.setSelecteditem();
  }

  
  get f() { return this.eventAttendeesForm.controls; }

  private createForm() {
    this.eventAttendeesForm = this.formBuilder.group({
      attendeesID: 0,
      fullName: ['', Validators.required],
      email: ['',Validators.required],
      phoneNo: ['',Validators.required],
      
    });
  }


  private editForm(obj) {
    
    this.f.fullName.setValue(obj.fullName);
    this.f.email.setValue(obj.email);
    this.f.phoneNo.setValue(obj.phoneNo);
     
  }
   
  setSelecteditem() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingItems = true;
        this.f.attendeesID.setValue(sid);
        this.eventAttendeesService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingItems = false;
        });
      }
    })
  }

  onSubmit() {
    
    this.eventAttendeesForm.markAllAsTouched();
    this.submitted = true;
    if (this.eventAttendeesForm.invalid) { return; }
    this.loading = true;
    
    //this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
     
    if (parseInt(this.f.attendeesID.value) === 0) {

      //Insert item
      console.log(JSON.stringify(this.eventAttendeesForm.value));
      this.eventAttendeesService.insert(this.eventAttendeesForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Event added successfully.")
          this.router.navigate(['/admin/eventattendees']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert Event.")
        this.loading = false;
      });
    } else {
      //Update item
      this.eventAttendeesService.update(this.eventAttendeesForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Event updated successfully.")
          this.router.navigate(['/admin/event']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update Event.")
        this.loading = false;
      });
    }
  }
  pad(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }
  
  private loadEvent() {
    debugger
    this.eventAttendeesService.loadActiveEvents().subscribe((res: any) => {
      this.EventList = res;
      console.log(res);
    });
  }
  // private loadOrganizer() {
  //   this.eventService.loadOrganizer().subscribe((res: any) => {
  //     this.OrganizerList = res;
  //   });
  // }
  // private loadSpeaker() {
  //   this.eventService.loadSpeaker().subscribe((res: any) => {

  //     this.SpeakerList = res;
  //   });
  // }
 
  // removeImage(obj) {
  //   const index = this.Images.indexOf(obj);
  //   this.Images.splice(index, 1);
  //   this.f.imagesSource.setValue(this.Images);
  // }
}
