import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { PromotionService } from 'src/app/_services/promotion.service';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';

@Component({
  selector: 'app-addpromotion',
  templateUrl: './addpromotion.component.html',
  styleUrls: ['./addpromotion.component.css']
})
export class AddpromotionComponent implements OnInit {

  submitted = false;
  promotionForm: FormGroup;
  loading = false;
  loadingOffers = false;
  ButtonText = "Save"; 

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private promotion: PromotionService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedOffers();
  }

  get f() { return this.promotionForm.controls; }

  private createForm() {
    this.promotionForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      discount: [''],
      statusID: [true],
      promotionID: 0,
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.discount.setValue(obj.discount);
    this.f.promotionID.setValue(obj.offerID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedOffers() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingOffers = true;
        this.f.promotionID.setValue(sid);
        this.promotion.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingOffers = false;
        });
      }
    })
  }

  onSubmit() {
    this.promotionForm.markAllAsTouched();
    this.submitted = true;
    if (this.promotionForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.promotionID.value) === 0) {
      //Insert offers
      debugger
      this.promotion.insert(this.promotionForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/offers']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update offers
      this.promotion.update(this.promotionForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/offers']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }

}
