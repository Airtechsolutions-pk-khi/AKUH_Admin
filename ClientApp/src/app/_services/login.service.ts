import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of, Subject } from 'rxjs';

import { Admin } from '../_models/Admin';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  public currentUser: Observable<Admin>;
  private currentUserSubject: BehaviorSubject<Admin>;
  

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<Admin>(JSON.parse(sessionStorage.getItem('_autheticatedUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): Admin {
    return this.currentUserSubject.value;
  }

  login(username, password) {
    return this.http.get<any[]>(`api/login/authenticate/${username}/${password}`);
   
  }
  getAllLocations(brandId) {
    return this.http.get<Location[]>(`api/location/all/${brandId}`);
  }
}
