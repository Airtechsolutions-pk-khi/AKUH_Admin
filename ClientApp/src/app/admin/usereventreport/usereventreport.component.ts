import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { EventdetailReport, SalesdetailReport } from 'src/app/_models/Report';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { delay, map } from 'rxjs/operators';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Event } from 'src/app/_models/Event';
import { EventAttendees } from '../../_models/EventAttendees';
@Component({
  selector: 'app-usereventreport',
  templateUrl: './usereventreport.component.html',
  providers: [ExcelService]
})

export class UserEventReportComponent implements OnInit {
  data$: Observable<EventdetailReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedEvent;
  Attendees: EventAttendees[] = [];
  selectedEvents: string[];
  attendeesID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  orderDetails: EventdetailReport[] = [];
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: ReportService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public excelService: ExcelService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    // this.selectedLocation = this.ls.getSelectedLocation().locationID


    this.LoadAttendee();
  }

  ngOnInit() {
    
  }

  getData(attendeesIDs) {
    debugger
    this.service.AttendeesRpt(attendeesIDs, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
          debugger
          this.orderDetails = res;
        }
        else
          this.ts.showError("Error", "Something went wrong");

      }, error => {
        this.ts.showError("Error", "Failed to delete record.")
      });
  }

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
  exportAsXLSX(): void {
    debugger;
    this.excelService.exportAsExcelFile(this.orderDetails, 'Report_Export');
  }
  LoadAttendee() {
    debugger
    this.service.loadAttendees().subscribe((res: any) => {
      debugger
      this.Attendees = res;
      this.attendeesID = this.selectedEvent;
      this.loadEventsMulti()
        .subscribe((res) => {
        this.Attendees = res;
        var arr=[];
        this.Attendees.forEach(element => {
          arr.push(element.attendeesID);
        });
        this.getData(this.selectedEvents.toString());   
      });
    });   
  }

  loadEventsMulti(term: string = null): Observable<EventAttendees[]> {
    let items = this.Attendees;
    if (term) {
      items = items.filter(x => x.fullName.toLocaleLowerCase().indexOf(term.toLocaleLowerCase()) > -1);
    }
    return of(items).pipe(delay(500));
  }
  Filter() {
    debugger
    this.getData(this.selectedEvents.toString());
  }
}
