import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { FormPermission } from '../_models/FormPermission';

interface SearchPermissionResult {
  data: FormPermission[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
function sort(data: FormPermission[], column: SortColumn, direction: string): FormPermission[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}
function matches(data: FormPermission, term: string) {
  return data.roleName.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})
export class FormPermissionService {

  constructor(private http: HttpClient) {
  }

  private _allData$ = new BehaviorSubject<FormPermission[]>([]);
  private _data$ = new BehaviorSubject<FormPermission[]>([]);
  public permission: FormPermission[];

  get data$() {
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }

  getById(rolename) {
    debugger
    return this.http.get<FormPermission[]>(`api/formpermission/permission/${rolename}`);
    
  }

  getAllData() {
    const url = `api/formpermission/all`;
    console.log(url);
  }

  insert(data) {
    return this.http.post('api/formpermission/insert', data)
      .pipe(map(res => {

        console.log(res);
        return res;
      }));
  }
  update(updateData) {
    debugger
    return this.http.post(`api/formpermission/update`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
  delete(data) {
    debugger
    return this.http.post(`api/formpermission/delete`, data);
  }
 
}
