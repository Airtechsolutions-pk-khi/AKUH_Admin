import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { DiagnosticCategoryService } from 'src/app/_services/diagnosticcategories.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-addcategory',
  templateUrl: './addcategory.component.html',
})
export class AddCategoryComponent implements OnInit {
  submitted = false;
  categoryForm: FormGroup;
  loading = false;
  loadingReport = false;
  ButtonText = "Save";
  CustomerList = [];
  selectedCustomerIds = [];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private categoryService: DiagnosticCategoryService

  ) {
    this.createForm();
    //this.loadCustomer();
  }

  ngOnInit() {
    this.setSelectedReport();
  }
  get f() { return this.categoryForm.controls; }

  private createForm() {
    this.categoryForm = this.formBuilder.group({
      categoryName: ['', Validators.required],
      statusID: [true],
      diagnosticCatID: [0],
      image: [''],
    });
  }
  private editForm(obj) {
    this.f.categoryName.setValue(obj.categoryName);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }
  setSelectedReport() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingReport = true;
        this.f.diagnosticCatID.setValue(sid);
        this.categoryService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingReport = false;
        });
      }
    })
  }
  onSubmit() {
    this.categoryForm.markAllAsTouched();
    this.submitted = true;
    if (this.categoryForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.diagnosticCatID.value) === 0) {
      //Insert customer
      console.log(JSON.stringify(this.categoryForm.value));
      this.categoryService.insert(this.categoryForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/laboratory/diagnosticcategory']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update customer
      this.categoryService.update(this.categoryForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/laboratory/diagnosticcategory']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }

  //private loadCustomer() {
  //  debugger
  //  this.categoryService.loadCustomer().subscribe((res: any) => {
  //    this.CustomerList = res;
  //  });
  //}
}
