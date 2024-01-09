import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { SpecialityService } from '../../../../_services/speciality.service';

@Component({
  selector: 'app-addspeciality',
  templateUrl: './addspeciality.component.html',
})
export class AddSpecialityComponent implements OnInit {

  submitted = false;
  specialityForm: FormGroup;
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
    private services: SpecialityService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.specialityForm.controls; }

  private createForm() {
    this.specialityForm = this.formBuilder.group({
      name: ['', Validators.required],
      urduName: [''],
      statusID: [true],
      specialistID: 0,
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.urduName.setValue(obj.urduName);
    this.f.specialistID.setValue(obj.specialistID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }
  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingService = true;
        this.f.specialistID.setValue(sid);
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
    this.specialityForm.markAllAsTouched();
    this.submitted = true;
    if (this.specialityForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.specialistID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.specialityForm.value));
      this.services.insert(this.specialityForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/speciality']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.services.update(this.specialityForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/settings/speciality']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
