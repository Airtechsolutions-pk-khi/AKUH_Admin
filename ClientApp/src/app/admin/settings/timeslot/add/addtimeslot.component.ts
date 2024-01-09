import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { TimeSlotService } from '../../../../_services/timeslot.service';

@Component({
  selector: 'app-addtimeslot',
  templateUrl: './addtimeslot.component.html',
})
export class AddTimeSlotComponent implements OnInit {

  submitted = false;
  timeslotForm: FormGroup;
  loading = false;
  loadingService = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  NursingTypeActive = [];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private services: TimeSlotService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.timeslotForm.controls; }

  private createForm() {
    this.timeslotForm = this.formBuilder.group({
      timeSlot: ['', Validators.required],
      statusID: [true],
      timeSlotID: 0,
    });
  }

  private editForm(obj) {
    debugger
    this.f.timeSlot.setValue(obj.timeSlot);
    this.f.timeSlotID.setValue(obj.timeSlotID);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
  }
  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingService = true;
        this.f.timeSlotID.setValue(sid);
        this.services.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingService = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.timeslotForm.markAllAsTouched();
    this.submitted = true;
    if (this.timeslotForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt(this.f.timeSlotID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.timeslotForm.value));
      this.services.insert(this.timeslotForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/timeslot']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.services.update(this.timeslotForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/settings/timeslot']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
