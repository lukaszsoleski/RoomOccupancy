import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { MustMatch } from 'src/app/common/validators/must-match.validator';
import { UsersService } from 'src/app/services/users.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

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
              private router: Router,
              private spinner: NgxSpinnerService) { }

  initRegistrationForm() {
    this.registerForm =  this.fb.group({
      firstName: ['lukasz', Validators.required],
      lastName: ['soles', Validators.required],
      email: ['luk@gmail.com', Validators.required],
      password: ['1234567aA!', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['1234567aA!']
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }
  protected onSubmit() {
    if (this.registerForm.invalid) { return; }
    this.spinner.show();
    this.userService.register(this.registerForm.value).subscribe(x => {
      this.spinner.hide();
      this.toast.success('Link aktywacyjny został przesłany pod podany adres email.');
      this.router.navigate([`/login`]);
    }, () => this.spinner.hide());
  }

  ngOnInit() {
    this.initRegistrationForm();
  }
}
