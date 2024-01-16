import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PartnerComponent } from '../partner.component';
import { PartnerService } from '../../../_services/partner.service';

@Component({
  selector: 'app-addpartner',
  templateUrl: './addpartner.component.html',
  styleUrls: ['./addpartner.component.css']
})
export class AddPartnerComponent implements OnInit {
  submitted = false;
  partnerForm: FormGroup;
  loading = false;
  loadingPartner = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private partnerService: PartnerService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.partnerForm.controls; }

  private createForm() {
    this.partnerForm = this.formBuilder.group({
      name: ['', Validators.required],
      statusID: [true],
      partnerID: 0,
      image: [''],
    });
  }

  private editForm(obj) {    
    this.f.name.setValue(obj.name);
    this.f.partnerID.setValue(obj.partnerID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingPartner = true;
        this.f.partnerID.setValue(sid);
        this.partnerService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingPartner = false;
        });
      }
    })
  }

  onSubmit() {
    this.partnerForm.markAllAsTouched();
    this.submitted = true;
    if (this.partnerForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.partnerID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.partnerForm.value));
      this.partnerService.insert(this.partnerForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/partner']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.partnerService.update(this.partnerForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/partner']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

