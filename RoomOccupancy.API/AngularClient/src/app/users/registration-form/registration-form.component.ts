import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { MustMatch } from 'src/app/common/validators/must-match.validator';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class RegistrationFormComponent implements OnInit {

  get f() { return this.registerForm.controls; }

  registerForm: FormGroup;
  constructor(private fb: FormBuilder) { }

  initRegistrationForm() {
    this.registerForm =  this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      // password: ['', [Validators.required, Validators.minLength(6)]],
      // confirmPassword: ['']
    }, {
      // validator: MustMatch('password', 'confirmPassword')
    });
  }
  protected onSubmit() {

  }
  ngOnInit() {
    this.initRegistrationForm();
  }

}
