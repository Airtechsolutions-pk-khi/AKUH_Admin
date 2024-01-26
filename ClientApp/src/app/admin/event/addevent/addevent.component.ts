import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
//import { ItemsService } from 'src/app/_services/items.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { EventService } from 'src/app/_services/event.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
//import { CategoryService } from 'src/app/_services/category.service';
import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';
const now = new Date();

@Component({
  selector: 'app-addevent',
  templateUrl: './addevent.component.html',
  styleUrls: ['./addevent.component.css'],
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})

export class AddEventComponent implements OnInit {

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
  eventForm: FormGroup;
  loading = false;
  loadingItems = false;
  Images = [];
  EventCategoryList = [];
  selectedEventCategoryIds: string[];
  OrganizerList = [];
  selectedOrganizerIds: string[];
  SpeakerList = [];
  selectedSpeakerIds: string[];
  ModifiersList = [];
  AddonsList = [];
  selectedModifierIds: string[];
  selectedAddonIds: string[];
  //eventTime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  eventTime =
    {
      hour: new Date().getHours() % 12 || 12,
      minute: new Date().getMinutes(),
      ampm: new Date().getHours() >= 12 ? 'PM' : 'AM'
    };


  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private eventService: EventService

  ) {
    this.createForm();

    this.loadEventCategory();
    this.loadOrganizer();
    this.loadSpeaker();

  }

  ngOnInit() {
    const date: NgbDate = new NgbDate(now.getFullYear(), now.getMonth() + 1, 1);
    this._datepicker.fromDate = date;
    this.setSelecteditem();
  }

  get f() { return this.eventForm.controls; }

  private createForm() {
    this.eventForm = this.formBuilder.group({
      eventID: 0,
      name: ['', Validators.required],
      //type: [''],
      description: [''],
      statusID: [true],
      location: [''],
      fromDate: '',
      toDate: '',
      eventCity: [''],
      eventCategoryID: [null],
      locationLink: [''],
      phoneNo: [''],
      eventTime: [''],
      email: [''],
      eventCategories: [],
      speakers: [],
      organizers: [],
      remainingTicket: [0],
      //facebook: [''],
      //instagram: [''],
      //twitter: [''],
      image: [''],
      displayOrder: [0],
      file: [''],
      imagesSource: [''],
      isFeatured: [false]
    });
  }


  private editForm(obj) {
    debugger
    this.f.eventID.setValue(obj.eventID);
    this.f.name.setValue(obj.name);
    //this.f.type.setValue(obj.type);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.location.setValue(obj.location);
    this.f.fromDate.setValue(obj.fromDate);
    this.f.toDate.setValue(obj.toDate);
    //this.f.eventTime.setValue(obj.eventTime);
    this.eventTime = {
      hour: new Date("1/1/1900 " + obj.eventTime).getHours() % 12 || 12,
      minute: new Date("1/1/1900 " + obj.eventTime).getMinutes(),
      ampm: new Date("1/1/1900 " + obj.eventTime).getHours() >= 12 ? 'PM' : 'AM'
    };

    this.f.eventCity.setValue(obj.eventCity);
    this.f.eventCategoryID.setValue(obj.eventCategoryID);
    this.f.locationLink.setValue(obj.locationLink);
    this.f.phoneNo.setValue(obj.phoneNo);
    this.f.email.setValue(obj.email);
    this.f.remainingTicket.setValue(obj.remainingTicket);
    //this.f.facebook.setValue(obj.facebook);
    //this.f.instagram.setValue(obj.instagram);
    //this.f.twitter.setValue(obj.twitter);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.isFeatured.setValue(obj.isFeatured);
    //this.eventTime = { hour: new Date("1/1/1900 " + obj.eventTime).getHours(), minute: new Date("1/1/1900 " + obj.eventTime).getMinutes() };
    if (obj.organizers != "") {
      debugger
      var stringToConvert = obj.organizers;
      this.selectedOrganizerIds = stringToConvert.split(',').map(Number);
      this.f.organizers.setValue(obj.organizers);
    }

    if (obj.speakers != "") {
      debugger
      var stringToConvert = obj.speakers;
      this.selectedSpeakerIds = stringToConvert.split(',').map(Number);
      this.f.speakers.setValue(obj.speakers);
    }

    //if (obj.eventCategories != "") {
    //  debugger
    //  var stringToConvert = obj.eventCategories;
    //  this.selectedEventCategoryIds = stringToConvert.split(',').map(Number);
    //  this.f.eventCategories.setValue(obj.eventCategories);
    //}
    this.loadItemImages(this.f.eventID.value);
  }
  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
  setSelecteditem() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingItems = true;
        this.f.eventID.setValue(sid);
        this.eventService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingItems = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.eventForm.markAllAsTouched();
    this.submitted = true;
    if (this.eventForm.invalid) { return; }
    this.loading = true;
    this.f.eventCategories.setValue(this.selectedEventCategoryIds == undefined ? "" : this.selectedEventCategoryIds.toString());
    this.f.speakers.setValue(this.selectedSpeakerIds == undefined ? "" : this.selectedSpeakerIds.toString());
    this.f.organizers.setValue(this.selectedOrganizerIds == undefined ? "" : this.selectedOrganizerIds.toString());
    //this.f.eventTime.setValue(this.eventTime.hour + ":" + this.eventTime.minute);
    //this.f.eventTime.setValue(this.eventTime.hour + ":" + this.eventTime.minute + " " + this.eventTime.ampm);

    const formattedHour = (this.eventTime.hour % 12 || 12);
    const formattedMinute = this.pad(this.eventTime.minute);
    const formattedAMPM = this.eventTime.hour >= 12 ? 'PM' : 'AM'
    const formattedTime = `${formattedHour}:${formattedMinute} ${formattedAMPM}`;
    this.f.eventTime.setValue(formattedTime);

    this.f.fromDate.setValue(this.parseDate(this._datepicker.fromDate));
    this.f.toDate.setValue(this.parseDate(this._datepicker.toDate));
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    //this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.eventID.value) === 0) {

      //Insert item
      console.log(JSON.stringify(this.eventForm.value));
      this.eventService.insert(this.eventForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Event added successfully.")
          this.router.navigate(['/admin/event']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert Event.")
        this.loading = false;
      });
    } else {
      //Update item
      this.eventService.update(this.eventForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Event updated successfully.")
          this.router.navigate(['/admin/event']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update Event.")
        this.loading = false;
      });
    }
  }
  pad(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }
  onFileChange(event) {
    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        var reader = new FileReader();
        debugger;
        var fileSize = event.target.files[i].size / 100000;
        if (fileSize > 5) { alert("Filesize exceed 500 KB"); }
        else {
          reader.onload = (event: any) => {
            console.log(event.target.result);
            this.Images.push(event.target.result);
            this.eventForm.patchValue({
              imagesSource: this.Images
            });
          }
          reader.readAsDataURL(event.target.files[i]);
        }
      }
    }
  }
  private loadEventCategory() {
    this.eventService.loadActiveCategories().subscribe((res: any) => {
      this.EventCategoryList = res;
    });
  }
  private loadOrganizer() {
    this.eventService.loadOrganizer().subscribe((res: any) => {
      this.OrganizerList = res;
    });
  }
  private loadSpeaker() {
    this.eventService.loadSpeaker().subscribe((res: any) => {

      this.SpeakerList = res;
    });
  }
  private loadItemImages(id) {
    this.eventService.loadEventImages(id).subscribe((res: any) => {
      this.Images = res;
      this.f.imagesSource.setValue(this.Images);
    });
  }
  removeImage(obj) {
    const index = this.Images.indexOf(obj);
    this.Images.splice(index, 1);
    this.f.imagesSource.setValue(this.Images);
  }
}
