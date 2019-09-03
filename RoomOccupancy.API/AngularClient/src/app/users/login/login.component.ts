import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl } from '@angular/forms';

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
  constructor(private formBuilder: FormBuilder) { }

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


