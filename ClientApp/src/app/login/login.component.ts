import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../_services/login.service';
import { first } from 'rxjs/operators';
import { LocalStorageService } from '../_services/local-storage.service';
import { ToastService } from '../_services/toastservice';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { environment } from 'src/environments/environment.prod';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  notification;
  doctor;
  mamjiUser;
  pharmacy;
  reception;
  diagnostic;
  reports;
  settings;

  constructor(private formBuilder: FormBuilder,
    public service: LoginService,
    public userService: UserService,
    public ts: ToastService,
    private router: Router,
    private ls: LocalStorageService) { 
    }


  ngOnInit() {
    this.createForm();
  }
  onSubmit() {
debugger
    this.loginForm.markAllAsTouched();
    if (this.loginForm.invalid) {
      return;
    }

    
    this.service.login(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          
          if (data != null) {        
            
            this.ls.setSelectedBrand(data);
            //this.userService.getAllData();
             
            //environment.rootScope = data;
            
            this.router.navigate(["/admin/dashboard"]);     
            
          }
          else {
            this.ts.showError("Error", "Email or password is not correct.");
          }
        },
        error => {
          this.ts.showError("Error", "Something went wrong.");
        });  
  }

  get f() { return this.loginForm.controls; }
  private createForm() {

    this.loginForm = this.formBuilder.group({

      email: ['', Validators.required],
      password: ['', Validators.required],

    });
  }
 
}
