import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { Appsetting } from 'src/app/_models/Appsetting';
import { ToastService } from 'src/app/_services/toastservice';
import { AppsettingService } from 'src/app/_services/appsetting.service';

@Component({
  selector: 'app-appsettings',
  templateUrl: './appsettings.component.html',
  providers: []
})

export class AppsettingComponent implements OnInit {
  data$: Observable<Appsetting[]>;
  oldData: Appsetting[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedsetting;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: AppsettingService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router) {
    /*this.selectedBrand =this.ls.getSelectedBrand().brandID;*/

    this.loading$ = service.loading$;
    this.submit = false;

  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    debugger
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

  Edit(appsetting) {
    this.router.navigate(["admin/settings/appsettings/edit", appsetting]);
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
