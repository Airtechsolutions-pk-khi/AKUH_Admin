import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Doctors } from 'src/app/_models/Doctors';
import { DoctorsService } from 'src/app/_services/doctors.service';
import { SignalrService } from 'src/app/_services/SignalrService';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
  providers: [ExcelService]
})
export class DoctorComponent implements OnInit {
  data$: Observable<Doctors[]>;
  oldData: Doctors[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedDoctor;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
    closeResult: string;

  constructor(public service: DoctorsService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router,
    private modalService: NgbModal
    ) {
    //this.selectedDoctor =this.ls.getSelectedDoctor().doctorID;
    this.loading$ = service.loading$;
    this.submit = false;

  }

  exportAsXLSX(): void {
    this.service.ExportList().subscribe((res: any) => {
      this.excelService.exportAsExcelFile(res, 'Report_Export');
    }, error => {
      this.ts.showError("Error", "Failed to export")
    });
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

  Edit(doctors) {
    this.router.navigate(["admin/managedoctor/doctor/edit", doctors]);
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
