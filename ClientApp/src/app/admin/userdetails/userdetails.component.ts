import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-userdetails',
  templateUrl: './userdetails.component.html',
  providers: []
})
export class UserDetailComponent implements OnInit {
  public user = new User();  
  constructor(public service: UserService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {  }

  ngOnInit() {
    this.setSelecteduser();
  }
  setSelecteduser() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.service.getById(sid).subscribe(res => {
          debugger
          this.editForm(res);
        });
      }
    })
  }
  updateuserstatus(user, status) {
    debugger
    user.statusID  = status;
    //Update 
    this.service.statusUpdate(user).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['admin/userdetails']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  private editForm(obj) {
    this.user = obj;
    debugger
  }
}
