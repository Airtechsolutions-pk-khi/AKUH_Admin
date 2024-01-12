import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrganizerComponent } from '../organizer.component';
import { OrganizerService } from '../../../_services/organizer.service';

@Component({
  selector: 'app-addorganizer',
  templateUrl: './addorganizer.component.html',
  styleUrls: ['./addorganizer.component.css']
})
export class AddOrganizerComponent implements OnInit {
  submitted = false;
  organizerForm: FormGroup;
  loading = false;
  loadingOrganizer = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private organizerService: OrganizerService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.organizerForm.controls; }

  private createForm() {
    this.organizerForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      statusID: [true],
      organizerID: 0,
      image: [''],
    });
  }

  private editForm(obj) {    
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.organizerID.setValue(obj.organizerID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingOrganizer = true;
        this.f.organizerID.setValue(sid);
        this.organizerService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingOrganizer = false;
        });
      }
    })
  }

  onSubmit() {

    this.organizerForm.markAllAsTouched();
    this.submitted = true;
    if (this.organizerForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.organizerID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.organizerForm.value));
      this.organizerService.insert(this.organizerForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/organizer']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.organizerService.update(this.organizerForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/organizer']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

