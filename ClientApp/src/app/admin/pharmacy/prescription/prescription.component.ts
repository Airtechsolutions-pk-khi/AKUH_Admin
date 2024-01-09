import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Observable, Observer, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Prescription } from 'src/app/_models/Prescription';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import { HttpClient } from '@angular/common/http';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from '../../../datepicker-range/datepicker-range-popup';

const now = new Date();

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css'],
  providers: [ExcelService]
})
export class PrescriptionComponent implements OnInit {
  data$: Observable<Prescription[]>;  
  oldData: Prescription[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedPrescription;
  name = "Mr";
  userName = "";
  base64Image: any;
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: PrescriptionService, private httpClient: HttpClient,
    public ls :LocalStorageService,
    public excelService: ExcelService,
    public ts :ToastService,
    public router:Router) {
    //this.selectedPrescription =this.ls.getSelectedPrescription().prescriptionID;
    this.userName = this.ls.getSelectedBrand().userName;

     this.loading$ = service.loading$;
     this.submit = false;
     
   }
   exportAsXLSX(): void {
    this.service.ExportList(this.selectedPrescription).subscribe((res: any) => {    
/*      this.excelService.exportAsExcelFile(res, 'Report_Export');*/
    }, error => {
      this.ts.showError("Error","Failed to export")
    });
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

  View(prescription) {
    debugger
    this.router.navigate(["admin/pharmacy/prescription/edit", prescription]);
  }

  Edit(prescription) {
    debugger
    this.router.navigate(["admin/pharmacy/prescription/edit", prescription]);
  }

  Delete(data) {
    debugger
    data.userName = this.userName;
    this.service.delete(data).subscribe((res: any) => {
      if(res!=0){
        this.ts.showSuccess("Success","Record deleted successfully.")
        this.getData();
      }
      else
      this.ts.showError("Error","Failed to delete record.")
    
    }, error => {
      this.ts.showError("Error","Failed to delete record.")
    });
  }
  downloadImage(img) {
    debugger
    var a = this.service.getById(img);
    const imgUrl = img.src;
    const imgName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
    this.httpClient.get(imgUrl, { responseType: 'blob' as 'json' })
      .subscribe((res: any) => {
        const file = new Blob([res], { type: res.type });

        // IE
        //if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        //  window.navigator.msSaveOrOpenBlob(file);
        //  return;
        //}

        const blob = window.URL.createObjectURL(file);
        const link = document.createElement('a');
        link.href = blob;
        link.download = imgName;

        // Version link.click() to work at firefox
        link.dispatchEvent(new MouseEvent('click', {
          bubbles: true,
          cancelable: true,
          view: window
        }));

        setTimeout(() => { // firefox
          window.URL.revokeObjectURL(blob);
          link.remove();
        }, 100);
      });
  }
  Filter() {
    this.service.getAllData(this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
  }
  //updateAppointment(appointment, status) {
  //  debugger
  //  appointment.appointmentStatus = status;
  //  appointment.statusMsg = this.StatusMsg;
  //  //Update 
  //  this.service.statusUpdate(appointment).subscribe(data => {

  //    if (data != 0) {
  //      this.ts.showSuccess("Success", "Record updated successfully.")
  //      this.router.navigate(['reception/appointment']);
  //    }
  //  }, error => {
  //    this.ts.showError("Error", "Failed to update record.")
  //  });
  //}
 }
