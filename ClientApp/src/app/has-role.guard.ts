import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from './_services/user.service';

@Injectable({
  providedIn: 'root'
})
export class HasRoleGuard implements CanActivate {

constructor(private userService: UserService, private router: Router){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
       
     this.userService.getAllData();
     //return this.userService.userModel.type.includes(route.data.type);
     return true;
  }
  
}
