import { ToastrService } from 'ngx-toastr';
import { UsersService } from './../../services/users.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  public get email(): AbstractControl {
    return this.loginForm.get('emailFormControl');
  }
  public get password(): AbstractControl {
    return this.loginForm.get('passwordFormControl');
  }

  protected loginForm: FormGroup;
  constructor(private formBuilder: FormBuilder,
              private usersService: UsersService,
              private router: Router)
               { }
  protected onSubmit(){
    let pass = this.password.value;
    let email = this.email.value;

    this.usersService.login(email, pass).subscribe(x => {
      this.router.navigate([`/campus`]);
    });

  }
  ngOnInit() {

    this.loginForm = this.formBuilder.group({
      emailFormControl: ['', [
        Validators.required,
        Validators.email,
      ]],
      passwordFormControl: ['']
    });

    this.loginForm.valueChanges.subscribe(x => {
      console.log(this.loginForm.invalid);
    });
  }

}


