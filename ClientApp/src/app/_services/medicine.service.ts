import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { Medicine } from '../_models/Medicine';
import { MedicalServiceType } from '../_models/MedicalServiceType';

interface SearchMedicineResult {
  data: Medicine[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
function sort(data: Medicine[], column: SortColumn, direction: string): Medicine[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}
function matches(data: Medicine, term: string) {
  return data.name.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

  constructor(private http: HttpClient) {
  }

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<Medicine[]>([]);
  private _data$ = new BehaviorSubject<Medicine[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public medicine: Medicine[];
  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set searchTerm(searchTerm: any) { this._set({ searchTerm }); }
  set sortColumn(sortColumn: SortColumn) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }

  get data$() {
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }

  ExportList(medicineID) {
    return this.http.get<Medicine[]>(`api/medicine/all`);
  }
  getById(id) {
    return this.http.get<Medicine[]>(`api/medicine/medicine/${id}`);
  }
  getAllData() {
    const url = `api/medicine/all`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<Medicine[]>(url).subscribe(res => {
        this.medicine = res;

        this._data$.next(this.medicine);
        this._allData$.next(this.medicine);

        this._search$.pipe(
          switchMap(() => this._search()),
          tap(() => this._loading$.next(false))
        ).subscribe(result => {
          this._data$.next(result.data);
          this._total$.next(result.total);
        });

        this._search$.next();
      });
  }
  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<SearchMedicineResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.medicine, sortColumn, sortDirection);

    //// 2. filter
    sortedData = sortedData.filter(data => matches(data, searchTerm));
    const total = sortedData.length;

    // 3. paginate
    const data = sortedData.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({ data, total });
  }


  clear() {
    // clear by calling subject.next() without parameters
    this._search$.next();
    this._data$.next(null);
    this._allData$.next(null);
    this._total$.next(null);
    this._loading$.next(null);
    this._state = {
      page: 1,
      pageSize: 10,
      searchTerm: '',
      sortColumn: '',
      sortDirection: ''
    };
  }
  insert(data) {
    return this.http.post('api/medicine/insert', data)
      .pipe(map(res => {

        console.log(res);
        return res;
      }));
  }
  update(updateData) {
    return this.http.post(`api/medicine/update`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
  delete(data) {
    debugger
    return this.http.post(`api/medicine/delete`, data);
  }
 
}