import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { CorporateClientService } from 'src/app/_services/corporateclient.service';

@Component({
  selector: 'app-addcorporateclient',
  templateUrl: './addcorporateclient.component.html',
  styleUrls: ['./addcorporateclient.component.css']
})
export class addcorporateclientComponent implements OnInit {
 
  submitted = false;
  corporateclientForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  NursingTypeActive :any= ['Header','Featured'];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private addcorporateclientService: CorporateClientService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.corporateclientForm.controls; }

  private createForm() {
    this.corporateclientForm = this.formBuilder.group({
      clientName: [''],      
      statusID: [true],
      corporateClientID: 0,
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.clientName.setValue(obj.clientName);     
    this.f.corporateClientID.setValue(obj.corporateClientID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.corporateClientID.setValue(sid);
        this.addcorporateclientService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.corporateclientForm.markAllAsTouched();
    this.submitted = true;
    if (this.corporateclientForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    

    if (parseInt(this.f.corporateClientID.value) === 0) {
      //Insert banner
      console.log(JSON.stringify(this.corporateclientForm.value));
      this.addcorporateclientService.insert(this.corporateclientForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/corporateclient']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.addcorporateclientService.update(this.corporateclientForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/settings/corporateclient']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
