import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'src/app/_services/message.service';
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';


@Component({
  selector: 'app-addmessage',
  templateUrl: './addmessage.component.html',
  styleUrls: ['./addmessage.component.css'],
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})
export class AddMessageComponent implements OnInit {

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
  messageForm: FormGroup;
  loading = false;
  loadingMessage = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private messageService: MessageService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.messageForm.controls; }

  private createForm() {
    this.messageForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      designation:[''],
      statusID: [true],
      messageID: 0,
      image: [''],
    });
  }

  private editForm(obj) {   
    debugger 
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.designation.setValue(obj.designation);
    this.f.messageID.setValue(obj.messageID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingMessage = true;
        this.f.messageID.setValue(sid);
        this.messageService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingMessage = false;
        });
      }
    })
  }

  onSubmit() {
debugger
    this.messageForm.markAllAsTouched();
    this.submitted = true;
    if (this.messageForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.messageID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.messageForm.value));
      this.messageService.insert(this.messageForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/message']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.messageService.update(this.messageForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/message']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

