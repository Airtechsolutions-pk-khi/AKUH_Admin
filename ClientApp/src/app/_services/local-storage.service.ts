import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  selectedBrand: any;
  selectedLocation: any;

  constructor(private http: HttpClient) {
    
  }


  setSelectedBrand(brand) {
    sessionStorage.setItem('_autheticatedUser', JSON.stringify(brand));
  }

  setSelectedLocation(location) {
    sessionStorage.setItem('selectedLocation', JSON.stringify(location));
  }
  setSelectedUser(user) {
    sessionStorage.setItem('selectedUser', JSON.stringify(user));
  }
  getSelectedLocation() {
    return JSON.parse(sessionStorage.getItem('selectedLocation'));
  }
  setLocation(location) {
    sessionStorage.setItem('_Locations', JSON.stringify(location));
  }
  setUser(user) {
    sessionStorage.setItem('_Users', JSON.stringify(user));
  }
  getLocation() {
    
    return JSON.parse(sessionStorage.getItem('_Locations'));
  }
  getUsers() {
    return JSON.parse(sessionStorage.getItem('_Users'));
  }
  
  getSelectedBrand() {
    debugger
    return JSON.parse(sessionStorage.getItem('_autheticatedUser'));
  }
  getSelectedAttendee() {
    debugger
    return JSON.parse(sessionStorage.getItem('_autheticatedUser'));
  }

  getSelectedUser() {
    let userInfo = JSON.parse(sessionStorage.getItem("currentUser"));
    if(userInfo !==null){
      userInfo = JSON.parse(userInfo.data);
      return userInfo;
    }
  }
  login(username, password) {
    debugger
    let userInfo = JSON.parse(sessionStorage.getItem(`api/login/authenticate/${username}/${password}`))
    if(userInfo !== null)
    {
    userInfo = JSON.parse(userInfo.data);
    var userName = userInfo.email;
    return userName;
  }
  }
  getSelectedDoctor() {
    return JSON.parse(sessionStorage.getItem('_autheticatedUser'));
  }
  getSelectedPrescription() {
    return JSON.parse(sessionStorage.getItem('_autheticatedUser'));
  }
}
