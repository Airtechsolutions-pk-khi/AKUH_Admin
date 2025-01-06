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
//import { ToolbarService, LinkService, ImageService, HtmlEditorService } from '@syncfusion/ej2-angular-richtexteditor';
import { WorkshopService } from 'src/app/_services/workshop.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
const now = new Date();

@Component({
  selector: 'app-addworkshop',
  templateUrl: './addworkshop.component.html',
  styleUrls: ['./addworkshop.component.css'],
  //providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})

export class AddWorkshopComponent implements OnInit {

  // public tools: object = {
  //   items: ['Undo', 'Redo', '|',
  //     'Bold', 'Italic', 'Underline', 'StrikeThrough', '|',
  //     'FontName', 'FontSize', 'FontColor', 'BackgroundColor', '|',
  //     'SubScript', 'SuperScript', '|',
  //     'LowerCase', 'UpperCase', '|',
  //     'Formats', 'Alignments', '|', 'OrderedList', 'UnorderedList', '|',
  //     'Indent', 'Outdent', '|', 'CreateLink']
  // //    'Image', '|', 'ClearFormat', 'Print', 'SourceCode', '|', 'FullScreen']
  // };
  // public quickTools: object = {
  //   image: [
  //     'Replace', 'Align', 'Caption', 'Remove', 'InsertLink', '-', 'Display', 'AltText', 'Dimension']
  // };
  editorConfig: AngularEditorConfig = {
    editable: true,
    enableToolbar: true,
    showToolbar: true,
  };

  submitted = false;
  workshopForm: FormGroup;
  loading = false;
  loadingItems = false;
  OrganizerList = [];
  selectedOrganizerIds: string[];
  //startTime == [];
  //{
  //  hour: new Date().getHours() % 12 || 12,
  //  minute: new Date().getMinutes(),
  //  ampm: new Date().getHours() >= 12 ? 'PM' : 'AM'
  //};
  //endTime == [];
  //{
  //  hour: new Date().getHours() % 12 || 12,
  //  minute: new Date().getMinutes(),
  //  ampm: new Date().getHours() >= 12 ? 'PM' : 'AM'
  //};
  startTime: { hour: number; minute: number; } | null;
  endTime: { hour: number; minute: number; } | null;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private workshopService: WorkshopService,
    private service: EventService

  ) {
    this.createForm();
    this.loadOrganizer();
  }

  ngOnInit() {
    this.setSelecteditem();
  }

  get f() { return this.workshopForm.controls; }

  private createForm() {
    this.workshopForm = this.formBuilder.group({
      workshopID: 0,
      name: ['', Validators.required],
      description: [''],
      statusID: [true],
      location: [''],
      pdfLink: [''],
      link: [''],
      date: [''],
      startTime: [''],
      endTime: [''],
      organizerID: [],
      image: [''],
      displayOrder: [0],
    });
  }


  private editForm(obj) {
    debugger
    this.f.workshopID.setValue(obj.workshopID);
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    //this.f.date.setValue(obj.date);
    this.workshopForm.patchValue({
      date: this.formatDate(obj.date),
    });
    this.f.pdfLink.setValue(obj.pdfLink);
    this.startTime = obj.startTime
      ? this.parseTime12Hour(obj.startTime)
      : { hour: 0, minute: 0 };

    this.endTime = obj.endTime
      ? this.parseTime12Hour(obj.endTime)
      : { hour: 0, minute: 0 };
    //this.startTime = {
    //  hour: new Date("1/1/1900 " + obj.startTime).getHours() % 12 || 12,
    //  minute: new Date("1/1/1900 " + obj.startTime).getMinutes(),
    //  ampm: new Date("1/1/1900 " + obj.startTime).getHours() >= 12 ? 'PM' : 'AM'
    //};
    //this.endTime = {
    //  hour: new Date("1/1/1900 " + obj.endTime).getHours() % 12 || 12,
    //  minute: new Date("1/1/1900 " + obj.endTime).getMinutes(),
    //  ampm: new Date("1/1/1900 " + obj.endTime).getHours() >= 12 ? 'PM' : 'AM'
    //};
    this.f.link.setValue(obj.link);
    this.f.displayOrder.setValue(obj.displayOrder);
    if (obj.organizerID != "") {
      debugger
      var stringToConvert = obj.organizerID;
      this.f.organizerID.setValue(obj.organizerID);
    }
    this.imgComp.imageUrl = obj.image;
  }

  parseTime12Hour(time: string): { hour: number; minute: number } {
    const [hourMinute, period] = time.split(' ');
    const [hour, minute] = hourMinute.split(':').map(Number);
    return {
      hour: period === 'PM' && hour !== 12 ? hour + 12 : hour % 12,
      minute: minute || 0,
    };
  }

  private formatDate(date: Date): string {
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
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
        this.f.workshopID.setValue(sid);
        this.workshopService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingItems = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.workshopForm.markAllAsTouched();
    this.submitted = true;
    if (this.workshopForm.invalid) { return; }
    this.loading = true;
    const formattedHour = (this.startTime.hour % 12 || 12);
    const formattedMinute = this.pad(this.startTime.minute);
    const formattedAMPM = this.startTime.hour >= 12 ? 'PM' : 'AM'
    const formattedTime = `${formattedHour}:${formattedMinute} ${formattedAMPM}`;
    this.f.startTime.setValue(formattedTime);

    const formattedEndHour = (this.endTime.hour % 12 || 12);
    const formattedEndMinute = this.pad(this.endTime.minute);
    const formattedEndAMPM = this.endTime.hour >= 12 ? 'PM' : 'AM'
    const formattedEndTime = `${formattedEndHour}:${formattedEndMinute} ${formattedEndAMPM}`;
    this.f.endTime.setValue(formattedEndTime);
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.workshopID.value) === 0) {

      //Insert item
      console.log(JSON.stringify(this.workshopForm.value));
      this.workshopService.insert(this.workshopForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Workshop added successfully.")
          this.router.navigate(['/admin/workshop']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert Workshop.")
        this.loading = false;
      });
    } else {
      //Update item
      this.workshopService.update(this.workshopForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Workshop updated successfully.")
          this.router.navigate(['/admin/workshop']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update Workshop.")
        this.loading = false;
      });
    }
  }
  pad(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }

  private loadOrganizer() {
    this.service.loadOrganizer().subscribe((res: any) => {
      this.OrganizerList = res;
    });
  }
}
