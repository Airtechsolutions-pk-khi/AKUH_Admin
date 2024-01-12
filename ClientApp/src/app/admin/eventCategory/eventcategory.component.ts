import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { EventCategory } from '../../_models/EventCategory';
import { EventCategoryService } from '../../_services/eventCategory.service';

@Component({
  selector: 'app-eventcategory',
  templateUrl: './eventcategory.component.html',
  providers: [ExcelService]
})

export class EventCategoryComponent implements OnInit {
  data$: Observable<EventCategory[]>;
  oldData: EventCategory[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: EventCategoryService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    this.loading$ = service.loading$;
    this.submit = false;
  }

  ngOnInit() {
    this.getData();
  }
  exportAsXLSX(): void {
    this.service.ExportList(this.selectedBrand).subscribe((res: any) => {
      //  this.excelService.exportAsExcelFile(res, 'Report_Export');
    }, error => {
      this.ts.showError("Error", "Failed to export")
    });
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

  Edit(data) {
    this.router.navigate(["admin/eventcategory/edit", data]);
  }

  Delete(obj) {
    this.service.delete(obj).subscribe((res: any) => {
      if (res != 0) {
        this.ts.showSuccess("Success", "Record deleted successfully.")
        this.getData();
      }
      else
        this.ts.showError("Error", "Failed to delete record.");

    }, error => {
      this.ts.showError("Error", "Failed to delete record.")
    });
  }
}
