import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { CouponService } from 'src/app/_services/coupon.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-addcoupon',
  templateUrl: './addcoupon.component.html',
})
export class AddCouponComponent implements OnInit {

  submitted = false;
  couponForm: FormGroup;
  loading = false;
  loadingCoupon = false;
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
    private couponService: CouponService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.couponForm.controls; }

  private createForm() {
    this.couponForm = this.formBuilder.group({
      title: ['', Validators.required],
      type: ['', Validators.required],
      amount: [''],
      statusID: [true],
      couponID: 0,
      couponCode: [''],
    });
  }

  private editForm(obj) {
    this.f.title.setValue(obj.title);
    this.f.type.setValue(obj.type);
    this.f.couponID.setValue(obj.couponID);
    this.f.couponCode.setValue(obj.couponCode);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.amount.setValue(obj.amount);
  }

  setSelectedCustomer() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCoupon = true;
        this.f.couponID.setValue(sid);
        this.couponService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCoupon = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.couponForm.markAllAsTouched();
    this.submitted = true;
    if (this.couponForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt(this.f.couponID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.couponForm.value));
      this.couponService.insert(this.couponForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/settings/coupon']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.couponService.update(this.couponForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/settings/coupon']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }



}
