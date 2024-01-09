import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Laboratory } from 'src/app/_models/Laboratory';
import { LaboratoryService } from 'src/app/_services/laboratory.service';
@Component({
  selector: 'app-laboratoryinquiry',
  templateUrl: './laboratoryinquiry.component.html',
  styleUrls: ['./laboratoryinquiry.component.css'],
  providers: [ExcelService]
})
export class LaboratoryinquiryComponent implements OnInit {
  data$: Observable<Laboratory[]>;
  oldData: Laboratory[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedReport;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: LaboratoryService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router) {

    this.loading$ = service.loading$;
    this.submit = false;

  }

  exportAsXLSX(): void {
    this.service.ExportList(this.selectedReport).subscribe((res: any) => {
    //  this.excelService.exportAsExcelFile(res, 'Report_Export');
    }, error => {
      this.ts.showError("Error", "Failed to export")
    });
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
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

  Edit(laboratory) {
    this.router.navigate(["admin/reception/laboratoryinquiry/edit", laboratory]);
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
}
