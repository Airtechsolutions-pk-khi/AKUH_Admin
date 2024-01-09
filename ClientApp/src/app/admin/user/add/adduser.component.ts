import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { UserService } from 'src/app/_services/user.service';
import { ToastService } from 'src/app/_services/toastservice';
//import { debug } from 'console';

@Component({
  selector: 'app-adduser',
  templateUrl: './adduser.component.html',
})
export class AdduserComponent implements OnInit {

  submitted = false;
  userForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save";
  showPassword = false;
  passwordFieldType = 'password';

  // ... other component methods and lifecycle hooks

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
    this.passwordFieldType = this.showPassword ? 'text' : 'password';
  }

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private userService: UserService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.userForm.controls; }

  private createForm() {
    this.userForm = this.formBuilder.group({
      userName: ['', Validators.required],
      email: ['', Validators.required],
      statusID: [true],
      password: ['', Validators.required],
      type: ['', Validators.required],
      id: 0,
    });
  }

  private editForm(obj) {
    this.f.userName.setValue(obj.userName);
    this.f.email.setValue(obj.email);
    this.f.password.setValue(obj.password);
    this.f.type.setValue(obj.type);
    this.f.id.setValue(obj.id);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    //this.f.statusID.setValue(obj.statusID.value === true ? 1 : 2);
  }

  setSelectedCustomer() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.id.setValue(sid);
        this.userService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }
  onSubmit() {
    debugger
    this.userForm.markAllAsTouched();
    this.submitted = true;
    if (this.userForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt(this.f.id.value) === 0) {
      //Insert customer
      console.log(JSON.stringify(this.userForm.value));
      this.userService.insert(this.userForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/user']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update customer
      this.userService.update(this.userForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/user']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
