import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';

import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Faq } from 'src/app/_models/Faq';
import { FaqService } from 'src/app/_services/faq.service';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  providers: [ExcelService]
})

export class FaqComponent implements OnInit {
  data$: Observable<Faq[]>;
  oldData: Faq[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  locationSubscription: Subscription;
  submit: boolean;
  closeResult: string;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: FaqService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router,
    private modalService: NgbModal) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    this.loading$ = service.loading$;
    this.submit = false;
  }

  ngOnInit() {
    this.getData();
  }
  exportAsXLSX(): void {
    debugger
    this.service.ExportList().subscribe((res: any) => {
       this.excelService.exportAsExcelFile(res, 'Report_Export');
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
    this.router.navigate(["admin/faq/edit", data]);
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
