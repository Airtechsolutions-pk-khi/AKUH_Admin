import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';

import { Event, EventImageJunc } from '../_models/Event';
import { Category } from '../_models/Cateogry';
import { EventCategory } from '../_models/EventCategory';
import { Organizer } from '../_models/Organizer';
import { Speaker } from '../_models/Speaker';

interface SearchEventResult {
  data: Event[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: Event[], column: SortColumn, direction: string): Event[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: Event, term: string) {
  
  return data.name.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class EventService {

  constructor(private http: HttpClient) {
  }

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<Event[]>([]);
  private _data$ = new BehaviorSubject<Event[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public event: Event[];
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
  ExportList() {
    return this.http.get<Event[]>(`api/event/all`);
  }
  loadCategories(brandId) {
    return this.http.get<Category[]>( `api/category/all/${brandId}`);
  }
  loadActiveCategories() {    
    return this.http.get<EventCategory[]>( `api/eventcategory/DropdownAll`);
  }
  loadOrganizer() {    
    return this.http.get<Organizer[]>( `api/organizer/DropdownAll`);
  }
  loadSpeaker() {    
    return this.http.get<Speaker[]>( `api/speaker/DropdownAll`);
  }
  loadItems(brandId) {
    return this.http.get<Category[]>( `api/item/all/${brandId}`);
  }
  loadModifierList(brandId) {
    return this.http.get<Category[]>( `api/modifier/all/${brandId}`);
  }
  loadAddonList(brandId) {    
    return this.http.get<Category[]>( `api/addons/all/${brandId}`);
  }
  loadEventImages(id) {
    return this.http.get<EventImageJunc[]>(`api/event/images/${id}`);
  }
  getById(id) {
    return this.http.get<Event[]>(`api/event/${id}`);
  }
  getTodaysItems(brandId) {
    return this.http.get<Event[]>(`api/item/settings/${brandId}`);
  }
  getAllData() {

    const url = `api/event/all`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<Event[]>(url).subscribe(res => {
        this.event = res;
          
        this._data$.next(this.event);
        this._allData$.next(this.event);

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

  private _search(): Observable<SearchEventResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.event, sortColumn, sortDirection);

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
    debugger
    return this.http.post(`api/event/insert`, data)
      .pipe(map(res => {
        
        console.log(res);
        return res;
      }));
  }

  update(updateData) {
    return this.http.post(`api/event/update`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
  updateSettings(updateData) {
     
    return this.http.post(`api/item/update/settings`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
  delete(updateData) {
    return this.http.post(`api/event/delete`, updateData);
  }
  //  delete(id) {
  //    return this.http.delete<any[]>(`api/items/delete/${id}`);
  //  }

}
