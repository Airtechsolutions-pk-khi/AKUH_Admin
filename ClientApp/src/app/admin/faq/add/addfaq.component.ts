import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FaqService } from 'src/app/_services/faq.service';
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';

@Component({
  selector: 'app-addfaq',
  templateUrl: './addfaq.component.html',
  styleUrls: ['./addfaq.component.css'],
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]

})
export class AddFaqComponent implements OnInit {
  public tools: object = {
    items: ['Undo', 'Redo', '|',
      'Bold', 'Italic', 'Underline', 'StrikeThrough', '|',
      'FontName', 'FontSize', 'FontColor', 'BackgroundColor', '|',
      'SubScript', 'SuperScript', '|',
      'LowerCase', 'UpperCase', '|',
      'Formats', 'Alignments', '|', 'OrderedList', 'UnorderedList', '|',
      'Indent', 'Outdent', '|', 'CreateLink']
    //    'Image', '|', 'ClearFormat', 'Print', 'SourceCode', '|', 'FullScreen']
  };
  public quickTools: object = {
    image: [
      'Replace', 'Align', 'Caption', 'Remove', 'InsertLink', '-', 'Display', 'AltText', 'Dimension']
  };

  submitted = false;
  faqForm: FormGroup;
  loading = false;
  loadingOrganizer = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private faqService: FaqService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.faqForm.controls; }

  private createForm() {
    this.faqForm = this.formBuilder.group({
      faqQ: ['', Validators.required],
      faqA: [''],
      statusID: [true],
      faqID: 0,
    });
  }

  private editForm(obj) {
    this.f.faqQ.setValue(obj.faqQ);
    this.f.faqA.setValue(obj.faqA);
    this.f.faqID.setValue(obj.faqID);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);

  }

  setSelectedaddon() {

    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingOrganizer = true;
        this.f.faqID.setValue(sid);
        this.faqService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingOrganizer = false;
        });
      }
    })
  }

  onSubmit() {

    this.faqForm.markAllAsTouched();
    this.submitted = true;
    if (this.faqForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt(this.f.faqID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.faqForm.value));
      this.faqService.insert(this.faqForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/faq']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {

      this.faqService.update(this.faqForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/faq']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

