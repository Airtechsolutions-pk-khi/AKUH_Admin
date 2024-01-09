import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { FormPermissionService } from 'src/app/_services/formpermission.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormPermission } from '../../_models/FormPermission';
import { UserService } from 'src/app/_services/user.service';
//import { debug } from 'console';

@Component({
  selector: 'app-formpermission',
  templateUrl: './formpermission.component.html'
})
export class FormPermissionComponent implements OnInit {
  public formName = new FormPermission();
  submitted = false;
  loadingPermission = false;
  permissionForm: FormGroup;
  loading = false;
  loadingformpermission = false;
  ButtonText = "Save";
  UserList = [];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    public userService: UserService,
    private permissionService: FormPermissionService

  ) {
    this.createForm();
    this.loadUser();
  }

  get f() { return this.permissionForm.controls; }

  private createForm() {
    this.permissionForm = this.formBuilder.group({
      notification: [true],
      doctor: [true],
      mamjiUser: [true],
      pharmacy: [true],
      reception: [true],
      diagnostic: [true],
      reports: [true],
      setting: [true],
      roleName: [''],
      formPermissionID: [0],
    });
  }

  private editForm(obj) {
    this.f.notification.setValue(obj.notification == 1 ? true : false);
    this.f.doctor.setValue(obj.doctor == 1 ? true : false);
    this.f.mamjiUser.setValue(obj.mamjiUser == 1 ? true : false);
    this.f.pharmacy.setValue(obj.pharmacy == 1 ? true : false);
    this.f.reception.setValue(obj.reception == 1 ? true : false);
    this.f.diagnostic.setValue(obj.diagnostic == 1 ? true : false);
    this.f.reports.setValue(obj.reports == 1 ? true : false);
    this.f.setting.setValue(obj.setting == 1 ? true : false);
    //this.f.formPermissionID.setValue(obj.formPermissionID);
    this.f.roleName.setValue(obj.roleName);
  }
  ngOnInit() {
    //this.setSelectedPermission();

  }

  onSelect(formName) {
    this.permissionService.getById(formName).subscribe(res => {
      //Set Forms
      if (res != null) {
        this.editForm(res);
        this.formName = res[0];
      }

    });
  }

  onSubmit() {
    {
      this.permissionForm.markAllAsTouched();
      this.submitted = true;
      if (this.permissionForm.invalid) { return; }
      this.loading = true;
      this.f.notification.setValue(this.f.notification.value == true ? 1 : 0);
      this.f.doctor.setValue(this.f.doctor.value == true ? 1 : 0);
      this.f.mamjiUser.setValue(this.f.mamjiUser.value == true ? 1 : 0);
      this.f.pharmacy.setValue(this.f.pharmacy.value == true ? 1 : 0);
      this.f.reception.setValue(this.f.reception.value == true ? 1 : 0);
      this.f.diagnostic.setValue(this.f.diagnostic.value == true ? 1 : 0);
      this.f.reports.setValue(this.f.reports.value == true ? 1 : 0);
      this.f.setting.setValue(this.f.setting.value == true ? 1 : 0);
      //this.f.permissionID.setValue(this.f.permissionID.value);
      this.f.roleName.setValue(this.f.roleName.value);

      this.permissionService.update(this.permissionForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/permission']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
  private loadUser() {
    this.userService.loadUser().subscribe((res: any) => {
      this.UserList = res;
    });

    // this.UserList = [
    //   { "type": "Super Admin" },
    //   { "type": "Admin" },
    //   { "type": "Pharmacy" },
    //   { "type": "Reception" },
    //   { "type": "Laboratory" },
    // ];
  }


}
