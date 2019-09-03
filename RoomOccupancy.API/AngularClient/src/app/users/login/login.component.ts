import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
=======
import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
>>>>>>> 0822785c36c52fa308e1910c8a43379c8941e5c9

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

<<<<<<< HEAD
=======

  public get email(): AbstractControl {
    return this.loginForm.get('emailFormControl');
  }
  public get password(): AbstractControl {
    return this.loginForm.get('passwordFormControl');
  }

>>>>>>> 0822785c36c52fa308e1910c8a43379c8941e5c9
  protected loginForm: FormGroup;
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {

    this.loginForm = this.formBuilder.group({
      emailFormControl: ['', [
        Validators.required,
        Validators.email,
      ]],
<<<<<<< HEAD
      passwordFormControl: ['', Validators.required]
      });
    }
  }

}
=======
      passwordFormControl: ['']
    });

    this.loginForm.valueChanges.subscribe(x => {
      console.log(this.loginForm.invalid);
    });
  }

}


>>>>>>> 0822785c36c52fa308e1910c8a43379c8941e5c9
