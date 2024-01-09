import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Coupon } from 'src/app/_models/Coupon';
import { CouponService } from 'src/app/_services/coupon.service';

@Component({
  selector: 'app-coupon',
  templateUrl: './coupon.component.html',
  styleUrls: ['./coupon.component.css'],
  providers: [ExcelService]
})
export class CouponComponent implements OnInit {
  data$: Observable<Coupon[]>;  
  oldData: Coupon[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedCoupon;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  
  constructor(public service: CouponService,
    public ls :LocalStorageService,
    public excelService: ExcelService,
    public ts :ToastService,
    public router:Router) {
/*    this.selectedCoupon =this.ls.getSelectedDoctor().doctorID;*/
 
     this.loading$ = service.loading$;
     this.submit = false;
     
   }
 
  // exportAsXLSX(): void {
  //  this.service.ExportList(this.selectedDoctor).subscribe((res: any) => {    
  //  //  this.excelService.exportAsExcelFile(res, 'Report_Export');
  //  }, error => {
  //    this.ts.showError("Error","Failed to export")
  //  });
  //}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.service.getAllData();
    this.data$ = this.service.data$;
    this.total$ = this.service.total$;
    this.loading$ = this.service.loading$;
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
  Edit(coupons) {
    this.router.navigate(["admin/settings/coupon/edit", coupons]);
  }


Delete(item) {
  debugger
this.service.delete(item).subscribe((res: any) => {
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

}
