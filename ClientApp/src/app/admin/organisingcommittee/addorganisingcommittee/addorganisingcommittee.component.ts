import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
 
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';
import { OrganisingCommitteeService } from 'src/app/_services/organisingcommittee.service';
@Component({
  selector: 'app-addorganisingcommittee',
  templateUrl: './addorganisingcommittee.component.html',
  styleUrls: ['./addorganisingcommittee.component.css'],
  // providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})
export class AddOrganisingCommitteeComponent implements OnInit {

  // public tools: object = {
  //   items: ['Undo', 'Redo', '|',
  //     'Bold', 'Italic', 'Underline', 'StrikeThrough', '|',
  //     'FontName', 'FontSize', 'FontColor', 'BackgroundColor', '|',
  //     'SubScript', 'SuperScript', '|',
  //     'LowerCase', 'UpperCase', '|',
  //     'Formats', 'Alignments', '|', 'OrderedList', 'UnorderedList', '|',
  //     'Indent', 'Outdent', '|', 'CreateLink']
  // };
  // public quickTools: object = {
  //   image: [
  //     'Replace', 'Align', 'Caption', 'Remove', 'InsertLink', '-', 'Display', 'AltText', 'Dimension']
  // };


  submitted = false;
  organisingcommitteeForm: FormGroup;
  loading = false;
  loadingorganisingcommittee = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private organisingcommitteeService: OrganisingCommitteeService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.organisingcommitteeForm.controls; }

  private createForm() {
    this.organisingcommitteeForm = this.formBuilder.group({
      name: ['', Validators.required],
      designation: [''],     
      statusID: [true],
      id: 0,
      image: [''],
      displayOrder: 0,
     
    });
  }

  private editForm(obj) {    
    this.f.name.setValue(obj.name);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.designation.setValue(obj.designation);    
    this.f.id.setValue(obj.id);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingorganisingcommittee = true;
        this.f.id.setValue(sid);
        this.organisingcommitteeService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingorganisingcommittee = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.organisingcommitteeForm.markAllAsTouched();
    this.submitted = true;
    if (this.organisingcommitteeForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.id.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.organisingcommitteeForm.value));
      this.organisingcommitteeService.insert(this.organisingcommitteeForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/organisingcommittee']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.organisingcommitteeService.update(this.organisingcommitteeForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/organisingcommittee']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

