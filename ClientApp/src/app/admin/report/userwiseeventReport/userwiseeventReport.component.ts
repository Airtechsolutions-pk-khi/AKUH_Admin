//import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
//import { Observable, of, Subscription } from 'rxjs';
//import { NgbdSortableHeader, SortEvent, } from 'src/app/_directives/sortable.directive';
//import { LocalStorageService } from 'src/app/_services/local-storage.service';
//import { Router } from '@angular/router';
//import { ToastService } from 'src/app/_services/toastservice';
//import { ReportService } from 'src/app/_services/report.service';
//import { UserwiseReport } from 'src/app/_models/Report';
//import { Location } from 'src/app/_models/Location';
//import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
//import { delay, map, switchMap, tap } from 'rxjs/operators';
//import { ExcelService } from 'src/ExportExcel/excel.service';

//@Component({
//  selector: 'app-userwiseevent',
//  templateUrl: './userwiseevent.component.html',
//  styleUrls: ['./userwiseevent.component.css'],
//  providers: [ExcelService]
//})

//export class UserWiseEventComponent implements OnInit {
//  data$: Observable<UserwiseReport[]>;

//  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
//  private selectedBrand;
//  private selectedLocation;
//  Locations: Location[] = [];
//  selectedLocations = [];
//  locationID = 0;
//  selectedAttendee = 0;
//  total$: Observable<number>;
//  loading$: Observable<boolean>;
//  locationSubscription: Subscription;
//  submit: boolean;
//  userwise: UserwiseReport[] = [];
//  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
//  @ViewChild('locationDrp') drplocation: any;
//  constructor(public service: ReportService,
//    public ls: LocalStorageService,
//    public ts: ToastService,
//    public excelService: ExcelService,
//    public router: Router) {
//    this.selectedBrand = this.ls.getSelectedBrand().brandID;
//    // this.selectedLocation = this.ls.getSelectedLocation().locationID

//    this.loadLocations();
//  }

//  ngOnInit() {

//  }

//  parseDate(obj) {
//    return obj.year + "-" + obj.month + "-" + obj.day;;
//  }

//  getData(locationIDs) {
//    debugger
//    this.service.SalesDetailRpt2(this.selectedAttendee);
//    this.data$ = this.service.data$;
//    this.total$ = this.service.total$;
//    this.loading$ = this.service.loading$;
//  }
//  onSort({ column, direction }: SortEvent) {

//    this.headers.forEach(header => {
//      if (header.sortable !== column) {
//        header.direction = '';
//      }
//    });
//    this.service.sortColumn = column;
//    this.service.sortDirection = direction;
//  }

//  loadLocations() {
//    this.service.loadLocations(this.selectedBrand).subscribe((res: any) => {

//      this.Locations = res;
//      this.locationID = this.selectedLocation;

//      this.loadLocationsMulti()
//        .pipe(map(x => x.filter(y => !y.disabled)))
//        .subscribe((res) => {
//          this.Locations = res;
//          var arr = [];
//          this.Locations.forEach(element => {
//            arr.push(element.locationID);
//          });
//          this.selectedLocations = arr;
//          this.getData(this.selectedLocations.toString());
//        });

//    });
//  }

//  loadLocationsMulti(term: string = null): Observable<Location[]> {
//    let items = this.Locations;
//    if (term) {
//      items = items.filter(x => x.name.toLocaleLowerCase().indexOf(term.toLocaleLowerCase()) > -1);
//    }
//    return of(items).pipe(delay(500));
//  }

//  Filter() {

//    this.getData(this.selectedLocations.toString());
//  }

//  printout(html) {

//    var newWindow = window.open('_self');
//    newWindow.document.write(html);
//    newWindow.print();
//  }
//}
