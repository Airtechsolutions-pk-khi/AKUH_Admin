import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { AppsettingService } from 'src/app/_services/appsetting.service';
import { ToastService } from 'src/app/_services/toastservice';
import { Appsetting } from '../../../../_models/Appsetting';

@Component({
  selector: 'app-Addsettings',
  templateUrl: './Addsettings.component.html',
})
export class AddsettingsComponent implements OnInit {

  submitted = false;
  settingForm: FormGroup;
  loading = false;
  loadingSetting = false;
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
    private settingService: AppsettingService

  ) {
    this.createForm();
    
  }

  ngOnInit() {
    this.setSelectedSetting();
  }

  get f() { return this.settingForm.controls; }

  private createForm() {
    this.settingForm = this.formBuilder.group({
      discount: [0],
      deliveryCharges: [0],
      minimumOrderAmount: [0],
      tax: [0],
      statusID: [true],
      appSettingID: 1,
    });
  }

  private editForm(obj) {
    this.f.discount.setValue(obj.discount);
    this.f.deliveryCharges.setValue(obj.deliveryCharges);
    this.f.appSettingID.setValue(obj.appSettingID);
    this.f.minimumOrderAmount.setValue(obj.minimumOrderAmount);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.tax.setValue(obj.tax);
  }

  setSelectedSetting() {
    
    this.loadingSetting = true;
    this.settingService.getById(1).subscribe(res => {
      //Set Forms
      this.editForm(res);
      this.loadingSetting = false;
    });
    // this.route.paramMap.subscribe(param => {
    //   const sid = +param.get('id');
    //   if (sid) {
    //     this.loadingSetting = true;
    //     this.f.appSettingID.setValue(sid);
    //     this.settingService.getById(sid).subscribe(res => {
    //       //Set Forms
    //       this.editForm(res);
    //       this.loadingSetting = false;
    //     });
    //   }
    // })
  }

  onSubmit() {
    debugger
    this.settingForm.markAllAsTouched();
    this.submitted = true;
    if (this.settingForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt('1') === 0) {
      //Insert banner
      console.log(JSON.stringify(this.settingForm.value));
      this.settingService.insert(this.settingForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/appsettings']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } 
    else {
      //Update 
      this.settingService.update(this.settingForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.setSelectedSetting();
          this.router.navigate(['/admin/settings/appsettings/add']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
