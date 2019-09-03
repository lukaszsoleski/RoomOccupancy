import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class RegistrationFormComponent implements OnInit {

  public get email(): AbstractControl {
    return this.registrationForm.get('email');
  }
  public get firstName(): AbstractControl {
    return this.registrationForm.get('firstName');
  }
  public get lastName(): AbstractControl {
    return this.registrationForm.get('lastName');
  }
  public get password(): AbstractControl {
    return this.registrationForm.get('password');
  }
  public get confirmPassword(): AbstractControl {
    return this.registrationForm.get('confirmPassword');
  }

  registrationForm: FormGroup;
  constructor(private fb: FormBuilder) { }
  initRegistrationForm() {
    this.registrationForm =  this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['',[Validators.required, Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$)')]],
      ConfirmPassword: ['']
    });
  }

  ngOnInit() {
    this.initRegistrationForm()
  }

}
