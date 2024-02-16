import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
 
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';
import { PopupBannerService } from 'src/app/_services/popupbanner.service';


@Component({
  selector: 'app-addpopupbanner',
  templateUrl: './addpopupbanner.component.html',
  styleUrls: ['./addpopupbanner.component.css']
  
})
export class AddPopupBannerComponent implements OnInit {

  submitted = false;
  popupbannerForm: FormGroup;
  loading = false;
  loadingMessage = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private popupbannerService: PopupBannerService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.popupbannerForm.controls; }

  private createForm() {
    this.popupbannerForm = this.formBuilder.group({
      name: ['', Validators.required],
     
      statusID: [true],
     
      id: 0,
      image: [''],
    });
  }

  private editForm(obj) {   
    debugger 
    this.f.name.setValue(obj.name);
    
    this.f.id.setValue(obj.id);
    this.f.image.setValue(obj.image);
    
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingMessage = true;
        this.f.id.setValue(sid);
        this.popupbannerService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingMessage = false;
        });
      }
    })
  }

  onSubmit() {
debugger
    this.popupbannerForm.markAllAsTouched();
    this.submitted = true;
    if (this.popupbannerForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.id.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.popupbannerForm.value));
      this.popupbannerService.insert(this.popupbannerForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/settings/popupbanner']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.popupbannerService.update(this.popupbannerForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/settings/popupbanner']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

