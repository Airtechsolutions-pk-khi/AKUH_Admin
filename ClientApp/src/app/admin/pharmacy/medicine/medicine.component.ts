import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { MedicineService } from 'src/app/_services/medicine.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { Medicine } from 'src/app/_models/Medicine';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  providers: [ExcelService]
})
export class MedicineComponent implements OnInit {
  data$: Observable<Medicine[]>;
  oldData: Medicine[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  closeResult: string;  

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: MedicineService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router,
    private modalService: NgbModal) {

    this.loading$ = service.loading$;
    this.submit = false;
  }
  ngOnInit() {
    this.getData();
  }
  //exportAsXLSX(): void {
  //  this.service.ExportList(this.selectedBrand).subscribe((res: any) => {
  //    this.excelService.exportAsExcelFile(res, 'Report_Export');
  //  }, error => {
  //    this.ts.showError("Error", "Failed to export")
  //  });
  //}
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
  Edit(medicine) {
    this.router.navigate(["admin/pharmacy/medicine/edit", medicine]);
  }
  Update(medicine) {
    debugger
    this.service.update(medicine).subscribe((res: any) => {
      if (res != 0) {
        this.ts.showSuccess("Success", "Price Updated Successfully.")
        this.getData();
      }
    })
  }

  Delete(obj) {
    debugger
    this.service.delete(obj).subscribe((res: any) => {
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
  open(content, obj) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
      if (result === 'yes') {
        this.Delete(obj);
      }
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
