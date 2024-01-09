import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { MedicineService } from 'src/app/_services/medicine.service';
import { ToastService } from 'src/app/_services/toastservice';
//import { debug } from 'console';

@Component({
  selector: 'app-addmedicine',
  templateUrl: './addmedicine.component.html'
})
export class AddmedicineComponent implements OnInit {

  submitted = false;
  medicineForm: FormGroup;
  loading = false;
  loadingmedicine = false;
  ButtonText = "Save";

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private medicineService: MedicineService

  ) {
    this.createForm();
  }

  ngOnInit() {
   this.setSelectedmedicine();
  }

  get f() { return this.medicineForm.controls; }

  private createForm() {
    this.medicineForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      statusID: [true],
      brandDetails: ['', Validators.required],
      price: ['', Validators.required],
      quantityDescription: ['', Validators.required],
      medicineID: [0],
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.brandDetails.setValue(obj.brandDetails);
    this.f.price.setValue(obj.price);
    this.f.quantityDescription.setValue(obj.quantityDescription);
    this.f.medicineID.setValue(obj.medicineID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
  }

   setSelectedmedicine() {
     this.route.paramMap.subscribe(param => {
       const sid = +param.get('id');
       if (sid) {
         this.loadingmedicine = true;
         this.f.medicineID.setValue(sid);
         this.medicineService.getById(sid).subscribe(res => {
           //Set Forms
           this.editForm(res);
           this.loadingmedicine = false;
         });
       }
     })
   }

  onSubmit() {
    this.medicineForm.markAllAsTouched();
    this.submitted = true;
    if (this.medicineForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.medicineID.value) === 0) {
      //Insert medicine
      console.log(JSON.stringify(this.medicineForm.value));
      this.medicineService.insert(this.medicineForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/pharmacy/medicine']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update medicine
      this.medicineService.update(this.medicineForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/pharmacy/medicine']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
