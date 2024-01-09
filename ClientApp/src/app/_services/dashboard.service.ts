import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { DashboardSummary } from '../_models/Dashboard';


interface SearchDoctorsResult {
  data: DashboardSummary[];
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) {
  }

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<DashboardSummary[]>([]);
  private _data$ = new BehaviorSubject<DashboardSummary[]>([]);
  public dashboardSummary: DashboardSummary[];

  get loading$() { return this._loading$.asObservable(); }

  get data$() {
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }

  getAllData() {
    return this.http.get<any[]>(`api/dashboard/all`);
  }
  getChart() {
    return this.http.get<any[]>(`api/dashboard/getcharts`);
  }
}
