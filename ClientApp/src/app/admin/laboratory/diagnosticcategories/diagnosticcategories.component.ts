import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { DiagnosticCategories } from 'src/app/_models/DiagnosticCategories';
import { DiagnosticCategoryService } from 'src/app/_services/diagnosticcategories.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';

@Component({
  selector: 'app-diagnosticcategories',
  templateUrl: './diagnosticcategories.component.html',
  styleUrls: ['./diagnosticcategories.component.css'],
  providers: [ExcelService]
})
export class DiagnosticCategoriesComponent implements OnInit {
  data$: Observable<DiagnosticCategories[]>;
  oldData: DiagnosticCategories[];
  total$: Observable<number>;
  loading$: Observable<boolean>;

  locationSubscription: Subscription;
  submit: boolean;

  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
    closeResult: string;

  constructor(public service: DiagnosticCategoryService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router,
    public modalService: NgbModal) {  
      
      this.loading$ = service.loading$;
      this.submit = false;
    }

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
  Edit(medicine) {
    this.router.navigate(["admin/laboratory/diagnosticcategory/edit", medicine]);
  }
  Delete(obj) {
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
