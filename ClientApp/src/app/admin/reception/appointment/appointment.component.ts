import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Appointment } from 'src/app/_models/Appointment';
import { AppointmentService } from 'src/app/_services/appointment.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from '../../../datepicker-range/datepicker-range-popup';

const now = new Date();

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css'],
  providers: [ExcelService]
})
export class AppointmentComponent implements OnInit {
  data$: Observable<Appointment[]>;
  oldData: Appointment[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedAppointment;
  userName = "";
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: AppointmentService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router) {
    //this.selectedDoctor =this.ls.getSelectedAppointment().appointmentID;
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
    console.log('data$');
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

  Delete(data) {
    this.service.delete(data).subscribe((res: any) => {
      if (res != 0) {
        this.ts.showSuccess("Success", "Record deleted successfully.")
        this.getData();
      }
      else
        this.ts.showError("Error", "Failed to delete record.")

    }, error => {
      this.ts.showError("Error", "Failed to delete record.")
    });
  }

  Filter() {
    this.service.getAllData(this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
  }

  View(appointment) {
    this.router.navigate(["admin/appointment/view", appointment]);
  }
  //Print(sid) {
  //  this.service.printorder(sid, this.selectedBrand).subscribe((res: any) => {
  //    //Set Forms

  //    if (res.status == 1) {
  //      this.printout(res.html);
  //    }
  //    else
  //      this.ts.showError("Error", "Failed to print.")
  //  });
  //}
}
