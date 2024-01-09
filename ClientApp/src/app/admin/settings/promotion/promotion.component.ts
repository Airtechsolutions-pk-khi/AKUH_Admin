import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { Promotion } from 'src/app/_models/Promotion';
import { ToastService } from 'src/app/_services/toastservice';
import { PromotionService } from 'src/app/_services/promotion.service';

@Component({
  selector: 'app-promotion',
  templateUrl: './promotion.component.html',
  providers: []
})

export class PromotionComponent implements OnInit {
  data$: Observable<Promotion[]>;
  oldData: Promotion[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: PromotionService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router) {

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

  Edit(offers) {
    this.router.navigate(["admin/promotion/edit", offers]);
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

}
