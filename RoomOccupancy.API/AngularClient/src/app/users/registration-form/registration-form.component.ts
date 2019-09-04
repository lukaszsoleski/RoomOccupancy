import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { MustMatch } from 'src/app/common/validators/must-match.validator';
import { UsersService } from 'src/app/services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class RegistrationFormComponent implements OnInit {

  get f() { return this.registerForm.controls; }

  registerForm: FormGroup;
  constructor(private fb: FormBuilder,
              private userService: UsersService,
              private toast: ToastrService,
              private router: Router) { }

  initRegistrationForm() {
    this.registerForm =  this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['']
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }
  protected onSubmit() {
    this.userService.register(this.registerForm.value).subscribe(x => {
      this.toast.success('Konto zostało utworzone. Sprawdź skrzynkę pocztową aby dokończyć proces rejestracji.');
      this.userService.login(this.f.email.value, this.f.password.value).subscribe(y => {
        //TODO: auto refresh token using interceptor
      });
    });
  }
  ngOnInit() {
    this.initRegistrationForm();
  }

}
