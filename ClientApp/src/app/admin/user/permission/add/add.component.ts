import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { UserService } from 'src/app/_services/user.service';
import { ToastService } from 'src/app/_services/toastservice';
import { PermissionForms } from '../../../../_models/Permission';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
})
export class AddComponent implements OnInit {
  //public permission = new Permission();
  public formName = new PermissionForms();
  submitted = false;
  loading = false;
  ButtonText = "Save";
  UserList = [];


  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private userService: UserService

  ) {
    // this.createForm();
    this.loadUser();
  }

  ngOnInit() {
    //this.setSelectedCustomer();
  }

  // get f() { return this.permissionForm.controls; }

  // private createForm() {
  //   this.permissionForm = this.formBuilder.group({
  //     formName: ['', Validators.required],
  //     formAccess: true,
  //     roleName: [''],
  //     permissionID: [0],
  //   });
  // }



  //setSelectedCustomer() {
  //  this.route.paramMap.subscribe(param => {
  //    const sid = +param.get('id');
  //    if (sid) {
  //      this.loadingCustomer = true;
  //      this.f.id.setValue(sid);
  //      this.userService.getPermissionId(sid).subscribe(res => {
  //        //Set Forms
  //        this.editForm(res);
  //        this.loadingCustomer = false;
  //      });
  //    }
  //  })
  //}

  onSelect(rolename) {
    debugger
    this.userService.getPermissionId(rolename).subscribe(res => {
      //Set Forms
      if (res != null) {
        this.formName = res[0];
      }
      debugger  
    });
  }
  onSubmit() {
    debugger
    
    //Update customer
    this.userService.permissionupdate(this.formName).subscribe(obj => {
      this.loading = false;
      if (obj != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/permission']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
      this.loading = false;
    });
  }

  private loadUser() {
    //this.userService.loadUser().subscribe((res: any) => {
    //  this.UserList = res;
    //});
    debugger 
    this.UserList = [
      { "type": "SuperAdmin" },
      { "type": "Admin" },
      { "type": "Pharmacy" },
      { "type": "Reception" },
      { "type": "Laboratory" },
    ];
  }
}
