import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { AppsettingService } from 'src/app/_services/appsetting.service';
import { ToastService } from 'src/app/_services/toastservice';
import { Appsetting } from '../../../../_models/Appsetting';
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';
@Component({
  selector: 'app-Addsettings',
  templateUrl: './Addsettings.component.html',
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})
export class AddsettingsComponent implements OnInit {
  public tools: object = {
    items: ['Undo', 'Redo', '|',
      'Bold', 'Italic', 'Underline', 'StrikeThrough', '|',
      'FontName', 'FontSize', 'FontColor', 'BackgroundColor', '|',
      'SubScript', 'SuperScript', '|',
      'LowerCase', 'UpperCase', '|',
      'Formats', 'Alignments', '|', 'OrderedList', 'UnorderedList', '|',
      'Indent', 'Outdent', '|', 'CreateLink'
    ]
  };
  // public quickTools: object = {
  //   image: [
  //     'Replace', 'Align', 'Caption', 'Remove', 'InsertLink', '-', 'Display', 'AltText', 'Dimension']
  // };
  submitted = false;
  settingForm: FormGroup;
  loading = false;
  loadingSetting = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  // @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  @ViewChild('splashImageUpload', { static: true }) splashImageUpload: ImageuploadComponent;
  @ViewChild('chairImageUpload', { static: true }) chairImageUpload: ImageuploadComponent;
  @ViewChild('conferenceChairImageUpload', { static: true }) conferenceChairImageUpload: ImageuploadComponent;
  

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private settingService: AppsettingService

  ) {
    this.createForm();
    
  }

  ngOnInit() {
    this.setSelectedSetting();
  }

  get f() { return this.settingForm.controls; }

  private createForm() {
    this.settingForm = this.formBuilder.group({
      appName: [''],
      appVersion: [''],
      about: [''],
      privacyPolicy: [''],
      splashScreen: [''],
      msgChair: [''],
      msgConferenceChair: [''],
      imgChair: [''],
      imgConChair: [''],
      facebookUrl: [''],
      instagramUrl: [''],
      twitterUrl: [''],
      youtubeUrl: [''],
      statusID: [true],
      settingID: 1,
    });
  }

  private editForm(obj) {
    debugger
    this.f.settingID.setValue(obj.settingID);
    this.f.appName.setValue(obj.appName);
    this.f.appVersion.setValue(obj.appVersion);
    this.f.about.setValue(obj.about);
    this.f.privacyPolicy.setValue(obj.privacyPolicy);
    this.f.msgChair.setValue(obj.msgChair);
    this.f.imgChair.setValue(obj.imgChair);
    this.f.msgConferenceChair.setValue(obj.msgConferenceChair);
    this.f.imgConChair.setValue(obj.imgConChair);
    this.f.splashScreen.setValue(obj.splashScreen);
    this.f.facebookUrl.setValue(obj.facebookUrl);
    this.f.instagramUrl.setValue(obj.instagramUrl);
    this.f.twitterUrl.setValue(obj.twitterUrl);
    this.f.youtubeUrl.setValue(obj.youtubeUrl);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.splashImageUpload.imageUrl = obj.splashScreen;
    this.chairImageUpload.imageUrl = obj.imgChair;
    this.conferenceChairImageUpload.imageUrl = obj.imgConChair;
  }

  setSelectedSetting() {
    debugger
    this.loadingSetting = true;
    this.settingService.getById(1).subscribe(res => {
      //Set Forms
      this.editForm(res);
      this.loadingSetting = false;
    });
     
  }

  onSubmit() {
    debugger
    this.settingForm.markAllAsTouched();
    this.submitted = true;
    if (this.settingForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    this.f.splashScreen.setValue(this.splashImageUpload.imageUrl);
    this.f.imgChair.setValue(this.chairImageUpload.imageUrl);
    this.f.imgConChair.setValue(this.conferenceChairImageUpload.imageUrl);

    // this.f.splashScreen.setValue(this.imgComp.imageUrl);
    // this.f.imgChair.setValue(this.imgComp.imageUrl);
    // this.f.imgConChair.setValue(this.imgComp.imageUrl);
    if (parseInt('1') === 0) {
      //Insert banner
      console.log(JSON.stringify(this.settingForm.value));
      this.settingService.insert(this.settingForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/settings/appsettings']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } 
    else {
      //Update 
      this.settingService.update(this.settingForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.setSelectedSetting();
          this.router.navigate(['/admin/settings/appsettings/add']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
