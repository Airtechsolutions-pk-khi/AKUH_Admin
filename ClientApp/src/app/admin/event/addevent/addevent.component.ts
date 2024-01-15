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
const now = new Date();
@Component({
  selector: 'app-addevent',
  templateUrl: './addevent.component.html',
  styleUrls: ['./addevent.component.css']
})

export class AddEventComponent implements OnInit {
  submitted = false;
  eventForm: FormGroup;
  loading = false;
  loadingItems = false;
  Images = [];
  EventCategoryList = [];
  selectedEventCategoryIds: string[];
  OrganizerList =[];
  selectedOrganizerIds: string[];
  SpeakerList=[];
  selectedSpeakerIds:string[];
  ModifiersList = [];
  AddonsList = [];
  selectedModifierIds: string[];
  selectedAddonIds: string[];

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
    this._datepicker.fromDate=date;
  }

  get f() { return this.eventForm.controls; }

  private createForm() {
    this.eventForm = this.formBuilder.group({
      eventID: 0,
      name: ['', Validators.required],
      type: [''],
      description: [''],
      statusID: [true],
      location: [''],
      fromDate:'',
      toDate:'',
      eventCity: [''],
      eventCategoryID: [null],
      locationLink: [''],    
      phoneNo: [''],
      email: [''],
      eventCategories: [],
      speakers: [],
      organizers: [],
      remainingTicket: [0],
      facebook: [''],
      instagram: [''],
      twitter: [''],
      image: [''],
      displayOrder: [0],
      file: [''],
      imagesSource: [''],
      isFeatured:[false]
    });
  }

  private editForm(obj) {

    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.price.setValue(obj.price);
    this.f.cost.setValue(obj.cost);
    this.f.calories.setValue(obj.calories);
    this.f.itemID.setValue(obj.itemID);

    
    if (obj.modifiers != "") {
      var stringToConvert = obj.modifiers;
      this.selectedModifierIds = stringToConvert.split(',').map(Number);
      this.f.modifiers.setValue(obj.modifiers);
    }
    
    if (obj.addons != "") {
      var stringToConvert = obj.addons;
      this.selectedAddonIds = stringToConvert.split(',').map(Number);
      this.f.addons.setValue(obj.addons);
    }
    this.f.categoryID.setValue(obj.categoryID);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.isFeatured.setValue(obj.isFeatured);
    obj.isApplyDiscount = obj.isApplyDiscount == null ? true : obj.isApplyDiscount;
    this.f.isApplyDiscount.setValue(obj.isApplyDiscount);
    this.imgComp.imageUrl = obj.image;

  }
  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
  setSelecteditem() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingItems = true;
        this.f.itemID.setValue(sid);
        this.eventService.getById(sid, this.f.brandID.value).subscribe(res => {
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
          this.router.navigate(['/admin/item']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update Event.")
        this.loading = false;
      });
    }
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
     debugger
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
  
 
}
