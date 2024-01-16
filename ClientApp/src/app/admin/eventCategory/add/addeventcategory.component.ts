import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EventCategoryComponent } from '../../eventCategory/eventcategory.component';
import { EventCategoryService } from '../../../_services/eventCategory.service';

@Component({
  selector: 'app-addeventcategory',
  templateUrl: './addeventcategory.component.html',
  styleUrls: ['./addeventcategory.component.css']
})
export class AddEventCategoryComponent implements OnInit {
  submitted = false;
  eventForm: FormGroup;
  loading = false;
  loadingOrganizer = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private eventService: EventCategoryService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.eventForm.controls; }

  private createForm() {
    this.eventForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      image: [''],
      statusID: [true],
      eventCategoryID: 0,
    });
  }

  private editForm(obj) {    
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.eventCategoryID.setValue(obj.eventCategoryID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingOrganizer = true;
        this.f.eventCategoryID.setValue(sid);
        this.eventService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingOrganizer = false;
        });
      }
    })
  }

  onSubmit() {

    this.eventForm.markAllAsTouched();
    this.submitted = true;
    if (this.eventForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.eventCategoryID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.eventForm.value));
      this.eventService.insert(this.eventForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/eventcategory']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.eventService.update(this.eventForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/eventcategory']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

