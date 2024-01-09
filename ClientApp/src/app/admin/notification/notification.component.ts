import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Notification } from 'src/app/_models/Notification';
import { NotificationService } from 'src/app/_services/notification.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from '../../datepicker-range/datepicker-range-popup';

const now = new Date();
@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  providers: [ExcelService]
})
export class NotificationComponent implements OnInit {
  data$: Observable<Notification[]>;
  oldData: Notification[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedNotification;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: NotificationService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router) {

    this.loading$ = service.loading$;
    this.submit = false;

  }

  ngOnInit() {
    const date: NgbDate = new NgbDate(now.getFullYear(), now.getMonth() + 1, now.getDate());
    this._datepicker.fromDate = date;
    this._datepicker.toDate = date;

    this.getData();
  }

  getData() {
    this.service.getAllData(this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
    this.data$ = this.service.data$;
    this.total$ = this.service.total$;
    this.loading$ = this.service.loading$;
  }

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;
  }

  onSort({ column, direction }: SortEvent) {

    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }
  updateStatus(item) {
    item.isRead = 1 ? true : false;
    //Update 
    this.service.status(item).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/notification']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  Filter() {
    this.service.getAllData(this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
  }
}
