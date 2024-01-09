import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { BannerService } from 'src/app/_services/banner.service';
import { ToastService } from 'src/app/_services/toastservice';
import { MedicalService } from '../../../../_services/medical.service';

@Component({
  selector: 'app-addservice',
  templateUrl: './addservice.component.html',
})
export class AddServiceComponent implements OnInit {

  submitted = false;
  servicesForm: FormGroup;
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
    private services : MedicalService

  ) {
    this.createForm();
    this.loadActiveType();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.servicesForm.controls; }

  private createForm() {
    this.servicesForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      requirment: [''],
      fees: [0],
      statusID: [true],
      medicalServiceID: 0,
      nursingTypeID: 0,
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.requirment.setValue(obj.requirment);
    this.f.fees.setValue(obj.fees);
    this.f.medicalServiceID.setValue(obj.medicalServiceID);
    this.f.nursingTypeID.setValue(obj.nursingTypeID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }
  private loadActiveType() {
     
    this.services.loadActiveTyp().subscribe((res: any) => {
     
      this.NursingTypeActive = res;
    });
  }
  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingService = true;
        this.f.medicalServiceID.setValue(sid);
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
    this.servicesForm.markAllAsTouched();
    this.submitted = true;
    if (this.servicesForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.medicalServiceID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.servicesForm.value));
      this.services.insert(this.servicesForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/medicalservices']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.services.update(this.servicesForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/settings/medicalservices']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
