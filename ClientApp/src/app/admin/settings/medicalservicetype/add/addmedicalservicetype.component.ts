import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { BannerService } from 'src/app/_services/banner.service';
import { ToastService } from 'src/app/_services/toastservice';
import { MedicalServiceTypes } from 'src/app/_services/medicalservicetype.service';



@Component({
  selector: 'app-addservice',
  templateUrl: './addmedicalservicetype.component.html',
})
export class AddMedicalServicetypeComponent implements OnInit {

  submitted = false;
  servicesForm: FormGroup;
  loading = false;
  loadingService = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private services : MedicalServiceTypes

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedType();
  }

  get f() { return this.servicesForm.controls; }

  private createForm() {
    this.servicesForm = this.formBuilder.group({
      type: ['', Validators.required],
      
      statusID: [true],
       
      nursingTypeID: 0,
      
    });
  }

  private editForm(obj) {
    this.f.type.setValue(obj.type);
    
    this.f.nursingTypeID.setValue(obj.nursingTypeID);
   
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
     
  }

  setSelectedType() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingService = true;
        this.f.nursingTypeID.setValue(sid);
        this.services.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingService = false;
        });
      }
    })
  }

  onSubmit() {
    this.servicesForm.markAllAsTouched();
    this.submitted = true;
    if (this.servicesForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
     

    if (parseInt(this.f.nursingTypeID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.servicesForm.value));
      this.services.insert(this.servicesForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/medicalservicetype']);
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
          this.router.navigate(['/admin/settings/medicalservicetype']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
