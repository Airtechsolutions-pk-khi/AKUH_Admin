import { Component, OnInit, NgModule } from '@angular/core';
import { Router } from '@angular/router';
// import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment.prod';
// import { LoginService } from '../services/login.service';
// import { DashboardService } from '../services/dashboard.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { LocationsService } from '../_services/locations.service';
import { LocalStorageService } from '../_services/local-storage.service';
import { UserService } from '../_services/user.service';
import { LoginService } from '../_services/login.service';
import { Permission, PermissionForms } from '../_models/Permission';
import { element } from 'protractor';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],

})
export class LayoutComponent implements OnInit {
  _Langname = "";
  name = "";
  email = "";
  
  // roleName;
  // isDiagnostic: boolean = true;
  // isDoctor: boolean = true;
  // isUser: boolean = true;
  // isNoti: boolean = true;
  // isPharmacy: boolean = true;
  // isReception: boolean = true;
  // isReport: boolean = true;
  // isSetting: boolean = true;

  // public permission = new Permission();
  

  ngOnInit() {
    //this.type = this.ls.getSelectedBrand().type;
  }
  constructor(private router: Router
    , public service: LocationsService
    , public userService: UserService
    , public ls: LocalStorageService) {
      // this.userName = this.ls.getSelectedBrand().userName;
      // this.type = this.ls.getSelectedBrand().type;
       
      // //  this.permission=environment.rootScope;
      // this.permission=this.ls.getSelectedBrand(); 
      //  var role = this.permission.permissionForm.find(element => element.roleName == this.type); 
      //  var roleNameType= role.roleName;
      //  this.isDiagnostic = role.diagnostic === 1 ? true : false;
      //  this.isDoctor = role.doctor === 1 ? true : false;
      //  this.isUser = role.mamjiUser === 1 ? true : false;
      //  this.isNoti = role.notification === 1 ? true : false;
      //  this.isPharmacy = role.pharmacy === 1 ? true : false;
      //  this.isReception = role.reception === 1 ? true : false;
      //  this.isReport = role.reports === 1 ? true : false;
      //  this.isSetting = role.setting === 1 ? true : false;
       

                                 
  }
  Logout() {

    sessionStorage.clear();
    this.router.navigate(['/']);
  }

  // private loadLocations() {
  //   var loc = this.ls.getUsers();
  //   if (loc != null) {
  //     this.Locations = this.ls.getUsers();
  //     this.locationID = this.ls.getSelectedUser().locationID;
  //   }
  //   else {
  //     this.userService.getAllData().subscribe((res: any) => {
  //       debugger
  //       if (res.length > 0) {
  //         this.ls.setUser(res);
  //         this.ls.setSelectedUser(res[0]);
  //         this.locationID =res[0].locationID;
  //         this.Locations =res;
  //       }
  //       else {
  //         this.router.navigate(['/']);
  //       }
  //     });
  //   }
  //   this.Locations = this.ls.getLocation();
  //   this.locationID = this.ls.getSelectedLocation().locationID;

  // }
  changeloc(LocObj) {

    //this.locationID = this.ls.selectedLocation().locationID;
  }
}
